using System;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000032 RID: 50
	public class CopyPasetLessonsManager
	{
		// Token: 0x06000188 RID: 392 RVA: 0x000061A5 File Offset: 0x000043A5
		private CopyPasetLessonsManager()
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000061AD File Offset: 0x000043AD
		public static CopyPasetLessonsManager Instance()
		{
			CopyPasetLessonsManager result;
			if ((result = CopyPasetLessonsManager._instance) == null)
			{
				result = (CopyPasetLessonsManager._instance = new CopyPasetLessonsManager());
			}
			return result;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000061C3 File Offset: 0x000043C3
		public void Copy(Book book)
		{
			this._copyedBook = book;
			MessengerHelper.ShowToast("复制成功");
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000061D8 File Offset: 0x000043D8
		public void PasteTo(Book targetBook)
		{
			if (this._copyedBook != null)
			{
				CommonDialog.Instance.Confirm(string.Concat(new string[]
				{
					"确定把系列《",
					this._copyedBook.BookSeries.Name,
					"》的《",
					this._copyedBook.Name,
					"》所有内容拷贝至当前课本吗？"
				}), "拷贝课本", async delegate(bool b)
				{
					if (b && await AppData.Current.CopyBookToTargetBook(this._copyedBook.Id, targetBook.Id))
					{
						MessengerHelper.ShowToast("粘贴成功");
					}
				});
			}
		}

		// Token: 0x04000052 RID: 82
		private static CopyPasetLessonsManager _instance;

		// Token: 0x04000053 RID: 83
		private Book _copyedBook;
	}
}
