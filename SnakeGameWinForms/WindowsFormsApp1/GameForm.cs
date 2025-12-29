using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SnakeGameWinForms
{
    public partial class GameForm : Form
    {
        private Timer gameTimer;
        private List<Point> snake;
        private List<Point> foods;
        private Point? bonusFood;
        private int bonusFoodTimer;
        private string direction;
        private bool isPaused;
        private int score;
        private int highScore;

        private const int GridSize = 20;
        private const int CellSize = 20;
        private const string HighScoreFile = "highscore.txt";

        public GameForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            LoadHighScore();
            InitGame();
        }

        private void InitGame()
        {
            snake = new List<Point> { new Point(10, 10) };
            direction = "RIGHT";
            score = 0;
            isPaused = false;

            foods = new List<Point>();
            SpawnFood();
            bonusFood = null;
            bonusFoodTimer = 0;

            gameTimer = new Timer();
            gameTimer.Interval = 150;
            gameTimer.Tick += Update;
            gameTimer.Start();

            this.KeyDown += KeyPressed;
        }

        private void SpawnFood()
        {
            Random rand = new Random();
            foods.Clear();
            for (int i = 0; i < 3; i++)
            {
                Point p;
                do
                {
                    p = new Point(rand.Next(0, GridSize), rand.Next(0, GridSize));
                } while (snake.Contains(p));
                foods.Add(p);
            }
        }

        private void SpawnBonusFood()
        {
            Random rand = new Random();
            Point p;
            do
            {
                p = new Point(rand.Next(0, GridSize), rand.Next(0, GridSize));
            } while (snake.Contains(p) || foods.Contains(p));
            bonusFood = p;
            bonusFoodTimer = 50; // katoaa 50 päivityksen jälkeen
        }

        private void Update(object sender, EventArgs e)
        {
            if (isPaused) return;

            Point head = snake[0];
            Point newHead = head;

            switch (direction)
            {
                case "UP": newHead.Y -= 1; break;
                case "DOWN": newHead.Y += 1; break;
                case "LEFT": newHead.X -= 1; break;
                case "RIGHT": newHead.X += 1; break;
            }

            // Collision check
            if (newHead.X < 0 || newHead.Y < 0 || newHead.X >= GridSize || newHead.Y >= GridSize || snake.Contains(newHead))
            {
                gameTimer.Stop();
                if (score > highScore)
                {
                    highScore = score;
                    SaveHighScore();
                    MessageBox.Show("Game Over! New High Score: " + score, "Game Over");
                }
                else
                {
                    MessageBox.Show("Game Over! Score: " + score, "Game Over");
                }
                Application.Exit();
                return;
            }

            snake.Insert(0, newHead);

            // Food eaten
            if (foods.Contains(newHead))
            {
                score += 10;
                SpawnFood();

                // occasionally spawn bonus
                if (score % 30 == 0 && bonusFood == null)
                {
                    SpawnBonusFood();
                }
            }
            else if (bonusFood != null && newHead == bonusFood)
            {
                score += 50;
                bonusFood = null;
                bonusFoodTimer = 0;
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }

            // Bonus food timer
            if (bonusFood != null)
            {
                bonusFoodTimer--;
                if (bonusFoodTimer <= 0)
                {
                    bonusFood = null;
                }
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Background (grass-like)
            g.Clear(Color.LightGreen);

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    g.DrawRectangle(Pens.WhiteSmoke, new Rectangle(i * CellSize, j * CellSize, CellSize, CellSize));
                }
            }

            // Snake
            for (int i = 0; i < snake.Count; i++)
            {
                Brush color = (i == 0) ? Brushes.DarkGreen : Brushes.Green;
                g.FillEllipse(color, new Rectangle(snake[i].X * CellSize, snake[i].Y * CellSize, CellSize, CellSize));
            }

            // Foods
            foreach (var f in foods)
            {
                g.FillEllipse(Brushes.Red, new Rectangle(f.X * CellSize, f.Y * CellSize, CellSize, CellSize));
                g.FillRectangle(Brushes.Green, new Rectangle(f.X * CellSize + 8, f.Y * CellSize - 5, 4, 5));
            }

            // Bonus food (golden apple)
            if (bonusFood != null)
            {
                g.FillEllipse(Brushes.Gold, new Rectangle(bonusFood.Value.X * CellSize, bonusFood.Value.Y * CellSize, CellSize, CellSize));
                g.DrawEllipse(Pens.OrangeRed, new Rectangle(bonusFood.Value.X * CellSize, bonusFood.Value.Y * CellSize, CellSize, CellSize));
            }

            // Score + High Score
            using (Font font = new Font("Arial", 12, FontStyle.Bold))
            {
                g.DrawString("Score: " + score, font, Brushes.Black, new PointF(5, 5));
                g.DrawString("High Score: " + highScore, font, Brushes.Black, new PointF(5, 25));
                if (isPaused)
                {
                    g.DrawString("PAUSED", new Font("Arial", 20, FontStyle.Bold), Brushes.DarkRed, new PointF(100, 100));
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // disable flicker
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isPaused = !isPaused;
                return;
            }

            if (isPaused) return;

            switch (e.KeyCode)
            {
                case Keys.Up: if (direction != "DOWN") direction = "UP"; break;
                case Keys.Down: if (direction != "UP") direction = "DOWN"; break;
                case Keys.Left: if (direction != "RIGHT") direction = "LEFT"; break;
                case Keys.Right: if (direction != "LEFT") direction = "RIGHT"; break;
            }
        }

        private void LoadHighScore()
        {
            if (File.Exists(HighScoreFile))
            {
                string text = File.ReadAllText(HighScoreFile);
                int.TryParse(text, out highScore);
            }
            else
            {
                highScore = 0;
            }
        }

        private void SaveHighScore()
        {
            File.WriteAllText(HighScoreFile, highScore.ToString());
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
        }
    }
}
