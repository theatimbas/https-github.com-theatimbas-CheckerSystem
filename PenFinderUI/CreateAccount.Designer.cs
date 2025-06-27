namespace PenFinderUI
{
    partial class CreateAccount
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
            lblUserName = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnRegister = new Button();
            SuspendLayout();
            // 
            // lblPenFinder
            // 
            lblPenFinder.AutoSize = true;
            lblPenFinder.Font = new Font("Ravie", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPenFinder.Location = new Point(51, 36);
            lblPenFinder.Name = "lblPenFinder";
            lblPenFinder.Size = new Size(422, 39);
            lblPenFinder.TabIndex = 0;
            lblPenFinder.Text = "Create Your Account";
            lblPenFinder.Click += lblPenFinder_Click;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Bahnschrift", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUserName.Location = new Point(51, 136);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(129, 29);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Username:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Bahnschrift", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(217, 136);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(214, 33);
            txtUsername.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Bahnschrift", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassword.Location = new Point(56, 211);
            lblPassword.Name = "lblPassword";
            lblPassword.RightToLeft = RightToLeft.Yes;
            lblPassword.Size = new Size(124, 29);
            lblPassword.TabIndex = 3;
            lblPassword.Text = ":Password";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Bahnschrift", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(217, 211);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(214, 33);
            txtPassword.TabIndex = 4;
            // 
            // btnRegister
            // 
            btnRegister.Font = new Font("Bahnschrift", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegister.Location = new Point(174, 295);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(169, 45);
            btnRegister.TabIndex = 5;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnCreate_Click;
            // 
            // CreateAccount
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.photo_2025_06_23_11_42_09;
            ClientSize = new Size(531, 379);
            Controls.Add(btnRegister);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUserName);
            Controls.Add(lblPenFinder);
            Name = "CreateAccount";
            Text = "CreateAccount";
            Load += CreateAccount_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPenFinder;
        private Label lblUserName;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnRegister;
    }
}