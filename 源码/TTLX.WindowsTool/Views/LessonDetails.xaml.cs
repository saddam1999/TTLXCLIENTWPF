using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x02000010 RID: 16
	public partial class LessonDetails : UserControl, INav
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003D89 File Offset: 0x00001F89
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003D9B File Offset: 0x00001F9B
		public Lesson LessonInfo
		{
			get
			{
				return (Lesson)base.GetValue(LessonDetails.LessonInfoProperty);
			}
			set
			{
				base.SetValue(LessonDetails.LessonInfoProperty, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003DA9 File Offset: 0x00001FA9
		public BookTypeEnum BookType
		{
			get
			{
				return AppData.Current.CurrentBook.Type;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003DBC File Offset: 0x00001FBC
		public LessonDetails(int id = -1)
		{
			this.InitializeComponent();
			this._lessonId = id;
			base.DataContext = this;
			this.Init();
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003E0D File Offset: 0x0000200D
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			BindingOperations.ClearAllBindings(this);
			AppData.Current.ClearLesson();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003E1F File Offset: 0x0000201F
		private void Init()
		{
			base.SetBinding(LessonDetails.LessonInfoProperty, new Binding("CurrentLesson")
			{
				Source = AppData.Current
			});
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003E44 File Offset: 0x00002044
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (this._lessonId != -1)
			{
				if (!(await AppData.Current.GetLessonInfo(this._lessonId)))
				{
					MainNavService.Instance.GoBack();
					return;
				}
				Lesson lessonInfo = this.LessonInfo;
				NavControlEx.SetNavDisplayName(this, (lessonInfo != null) ? lessonInfo.Name : null);
			}
			else
			{
				AppData.Current.NewLesson();
				NavControlEx.SetNavDisplayName(this, "新建课程");
			}
			LessonDetails lessonDetails = this;
			Lesson initLesson = lessonDetails._initLesson;
			lessonDetails._initLesson = await ObjectHelper.DeepCopyAsync<Lesson>(this.LessonInfo);
			lessonDetails = null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003E80 File Offset: 0x00002080
		private async void XBtnSaveLesson_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.LessonInfo.IsValidated)
			{
				if (this.LessonInfo.IsSaved)
				{
					if (await AppData.Current.UpdateLesson())
					{
						NavControlEx.SetNavDisplayName(this, this.LessonInfo.Name);
					}
				}
				else if (await AppData.Current.CreateLesson())
				{
					NavControlEx.SetNavDisplayName(this, this.LessonInfo.Name);
				}
				LessonDetails lessonDetails = this;
				Lesson initLesson = lessonDetails._initLesson;
				lessonDetails._initLesson = await ObjectHelper.DeepCopyAsync<Lesson>(this.LessonInfo);
				lessonDetails = null;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003EB9 File Offset: 0x000020B9
		private void XSelectHorCover_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.LessonInfo.ImgLandscapeUrl = p;
			}, 16, 9, 480));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003EDA File Offset: 0x000020DA
		private void XSelectVerCover_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.LessonInfo.ImgPortraitUrl = p;
			}, 3, 4, 480));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003EFC File Offset: 0x000020FC
		private void XBtnAddCon_OnClick(object sender, RoutedEventArgs e)
		{
			BookTypeEnum bookType = this.BookType;
			switch (bookType)
			{
			case BookTypeEnum.横屏听读课本:
			case BookTypeEnum.素材课本:
				this.TopicTypeOnSelect(TopicTypeEnum.听读横图);
				return;
			case BookTypeEnum.配音课本:
				this.TopicTypeOnSelect(TopicTypeEnum.配音);
				return;
			case BookTypeEnum.点读课本:
				this.TopicTypeOnSelect(TopicTypeEnum.点读);
				return;
			case BookTypeEnum.儿歌课本:
				this.XSelection.SetItemsSource(new TopicTypeEnum[]
				{
					TopicTypeEnum.儿歌音频,
					TopicTypeEnum.儿歌视频
				});
				this.XPopTopicTypeSelection.IsOpen = true;
				return;
			case BookTypeEnum.文本课本:
				this.TopicTypeOnSelect(TopicTypeEnum.纯文本);
				return;
			case BookTypeEnum.竖屏听读课本:
				this.TopicTypeOnSelect(TopicTypeEnum.听读竖图);
				return;
			case BookTypeEnum.混合课本:
				this.XSelection.SetItemsSource(Topic.ContentType);
				this.XPopTopicTypeSelection.IsOpen = true;
				return;
			case BookTypeEnum.教研题包:
				break;
			default:
				if (bookType == BookTypeEnum.测评课本)
				{
					this.XSelection.SetItemsSource(new TopicTypeEnum[]
					{
						TopicTypeEnum.新单屏练习,
						TopicTypeEnum.新分屏练习
					});
					this.XPopTopicTypeSelection.IsOpen = true;
					return;
				}
				break;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003FDE File Offset: 0x000021DE
		private void XBtnAddExer_OnClick(object sender, RoutedEventArgs e)
		{
			this.XSelection.SetItemsSource(Topic.ExerciseType);
			this.XPopTopicTypeSelection.IsOpen = true;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003FFC File Offset: 0x000021FC
		public bool IsReadyNav()
		{
			if (this.LessonInfo == null)
			{
				LogHelper.Error("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@返回前LessonInfo为空@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
				return true;
			}
			return !this.LessonInfo.HasChanged(this._initLesson);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004026 File Offset: 0x00002226
		private void TopicTypeOnSelect(TopicTypeEnum t)
		{
			MainNavService.Instance.NavigateTo(new TopicDetails(t));
			this.XPopTopicTypeSelection.IsOpen = false;
		}

		// Token: 0x04000044 RID: 68
		public static readonly DependencyProperty LessonInfoProperty = DependencyProperty.Register("LessonInfo", typeof(Lesson), typeof(LessonDetails), new PropertyMetadata(null));

		// Token: 0x04000045 RID: 69
		private readonly int _lessonId;

		// Token: 0x04000046 RID: 70
		private Lesson _initLesson;
	}
}
