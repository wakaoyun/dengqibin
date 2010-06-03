namespace Chess
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Red = new System.Windows.Forms.RadioButton();
            this.Black = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PtoM = new System.Windows.Forms.RadioButton();
            this.PtoP = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Red);
            this.groupBox1.Controls.Add(this.Black);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "执棋";
            // 
            // Red
            // 
            this.Red.AutoSize = true;
            this.Red.Checked = true;
            this.Red.Location = new System.Drawing.Point(80, 23);
            this.Red.Name = "Red";
            this.Red.Size = new System.Drawing.Size(61, 17);
            this.Red.TabIndex = 1;
            this.Red.TabStop = true;
            this.Red.Text = "红棋先";
            this.Red.UseVisualStyleBackColor = true;
            // 
            // Black
            // 
            this.Black.AutoSize = true;
            this.Black.Location = new System.Drawing.Point(13, 23);
            this.Black.Name = "Black";
            this.Black.Size = new System.Drawing.Size(61, 17);
            this.Black.TabIndex = 0;
            this.Black.Text = "黑棋先";
            this.Black.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PtoM);
            this.groupBox2.Controls.Add(this.PtoP);
            this.groupBox2.Location = new System.Drawing.Point(174, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 63);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "下棋方式";
            // 
            // PtoM
            // 
            this.PtoM.AutoSize = true;
            this.PtoM.Checked = true;
            this.PtoM.Location = new System.Drawing.Point(108, 23);
            this.PtoM.Name = "PtoM";
            this.PtoM.Size = new System.Drawing.Size(73, 17);
            this.PtoM.TabIndex = 0;
            this.PtoM.TabStop = true;
            this.PtoM.Text = "人机对战";
            this.PtoM.UseVisualStyleBackColor = true;
            // 
            // PtoP
            // 
            this.PtoP.AutoSize = true;
            this.PtoP.Location = new System.Drawing.Point(13, 23);
            this.PtoP.Name = "PtoP";
            this.PtoP.Size = new System.Drawing.Size(73, 17);
            this.PtoP.TabIndex = 0;
            this.PtoP.Text = "人人对战";
            this.PtoP.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(129, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 131);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton PtoM;
        private System.Windows.Forms.RadioButton PtoP;
        private System.Windows.Forms.RadioButton Red;
        private System.Windows.Forms.RadioButton Black;
        private System.Windows.Forms.Button button1;
    }
}