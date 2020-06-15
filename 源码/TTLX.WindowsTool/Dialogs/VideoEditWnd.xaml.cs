using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using MahApps.Metro.Controls;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x0200008F RID: 143
	public partial class VideoEditWnd : MetroWindow
	{
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600069D RID: 1693 RVA: 0x0001FBD0 File Offset: 0x0001DDD0
		// (remove) Token: 0x0600069E RID: 1694 RVA: 0x0001FC08 File Offset: 0x0001DE08
		private event Action<string> SaveAction;

		// Token: 0x0600069F RID: 1695 RVA: 0x0001FC3D File Offset: 0x0001DE3D
		public VideoEditWnd(string mediaPath, Action<string> onSaved)
		{
			this.InitializeComponent();
			this.XMedia.Source = new Uri(mediaPath);
			this._path = mediaPath;
			this.SaveAction = onSaved;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001FC6C File Offset: 0x0001DE6C
		private async void XBtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			int valueOrDefault = this.XMedia.NaturalVideoWidth.GetValueOrDefault(0);
			int valueOrDefault2 = this.XMedia.NaturalVideoHeight.GetValueOrDefault(0);
			if (valueOrDefault != 0 && valueOrDefault2 != 0)
			{
				int num = 960;
				int num2;
				int num3;
				if (valueOrDefault > valueOrDefault2)
				{
					num2 = num;
					num3 = (int)(num2 / valueOrDefault * valueOrDefault2);
				}
				else
				{
					num3 = num;
					num2 = (int)(num3 / valueOrDefault2 * valueOrDefault);
				}
				this.Close();
				double lowerValue = this.XRangeSlider.LowerValue;
				double upperValue = this.XRangeSlider.UpperValue;
				string outputPath = Helper.GetTempMp4Path();
				if (await MediaHelper.CutVideoToMp4(this._path, outputPath, num2, num3, lowerValue, upperValue))
				{
					Action<string> saveAction = this.SaveAction;
					if (saveAction != null)
					{
						saveAction(outputPath);
					}
				}
				else
				{
					MessengerHelper.ShowToast("视频加载发生异常，重试或者更换视频");
				}
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001FCA8 File Offset: 0x0001DEA8
		private async void XBtnCropSave_OnClick(object sender, RoutedEventArgs e)
		{
			int valueOrDefault = this.XMedia.NaturalVideoWidth.GetValueOrDefault(0);
			int valueOrDefault2 = this.XMedia.NaturalVideoHeight.GetValueOrDefault(0);
			if (valueOrDefault != 0 && valueOrDefault2 != 0)
			{
				int left = 0;
				int top = 0;
				int cropWidth = valueOrDefault;
				int cropHeight = valueOrDefault2;
				if (valueOrDefault / valueOrDefault2 > 1)
				{
					left = (valueOrDefault - valueOrDefault2 * 16 / 9) / 2;
					cropWidth = valueOrDefault2 * 16 / 9;
				}
				else
				{
					top = (valueOrDefault2 - valueOrDefault * 9 / 16) / 2;
					cropHeight = valueOrDefault * 9 / 16;
				}
				this.Close();
				double lowerValue = this.XRangeSlider.LowerValue;
				double upperValue = this.XRangeSlider.UpperValue;
				string outputPath = Helper.GetTempMp4Path();
				if (await MediaHelper.CutVideoToMp4(this._path, outputPath, cropWidth, cropHeight, left, top, lowerValue, upperValue))
				{
					Action<string> saveAction = this.SaveAction;
					if (saveAction != null)
					{
						saveAction(outputPath);
					}
				}
				else
				{
					MessengerHelper.ShowToast("视频加载发生异常，重试或者更换视频");
				}
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001FCE4 File Offset: 0x0001DEE4
		private void XBtnPlay_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.XBtnPlay.IsChecked == true)
			{
				this.XViewer.ScrollToBottom();
				TimeSpan from = TimeSpan.FromMilliseconds(this.XRangeSlider.LowerValue);
				TimeSpan to = TimeSpan.FromMilliseconds(this.XRangeSlider.UpperValue);
				this.XMedia.PlayPart(from, to);
				return;
			}
			this.XViewer.ScrollToTop();
			this.XMedia.Stop();
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001FD66 File Offset: 0x0001DF66
		private void XRangeSlider_OnLowerValueChanged(object sender, RangeParameterChangedEventArgs e)
		{
			this.XMedia.Position = TimeSpan.FromMilliseconds(this.XRangeSlider.LowerValue);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001FD83 File Offset: 0x0001DF83
		private void XRangeSlider_OnUpperValueChanged(object sender, RangeParameterChangedEventArgs e)
		{
			this.XMedia.Position = TimeSpan.FromMilliseconds(this.XRangeSlider.UpperValue);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
		private void XMedia_OnMediaStop()
		{
			if (this.XBtnPlay.IsChecked == true)
			{
				this.XViewer.ScrollToTop();
				this.XBtnPlay.IsChecked = new bool?(false);
			}
		}

		// Token: 0x04000331 RID: 817
		private string _path;
	}
}
