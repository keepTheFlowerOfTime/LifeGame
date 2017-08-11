namespace LifeGameShow
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c_lifeGameShow = new System.Windows.Forms.PictureBox();
            this.c_startButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c_lifeGameShow)).BeginInit();
            this.SuspendLayout();
            // 
            // c_lifeGameShow
            // 
            this.c_lifeGameShow.Location = new System.Drawing.Point(12, 12);
            this.c_lifeGameShow.Name = "c_lifeGameShow";
            this.c_lifeGameShow.Size = new System.Drawing.Size(605, 605);
            this.c_lifeGameShow.TabIndex = 0;
            this.c_lifeGameShow.TabStop = false;
            // 
            // c_startButton
            // 
            this.c_startButton.Location = new System.Drawing.Point(666, 257);
            this.c_startButton.Name = "c_startButton";
            this.c_startButton.Size = new System.Drawing.Size(75, 23);
            this.c_startButton.TabIndex = 1;
            this.c_startButton.Text = "开始";
            this.c_startButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 621);
            this.Controls.Add(this.c_startButton);
            this.Controls.Add(this.c_lifeGameShow);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.c_lifeGameShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox c_lifeGameShow;
        private System.Windows.Forms.Button c_startButton;
    }
}