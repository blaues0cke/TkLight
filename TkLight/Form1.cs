using System;
using System.Drawing;
using System.Windows.Forms;
namespace TkLight
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.GroupBox groupBox4;
        public Form1()
        {
            string[] names = new string[3] { "Rot", "Grün", "Blau" };
            this.groupBox4 = new GroupBox();
            this.SuspendLayout();
            this.groupBox4.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox4.Size = new System.Drawing.Size(386, 227);
            this.groupBox4.Text = "Farbeinstellungen";
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.groupBox4.ResumeLayout(false);
            for (int i = 1; i <= 3; i++)
            {
                GroupBox Box = new GroupBox();
                Box.Name = "groupBox" + i.ToString();
                Box.SuspendLayout();
                Box.Dock = System.Windows.Forms.DockStyle.Top;
                Box.Name = "groupBox" + i;
                Box.Padding = new System.Windows.Forms.Padding(10);
                Box.Size = new System.Drawing.Size(366, 65);
                Box.ResumeLayout(false);
                Box.PerformLayout();
                TrackBar Bar = new TrackBar();
                ((System.ComponentModel.ISupportInitialize)(Bar)).BeginInit();
                Bar.Dock = System.Windows.Forms.DockStyle.Fill;
                Bar.Maximum = 255;
                Bar.Name = "trackBar" + i;
                Bar.TickStyle = System.Windows.Forms.TickStyle.None;
                Bar.Scroll += new System.EventHandler(this.ChangeBackgroundColor);
                ((System.ComponentModel.ISupportInitialize)(Bar)).EndInit();
                Box.Text = names[i - 1] + "-Anteil";
                Box.Controls.Add(Bar);
                this.groupBox4.Controls.Add(Box);
            }
            this.ResumeLayout(false);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int Height, Width, X, Y;
            Height = Width = X = Y = 0;
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                Height = Screen.AllScreens[i].Bounds.Height > Height ? Screen.AllScreens[i].Bounds.Height : Height;
                Width += Screen.AllScreens[i].Bounds.Width;
                X = Screen.AllScreens[i].Bounds.X < X ? Screen.AllScreens[i].Bounds.X : X;
                Y = Screen.AllScreens[i].Bounds.Y < Y ? Screen.AllScreens[i].Bounds.Y : Y;
            }
            Location = new Point(X, Y);
            Size = new Size(Width, Height);
            groupBox4.Location = new Point(Width / 2 - groupBox4.Width / 2, Height / 2 - groupBox4.Height / 2);
        }
        private void ChangeBackgroundColor(object sender, EventArgs e)
        {
            string n = (Convert.ToInt16(((TrackBar)sender).Name.Substring(((TrackBar)sender).Name.Length - 1, 1))).ToString();
            groupBox4.Controls["groupBox" + n].Text = groupBox4.Controls["groupBox" + n].Text.Substring(0, (groupBox4.Controls["groupBox" + n].Text.IndexOf("teil") + 4)) + " (" + ((TrackBar)sender).Value.ToString() + ")";
            BackColor = Color.FromArgb(((TrackBar)groupBox4.Controls["groupBox1"].Controls["trackBar1"]).Value, ((TrackBar)groupBox4.Controls["groupBox2"].Controls["trackBar2"]).Value, ((TrackBar)groupBox4.Controls["groupBox3"].Controls["trackBar3"]).Value);
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: groupBox4.Visible = !groupBox4.Visible; break;
                case MouseButtons.Right: Application.Exit(); break;
            }
        }
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
