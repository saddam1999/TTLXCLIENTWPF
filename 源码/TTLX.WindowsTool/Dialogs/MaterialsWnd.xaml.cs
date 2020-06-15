using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.MaterialMedia;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x02000089 RID: 137
	public partial class MaterialsWnd : CMetroWindow
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0001EF90 File Offset: 0x0001D190
		public MaterialsWnd(bool? mode = null, Action<MediaItem> onSelected = null)
		{
			this.InitializeComponent();
			if (mode != null)
			{
				this.XTab.Visibility = Visibility.Collapsed;
				if (mode.Value)
				{
					this.XPicLst.Visibility = Visibility.Visible;
					this.XAdoLst.Visibility = Visibility.Hidden;
				}
				else
				{
					this.XPicLst.Visibility = Visibility.Hidden;
					this.XAdoLst.Visibility = Visibility.Visible;
				}
				this.XPicLst.IsEditMode = false;
				this.XAdoLst.IsEditMode = false;
			}
			else
			{
				this.XPicLst.IsEditMode = true;
				this.XAdoLst.IsEditMode = true;
			}
			this._onSelectMediaItem = onSelected;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001F032 File Offset: 0x0001D232
		private void OnMediaItemSelected(MediaItem item)
		{
			if (item != null)
			{
				Action<MediaItem> onSelectMediaItem = this._onSelectMediaItem;
				if (onSelectMediaItem != null)
				{
					onSelectMediaItem(item);
				}
			}
			base.Close();
		}

		// Token: 0x04000307 RID: 775
		private readonly Action<MediaItem> _onSelectMediaItem;
	}
}
