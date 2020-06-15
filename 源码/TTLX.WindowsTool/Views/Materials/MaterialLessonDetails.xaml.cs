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

namespace TTLX.WindowsTool.Views.Materials
{
	// Token: 0x02000079 RID: 121
	public partial class MaterialLessonDetails : UserControl, INav
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0001BA26 File Offset: 0x00019C26
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x0001BA38 File Offset: 0x00019C38
		public Lesson LessonInfo
		{
			get
			{
				return (Lesson)base.GetValue(MaterialLessonDetails.LessonInfoProperty);
			}
			set
			{
				base.SetValue(MaterialLessonDetails.LessonInfoProperty, value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001BA46 File Offset: 0x00019C46
		public BookTypeEnum BookType
		{
			get
			{
				return AppData.Current.CurrentBook.Type;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001BA58 File Offset: 0x00019C58
		public MaterialLessonDetails(int id = -1)
		{
			this.InitializeComponent();
			this._lessonId = id;
			base.DataContext = this;
			this.Init();
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001BAAC File Offset: 0x00019CAC
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
				NavControlEx.SetNavDisplayName(this, "新建专辑");
			}
			MaterialLessonDetails materialLessonDetails = this;
			Lesson initLesson = materialLessonDetails._initLesson;
			materialLessonDetails._initLesson = await ObjectHelper.DeepCopyAsync<Lesson>(this.LessonInfo);
			materialLessonDetails = null;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001BAE5 File Offset: 0x00019CE5
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			BindingOperations.ClearAllBindings(this);
			AppData.Current.ClearLesson();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001BAF7 File Offset: 0x00019CF7
		private void Init()
		{
			base.SetBinding(MaterialLessonDetails.LessonInfoProperty, new Binding("CurrentLesson")
			{
				Source = AppData.Current,
				Mode = BindingMode.TwoWay
			});
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001BB24 File Offset: 0x00019D24
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
				MaterialLessonDetails materialLessonDetails = this;
				Lesson initLesson = materialLessonDetails._initLesson;
				materialLessonDetails._initLesson = await ObjectHelper.DeepCopyAsync<Lesson>(this.LessonInfo);
				materialLessonDetails = null;
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001BB5D File Offset: 0x00019D5D
		private void XBtnAddCon_OnClick(object sender, RoutedEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new TopicDetails(TopicTypeEnum.听读横图));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001BB6F File Offset: 0x00019D6F
		public bool IsReadyNav()
		{
			return !this.LessonInfo.HasChanged(this._initLesson);
		}

		// Token: 0x04000271 RID: 625
		public static readonly DependencyProperty LessonInfoProperty = DependencyProperty.Register("LessonInfo", typeof(Lesson), typeof(MaterialLessonDetails), new PropertyMetadata(null));

		// Token: 0x04000272 RID: 626
		private readonly int _lessonId;

		// Token: 0x04000273 RID: 627
		private Lesson _initLesson;
	}
}
