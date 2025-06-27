namespace PenFinderUI
{
    partial class UpdatePassword
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
            label1 = new Label();
            txtNewPassword = new TextBox();
            btnUpdate = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(59, 19);
            label1.Name = "label1";
            label1.Size = new Size(143, 19);
            label1.TabIndex = 0;
            label1.Text = "Enter New Password:";
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(30, 50);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(200, 25);
            txtNewPassword.TabIndex = 1;
            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Bahnschrift SemiCondensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpdate.Location = new Point(40, 95);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(77, 29);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdatePassword_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Bahnschrift SemiCondensed", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(142, 97);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(79, 27);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // UpdatePassword
            // 
            ClientSize = new Size(260, 140);
            Controls.Add(label1);
            Controls.Add(txtNewPassword);
            Controls.Add(btnUpdate);
            Controls.Add(btnCancel);
            Font = new Font("Ravie", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "UpdatePassword";
            Text = "Update Password";
            Load += UpdatePassword_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private TextBox txtNewPassword;
        private Button btnUpdate;
        private Button btnCancel;
    }
}
