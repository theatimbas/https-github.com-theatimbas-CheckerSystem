namespace PenFinderUI
{
    partial class Dashboard
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
            panelMenu = new Panel();
            btnUpdate = new Button();
            btnLogOut = new Button();
            btnfav = new Button();
            btnDelete = new Button();
            btnGenres = new Button();
            lblmenu = new Label();
            lblgreeting = new Label();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.Plum;
            panelMenu.BorderStyle = BorderStyle.Fixed3D;
            panelMenu.Controls.Add(btnUpdate);
            panelMenu.Controls.Add(btnLogOut);
            panelMenu.Controls.Add(btnfav);
            panelMenu.Controls.Add(btnDelete);
            panelMenu.Controls.Add(btnGenres);
            panelMenu.Controls.Add(lblmenu);
            panelMenu.Controls.Add(lblgreeting);
            panelMenu.Dock = DockStyle.Fill;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(544, 398);
            panelMenu.TabIndex = 0;
            panelMenu.Paint += panelMenu_Paint;
            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Bahnschrift SemiCondensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpdate.Location = new Point(180, 227);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(155, 39);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Change Password";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdatePassword_Click;
            // 
            // btnLogOut
            // 
            btnLogOut.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogOut.Location = new Point(180, 339);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(155, 35);
            btnLogOut.TabIndex = 1;
            btnLogOut.Text = "Log Out";
            btnLogOut.UseVisualStyleBackColor = true;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // btnfav
            // 
            btnfav.Font = new Font("Bahnschrift SemiCondensed", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnfav.Location = new Point(180, 113);
            btnfav.Name = "btnfav";
            btnfav.Size = new Size(155, 33);
            btnfav.TabIndex = 1;
            btnfav.Text = "Favorites";
            btnfav.UseVisualStyleBackColor = true;
            btnfav.Click += btnFavorites_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(180, 285);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(155, 39);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Delete Account";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnGenres
            // 
            btnGenres.Font = new Font("Bahnschrift SemiCondensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGenres.Location = new Point(180, 163);
            btnGenres.Name = "btnGenres";
            btnGenres.Size = new Size(155, 48);
            btnGenres.TabIndex = 1;
            btnGenres.Text = "Browse Genres and \r\n  Books";
            btnGenres.UseVisualStyleBackColor = true;
            btnGenres.Click += btnBrowseGenres_Click;
            // 
            // lblmenu
            // 
            lblmenu.AutoSize = true;
            lblmenu.Font = new Font("Ravie", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblmenu.Location = new Point(168, 59);
            lblmenu.Name = "lblmenu";
            lblmenu.Size = new Size(167, 30);
            lblmenu.TabIndex = 1;
            lblmenu.Text = "Main Menu";
            // 
            // lblgreeting
            // 
            lblgreeting.AutoSize = true;
            lblgreeting.Font = new Font("Ravie", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblgreeting.Location = new Point(105, 7);
            lblgreeting.Name = "lblgreeting";
            lblgreeting.Size = new Size(143, 30);
            lblgreeting.TabIndex = 1;
            lblgreeting.Text = "Welcome,";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Plum;
            ClientSize = new Size(544, 398);
            Controls.Add(panelMenu);
            Name = "Dashboard";
            Text = "Dashboard";
            Load += Dashboard_Load;
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Label lblgreeting;
        private Label lblmenu;
        private Button btnfav;
        private Button btnGenres;
        private Button btnLogOut;
        private Button btnDelete;
        private Button btnUpdate;
    }
}