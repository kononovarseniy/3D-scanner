using Scan3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextureMappingExperiments
{
    public partial class Form1 : Form
    {
        Bitmap bmp = (Bitmap)Bitmap.FromFile("img.png");
        Bitmap dst;
        BitmapData srcData;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            srcData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            timer1.Enabled = true;
            dst = new Bitmap(bmp.Width, bmp.Height);
        }

        double alf = 0;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            alf += Math.PI / 360;
            var data = dst.LockBits(new Rectangle(Point.Empty, dst.Size), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            Vector2 a = new Vector2(-50, -50);
            Vector2 b = new Vector2(50, -50);
            Vector2 c = new Vector2(0, 50);

            Vector2 sa = new Vector2(250, 150);
            Vector2 sb = new Vector2(150, 150);
            Vector2 sc = new Vector2(0, 250);

            Vector2 o = new Vector2(100, 100);

            Quaternion r = Quaternion.CreateFromYawPitchRoll(0, 0, (float)alf);
            a = o + Vector2.Transform(a, r);
            b = o + Vector2.Transform(b, r);
            c = o + Vector2.Transform(c, r);

            var dstT = Triangle.FromVertices(new[] { a, b, c });
            var srcT = Triangle.FromVertices(new[] { sa, sb, sc });

            srcT.MapTexture(srcData, data, dstT);

            dst.UnlockBits(data);
            e.Graphics.DrawImageUnscaled(dst, Point.Empty);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
