namespace PenFinderUI
{
    partial class MainFrame
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
            lblWelcome = new Label();
            btnCreate = new Button();
            btnLogIn = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Ravie", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWelcome.Location = new Point(62, 47);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(465, 39);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome to Pen Finder!";
            // 
            // btnCreate
            // 
            btnCreate.Font = new Font("Bahnschrift SemiCondensed", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCreate.Location = new Point(186, 119);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(205, 47);
            btnCreate.TabIndex = 1;
            btnCreate.Text = "Create Account";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreateAccount_Click;
            // 
            // btnLogIn
            // 
            btnLogIn.Font = new Font("Bahnschrift SemiCondensed", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogIn.Location = new Point(186, 192);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(205, 45);
            btnLogIn.TabIndex = 2;
            btnLogIn.Text = "Log In";
            btnLogIn.UseVisualStyleBackColor = true;
            btnLogIn.Click += btnLogin_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Bahnschrift SemiCondensed", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(186, 265);
            button1.Name = "button1";
            button1.Size = new Size(205, 42);
            button1.TabIndex = 3;
            button1.Text = "Exit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnExit_Click;
            // 
            // MainFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.photo_2025_06_23_11_42_09;
            ClientSize = new Size(572, 359);
            Controls.Add(button1);
            Controls.Add(btnLogIn);
            Controls.Add(btnCreate);
            Controls.Add(lblWelcome);
            Name = "MainFrame";
            Text = "MainFrame";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWelcome;
        private Button btnCreate;
        private Button btnLogIn;
        private Button button1;
    }
}