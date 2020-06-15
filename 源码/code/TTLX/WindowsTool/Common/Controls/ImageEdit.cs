namespace TTLX.WindowsTool.Common.Controls
{
    using ImageProcessor;
    using ImageProcessor.Imaging.Formats;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using TTLX.WindowsTool.Common.Utility;

    public class ImageEdit : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty SelRectXProperty = DependencyProperty.Register("SelRectX", typeof(double), typeof(ImageEdit), new PropertyMetadata(0.0));
        public static readonly DependencyProperty SelRectYProperty = DependencyProperty.Register("SelRectY", typeof(double), typeof(ImageEdit), new PropertyMetadata(0.0));
        public static readonly DependencyProperty SelRectWidthProperty = DependencyProperty.Register("SelRectWidth", typeof(double), typeof(ImageEdit), new PropertyMetadata(100.0, new PropertyChangedCallback(null, WidthOrHeightPropertyChangedCallback)));
        public static readonly DependencyProperty SelRectHeightProperty = DependencyProperty.Register("SelRectHeight", typeof(double), typeof(ImageEdit), new PropertyMetadata(100.0, new PropertyChangedCallback(null, WidthOrHeightPropertyChangedCallback)));
        private BitmapSource _bitmap;
        private bool DragInProgress;
        private Point LastPoint;
        private HitType MouseHitType;
        public static readonly DependencyProperty HasImageProperty = DependencyProperty.Register("HasImage", typeof(bool), typeof(ImageEdit), new PropertyMetadata(false));
        internal System.Windows.Controls.Image XImgTarget;
        internal Grid XGdRoot;
        internal System.Windows.Shapes.Rectangle XRectSelected;
        internal System.Windows.Shapes.Rectangle XLeftRect;
        internal System.Windows.Shapes.Rectangle XRightRect;
        internal System.Windows.Shapes.Rectangle XTopRect;
        internal System.Windows.Shapes.Rectangle XBottonRect;
        internal System.Windows.Controls.Image XImgCropped;
        private bool _contentLoaded;

        public ImageEdit()
        {
            this.InitializeComponent();
            base.DataContext = this;
            this.MinSelectedRectangleWidth = 30.0;
            this.MinSelectedRectangleHeight = 30.0;
        }

        public void CroppedImageByRatio(double width, double height)
        {
            double num = width / height;
            if (this.XImgTarget.ActualWidth < (this.XImgTarget.ActualHeight * num))
            {
                this.SelRectWidth = (int) this.XImgTarget.ActualWidth;
                this.SelRectHeight = (int) (this.XImgTarget.ActualWidth / num);
                this.SelRectX = 0.0;
                this.SelRectY = (this.XImgTarget.ActualHeight - this.SelRectHeight) / 2.0;
            }
            else
            {
                this.SelRectHeight = (int) this.XImgTarget.ActualHeight;
                this.SelRectWidth = (int) (this.XImgTarget.ActualHeight * num);
                this.SelRectX = (this.XImgTarget.ActualWidth - this.SelRectWidth) / 2.0;
                this.SelRectY = 0.0;
            }
            this.UpdateCroppedImg();
        }

        public void Init(string path)
        {
            BitmapImage image1 = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute)) {
                CreateOptions = BitmapCreateOptions.IgnoreColorProfile
            };
            this.Init((BitmapSource) image1);
        }

        public void Init(BitmapSource img)
        {
            this.HasImage = true;
            this._bitmap = img;
            this.XImgTarget.Source = this._bitmap;
            this.XImgTarget.UpdateLayout();
            this.SelRectX = 0.0;
            this.SelRectY = 0.0;
            this.SelRectWidth = this.XImgTarget.ActualWidth;
            this.SelRectHeight = this.XImgTarget.ActualHeight;
            this.UpdateCroppedImg();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/TTLX.WindowsTool.Common;component/controls/imageedit.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void RotateImage(int degree)
        {
            JpegFormat format1 = new JpegFormat {
                Quality = 100
            };
            ISupportedImageFormat format = format1;
            using (ImageFactory factory = new ImageFactory(false))
            {
                Bitmap bitmap = ImageHelper.BitmapSource2Bitmap(this._bitmap);
                System.Drawing.Image image = factory.Load(bitmap).Rotate((float) degree).Format(format).Image;
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Png);
                    stream.Seek(0L, SeekOrigin.Begin);
                    BitmapImage image2 = new BitmapImage();
                    image2.BeginInit();
                    image2.CacheOption = BitmapCacheOption.OnLoad;
                    image2.StreamSource = stream;
                    image2.EndInit();
                    this.Init((BitmapSource) image2);
                }
            }
        }

        private HitType SetHitType(System.Windows.Shapes.Rectangle rect, Point point)
        {
            double actualWidth = this.XGdRoot.ColumnDefinitions[0].ActualWidth;
            double actualHeight = this.XGdRoot.RowDefinitions[0].ActualHeight;
            double num3 = actualWidth + this.XRectSelected.Width;
            double num4 = actualHeight + this.XRectSelected.Height;
            if (point.get_X() < actualWidth)
            {
                return HitType.None;
            }
            if (point.get_X() > num3)
            {
                return HitType.None;
            }
            if (point.get_Y() < actualHeight)
            {
                return HitType.None;
            }
            if (point.get_Y() > num4)
            {
                return HitType.None;
            }
            if ((point.get_X() - actualWidth) < 10.0)
            {
                if ((point.get_Y() - actualHeight) < 10.0)
                {
                    return HitType.UL;
                }
                if ((num4 - point.get_Y()) < 10.0)
                {
                    return HitType.LL;
                }
                return HitType.L;
            }
            if ((num3 - point.get_X()) < 10.0)
            {
                if ((point.get_Y() - actualHeight) < 10.0)
                {
                    return HitType.UR;
                }
                if ((num4 - point.get_Y()) < 10.0)
                {
                    return HitType.LR;
                }
                return HitType.R;
            }
            if ((point.get_Y() - actualHeight) < 10.0)
            {
                return HitType.T;
            }
            if ((num4 - point.get_Y()) < 10.0)
            {
                return HitType.B;
            }
            return HitType.Body;
        }

        private void SetMouseCursor()
        {
            Cursor arrow = Cursors.Arrow;
            switch (this.MouseHitType)
            {
                case HitType.None:
                    arrow = Cursors.Arrow;
                    break;

                case HitType.Body:
                    arrow = Cursors.ScrollAll;
                    break;

                case HitType.UL:
                case HitType.LR:
                    arrow = Cursors.SizeNWSE;
                    break;

                case HitType.UR:
                case HitType.LL:
                    arrow = Cursors.SizeNESW;
                    break;

                case HitType.L:
                case HitType.R:
                    arrow = Cursors.SizeWE;
                    break;

                case HitType.T:
                case HitType.B:
                    arrow = Cursors.SizeNS;
                    break;
            }
            if (base.Cursor != arrow)
            {
                base.Cursor = arrow;
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.XImgTarget = (System.Windows.Controls.Image) target;
                    return;

                case 2:
                    this.XGdRoot = (Grid) target;
                    this.XGdRoot.MouseDown += new MouseButtonEventHandler(this.XGdRoot_MouseDown);
                    this.XGdRoot.MouseMove += new MouseEventHandler(this.XGdRoot_MouseMove);
                    this.XGdRoot.MouseUp += new MouseButtonEventHandler(this.XGdRoot_MouseUp);
                    this.XGdRoot.MouseWheel += new MouseWheelEventHandler(this.XGdRoot_OnMouseWheel);
                    return;

                case 3:
                    this.XRectSelected = (System.Windows.Shapes.Rectangle) target;
                    return;

                case 4:
                    this.XLeftRect = (System.Windows.Shapes.Rectangle) target;
                    return;

                case 5:
                    this.XRightRect = (System.Windows.Shapes.Rectangle) target;
                    return;

                case 6:
                    this.XTopRect = (System.Windows.Shapes.Rectangle) target;
                    return;

                case 7:
                    this.XBottonRect = (System.Windows.Shapes.Rectangle) target;
                    return;

                case 8:
                    this.XImgCropped = (System.Windows.Controls.Image) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UpdateCroppedImg()
        {
            try
            {
                this.X1 = this.SelRectX / this.XImgTarget.ActualWidth;
                this.Y1 = this.SelRectY / this.XImgTarget.ActualHeight;
                this.X2 = (this.SelRectX + this.SelRectWidth) / this.XImgTarget.ActualWidth;
                this.Y2 = (this.SelRectY + this.SelRectHeight) / this.XImgTarget.ActualHeight;
                this.X = Convert.ToInt32((double) ((this._bitmap.PixelWidth * this.SelRectX) / this.XImgTarget.ActualWidth));
                this.Y = Convert.ToInt32((double) ((this._bitmap.PixelHeight * this.SelRectY) / this.XImgTarget.ActualHeight));
                this.Width = Convert.ToInt32((double) ((this._bitmap.PixelWidth * this.SelRectWidth) / this.XImgTarget.ActualWidth));
                this.Height = Convert.ToInt32((double) ((this._bitmap.PixelHeight * this.SelRectHeight) / this.XImgTarget.ActualHeight));
                Int32Rect sourceRect = new Int32Rect(this.X, this.Y, this.Width, this.Height);
                int stride = (this._bitmap.Format.BitsPerPixel * sourceRect.get_Width()) / 8;
                byte[] pixels = new byte[sourceRect.get_Height() * stride];
                if (pixels.Length != 0)
                {
                    this._bitmap.CopyPixels(sourceRect, pixels, stride, 0);
                    this.XImgCropped.Source = BitmapSource.Create(this.Width, this.Height, 0.0, 0.0, PixelFormats.Bgra32, null, pixels, stride);
                }
            }
            catch (Exception exception)
            {
                LogHelper.Error("ImageEdit -> UpdateCroppedImg:", exception);
            }
        }

        private static void WidthOrHeightPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void XGdRoot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.MouseHitType != HitType.None)
            {
                this.XGdRoot.CaptureMouse();
                this.LastPoint = Mouse.GetPosition(this.XGdRoot);
                this.DragInProgress = true;
            }
        }

        private void XGdRoot_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.DragInProgress)
            {
                this.MouseHitType = this.SetHitType(this.XRectSelected, Mouse.GetPosition(this.XGdRoot));
                this.SetMouseCursor();
            }
            else
            {
                Point position = Mouse.GetPosition(this.XGdRoot);
                double num = position.get_X() - this.LastPoint.get_X();
                double num2 = position.get_Y() - this.LastPoint.get_Y();
                double actualWidth = this.XGdRoot.ColumnDefinitions[0].ActualWidth;
                double num4 = actualWidth;
                double actualHeight = this.XGdRoot.RowDefinitions[0].ActualHeight;
                double num6 = actualHeight;
                double width = this.XRectSelected.Width;
                double num8 = width;
                double height = this.XRectSelected.Height;
                double num10 = height;
                switch (this.MouseHitType)
                {
                    case HitType.Body:
                        actualWidth += num;
                        actualHeight += num2;
                        break;

                    case HitType.UL:
                        actualWidth += num;
                        actualHeight += num2;
                        width -= num;
                        height -= num2;
                        break;

                    case HitType.UR:
                        actualHeight += num2;
                        width += num;
                        height -= num2;
                        break;

                    case HitType.LR:
                        width += num;
                        height += num2;
                        break;

                    case HitType.LL:
                        actualWidth += num;
                        width -= num;
                        height += num2;
                        break;

                    case HitType.L:
                        actualWidth += num;
                        width -= num;
                        break;

                    case HitType.R:
                        width += num;
                        break;

                    case HitType.T:
                        actualHeight += num2;
                        height -= num2;
                        break;

                    case HitType.B:
                        height += num2;
                        break;
                }
                if (actualWidth < 0.0)
                {
                    actualWidth = num4;
                    width = num8;
                }
                if (actualHeight < 0.0)
                {
                    actualHeight = num6;
                    height = num10;
                }
                if (width < this.MinSelectedRectangleWidth)
                {
                    width = num8;
                    actualWidth = num4;
                }
                if (height < this.MinSelectedRectangleHeight)
                {
                    height = num10;
                    actualHeight = num6;
                }
                if ((actualWidth + width) > this.XGdRoot.ActualWidth)
                {
                    if (num4 == actualWidth)
                    {
                        width = this.XGdRoot.ActualWidth - actualWidth;
                    }
                    else
                    {
                        actualWidth = num4;
                    }
                }
                if ((actualHeight + height) > this.XGdRoot.ActualHeight)
                {
                    if (num6 == actualHeight)
                    {
                        height = this.XGdRoot.ActualHeight - actualHeight;
                    }
                    else
                    {
                        actualHeight = num6;
                    }
                }
                this.SelRectX = actualWidth;
                this.SelRectY = actualHeight;
                this.SelRectWidth = width;
                this.SelRectHeight = height;
                this.LastPoint = position;
                this.XGdRoot.UpdateLayout();
                this.UpdateCroppedImg();
            }
        }

        private void XGdRoot_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DragInProgress = false;
            this.XGdRoot.ReleaseMouseCapture();
        }

        private void XGdRoot_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double num = (this.SelRectWidth / this.SelRectHeight) * 4.0;
            double num2 = 4.0;
            double actualWidth = this.XGdRoot.ColumnDefinitions[0].ActualWidth;
            double num4 = actualWidth;
            double actualHeight = this.XGdRoot.RowDefinitions[0].ActualHeight;
            double num6 = actualHeight;
            double width = this.XRectSelected.Width;
            double num8 = width;
            double height = this.XRectSelected.Height;
            double num10 = height;
            if (e.Delta > 0)
            {
                width += num;
                height += num2;
                actualWidth -= num / 2.0;
                actualHeight -= num2 / 2.0;
                if (((actualWidth < 0.0) || ((actualWidth + width) > this.XGdRoot.ActualWidth)) || ((actualHeight < 0.0) || ((actualHeight + height) > this.XGdRoot.ActualHeight)))
                {
                    actualWidth = num4;
                    width = num8;
                    actualHeight = num6;
                    height = num10;
                }
            }
            else
            {
                width -= num;
                height -= num2;
                actualWidth += num / 2.0;
                actualHeight += num2 / 2.0;
                if ((width < this.MinSelectedRectangleWidth) || (height < this.MinSelectedRectangleHeight))
                {
                    width = num8;
                    actualWidth = num4;
                    height = num10;
                    actualHeight = num6;
                }
            }
            this.SelRectX = actualWidth;
            this.SelRectY = actualHeight;
            this.SelRectWidth = width;
            this.SelRectHeight = height;
            this.UpdateCroppedImg();
        }

        public double MinSelectedRectangleWidth { get; set; }

        public double MinSelectedRectangleHeight { get; set; }

        public double SelRectX
        {
            get => 
                ((double) base.GetValue(SelRectXProperty));
            set => 
                base.SetValue(SelRectXProperty, value);
        }

        public double SelRectY
        {
            get => 
                ((double) base.GetValue(SelRectYProperty));
            set => 
                base.SetValue(SelRectYProperty, value);
        }

        public double SelRectWidth
        {
            get => 
                ((double) base.GetValue(SelRectWidthProperty));
            set => 
                base.SetValue(SelRectWidthProperty, value);
        }

        public double SelRectHeight
        {
            get => 
                ((double) base.GetValue(SelRectHeightProperty));
            set => 
                base.SetValue(SelRectHeightProperty, value);
        }

        public double X1 { get; set; }

        public double Y1 { get; set; }

        public double X2 { get; set; }

        public double Y2 { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool HasImage
        {
            get => 
                ((bool) base.GetValue(HasImageProperty));
            set => 
                base.SetValue(HasImageProperty, value);
        }

        public Bitmap CroppedImage =>
            ImageHelper.BitmapSource2Bitmap((BitmapSource) this.XImgCropped.Source);

        public Bitmap OriginImage =>
            ImageHelper.BitmapSource2Bitmap((BitmapSource) this.XImgTarget.Source);

        private enum HitType
        {
            None,
            Body,
            UL,
            UR,
            LR,
            LL,
            L,
            R,
            T,
            B
        }
    }
}

