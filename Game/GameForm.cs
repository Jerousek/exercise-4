using GameLibrary;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Game
{
    public partial class GameForm : Form
    {
        Random random = new Random();
        Stats stats = new Stats();
        public event UpdatedStatsEventHandler? UpdatedStats;

        public GameForm()
        {
            stats.UpdatedStats += this.UpdatedStats;
            InitializeComponent();
        }

        virtual protected void onUpdatedStats()
        {
            correctLabel.Text = "Correct: " + stats.Correct;
            missedLabel.Text = "Missed: " + stats.Missed;
            accuracyLabel.Text = "Acc: " + stats.Accuracy;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            gameListBox.Items.Add((Keys)random.Next(65,91));
            gameListBox.Refresh();

            if(gameListBox.Items.Count == 6 ) 
            {
                gameListBox.Items.Clear();
                gameListBox.Items.Add("Game over!");
                gameTimer.Stop();
            }
        }

        private void gameListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameListBox.Items.Contains(e.KeyCode))
            {
                stats.Update(true);
                gameListBox.Items.Remove(e.KeyCode);
                gameListBox.Refresh();
                ZmenInterval();
                
            } else
            {
                stats.Update(false);
            }
            onUpdatedStats();
            if (gameListBox.Items.Contains("Game over!"))
            {
                ObnovHru();
            }
        }

        private void ObnovHru()
        {
            gameListBox.Items.Clear();
            gameTimer.Interval = 800;
            difficultyProgressBar.Value = 0;
            stats.Reset();
            gameTimer.Start();
        }

        private void ZmenInterval()
        {
            if (gameTimer.Interval > 400)
            {
                gameTimer.Interval -= 60;
                difficultyProgressBar.Value += 60;
            } 
            else if (gameTimer.Interval > 250)
            {
                gameTimer.Interval -= 15;
                difficultyProgressBar.Value += 15;
            } 
            else if(gameTimer.Interval > 150)
            {
                gameTimer.Interval -= 8;
                difficultyProgressBar.Value += 8;
            } else if (gameTimer.Interval < 8)
            {
                return;
            }
        }
    }
}
