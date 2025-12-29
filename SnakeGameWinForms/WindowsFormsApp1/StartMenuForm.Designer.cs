namespace SnakeGameWinForms
{
    partial class StartMenuForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnInstructions = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(100, 40);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(150, 40);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Game";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnInstructions
            // 
            this.btnInstructions.Location = new System.Drawing.Point(100, 100);
            this.btnInstructions.Name = "btnInstructions";
            this.btnInstructions.Size = new System.Drawing.Size(150, 40);
            this.btnInstructions.TabIndex = 1;
            this.btnInstructions.Text = "Instructions";
            this.btnInstructions.Click += new System.EventHandler(this.btnInstructions_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(100, 160);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 40);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // StartMenuForm
            // 
            this.ClientSize = new System.Drawing.Size(350, 250);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnInstructions);
            this.Controls.Add(this.btnExit);
            this.Name = "StartMenuForm";
            this.Text = "Snake Game Menu";
            this.Load += new System.EventHandler(this.StartMenuForm_Load);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnInstructions;
        private System.Windows.Forms.Button btnExit;
    }
}
