using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.IO;
using System.Reflection;
using System.Media;

namespace LoudnessWarning
{
    public partial class Form1 : Form
    {
        private WaveInEvent waveIn;
        private double currentDbLevel = 0;
        private int barLevel = 0;
        private int barLevelTarget = 0;
        private double warningLimitDb = 80;
        private bool isMonitoring = false;
        private bool isAlarmPlaying = false;
        private SoundPlayer alarmPlayer;
        private DateTime lastAlarmTime = DateTime.MinValue;
        private const int ALARM_COOLDOWN_MS = 10000; // 10 seconds cooldown between alarms

        public Form1()
        {
            InitializeComponent();
            InitializeAudio();
            lblStatus.Text = "Status: Ready";
            lblStatus.ForeColor = Color.Blue;
        }

        private void InitializeAudio()
        {
            try
            {
                // Initialize wave input for microphone
                waveIn = new WaveInEvent();
                waveIn.WaveFormat = new WaveFormat(44100, 1); // 44.1kHz, mono
                waveIn.BufferMilliseconds = 100;
                waveIn.DataAvailable += WaveIn_DataAvailable;

                // Initialize alarm sound player
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "LoudnessWarning.resources.Alarm.wav";
                var stream = assembly.GetManifestResourceStream(resourceName);
                
                if (stream != null)
                {
                    alarmPlayer = new SoundPlayer(stream);
                    alarmPlayer.LoadCompleted += (s, e) => { };
                    alarmPlayer.LoadAsync();
                }
                else
                {
                    // Fallback: try to load from file system
                    string alarmPath = Path.Combine(Application.StartupPath, "resources", "Alarm.wav");
                    if (File.Exists(alarmPath))
                    {
                        alarmPlayer = new SoundPlayer(alarmPath);
                        alarmPlayer.LoadAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing audio: {ex.Message}", "Audio Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (isAlarmPlaying) return; // Skip processing during alarm to avoid feedback loop

            // Calculate RMS (Root Mean Square) for volume level
            float sum = 0;
            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                short sample = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i]);
                float normalizedSample = sample / 32768f;
                sum += normalizedSample * normalizedSample;
            }

            float rms = (float)Math.Sqrt(sum / (e.BytesRecorded / 2));
            
            // Convert to decibels (with some baseline to avoid log(0))
            currentDbLevel = 20 * Math.Log10(Math.Max(rms, 0.00001)) + 94; // Add offset to get reasonable dB values
            
            // Clamp values to reasonable range
            currentDbLevel = Math.Max(0, Math.Min(120, currentDbLevel));

            // Update UI on main thread
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateUI));
            }
            else
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            lblCurrentLevel.Text = $"Current Level: {currentDbLevel:F1} dB";
            
            // Check for warning condition
            if (currentDbLevel > warningLimitDb && !isAlarmPlaying)
            {
                TriggerAlarm();
            }
            
            // Update sound bar
            pnlSoundBar.Invalidate();
        }

        private void TriggerAlarm()
        {
            // Check cooldown to prevent rapid alarm triggering
            if (DateTime.Now.Subtract(lastAlarmTime).TotalMilliseconds < ALARM_COOLDOWN_MS)
                return;

            if (alarmPlayer != null)
            {
                isAlarmPlaying = true;
                lblStatus.Text = "Status: WARNING - LOUD SOUND DETECTED!";
                lblStatus.ForeColor = Color.Red;
                
                try
                {
                    alarmPlayer.Play();
                    lastAlarmTime = DateTime.Now;
                    
                    // Reset alarm status after 1 second
                    Timer resetTimer = new Timer();
                    resetTimer.Interval = 1000;
                    resetTimer.Tick += (s, e) => {
                        isAlarmPlaying = false;
                        if (isMonitoring)
                        {
                            lblStatus.Text = "Status: Monitoring";
                            lblStatus.ForeColor = Color.Green;
                        }
                        resetTimer.Stop();
                        resetTimer.Dispose();
                    };
                    resetTimer.Start();
                }
                catch (Exception ex)
                {
                    isAlarmPlaying = false;
                    MessageBox.Show($"Error playing alarm: {ex.Message}", "Alarm Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isMonitoring)
                {
                    waveIn.StartRecording();
                    timer1.Start();
                    isMonitoring = true;
                    btnStartStop.Text = "Stop Monitoring";
                    lblStatus.Text = "Status: Monitoring";
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    waveIn.StopRecording();
                    timer1.Stop();
                    isMonitoring = false;
                    btnStartStop.Text = "Start Monitoring";
                    lblStatus.Text = "Status: Stopped";
                    lblStatus.ForeColor = Color.Blue;
                    currentDbLevel = 0;
                    pnlSoundBar.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error controlling microphone: {ex.Message}", "Microphone Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetLimit_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtWarningLimit.Text, out double newLimit))
            {
                if (newLimit >= 0 && newLimit <= 120)
                {
                    warningLimitDb = newLimit;
                    MessageBox.Show($"Warning limit set to {warningLimitDb} dB", "Limit Updated", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please enter a value between 0 and 120 dB", "Invalid Range", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number", "Invalid Input", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pnlSoundBar_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Graphics g = e.Graphics;
            
            // Clear the panel
            g.Clear(Color.LightGray);
            
            if (!isMonitoring && currentDbLevel == 0) return;

            // Calculate bar width based on current level (0-120 dB range)
            int maxWidth = panel.Width - 4;
            int barWidth = (int)((barLevel / 1000.0) * maxWidth);
            barWidth = Math.Max(0, Math.Min(maxWidth, barWidth));

            // Determine color based on warning threshold
            Color barColor;
            if (currentDbLevel > warningLimitDb)
            {
                barColor = Color.Orange; // Amber color for warning
            }
            else if (currentDbLevel > warningLimitDb * 0.8)
            {
                barColor = Color.Yellow; // Yellow for approaching warning
            }
            else
            {
                barColor = Color.Green; // Green for normal levels
            }

            // Draw the sound level bar
            if (barWidth > 0)
            {
                using (SolidBrush brush = new SolidBrush(barColor))
                {
                    g.FillRectangle(brush, 2, 2, barWidth, panel.Height - 4);
                }
            }

            // Draw warning threshold line
            int warningX = (int)((warningLimitDb / 120.0) * maxWidth) + 2;
            if (warningX > 2 && warningX < panel.Width - 2)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    g.DrawLine(pen, warningX, 0, warningX, panel.Height);
                }
            }

            // Draw scale marks
            using (Pen pen = new Pen(Color.Black, 1))
            {
                for (int db = 0; db <= 120; db += 20)
                {
                    int x = (int)((db / 120.0) * maxWidth) + 2;
                    g.DrawLine(pen, x, panel.Height - 10, x, panel.Height);
                    
                    // Draw scale labels
                    using (Font font = new Font("Arial", 7))
                    using (SolidBrush textBrush = new SolidBrush(Color.Black))
                    {
                        string label = db.ToString();
                        SizeF textSize = g.MeasureString(label, font);
                        g.DrawString(label, font, textBrush, x - textSize.Width / 2, panel.Height - 25);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Timer is used to refresh the UI periodically
            // Main audio processing happens in WaveIn_DataAvailable
            barLevelTarget = (int)(currentDbLevel / 120 * 1000);
            if (barLevel > barLevelTarget)
                barLevel -= 6;
            else
                barLevel += 20;

            // Saturate
            if (barLevel < 0)
                barLevel = 0;
            else if (barLevel > 1000)
                barLevel = 1000;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (waveIn != null)
                {
                    waveIn.StopRecording();
                    waveIn.Dispose();
                }
                
                if (alarmPlayer != null)
                {
                    alarmPlayer.Stop();
                    alarmPlayer.Dispose();
                }
            }
            catch (Exception ex)
            {
                // Log error but don't prevent form from closing
                System.Diagnostics.Debug.WriteLine($"Error during cleanup: {ex.Message}");
            }
        }
    }
}
