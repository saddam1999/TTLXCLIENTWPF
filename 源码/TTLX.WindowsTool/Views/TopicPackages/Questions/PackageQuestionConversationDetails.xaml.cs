using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Models.TopicPackage.Solutions;
using TTLX.WindowsTool.Views.TopicPackages.Controls;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x02000037 RID: 55
	public partial class PackageQuestionConversationDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000C8FD File Offset: 0x0000AAFD
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x06000299 RID: 665 RVA: 0x0000C908 File Offset: 0x0000AB08
		public PackageQuestionConversationDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this.QuestionInfo = question;
			this.XCmbRole.ItemsSource = new RoleTypeEnum[]
			{
				RoleTypeEnum.A,
				RoleTypeEnum.B
			};
			this.SendData = new ConversationData(RoleTypeEnum.A);
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000C98E File Offset: 0x0000AB8E
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000C985 File Offset: 0x0000AB85
		public ObservableCollection<ConversationData> ConversationDataCollection { get; set; } = new ObservableCollection<ConversationData>();

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000C996 File Offset: 0x0000AB96
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
		public ConversationData SendData
		{
			get
			{
				return (ConversationData)base.GetValue(PackageQuestionConversationDetails.SendDataProperty);
			}
			set
			{
				base.SetValue(PackageQuestionConversationDetails.SendDataProperty, value);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		private void Init()
		{
			ConversationData conversationData = new ConversationData(RoleTypeEnum.A);
			foreach (MediaItem mediaItem in this.Stem.Items)
			{
				if (mediaItem.Type.Equals(MediaItemType.富文本))
				{
					conversationData.Content = mediaItem;
				}
				else if (mediaItem.Type.Equals(MediaItemType.音频))
				{
					conversationData.Audio = mediaItem;
				}
				else
				{
					if (!mediaItem.Type.Equals(MediaItemType.文本))
					{
						throw new ArgumentOutOfRangeException();
					}
					conversationData.SetRole(mediaItem);
					this.ConversationDataCollection.Add(conversationData);
					conversationData = new ConversationData(RoleTypeEnum.A);
				}
			}
			foreach (TopicPackageQuestion topicPackageQuestion in this.SubQuestions)
			{
				if (topicPackageQuestion.SubQuestions != null)
				{
					foreach (TopicPackageQuestion topicPackageQuestion2 in topicPackageQuestion.SubQuestions)
					{
						int? index = topicPackageQuestion2.Content.Index;
						if (index != null)
						{
							this.ConversationDataCollection[index.Value].IsAddSubQuestions = true;
						}
					}
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000CB44 File Offset: 0x0000AD44
		private async void OnLoaded(object sender, RoutedEventArgs e)
		{
			PackageQuestionConversationDetails packageQuestionConversationDetails = this;
			ObservableCollection<ConversationData> initConversationData = packageQuestionConversationDetails._initConversationData;
			ObservableCollection<ConversationData> initConversationData2 = await ObjectHelper.DeepCopyAsync<ObservableCollection<ConversationData>>(this.ConversationDataCollection);
			packageQuestionConversationDetails._initConversationData = initConversationData2;
			packageQuestionConversationDetails = null;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000CB7D File Offset: 0x0000AD7D
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000CB85 File Offset: 0x0000AD85
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000CB97 File Offset: 0x0000AD97
		public ObservableCollection<TopicPackageQuestion> SubQuestions
		{
			get
			{
				return this.QuestionInfo.SubQuestions;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public void UpdateQuestionInfoCache()
		{
			if (!ObjectHelper.AreEqual(this._initConversationData, this.ConversationDataCollection))
			{
				this.QuestionInfo.HasChanged = true;
			}
			Dictionary<RoleTypeEnum, List<TopicPackageQuestion>> dictionary = new Dictionary<RoleTypeEnum, List<TopicPackageQuestion>>();
			this.Stem.Clear();
			for (int i = 0; i < this.ConversationDataCollection.Count; i++)
			{
				ConversationData conversationData = this.ConversationDataCollection[i];
				this.Stem.Add(conversationData.Content);
				this.Stem.Add(conversationData.Audio);
				this.Stem.Add(conversationData.Role);
				if (!dictionary.ContainsKey(conversationData.RoleType))
				{
					dictionary.Add(conversationData.RoleType, new List<TopicPackageQuestion>());
				}
				if (conversationData.IsAddSubQuestions)
				{
					dictionary[conversationData.RoleType].Add(new TopicPackageQuestion
					{
						Type = TopicPackageQuestionTypeEnum.子问题_听读,
						Content = new QuestionContent
						{
							Index = new int?(i)
						},
						Solution = new QuestionSolution
						{
							AudioEvalSolution = new AudioEvalSolution
							{
								EvalText = conversationData.Content.RichText
							}
						}
					});
				}
			}
			this.SubQuestions.Clear();
			foreach (KeyValuePair<RoleTypeEnum, List<TopicPackageQuestion>> keyValuePair in dictionary.Reverse<KeyValuePair<RoleTypeEnum, List<TopicPackageQuestion>>>())
			{
				this.SubQuestions.Add(new TopicPackageQuestion
				{
					Type = TopicPackageQuestionTypeEnum.子问题_对话,
					SubQuestions = new ObservableCollection<TopicPackageQuestion>(keyValuePair.Value)
				});
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000CD30 File Offset: 0x0000AF30
		private void XBtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			this.ConversationDataCollection.Add(this.SendData);
			RoleTypeEnum roleType = this.SendData.RoleType.Equals(RoleTypeEnum.A) ? RoleTypeEnum.B : RoleTypeEnum.A;
			this.SendData = new ConversationData(roleType);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000CD80 File Offset: 0x0000AF80
		private void PackageQuestionConversationItem_OnDelete(ConversationData data)
		{
			this.ConversationDataCollection.Remove(data);
		}

		// Token: 0x0400013D RID: 317
		public static readonly DependencyProperty SendDataProperty = DependencyProperty.Register("SendData", typeof(ConversationData), typeof(PackageQuestionConversationDetails), new PropertyMetadata(null));

		// Token: 0x0400013E RID: 318
		private ObservableCollection<ConversationData> _initConversationData;
	}
}
