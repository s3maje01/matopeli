namespace SnakeGameWinForms
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Name = "GameForm";
            this.Text = "Snake Game";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);

        }
    }
}
