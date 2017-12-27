namespace AIPlayer
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuralNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadNewBestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentBestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collisionPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sightRadiusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dontEvolveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelToolStripMenuItem,
            this.neuralNetToolStripMenuItem,
            this.drawToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(516, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // neuralNetToolStripMenuItem
            // 
            this.neuralNetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadNewBestToolStripMenuItem,
            this.saveCurrentBestToolStripMenuItem,
            this.dontEvolveToolStripMenuItem});
            this.neuralNetToolStripMenuItem.Name = "neuralNetToolStripMenuItem";
            this.neuralNetToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.neuralNetToolStripMenuItem.Text = "Neural Net";
            // 
            // loadNewBestToolStripMenuItem
            // 
            this.loadNewBestToolStripMenuItem.Name = "loadNewBestToolStripMenuItem";
            this.loadNewBestToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.loadNewBestToolStripMenuItem.Text = "Load new Best";
            this.loadNewBestToolStripMenuItem.Click += new System.EventHandler(this.loadNewBestToolStripMenuItem_Click);
            // 
            // saveCurrentBestToolStripMenuItem
            // 
            this.saveCurrentBestToolStripMenuItem.Name = "saveCurrentBestToolStripMenuItem";
            this.saveCurrentBestToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.saveCurrentBestToolStripMenuItem.Text = "Save current Best";
            this.saveCurrentBestToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentBestToolStripMenuItem_Click);
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collisionPointsToolStripMenuItem,
            this.sightRadiusToolStripMenuItem,
            this.raysToolStripMenuItem});
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.drawToolStripMenuItem.Text = "Draw";
            // 
            // collisionPointsToolStripMenuItem
            // 
            this.collisionPointsToolStripMenuItem.CheckOnClick = true;
            this.collisionPointsToolStripMenuItem.Name = "collisionPointsToolStripMenuItem";
            this.collisionPointsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.collisionPointsToolStripMenuItem.Text = "Collision Points";
            this.collisionPointsToolStripMenuItem.Click += new System.EventHandler(this.collisionPointsToolStripMenuItem_Click);
            // 
            // sightRadiusToolStripMenuItem
            // 
            this.sightRadiusToolStripMenuItem.Checked = true;
            this.sightRadiusToolStripMenuItem.CheckOnClick = true;
            this.sightRadiusToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sightRadiusToolStripMenuItem.Name = "sightRadiusToolStripMenuItem";
            this.sightRadiusToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.sightRadiusToolStripMenuItem.Text = "Sight Radius";
            this.sightRadiusToolStripMenuItem.Click += new System.EventHandler(this.sightRadiusToolStripMenuItem_Click);
            // 
            // raysToolStripMenuItem
            // 
            this.raysToolStripMenuItem.Checked = true;
            this.raysToolStripMenuItem.CheckOnClick = true;
            this.raysToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.raysToolStripMenuItem.Name = "raysToolStripMenuItem";
            this.raysToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.raysToolStripMenuItem.Text = "Rays";
            this.raysToolStripMenuItem.Click += new System.EventHandler(this.raysToolStripMenuItem_Click);
            // 
            // pb1
            // 
            this.pb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb1.Location = new System.Drawing.Point(0, 24);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(516, 361);
            this.pb1.TabIndex = 2;
            this.pb1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dontEvolveToolStripMenuItem
            // 
            this.dontEvolveToolStripMenuItem.CheckOnClick = true;
            this.dontEvolveToolStripMenuItem.Name = "dontEvolveToolStripMenuItem";
            this.dontEvolveToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.dontEvolveToolStripMenuItem.Text = "Dont evolve";
            this.dontEvolveToolStripMenuItem.Click += new System.EventHandler(this.dontEvolveToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.newToolStripMenuItem.Text = "New Best";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 385);
            this.Controls.Add(this.pb1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "AI Player";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.PictureBox pb1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem neuralNetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadNewBestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentBestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collisionPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sightRadiusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dontEvolveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    }
}

