namespace Checkers
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelRed = new System.Windows.Forms.Label();
            this.labelBlack = new System.Windows.Forms.Label();
            this.labelGameOver = new System.Windows.Forms.Label();
            this.labelWinner = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(25, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(550, 550);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(594, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(581, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Black Eaten";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(581, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Red Eaten";
            // 
            // labelRed
            // 
            this.labelRed.AutoSize = true;
            this.labelRed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelRed.Location = new System.Drawing.Point(694, 167);
            this.labelRed.Name = "labelRed";
            this.labelRed.Size = new System.Drawing.Size(13, 13);
            this.labelRed.TabIndex = 4;
            this.labelRed.Text = "0";
            // 
            // labelBlack
            // 
            this.labelBlack.AutoSize = true;
            this.labelBlack.Location = new System.Drawing.Point(694, 212);
            this.labelBlack.Name = "labelBlack";
            this.labelBlack.Size = new System.Drawing.Size(13, 13);
            this.labelBlack.TabIndex = 5;
            this.labelBlack.Text = "0";
            // 
            // labelGameOver
            // 
            this.labelGameOver.AutoSize = true;
            this.labelGameOver.Font = new System.Drawing.Font("Monotype Corsiva", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGameOver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelGameOver.Location = new System.Drawing.Point(575, 240);
            this.labelGameOver.Name = "labelGameOver";
            this.labelGameOver.Size = new System.Drawing.Size(174, 45);
            this.labelGameOver.TabIndex = 6;
            this.labelGameOver.Text = "Game Over";
            this.labelGameOver.Visible = false;
            // 
            // labelWinner
            // 
            this.labelWinner.AutoSize = true;
            this.labelWinner.Font = new System.Drawing.Font("Monotype Corsiva", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWinner.ForeColor = System.Drawing.Color.Lime;
            this.labelWinner.Location = new System.Drawing.Point(576, 300);
            this.labelWinner.Name = "labelWinner";
            this.labelWinner.Size = new System.Drawing.Size(163, 45);
            this.labelWinner.TabIndex = 7;
            this.labelWinner.Text = "Who wins";
            this.labelWinner.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 597);
            this.Controls.Add(this.labelWinner);
            this.Controls.Add(this.labelGameOver);
            this.Controls.Add(this.labelBlack);
            this.Controls.Add(this.labelRed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Checkers";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelRed;
        private System.Windows.Forms.Label labelBlack;
        private System.Windows.Forms.Label labelGameOver;
        private System.Windows.Forms.Label labelWinner;
    }
}

