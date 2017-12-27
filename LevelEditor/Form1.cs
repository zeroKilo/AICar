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

namespace LevelEditor
{
    public partial class Form1 : Form
    {
        public Level level;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.dxf|*.dxf";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                level = new Level();
                level.ImportDXF(d.FileName);
                Render();
                timer1.Enabled = true;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (level == null) return;
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.acl|*.acl";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                level.Save(d.FileName);
                MessageBox.Show("Done.");
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.acl|*.acl";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                level = new Level();
                level.Load(d.FileName);
                Render();
                timer1.Enabled = true;
            }
        }

        private void pb1_MouseClick(object sender, MouseEventArgs e)
        {
            if (level == null) return;
            float dx = e.X / (float)pb1.Width;
            float dy = 1f - e.Y / (float)pb1.Height;
            level.players[0].pos.X = (int)(level.BBoxMin.X + (level.BBoxMax.X - level.BBoxMin.X) * dx);
            level.players[0].pos.Y = (int)(level.BBoxMin.Y + (level.BBoxMax.Y - level.BBoxMin.Y) * dy);
            Render();
        }

        public void Render()
        {
            if (level == null) return;
            pb1.Image = level.Render(pb1.Width, pb1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            if (level == null) return;
            timer1.Enabled = false;
            level.players[0].rot += 1;
            Render();
            timer1.Enabled = true;
        }
    }
}
