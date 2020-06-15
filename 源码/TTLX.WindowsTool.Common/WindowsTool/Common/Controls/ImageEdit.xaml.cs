using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x0200003F RID: 63
	public partial class ImageEdit : UserControl
	{
		// Token: 0x060001BD RID: 445 RVA: 0x000077FB File Offset: 0x000059FB
		public ImageEdit()
		{
			this.InitializeComponent();
			base.DataContext = this;
			this.MinSelectedRectangleWidth = 30.0;
			this.MinSelectedRectangleHeight = 30.0;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007837 File Offset: 0x00005A37
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000782E File Offset: 0x00005A2E
		public double MinSelectedRectangleWidth { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007848 File Offset: 0x00005A48
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000783F File Offset: 0x00005A3F
		public double MinSelectedRectangleHeight { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007850 File Offset: 0x00005A50
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00007862 File Offset: 0x00005A62
		public double SelRectX
		{
			get
			{
				return (double)base.GetValue(ImageEdit.SelRectXProperty);
			}
			set
			{
				base.SetValue(ImageEdit.SelRectXProperty, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007875 File Offset: 0x00005A75
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00007887 File Offset: 0x00005A87
		public double SelRectY
		{
			get
			{
				return (double)base.GetValue(ImageEdit.SelRectYProperty);
			}
			set
			{
				base.SetValue(ImageEdit.SelRectYProperty, value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000789A File Offset: 0x00005A9A
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x000078AC File Offset: 0x00005AAC
		public double SelRectWidth
		{
			get
			{
				return (double)base.GetValue(ImageEdit.SelRectWidthProperty);
			}
			set
			{
				base.SetValue(ImageEdit.SelRectWidthProperty, value);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000078BF File Offset: 0x00005ABF
		private static void WidthOrHeightPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000078C1 File Offset: 0x00005AC1
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000078D3 File Offset: 0x00005AD3
		public double SelRectHeight
		{
			get
			{
				return (double)base.GetValue(ImageEdit.SelRectHeightProperty);
			}
			set
			{
				base.SetValue(ImageEdit.SelRectHeightProperty, value);
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000078E8 File Offset: 0x00005AE8
		private ImageEdit.HitType SetHitType(System.Windows.Shapes.Rectangle rect, System.Windows.Point point)
		{
			double left = this.XGdRoot.ColumnDefinitions[0].ActualWidth;
			double top = this.XGdRoot.RowDefinitions[0].ActualHeight;
			double right = left + this.XRectSelected.Width;
			double bottom = top + this.XRectSelected.Height;
			if (point.X < left)
			{
				return ImageEdit.HitType.None;
			}
			if (point.X > right)
			{
				return ImageEdit.HitType.None;
			}
			if (point.Y < top)
			{
				return ImageEdit.HitType.None;
			}
			if (point.Y > bottom)
			{
				return ImageEdit.HitType.None;
			}
			if (point.X - left < 10.0)
			{
				if (point.Y - top < 10.0)
				{
					return ImageEdit.HitType.UL;
				}
				if (bottom - point.Y < 10.0)
				{
					return ImageEdit.HitType.LL;
				}
				return ImageEdit.HitType.L;
			}
			else if (right - point.X < 10.0)
			{
				if (point.Y - top < 10.0)
				{
					return ImageEdit.HitType.UR;
				}
				if (bottom - point.Y < 10.0)
				{
					return ImageEdit.HitType.LR;
				}
				return ImageEdit.HitType.R;
			}
			else
			{
				if (point.Y - top < 10.0)
				{
					return ImageEdit.HitType.T;
				}
				if (bottom - point.Y < 10.0)
				{
					return ImageEdit.HitType.B;
				}
				return ImageEdit.HitType.Body;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007A24 File Offset: 0x00005C24
		private void SetMouseCursor()
		{
			Cursor desired_cursor = Cursors.Arrow;
			switch (this.MouseHitType)
			{
			case ImageEdit.HitType.None:
				desired_cursor = Cursors.Arrow;
				break;
			case ImageEdit.HitType.Body:
				desired_cursor = Cursors.ScrollAll;
				break;
			case ImageEdit.HitType.UL:
			case ImageEdit.HitType.LR:
				desired_cursor = Cursors.SizeNWSE;
				break;
			case ImageEdit.HitType.UR:
			case ImageEdit.HitType.LL:
				desired_cursor = Cursors.SizeNESW;
				break;
			case ImageEdit.HitType.L:
			case ImageEdit.HitType.R:
				desired_cursor = Cursors.SizeWE;
				break;
			case ImageEdit.HitType.T:
			case ImageEdit.HitType.B:
				desired_cursor = Cursors.SizeNS;
				break;
			}
			if (base.Cursor != desired_cursor)
			{
				base.Cursor = desired_cursor;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007AAC File Offset: 0x00005CAC
		private void XGdRoot_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this.MouseHitType == ImageEdit.HitType.None)
			{
				return;
			}
			this.XGdRoot.CaptureMouse();
			this.LastPoint = Mouse.GetPosition(this.XGdRoot);
			this.DragInProgress = true;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00007AE4 File Offset: 0x00005CE4
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00007ADB File Offset: 0x00005CDB
		public double X1 { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00007AF5 File Offset: 0x00005CF5
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00007AEC File Offset: 0x00005CEC
		public double Y1 { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007B06 File Offset: 0x00005D06
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00007AFD File Offset: 0x00005CFD
		public double X2 { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00007B17 File Offset: 0x00005D17
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00007B0E File Offset: 0x00005D0E
		public double Y2 { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007B28 File Offset: 0x00005D28
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00007B1F File Offset: 0x00005D1F
		public int X { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007B39 File Offset: 0x00005D39
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00007B30 File Offset: 0x00005D30
		public int Y { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007B4A File Offset: 0x00005D4A
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00007B41 File Offset: 0x00005D41
		public new int Width { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007B5B File Offset: 0x00005D5B
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00007B52 File Offset: 0x00005D52
		public new int Height { get; set; }

		// Token: 0x060001DE RID: 478 RVA: 0x00007B64 File Offset: 0x00005D64
		private void XGdRoot_MouseMove(object sender, MouseEventArgs e)
		{
			if (!this.DragInProgress)
			{
				this.MouseHitType = this.SetHitType(this.XRectSelected, Mouse.GetPosition(this.XGdRoot));
				this.SetMouseCursor();
				return;
			}
			System.Windows.Point point = Mouse.GetPosition(this.XGdRoot);
			double offset_x = point.X - this.LastPoint.X;
			double offset_y = point.Y - this.LastPoint.Y;
			double new_x = this.XGdRoot.ColumnDefinitions[0].ActualWidth;
			double i_x = new_x;
			double new_y = this.XGdRoot.RowDefinitions[0].ActualHeight;
			double i_y = new_y;
			double new_width = this.XRectSelected.Width;
			double i_w = new_width;
			double new_height = this.XRectSelected.Height;
			double i_h = new_height;
			switch (this.MouseHitType)
			{
			case ImageEdit.HitType.Body:
				new_x += offset_x;
				new_y += offset_y;
				break;
			case ImageEdit.HitType.UL:
				new_x += offset_x;
				new_y += offset_y;
				new_width -= offset_x;
				new_height -= offset_y;
				break;
			case ImageEdit.HitType.UR:
				new_y += offset_y;
				new_width += offset_x;
				new_height -= offset_y;
				break;
			case ImageEdit.HitType.LR:
				new_width += offset_x;
				new_height += offset_y;
				break;
			case ImageEdit.HitType.LL:
				new_x += offset_x;
				new_width -= offset_x;
				new_height += offset_y;
				break;
			case ImageEdit.HitType.L:
				new_x += offset_x;
				new_width -= offset_x;
				break;
			case ImageEdit.HitType.R:
				new_width += offset_x;
				break;
			case ImageEdit.HitType.T:
				new_y += offset_y;
				new_height -= offset_y;
				break;
			case ImageEdit.HitType.B:
				new_height += offset_y;
				break;
			}
			if (new_x < 0.0)
			{
				new_x = i_x;
				new_width = i_w;
			}
			if (new_y < 0.0)
			{
				new_y = i_y;
				new_height = i_h;
			}
			if (new_width < this.MinSelectedRectangleWidth)
			{
				new_width = i_w;
				new_x = i_x;
			}
			if (new_height < this.MinSelectedRectangleHeight)
			{
				new_height = i_h;
				new_y = i_y;
			}
			if (new_x + new_width > this.XGdRoot.ActualWidth)
			{
				if (i_x == new_x)
				{
					new_width = this.XGdRoot.ActualWidth - new_x;
				}
				else
				{
					new_x = i_x;
				}
			}
			if (new_y + new_height > this.XGdRoot.ActualHeight)
			{
				if (i_y == new_y)
				{
					new_height = this.XGdRoot.ActualHeight - new_y;
				}
				else
				{
					new_y = i_y;
				}
			}
			this.SelRectX = new_x;
			this.SelRectY = new_y;
			this.SelRectWidth = new_width;
			this.SelRectHeight = new_height;
			this.LastPoint = point;
			this.XGdRoot.UpdateLayout();
			this.UpdateCroppedImg();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007DC0 File Offset: 0x00005FC0
		private void UpdateCroppedImg()
		{
			try
			{
				this.X1 = this.SelRectX / this.XImgTarget.ActualWidth;
				this.Y1 = this.SelRectY / this.XImgTarget.ActualHeight;
				this.X2 = (this.SelRectX + this.SelRectWidth) / this.XImgTarget.ActualWidth;
				this.Y2 = (this.SelRectY + this.SelRectHeight) / this.XImgTarget.ActualHeight;
				this.X = Convert.ToInt32((double)this._bitmap.PixelWidth * this.SelRectX / this.XImgTarget.ActualWidth);
				this.Y = Convert.ToInt32((double)this._bitmap.PixelHeight * this.SelRectY / this.XImgTarget.ActualHeight);
				this.Width = Convert.ToInt32((double)this._bitmap.PixelWidth * this.SelRectWidth / this.XImgTarget.ActualWidth);
				this.Height = Convert.ToInt32((double)this._bitmap.PixelHeight * this.SelRectHeight / this.XImgTarget.ActualHeight);
				Int32Rect cut = new Int32Rect(this.X, this.Y, this.Width, this.Height);
				int stride = this._bitmap.Format.BitsPerPixel * cut.Width / 8;
				byte[] data = new byte[cut.Height * stride];
				if (data.Length != 0)
				{
					this._bitmap.CopyPixels(cut, data, stride, 0);
					this.XImgCropped.Source = BitmapSource.Create(this.Width, this.Height, 0.0, 0.0, PixelFormats.Bgra32, null, data, stride);
				}
			}
			catch (Exception e)
			{
				LogHelper.Error("ImageEdit -> UpdateCroppedImg:", e);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007FA8 File Offset: 0x000061A8
		private void XGdRoot_MouseUp(object sender, MouseButtonEventArgs e)
		{
			this.DragInProgress = false;
			this.XGdRoot.ReleaseMouseCapture();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007FBC File Offset: 0x000061BC
		private void XGdRoot_OnMouseWheel(object sender, MouseWheelEventArgs e)
		{
			double w = this.SelRectWidth / this.SelRectHeight * 4.0;
			double h = 4.0;
			double new_x = this.XGdRoot.ColumnDefinitions[0].ActualWidth;
			double i_x = new_x;
			double new_y = this.XGdRoot.RowDefinitions[0].ActualHeight;
			double i_y = new_y;
			double new_width = this.XRectSelected.Width;
			double i_w = new_width;
			double new_height = this.XRectSelected.Height;
			double i_h = new_height;
			if (e.Delta > 0)
			{
				new_width += w;
				new_height += h;
				new_x -= w / 2.0;
				new_y -= h / 2.0;
				if (new_x < 0.0 || new_x + new_width > this.XGdRoot.ActualWidth || new_y < 0.0 || new_y + new_height > this.XGdRoot.ActualHeight)
				{
					new_x = i_x;
					new_width = i_w;
					new_y = i_y;
					new_height = i_h;
				}
			}
			else
			{
				new_width -= w;
				new_height -= h;
				new_x += w / 2.0;
				new_y += h / 2.0;
				if (new_width < this.MinSelectedRectangleWidth || new_height < this.MinSelectedRectangleHeight)
				{
					new_width = i_w;
					new_x = i_x;
					new_height = i_h;
					new_y = i_y;
				}
			}
			this.SelRectX = new_x;
			this.SelRectY = new_y;
			this.SelRectWidth = new_width;
			this.SelRectHeight = new_height;
			this.UpdateCroppedImg();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008134 File Offset: 0x00006334
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

		// Token: 0x060001E3 RID: 483 RVA: 0x000081B1 File Offset: 0x000063B1
		public void Init(string path)
		{
			this.Init(new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute))
			{
				CreateOptions = BitmapCreateOptions.IgnoreColorProfile
			});
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000081CC File Offset: 0x000063CC
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000081DE File Offset: 0x000063DE
		public bool HasImage
		{
			get
			{
				return (bool)base.GetValue(ImageEdit.HasImageProperty);
			}
			set
			{
				base.SetValue(ImageEdit.HasImageProperty, value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000081F1 File Offset: 0x000063F1
		public Bitmap CroppedImage
		{
			get
			{
				return ImageHelper.BitmapSource2Bitmap((BitmapSource)this.XImgCropped.Source);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00008208 File Offset: 0x00006408
		public Bitmap OriginImage
		{
			get
			{
				return ImageHelper.BitmapSource2Bitmap((BitmapSource)this.XImgTarget.Source);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008220 File Offset: 0x00006420
		public void CroppedImageByRatio(double width, double height)
		{
			double ratio = width / height;
			if (this.XImgTarget.ActualWidth < this.XImgTarget.ActualHeight * ratio)
			{
				this.SelRectWidth = (double)((int)this.XImgTarget.ActualWidth);
				this.SelRectHeight = (double)((int)(this.XImgTarget.ActualWidth / ratio));
				this.SelRectX = 0.0;
				this.SelRectY = (this.XImgTarget.ActualHeight - this.SelRectHeight) / 2.0;
			}
			else
			{
				this.SelRectHeight = (double)((int)this.XImgTarget.ActualHeight);
				this.SelRectWidth = (double)((int)(this.XImgTarget.ActualHeight * ratio));
				this.SelRectX = (this.XImgTarget.ActualWidth - this.SelRectWidth) / 2.0;
				this.SelRectY = 0.0;
			}
			this.UpdateCroppedImg();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008308 File Offset: 0x00006508
		public void RotateImage(int degree)
		{
			ISupportedImageFormat format = new JpegFormat
			{
				Quality = 100
			};
			using (ImageFactory imageFactory = new ImageFactory(false))
			{
				Bitmap bmp = ImageHelper.BitmapSource2Bitmap(this._bitmap);
				System.Drawing.Image image = imageFactory.Load(bmp).Rotate((float)degree).Format(format).Image;
				using (MemoryStream ms = new MemoryStream())
				{
					image.Save(ms, ImageFormat.Png);
					ms.Seek(0L, SeekOrigin.Begin);
					BitmapImage bitmapImage = new BitmapImage();
					bitmapImage.BeginInit();
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.StreamSource = ms;
					bitmapImage.EndInit();
					this.Init(bitmapImage);
				}
			}
		}

		// Token: 0x0400007E RID: 126
		public static readonly DependencyProperty SelRectXProperty = DependencyProperty.Register("SelRectX", typeof(double), typeof(ImageEdit), new PropertyMetadata(0.0));

		// Token: 0x0400007F RID: 127
		public static readonly DependencyProperty SelRectYProperty = DependencyProperty.Register("SelRectY", typeof(double), typeof(ImageEdit), new PropertyMetadata(0.0));

		// Token: 0x04000080 RID: 128
		public static readonly DependencyProperty SelRectWidthProperty = DependencyProperty.Register("SelRectWidth", typeof(double), typeof(ImageEdit), new PropertyMetadata(100.0, new PropertyChangedCallback(ImageEdit.WidthOrHeightPropertyChangedCallback)));

		// Token: 0x04000081 RID: 129
		public static readonly DependencyProperty SelRectHeightProperty = DependencyProperty.Register("SelRectHeight", typeof(double), typeof(ImageEdit), new PropertyMetadata(100.0, new PropertyChangedCallback(ImageEdit.WidthOrHeightPropertyChangedCallback)));

		// Token: 0x04000082 RID: 130
		private BitmapSource _bitmap;

		// Token: 0x04000083 RID: 131
		private bool DragInProgress;

		// Token: 0x04000084 RID: 132
		private System.Windows.Point LastPoint;

		// Token: 0x04000085 RID: 133
		private ImageEdit.HitType MouseHitType;

		// Token: 0x0400008E RID: 142
		public static readonly DependencyProperty HasImageProperty = DependencyProperty.Register("HasImage", typeof(bool), typeof(ImageEdit), new PropertyMetadata(false));

		// Token: 0x02000078 RID: 120
		private enum HitType
		{
			// Token: 0x040001B8 RID: 440
			None,
			// Token: 0x040001B9 RID: 441
			Body,
			// Token: 0x040001BA RID: 442
			UL,
			// Token: 0x040001BB RID: 443
			UR,
			// Token: 0x040001BC RID: 444
			LR,
			// Token: 0x040001BD RID: 445
			LL,
			// Token: 0x040001BE RID: 446
			L,
			// Token: 0x040001BF RID: 447
			R,
			// Token: 0x040001C0 RID: 448
			T,
			// Token: 0x040001C1 RID: 449
			B
		}
	}
}
