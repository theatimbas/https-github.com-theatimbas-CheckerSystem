namespace PenFinderUI
{
    partial class LogIn
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
            lblPenFinder = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblpassword = new Label();
            txtPassword = new TextBox();
            btnLogIn = new Button();
            SuspendLayout();
            // 
            // lblPenFinder
            // 
            lblPenFinder.AutoSize = true;
            lblPenFinder.Font = new Font("Ravie", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPenFinder.Location = new Point(83, 48);
            lblPenFinder.Name = "lblPenFinder";
            lblPenFinder.Size = new Size(402, 39);
            lblPenFinder.TabIndex = 0;
            lblPenFinder.Text = "Log In your Account";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Bahnschrift", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsername.Location = new Point(73, 147);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(129, 29);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Bahnschrift", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(237, 143);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(228, 33);
            txtUsername.TabIndex = 2;
            // 
            // lblpassword
            // 
            lblpassword.AutoSize = true;
            lblpassword.Font = new Font("Bahnschrift", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblpassword.Location = new Point(73, 242);
            lblpassword.Name = "lblpassword";
            lblpassword.Size = new Size(124, 29);
            lblpassword.TabIndex = 3;
            lblpassword.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Bahnschrift", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(237, 238);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(228, 33);
            txtPassword.TabIndex = 4;
            // 
            // btnLogIn
            // 
            btnLogIn.Font = new Font("Bahnschrift", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogIn.Location = new Point(182, 323);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(169, 44);
            btnLogIn.TabIndex = 5;
            btnLogIn.Text = "Log In";
            btnLogIn.UseVisualStyleBackColor = true;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // LogIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.photo_2025_06_23_11_42_09;
            ClientSize = new Size(554, 416);
            Controls.Add(btnLogIn);
            Controls.Add(txtPassword);
            Controls.Add(lblpassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(lblPenFinder);
            Name = "LogIn";
            Text = "LogIn";
            Load += LogIn_Load;
            Click += btnLogIn_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPenFinder;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblpassword;
        private TextBox txtPassword;
        private Button btnLogIn;
    }
}