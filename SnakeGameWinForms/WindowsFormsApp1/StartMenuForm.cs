using System;
using System.Windows.Forms;

namespace SnakeGameWinForms
{
    public partial class StartMenuForm : Form
    {
        public StartMenuForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GameForm game = new GameForm();
            game.Show();
            this.Hide();
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use arrow keys to move the snake.\nEat apples to grow.\nDon't hit the walls or yourself!",
                            "Instructions",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartMenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
