using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using CefSharp;
using CefSharp.Wpf;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages
{
	// Token: 0x02000032 RID: 50
	public partial class TopicPackageQuestionCheckDetails : UserControl
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000AB14 File Offset: 0x00008D14
		public TopicPackageQuestionCheckDetails(int id)
		{
			this.InitializeComponent();
			this._lessonId = id;
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
			this._jsEvent.OpenParams += this.JsEventOnOpenParams;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		protected void ExecuteJavaScript(string s)
		{
			try
			{
				this.XBrowser.ExecuteScriptAsync(s);
			}
			catch
			{
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		private void JsEventOnOpenParams(string s)
		{
			DispatcherHelper.CheckBeginInvokeOnUI(delegate
			{
				if (this.CurrentQuestion != null)
				{
					JObject jobject = JObject.Parse(s);
					string str = this.CurrentQuestion.ToHtmlJsonString();
					string str2 = JsonConvert.SerializeObject(new
					{
						detail = new
						{
							__errorCode = 0,
							__errorStr = "",
							__signature = jobject["__signature"].ToString(),
							data = new
							{
								questions = "[" + str + "]"
							}
						}
					});
					string s2 = "javascript: window.dispatchEvent(new CustomEvent('ttlx::response', " + str2 + "))";
					this.ExecuteJavaScript(s2);
				}
			});
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000ABF5 File Offset: 0x00008DF5
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			AppData.Current.ClearTopicPackageLesson();
			ChromiumWebBrowser xbrowser = this.XBrowser;
			if (xbrowser == null)
			{
				return;
			}
			IBrowser browser = xbrowser.GetBrowser();
			if (browser == null)
			{
				return;
			}
			browser.CloseBrowser(true);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000AC1C File Offset: 0x00008E1C
		public Lesson LessonInfo
		{
			get
			{
				return AppData.Current.CurrentLesson;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000AC28 File Offset: 0x00008E28
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000AC3A File Offset: 0x00008E3A
		public int Index
		{
			get
			{
				return (int)base.GetValue(TopicPackageQuestionCheckDetails.IndexProperty);
			}
			set
			{
				base.SetValue(TopicPackageQuestionCheckDetails.IndexProperty, value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000AC4D File Offset: 0x00008E4D
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public int AllCount
		{
			get
			{
				return (int)base.GetValue(TopicPackageQuestionCheckDetails.AllCountProperty);
			}
			set
			{
				base.SetValue(TopicPackageQuestionCheckDetails.AllCountProperty, value);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000AC74 File Offset: 0x00008E74
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this._allQuestions.Clear();
			if (await AppData.Current.GetTopicPackageLesson(this._lessonId))
			{
				Lesson lessonInfo = this.LessonInfo;
				NavControlEx.SetNavDisplayName(this, ((lessonInfo != null) ? lessonInfo.Name : null) + " 预览");
				await AppData.Current.GetPackageQuestionsByLesson();
				await AppData.Current.GetBeforePackageQuestionsByLesson();
				foreach (BeforeLessonPackageGroup beforeLessonPackageGroup in AppData.Current.BeforeLessonPackageGroup)
				{
					foreach (TopicPackageQuestion item in beforeLessonPackageGroup.Questions)
					{
						this._allQuestions.Add(item);
					}
				}
				this._allQuestions.AddRange(AppData.Current.AfterQuestions);
				this.AllCount = this._allQuestions.Count;
				this.Index = ((this.AllCount == 0) ? 0 : 1);
				this.XBrowser.RegisterAsyncJsObject("ttlx_app_entity", this._jsEvent, true);
				this.XBrowser.Address = "http://6tiantian.com/web/question_bank/index.vhtml?entry=direct_preview";
				this.XBdBrowser.Child = this.XBrowser;
				this.ShowQuestionPreview(false);
			}
			else
			{
				MainNavService.Instance.GoBack();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public RelayCommand NextCmd
		{
			get
			{
				RelayCommand result;
				if ((result = this._nextCmd) == null)
				{
					result = (this._nextCmd = new RelayCommand(delegate()
					{
						if (this.Index < this._allQuestions.Count)
						{
							this.Index++;
						}
						else
						{
							MessengerHelper.ShowToast("这已经是最后一题了");
						}
						this.ShowQuestionPreview(false);
					}));
				}
				return result;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		public RelayCommand PreCmd
		{
			get
			{
				RelayCommand result;
				if ((result = this._preCmd) == null)
				{
					result = (this._preCmd = new RelayCommand(delegate()
					{
						if (this.Index > 1)
						{
							this.Index--;
						}
						else
						{
							MessengerHelper.ShowToast("这已经是第一题了");
						}
						this.ShowQuestionPreview(false);
					}));
				}
				return result;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000AD18 File Offset: 0x00008F18
		private async Task<bool> IsSkipCurrent()
		{
			int idx = this.Index;
			await TaskEx.Delay(200);
			return idx != this.Index;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000AD60 File Offset: 0x00008F60
		private void ShowQuestionPreview(bool ignore = false)
		{
			int i = this.Index - 1;
			if (ignore)
			{
				while (i < this._allQuestions.Count)
				{
					if (i < 0)
					{
						break;
					}
					this.Index = i + 1;
					this.CurrentQuestion = this._allQuestions[i];
					if (!this.CurrentQuestion.IsChecked)
					{
						break;
					}
					i++;
				}
			}
			else if (i < this._allQuestions.Count && i >= 0)
			{
				this.CurrentQuestion = this._allQuestions[i];
			}
			this.RefreshCurrentQuestionHtmlView();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000ADE5 File Offset: 0x00008FE5
		private void RefreshCurrentQuestionHtmlView()
		{
			if (this.XBrowser.IsBrowserInitialized)
			{
				this.XBrowser.Reload(true);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000AE00 File Offset: 0x00009000
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000AE12 File Offset: 0x00009012
		public TopicPackageQuestion CurrentQuestion
		{
			get
			{
				return (TopicPackageQuestion)base.GetValue(TopicPackageQuestionCheckDetails.CurrentQuestionProperty);
			}
			set
			{
				base.SetValue(TopicPackageQuestionCheckDetails.CurrentQuestionProperty, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000AE29 File Offset: 0x00009029
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000AE20 File Offset: 0x00009020
		public bool IsIgnoreChecked { get; set; } = true;

		// Token: 0x06000244 RID: 580 RVA: 0x0000AE31 File Offset: 0x00009031
		private void XBtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new PackageQuestionEditWnd(this.CurrentQuestion, async delegate(TopicPackageQuestion question)
			{
				if (await AppData.Current.SavePackageQuestion(this.CurrentQuestion))
				{
					this.RefreshCurrentQuestionHtmlView();
					MessengerHelper.ShowToast("保存成功");
				}
			}, delegate(TopicPackageQuestion q)
			{
				if (q.IsSaved)
				{
					CommonDialog.Instance.Confirm("确认删除该题目吗？", "确认操作", async delegate(bool b)
					{
						if (b && await AppData.Current.DeletePackageQuestion(q))
						{
							this._allQuestions.Remove(q);
						}
					});
					return;
				}
				this._allQuestions.Remove(q);
			}));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AE5C File Offset: 0x0000905C
		private async void XBtnPass_OnClick(object sender, RoutedEventArgs e)
		{
			bool flag = true;
			if (!this.CurrentQuestion.IsChecked)
			{
				flag = await AppData.Current.CheckPackageQuestion(this.CurrentQuestion);
			}
			if (flag)
			{
				this.CurrentQuestion.IsChecked = true;
				if (this.Index < this._allQuestions.Count)
				{
					this.Index++;
				}
				else
				{
					MessengerHelper.ShowToast("这已经是最后一题了");
				}
				this.ShowQuestionPreview(this.IsIgnoreChecked);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AE95 File Offset: 0x00009095
		private void DebugPanel_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				this.XBrowser.ShowDevTools();
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000AEAB File Offset: 0x000090AB
		private void XBar_OnScroll(object sender, ScrollEventArgs e)
		{
			this.ShowQuestionPreview(false);
		}

		// Token: 0x04000106 RID: 262
		private ChromiumWebBrowser XBrowser = new ChromiumWebBrowser();

		// Token: 0x04000107 RID: 263
		private TopicPackageQuestionCheckDetails.JsEvent _jsEvent = new TopicPackageQuestionCheckDetails.JsEvent();

		// Token: 0x04000108 RID: 264
		private readonly int _lessonId;

		// Token: 0x04000109 RID: 265
		public static readonly DependencyProperty IndexProperty = DependencyProperty.Register("Index", typeof(int), typeof(TopicPackageQuestionCheckDetails), new PropertyMetadata(0));

		// Token: 0x0400010A RID: 266
		public static readonly DependencyProperty AllCountProperty = DependencyProperty.Register("AllCount", typeof(int), typeof(TopicPackageQuestionCheckDetails), new PropertyMetadata(0));

		// Token: 0x0400010B RID: 267
		private readonly List<TopicPackageQuestion> _allQuestions = new List<TopicPackageQuestion>();

		// Token: 0x0400010C RID: 268
		private RelayCommand _nextCmd;

		// Token: 0x0400010D RID: 269
		private RelayCommand _preCmd;

		// Token: 0x0400010E RID: 270
		public static readonly DependencyProperty CurrentQuestionProperty = DependencyProperty.Register("CurrentQuestion", typeof(TopicPackageQuestion), typeof(TopicPackageQuestionCheckDetails), new PropertyMetadata(null));

		// Token: 0x02000123 RID: 291
		public class JsEvent
		{
			// Token: 0x14000032 RID: 50
			// (add) Token: 0x060008F8 RID: 2296 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
			// (remove) Token: 0x060008F9 RID: 2297 RVA: 0x0002E120 File Offset: 0x0002C320
			public event Action<string> OpenParams;

			// Token: 0x060008FA RID: 2298 RVA: 0x0002E155 File Offset: 0x0002C355
			public void getOpenParams(string str)
			{
				Action<string> openParams = this.OpenParams;
				if (openParams == null)
				{
					return;
				}
				openParams(str);
			}

			// Token: 0x060008FB RID: 2299 RVA: 0x0002E168 File Offset: 0x0002C368
			public void ready(string str)
			{
			}
		}
	}
}
