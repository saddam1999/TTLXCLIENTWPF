namespace TTLX.WindowsTool.Common.WaveRender
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class WaveRendererSettings
    {
        public WaveRendererSettings()
        {
            this.Width = 0x640;
            this.TopHeight = 50;
            this.BottomHeight = 50;
            this.PixelsPerPeak = 1;
            this.SpacerPixels = 0;
            this.BackgroundColor = Color.Beige;
            this.TopPeakPen = new Pen(Color.DeepSkyBlue);
            this.BottomPeakPen = new Pen(Color.Chocolate);
            this.TopSpacerPen = new Pen(Color.White);
            this.BottomSpacerPen = new Pen(Color.White);
        }

        public int Width { get; set; }

        public int TopHeight { get; set; }

        public int BottomHeight { get; set; }

        public int PixelsPerPeak { get; set; }

        public int SpacerPixels { get; set; }

        public Color BackgroundColor { get; set; }

        public virtual Pen TopPeakPen { get; set; }

        public virtual Pen TopSpacerPen { get; set; }

        public virtual Pen BottomPeakPen { get; set; }

        public virtual Pen BottomSpacerPen { get; set; }
    }
}

