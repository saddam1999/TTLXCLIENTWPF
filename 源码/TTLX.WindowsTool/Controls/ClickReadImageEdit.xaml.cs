using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x0200009D RID: 157
	public partial class ClickReadImageEdit : UserControl
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x00021D55 File Offset: 0x0001FF55
		public ClickReadImageEdit()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00021D75 File Offset: 0x0001FF75
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestions();
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00021D7D File Offset: 0x0001FF7D
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x00021D8F File Offset: 0x0001FF8F
		public ObservableCollection<Question> Questions
		{
			get
			{
				return (ObservableCollection<Question>)base.GetValue(ClickReadImageEdit.QuestionsProperty);
			}
			set
			{
				base.SetValue(ClickReadImageEdit.QuestionsProperty, value);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00021D9D File Offset: 0x0001FF9D
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00021DAF File Offset: 0x0001FFAF
		public string ImgPath
		{
			get
			{
				return (string)base.GetValue(ClickReadImageEdit.ImgPathProperty);
			}
			set
			{
				base.SetValue(ClickReadImageEdit.ImgPathProperty, value);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00021DBD File Offset: 0x0001FFBD
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x00021DCF File Offset: 0x0001FFCF
		public bool IsEditable
		{
			get
			{
				return (bool)base.GetValue(ClickReadImageEdit.IsEditableProperty);
			}
			set
			{
				base.SetValue(ClickReadImageEdit.IsEditableProperty, value);
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00021DE4 File Offset: 0x0001FFE4
		public void UpdateQuestions()
		{
			if (this.Questions != null && this.XCav.ActualWidth > 0.0 && this.XCav.ActualHeight > 0.0)
			{
				this.XCav.Children.Clear();
				for (int i = 0; i < this.Questions.Count; i++)
				{
					Question q = this.Questions[i];
					if (q.ClickReadInfo != null)
					{
						double x = this.XCav.ActualWidth * q.ClickReadInfo.Left;
						double y = this.XCav.ActualHeight * q.ClickReadInfo.Top;
						double width = this.XCav.ActualWidth * (q.ClickReadInfo.Right - q.ClickReadInfo.Left);
						double height = this.XCav.ActualHeight * (q.ClickReadInfo.Bottom - q.ClickReadInfo.Top);
						ClickReadImageEditItem clickReadImageEditItem = new ClickReadImageEditItem(x, y, width, height, q.Idx);
						clickReadImageEditItem.Delete += delegate()
						{
							Action<Question> deleteQuestion = this.DeleteQuestion;
							if (deleteQuestion == null)
							{
								return;
							}
							deleteQuestion(q);
						};
						clickReadImageEditItem.Edit += delegate()
						{
							this._editingQuestion = q;
							this._editingClickReadInfo = q.ClickReadInfo;
							q.ClickReadInfo = null;
							this.UpdateQuestions();
						};
						clickReadImageEditItem.Select += delegate()
						{
							Action<Question> questionLocation = this.QuestionLocation;
							if (questionLocation == null)
							{
								return;
							}
							questionLocation(q);
						};
						this.XCav.Children.Add(clickReadImageEditItem);
					}
				}
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000732 RID: 1842 RVA: 0x00021F84 File Offset: 0x00020184
		// (remove) Token: 0x06000733 RID: 1843 RVA: 0x00021FBC File Offset: 0x000201BC
		public event Action AddImage;

		// Token: 0x06000734 RID: 1844 RVA: 0x00021FF4 File Offset: 0x000201F4
		private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!this._isDragging)
			{
				this._isDragging = true;
				this._recSelection = new System.Windows.Shapes.Rectangle();
				this._recSelection.Fill = System.Windows.Media.Brushes.LightSkyBlue;
				this._recSelection.Opacity = 0.2;
				this.XCav.Children.Add(this._recSelection);
				this._downX = e.GetPosition(this.XCav).X;
				this._downY = e.GetPosition(this.XCav).Y;
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00022090 File Offset: 0x00020290
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (this._isDragging)
			{
				double x = e.GetPosition(this.XCav).X;
				double y = e.GetPosition(this.XCav).Y;
				double length = Math.Min(x, this._downX);
				double length2 = Math.Min(y, this._downY);
				Canvas.SetLeft(this._recSelection, length);
				Canvas.SetTop(this._recSelection, length2);
				this._recSelection.Width = Math.Abs(x - this._downX);
				this._recSelection.Height = Math.Abs(y - this._downY);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000736 RID: 1846 RVA: 0x00022138 File Offset: 0x00020338
		// (remove) Token: 0x06000737 RID: 1847 RVA: 0x00022170 File Offset: 0x00020370
		public event Action<Question> QuestionLocation;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000738 RID: 1848 RVA: 0x000221A8 File Offset: 0x000203A8
		// (remove) Token: 0x06000739 RID: 1849 RVA: 0x000221E0 File Offset: 0x000203E0
		public event Action<ClickReadInfo, Bitmap> AddQuestion;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600073A RID: 1850 RVA: 0x00022218 File Offset: 0x00020418
		// (remove) Token: 0x0600073B RID: 1851 RVA: 0x00022250 File Offset: 0x00020450
		public event Action<Question> DeleteQuestion;

		// Token: 0x0600073C RID: 1852 RVA: 0x00022288 File Offset: 0x00020488
		private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			try
			{
				if (this._isDragging)
				{
					this.XCav.Children.Remove(this._recSelection);
					if (this._recSelection.Width > 20.0 && this._recSelection.Height > 20.0)
					{
						ClickReadInfo clickReadInfo = new ClickReadInfo
						{
							Left = Canvas.GetLeft(this._recSelection) / this.XCav.ActualWidth,
							Top = Canvas.GetTop(this._recSelection) / this.XCav.ActualHeight,
							Right = (Canvas.GetLeft(this._recSelection) + this._recSelection.Width) / this.XCav.ActualWidth,
							Bottom = (Canvas.GetTop(this._recSelection) + this._recSelection.Height) / this.XCav.ActualHeight
						};
						if (this._editingQuestion != null)
						{
							this._editingQuestion.ClickReadInfo = clickReadInfo;
							this._editingQuestion = null;
							this.UpdateQuestions();
						}
						else
						{
							BitmapSource bitmapSource = (BitmapSource)this.XImg.Source;
							int pixelWidth = bitmapSource.PixelWidth;
							int pixelHeight = bitmapSource.PixelHeight;
							System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle
							{
								X = Convert.ToInt32(Canvas.GetLeft(this._recSelection) * (double)pixelWidth / this.XCav.ActualWidth),
								Y = Convert.ToInt32(Canvas.GetTop(this._recSelection) * (double)pixelHeight / this.XCav.ActualHeight),
								Width = Convert.ToInt32(this._recSelection.Width * (double)pixelWidth / this.XCav.ActualWidth),
								Height = Convert.ToInt32(this._recSelection.Height * (double)pixelHeight / this.XCav.ActualHeight)
							};
							Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
							BitmapData bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, bitmap.Size), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
							bitmapSource.CopyPixels(new Int32Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), bitmapData.Scan0, bitmapData.Height * bitmapData.Stride, bitmapData.Stride);
							bitmap.UnlockBits(bitmapData);
							Action<ClickReadInfo, Bitmap> addQuestion = this.AddQuestion;
							if (addQuestion != null)
							{
								addQuestion(clickReadInfo, bitmap);
							}
						}
					}
					this._isDragging = false;
				}
			}
			catch (Exception pException)
			{
				LogHelper.Error("ClickReadImageEdit->OnMouseLeftButtonUp:", pException);
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00022530 File Offset: 0x00020730
		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.UpdateQuestions();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00022538 File Offset: 0x00020738
		private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this._editingQuestion != null)
			{
				this._editingQuestion.ClickReadInfo = this._editingClickReadInfo;
				this.UpdateQuestions();
				this._editingQuestion = null;
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00022560 File Offset: 0x00020760
		private void Image_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.ImgPath))
			{
				Action addImage = this.AddImage;
				if (addImage == null)
				{
					return;
				}
				addImage();
			}
		}

		// Token: 0x04000378 RID: 888
		public static readonly DependencyProperty QuestionsProperty = DependencyProperty.Register("Questions", typeof(ObservableCollection<Question>), typeof(ClickReadImageEdit), new PropertyMetadata(null));

		// Token: 0x04000379 RID: 889
		public static readonly DependencyProperty ImgPathProperty = DependencyProperty.Register("ImgPath", typeof(string), typeof(ClickReadImageEdit), new PropertyMetadata(null));

		// Token: 0x0400037A RID: 890
		public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(ClickReadImageEdit), new PropertyMetadata(false));

		// Token: 0x0400037B RID: 891
		private Question _editingQuestion;

		// Token: 0x0400037C RID: 892
		private ClickReadInfo _editingClickReadInfo;

		// Token: 0x0400037E RID: 894
		private System.Windows.Shapes.Rectangle _recSelection;

		// Token: 0x0400037F RID: 895
		private bool _isDragging;

		// Token: 0x04000380 RID: 896
		private double _downX;

		// Token: 0x04000381 RID: 897
		private double _downY;
	}
}
