using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AICar;

namespace AIPlayer
{
    public partial class Form1 : Form
    {
        public Level level;

        public Form1()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.acl|*.acl";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                level = new Level();
                level.drawPoints = collisionPointsToolStripMenuItem.Checked;
                level.drawSight = sightRadiusToolStripMenuItem.Checked;
                level.drawRays = raysToolStripMenuItem.Checked;
                for (int i = 0; i < 10; i++)
                    level.players.Add(new Player());
                level.Load(d.FileName);
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (level == null) return;
            timer1.Enabled = false;
            try
            {
                pb1.Image = level.Render(pb1.Width, pb1.Height);
                level.Update();
            }
            catch { }
            timer1.Enabled = true;
        }

        private void saveCurrentBestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level == null) return;
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.nn|*.nn";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                level.bestNet.Save(d.FileName);
                MessageBox.Show("Done.");
            }
        }

        private void loadNewBestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (level == null) return;
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.nn|*.nn";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                level.bestNet.Load(d.FileName);
                level.bestTime = 0;
                level.generation = 0;
                level.test = 0;
                level.dontEvolve = true;
                dontEvolveToolStripMenuItem.Checked = true;
                foreach (Player p in level.players)
                {
                    p.pos = level.start;
                    p.rot = 0;
                    p.running.Restart();
                    p.net = level.bestNet.Clone();
                    p.baseRayDis = new List<float>(new float[7]);
                }
            }
        }

        private void collisionPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level == null) return;
            level.drawPoints = collisionPointsToolStripMenuItem.Checked;
        }

        private void sightRadiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level == null) return;
            level.drawSight = sightRadiusToolStripMenuItem.Checked;
        }

        private void raysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level == null) return;
            level.drawRays = raysToolStripMenuItem.Checked;
        }

        private void dontEvolveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level == null) return;
            level.dontEvolve = dontEvolveToolStripMenuItem.Checked;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level == null) return;
            level.bestTime = 0;
            level.bestNet = new NeuralNet();
            level.dontEvolve = true;
            dontEvolveToolStripMenuItem.Checked = true;
            foreach (Player p in level.players)
            {
                p.pos = level.start;
                p.rot = 0;
                p.running.Restart();
                p.net = level.bestNet.Clone();
                p.baseRayDis = new List<float>(new float[7]);
            }
        }
    }
}
