using System;
using Newtonsoft.Json;
using TTLX.WindowsTool.Models.TopicPackage.Solutions;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000051 RID: 81
	public class QuestionSolution
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000590E File Offset: 0x00003B0E
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00005905 File Offset: 0x00003B05
		[JsonProperty("inputStringSolution")]
		public InputStringSolution InputStringSolution { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000591F File Offset: 0x00003B1F
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00005916 File Offset: 0x00003B16
		[JsonProperty("matchingMatrixSolution")]
		public MatchingMatrixSolution MatchingMatrixSolution { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00005930 File Offset: 0x00003B30
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00005927 File Offset: 0x00003B27
		[JsonProperty("selectionSolution")]
		public QuestionSelectionSolution SelectionSolution { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00005941 File Offset: 0x00003B41
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00005938 File Offset: 0x00003B38
		[JsonProperty("audioEvalSolution")]
		public AudioEvalSolution AudioEvalSolution { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00005952 File Offset: 0x00003B52
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00005949 File Offset: 0x00003B49
		[JsonProperty("inputMediaItemSolution")]
		public InputMediaItemSolution InputMediaItemSolution { get; set; }
	}
}
