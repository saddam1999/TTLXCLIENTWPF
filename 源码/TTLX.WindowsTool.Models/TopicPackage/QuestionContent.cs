using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TTLX.WindowsTool.Models.TopicPackage.Contents;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200003E RID: 62
	public class QuestionContent
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00004BE1 File Offset: 0x00002DE1
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00004BD8 File Offset: 0x00002DD8
		[JsonProperty("index")]
		public int? Index { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00004BF2 File Offset: 0x00002DF2
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00004BE9 File Offset: 0x00002DE9
		[JsonProperty("matchingMatrix")]
		public MatchingMatrix MatchingMatrix { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00004C03 File Offset: 0x00002E03
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00004BFA File Offset: 0x00002DFA
		[JsonProperty("multiLanguageText")]
		public MultiLanguageText MultiLanguageText { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00004C14 File Offset: 0x00002E14
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00004C0B File Offset: 0x00002E0B
		[JsonProperty("selectTextCandidates")]
		public SelectTextCandidates SelectTextCandidates { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00004C25 File Offset: 0x00002E25
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00004C1C File Offset: 0x00002E1C
		[JsonProperty("selectMediaItemCandidates")]
		public SelectMediaItemCandidates SelectMediaItemCandidates { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00004C36 File Offset: 0x00002E36
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00004C2D File Offset: 0x00002E2D
		[JsonProperty("selection")]
		public QuestionSelection Selection { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00004C47 File Offset: 0x00002E47
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00004C3E File Offset: 0x00002E3E
		[JsonProperty("stem")]
		public QuestionStem Stem { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00004C58 File Offset: 0x00002E58
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00004C4F File Offset: 0x00002E4F
		[JsonProperty("interactionExtraData")]
		public InteractionExtraData InteractionExtraData { get; set; }

		// Token: 0x06000221 RID: 545 RVA: 0x00004C60 File Offset: 0x00002E60
		public void InitRoles()
		{
			this.Roles = new List<ConversationRole>();
			this.Roles.Add(new ConversationRole
			{
				Name = "A",
				Url = "http://oss.6tiantian.com/img_NGY3M2Q0OGYtNzYzYy00ZDU4LThlM2ItOTU0Yzg1NmU5MDRl.png"
			});
			this.Roles.Add(new ConversationRole
			{
				Name = "B",
				Url = "http://oss.6tiantian.com/img_NGY3M2Q0OGYtNzYzYy00ZDU4LThlM2ItOTU0Yzg1NmU5MDRl.png"
			});
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00004CCD File Offset: 0x00002ECD
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00004CC4 File Offset: 0x00002EC4
		[JsonProperty("roles")]
		public List<ConversationRole> Roles { get; set; }
	}
}
