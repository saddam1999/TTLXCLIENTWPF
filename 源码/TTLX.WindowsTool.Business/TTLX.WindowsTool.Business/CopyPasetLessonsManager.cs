using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class CopyPasetLessonsManager
	{
		private static CopyPasetLessonsManager _instance;

		private Book _copyedBook;

		private CopyPasetLessonsManager()
		{
		}

		public static CopyPasetLessonsManager Instance()
		{
			return _instance ?? (_instance = new CopyPasetLessonsManager());
		}

		public void Copy(Book book)
		{
			_copyedBook = book;
			MessengerHelper.ShowToast("复制成功");
		}

		public void PasteTo(Book targetBook)
		{
			if (_copyedBook != null)
			{
				CommonDialog.Instance.Confirm("确定把系列《" + _copyedBook.BookSeries.Name + "》的《" + _copyedBook.Name + "》所有内容拷贝至当前课本吗？", "拷贝课本", async delegate(bool b)
				{
					if (b && await AppData.Current.CopyBookToTargetBook(_copyedBook.Id, targetBook.Id))
					{
						MessengerHelper.ShowToast("粘贴成功");
					}
				});
			}
		}
	}
}
