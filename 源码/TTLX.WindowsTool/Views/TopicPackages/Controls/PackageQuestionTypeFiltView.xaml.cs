using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000059 RID: 89
	public partial class PackageQuestionTypeFiltView : UserControl, IStyleConnector
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x00014E48 File Offset: 0x00013048
		public PackageQuestionTypeFiltView()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00014E7A File Offset: 0x0001307A
		private void AddCollectionChangedEvent()
		{
			this.PackageQuestions.CollectionChanged -= this.PackageQuestionsOnCollectionChanged;
			this.PackageQuestions.CollectionChanged += this.PackageQuestionsOnCollectionChanged;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00014EAC File Offset: 0x000130AC
		private void PackageQuestionsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			using (IEnumerator enumerator = ((IEnumerable)this.XItems.Items).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					QuestionTypeFiltItem item = (QuestionTypeFiltItem)enumerator.Current;
					item.HasQuestions = this.PackageQuestions.Any((TopicPackageQuestion pq) => pq.Type.Equals(item.Name));
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00014F30 File Offset: 0x00013130
		public List<TopicPackageQuestionTypeEnum> EmptyTypes
		{
			get
			{
				List<TopicPackageQuestionTypeEnum> list = new List<TopicPackageQuestionTypeEnum>();
				foreach (object obj in ((IEnumerable)this.XItems.Items))
				{
					QuestionTypeFiltItem questionTypeFiltItem = (QuestionTypeFiltItem)obj;
					if (!questionTypeFiltItem.HasQuestions)
					{
						list.Add(questionTypeFiltItem.Name);
					}
				}
				return list;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00014FAD File Offset: 0x000131AD
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00014FA4 File Offset: 0x000131A4
		public string GroupGuid { get; set; } = Guid.NewGuid().ToString();

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000426 RID: 1062 RVA: 0x00014FB8 File Offset: 0x000131B8
		// (remove) Token: 0x06000427 RID: 1063 RVA: 0x00014FF0 File Offset: 0x000131F0
		public event Action<TopicPackageQuestionTypeEnum> QuestionTypeSelected;

		// Token: 0x06000428 RID: 1064 RVA: 0x00015028 File Offset: 0x00013228
		private void RBtn_OnClick(object sender, RoutedEventArgs e)
		{
			if (((RadioButton)sender).IsChecked == true)
			{
				Action<TopicPackageQuestionTypeEnum> questionTypeSelected = this.QuestionTypeSelected;
				if (questionTypeSelected != null)
				{
					questionTypeSelected((TopicPackageQuestionTypeEnum)((RadioButton)sender).Tag);
				}
				this.SelectedItem = (TopicPackageQuestionTypeEnum)((RadioButton)sender).Tag;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00015090 File Offset: 0x00013290
		private static void SelectedItemPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			((PackageQuestionTypeFiltView)dependencyObject).UpdateSelectedItem();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000150A0 File Offset: 0x000132A0
		private void UpdateSelectedItem()
		{
			if (this.SelectedItem.Equals(TopicPackageQuestionTypeEnum.全部))
			{
				this.XRbtnAll.IsChecked = new bool?(true);
				return;
			}
			foreach (object item in ((IEnumerable)this.XItems.Items))
			{
				RadioButton visualChild = VisualHelper.GetVisualChild<RadioButton>(this.XItems.ItemContainerGenerator.ContainerFromItem(item));
				if (visualChild != null && ((TopicPackageQuestionTypeEnum)visualChild.Tag).Equals(this.SelectedItem))
				{
					visualChild.IsChecked = new bool?(true);
					break;
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00015174 File Offset: 0x00013374
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00015186 File Offset: 0x00013386
		public TopicPackageQuestionTypeEnum SelectedItem
		{
			get
			{
				return (TopicPackageQuestionTypeEnum)base.GetValue(PackageQuestionTypeFiltView.SelectedItemProperty);
			}
			set
			{
				base.SetValue(PackageQuestionTypeFiltView.SelectedItemProperty, value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00015199 File Offset: 0x00013399
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x000151A6 File Offset: 0x000133A6
		public IEnumerable ItemsSource
		{
			get
			{
				return this.XItems.ItemsSource;
			}
			set
			{
				this.XItems.ItemsSource = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000151B4 File Offset: 0x000133B4
		public IEnumerable<TopicPackageQuestionTypeEnum> TypeItemsSource
		{
			get
			{
				if (this.XItems.ItemsSource != null)
				{
					return from item in (IEnumerable<QuestionTypeFiltItem>)this.XItems.ItemsSource
					select item.Name;
				}
				return null;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00015204 File Offset: 0x00013404
		private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PackageQuestionTypeFiltView packageQuestionTypeFiltView = d as PackageQuestionTypeFiltView;
			if (packageQuestionTypeFiltView != null && e.NewValue != null)
			{
				packageQuestionTypeFiltView.AddCollectionChangedEvent();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001522A File Offset: 0x0001342A
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0001523C File Offset: 0x0001343C
		public ObservableCollection<TopicPackageQuestion> PackageQuestions
		{
			get
			{
				return (ObservableCollection<TopicPackageQuestion>)base.GetValue(PackageQuestionTypeFiltView.PackageQuestionsProperty);
			}
			set
			{
				base.SetValue(PackageQuestionTypeFiltView.PackageQuestionsProperty, value);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000152CB File Offset: 0x000134CB
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 3)
			{
				((RadioButton)target).Click += this.RBtn_OnClick;
			}
		}

		// Token: 0x040001E6 RID: 486
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(TopicPackageQuestionTypeEnum), typeof(PackageQuestionTypeFiltView), new PropertyMetadata((TopicPackageQuestionTypeEnum)0, new PropertyChangedCallback(PackageQuestionTypeFiltView.SelectedItemPropertyChangedCallback)));

		// Token: 0x040001E7 RID: 487
		public static readonly DependencyProperty PackageQuestionsProperty = DependencyProperty.Register("PackageQuestions", typeof(ObservableCollection<TopicPackageQuestion>), typeof(PackageQuestionTypeFiltView), new PropertyMetadata(null, new PropertyChangedCallback(PackageQuestionTypeFiltView.PropertyChangedCallback)));
	}
}
