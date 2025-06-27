namespace PenFinderUI
{
    partial class DeleteAccount
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lblDelete;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.TextBox txtDelete;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnCancel;

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
            lblDelete = new Label();
            lblAccount = new Label();
            txtDelete = new TextBox();
            btndelete = new Button();
            btnCancel = new Button();
            lblWarning = new Label();
            SuspendLayout();
            // 
            // lblDelete
            // 
            lblDelete.AutoSize = true;
            lblDelete.Font = new Font("Agency FB", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDelete.Location = new Point(110, 19);
            lblDelete.Name = "lblDelete";
            lblDelete.Size = new Size(96, 24);
            lblDelete.TabIndex = 0;
            lblDelete.Text = "Delete Account";
            // 
            // lblAccount
            // 
            lblAccount.AutoSize = true;
            lblAccount.Font = new Font("Agency FB", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAccount.Location = new Point(22, 60);
            lblAccount.Name = "lblAccount";
            lblAccount.RightToLeft = RightToLeft.Yes;
            lblAccount.Size = new Size(147, 24);
            lblAccount.TabIndex = 1;
            lblAccount.Text = ":Enter Account to Delete";
            // 
            // txtDelete
            // 
            txtDelete.Location = new Point(57, 99);
            txtDelete.Name = "txtDelete";
            txtDelete.Size = new Size(191, 23);
            txtDelete.TabIndex = 2;
            // 
            // btndelete
            // 
            btndelete.Font = new Font("Agency FB", 12F, FontStyle.Bold);
            btndelete.Location = new Point(42, 142);
            btndelete.Name = "btndelete";
            btndelete.Size = new Size(90, 30);
            btndelete.TabIndex = 3;
            btndelete.Text = "Delete";
            btndelete.UseVisualStyleBackColor = true;
            btndelete.Click += btnDelete_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Agency FB", 12F, FontStyle.Bold);
            btnCancel.Location = new Point(166, 142);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(95, 30);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblWarning
            // 
            lblWarning.AutoSize = true;
            lblWarning.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblWarning.ForeColor = Color.Maroon;
            lblWarning.Location = new Point(57, 186);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(190, 15);
            lblWarning.TabIndex = 5;
            lblWarning.Text = "Are you sure you want to delete?";
            // 
            // DeleteAccount
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(303, 221);
            Controls.Add(lblWarning);
            Controls.Add(btnCancel);
            Controls.Add(btndelete);
            Controls.Add(txtDelete);
            Controls.Add(lblAccount);
            Controls.Add(lblDelete);
            Name = "DeleteAccount";
            Text = "Delete Account";
            Load += DeleteAccount_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
