using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x02000015 RID: 21
	public class MainNavService
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004C1D File Offset: 0x00002E1D
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00004C14 File Offset: 0x00002E14
		public ObservableCollection<UserControl> NavBarItems { get; set; }

		// Token: 0x060000C3 RID: 195 RVA: 0x00004C25 File Offset: 0x00002E25
		private MainNavService()
		{
			this._history = new Stack<UserControl>();
			this.NavBarItems = new ObservableCollection<UserControl>();
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004C43 File Offset: 0x00002E43
		public static MainNavService Instance
		{
			get
			{
				return MainNavService._instance;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004C4A File Offset: 0x00002E4A
		private Grid MainFrame
		{
			get
			{
				if (this._mainFrame == null)
				{
					this._mainFrame = (VisualHelper.GetDescendantFromName(Application.Current.MainWindow, "XGdMainFrame") as Grid);
				}
				return this._mainFrame;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004C7C File Offset: 0x00002E7C
		public void GoBack()
		{
			if (this._history.Count > 0)
			{
				UserControl userControl = this._history.Pop();
				this.MainFrame.Children.Remove(userControl);
				this.NavBarItems.Remove(userControl);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004CC1 File Offset: 0x00002EC1
		public void Clear()
		{
			this._history.Clear();
			this.NavBarItems.Clear();
			this.MainFrame.Children.Clear();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004CEC File Offset: 0x00002EEC
		public void NavigateTo(UserControl uc)
		{
			this.MainFrame.Children.Add(uc);
			this._history.Push(uc);
			this.NavBarItems.Add(uc);
			uc.Focusable = true;
			uc.IsTabStop = true;
			uc.Focus();
			Keyboard.Focus(uc);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004D40 File Offset: 0x00002F40
		public void JumpBackTo(UserControl target)
		{
			if (target == null)
			{
				return;
			}
			while (this._history.Any<UserControl>() && this._history.Peek() != target)
			{
				UserControl userControl = this._history.Peek();
				bool isContinue = true;
				INav nav = userControl as INav;
				if (nav != null && !nav.IsReadyNav())
				{
					CommonDialog.Instance.Confirm("您有未保存的修改信息，仍要切换页面？", "确定提示", delegate(bool b)
					{
						isContinue = b;
					});
				}
				if (!isContinue)
				{
					break;
				}
				this._history.Pop();
				this.MainFrame.Children.Remove(userControl);
				this.NavBarItems.Remove(userControl);
			}
		}

		// Token: 0x04000068 RID: 104
		private readonly Stack<UserControl> _history;

		// Token: 0x0400006A RID: 106
		private static MainNavService _instance = new MainNavService();

		// Token: 0x0400006B RID: 107
		private Grid _mainFrame;
	}
}
