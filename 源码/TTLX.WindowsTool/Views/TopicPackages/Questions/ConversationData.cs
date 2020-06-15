using System;
using GalaSoft.MvvmLight;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x02000039 RID: 57
	public class ConversationData : ObservableObject
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000CE70 File Offset: 0x0000B070
		public ConversationData(RoleTypeEnum roleType = RoleTypeEnum.A)
		{
			this.Role = new MediaItem
			{
				Type = MediaItemType.文本
			};
			this.Audio = new MediaItem
			{
				Type = MediaItemType.音频
			};
			this.Content = new MediaItem
			{
				Type = MediaItemType.富文本
			};
			this.RoleType = roleType;
			this._isAddSubQuestions = true;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CEC7 File Offset: 0x0000B0C7
		public ConversationData(MediaItem role, MediaItem audio, MediaItem con)
		{
			this.Role = role;
			this.Audio = audio;
			this.Content = con;
			Enum.TryParse<RoleTypeEnum>(role.Text, out this._roleType);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000CEF6 File Offset: 0x0000B0F6
		public void SetRole(MediaItem item)
		{
			this.Role = item;
			Enum.TryParse<RoleTypeEnum>(item.Text, out this._roleType);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000CF1A File Offset: 0x0000B11A
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000CF11 File Offset: 0x0000B111
		public MediaItem Role { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000CF2B File Offset: 0x0000B12B
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000CF22 File Offset: 0x0000B122
		public MediaItem Audio { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000CF3C File Offset: 0x0000B13C
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000CF33 File Offset: 0x0000B133
		public MediaItem Content { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000CF82 File Offset: 0x0000B182
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000CF44 File Offset: 0x0000B144
		public bool IsAddSubQuestions
		{
			get
			{
				return this._isAddSubQuestions;
			}
			set
			{
				this._isAddSubQuestions = value;
				this.RaisePropertyChanged<bool>(() => this.IsAddSubQuestions);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000CFAB File Offset: 0x0000B1AB
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public RoleTypeEnum RoleType
		{
			get
			{
				return this._roleType;
			}
			set
			{
				this._roleType = value;
				this.Role.Text = value.ToString();
			}
		}

		// Token: 0x0400014A RID: 330
		private bool _isAddSubQuestions;

		// Token: 0x0400014B RID: 331
		private RoleTypeEnum _roleType;
	}
}
