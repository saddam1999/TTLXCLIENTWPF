using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.KET
{
	// Token: 0x0200007E RID: 126
	public partial class KETLessonDetails : UserControl
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001CA3D File Offset: 0x0001AC3D
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public Lesson LessonInfo
		{
			get
			{
				return (Lesson)base.GetValue(KETLessonDetails.LessonInfoProperty);
			}
			set
			{
				base.SetValue(KETLessonDetails.LessonInfoProperty, value);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001CA60 File Offset: 0x0001AC60
		public KETLessonDetails(int id = -1)
		{
			this.InitializeComponent();
			this._lessonId = id;
			base.DataContext = this;
			this.Init();
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001CAB1 File Offset: 0x0001ACB1
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			BindingOperations.ClearAllBindings(this);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001CAB9 File Offset: 0x0001ACB9
		private void Init()
		{
			base.SetBinding(KETLessonDetails.LessonInfoProperty, new Binding("CurrentLesson")
			{
				Source = AppData.Current,
				Mode = BindingMode.TwoWay
			});
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
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
				await AppData.Current.GetPackageQuestionsByLesson();
			}
			else
			{
				AppData.Current.NewLesson();
				NavControlEx.SetNavDisplayName(this, "新建课程");
			}
			KETLessonDetails ketlessonDetails = this;
			Lesson initLesson = ketlessonDetails._initLesson;
			ketlessonDetails._initLesson = await ObjectHelper.DeepCopyAsync<Lesson>(this.LessonInfo);
			ketlessonDetails = null;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001CB20 File Offset: 0x0001AD20
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
				KETLessonDetails ketlessonDetails = this;
				Lesson initLesson = ketlessonDetails._initLesson;
				ketlessonDetails._initLesson = await ObjectHelper.DeepCopyAsync<Lesson>(this.LessonInfo);
				ketlessonDetails = null;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001CB5C File Offset: 0x0001AD5C
		private async void XBtnSaveQuestions_OnClick(object sender, RoutedEventArgs e)
		{
			await this.XLstQuestions.Save();
		}

		// Token: 0x0400029D RID: 669
		public static readonly DependencyProperty LessonInfoProperty = DependencyProperty.Register("LessonInfo", typeof(Lesson), typeof(KETLessonDetails), new PropertyMetadata(null));

		// Token: 0x0400029E RID: 670
		private readonly int _lessonId;

		// Token: 0x0400029F RID: 671
		private Lesson _initLesson;
	}
}
