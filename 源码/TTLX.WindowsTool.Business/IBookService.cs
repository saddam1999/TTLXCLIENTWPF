using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200000E RID: 14
	public interface IBookService
	{
		// Token: 0x060000AD RID: 173
		Task<Tuple<List<Book>, int>> GetBooksBySeries(Series s, int pageSize, int pageNum);

		// Token: 0x060000AE RID: 174
		Task<List<Book>> GetAllBooks();

		// Token: 0x060000AF RID: 175
		Task<List<Book>> GetMaterialBooks();

		// Token: 0x060000B0 RID: 176
		Task<List<Book>> SearchBooks(string str);

		// Token: 0x060000B1 RID: 177
		Task<Book> GetBookInfo(int bookId);

		// Token: 0x060000B2 RID: 178
		Task<int> CreateBook(Book book);

		// Token: 0x060000B3 RID: 179
		Task<bool> UpdateBook(Book book);

		// Token: 0x060000B4 RID: 180
		Task<bool> DeleteBook(Book book);

		// Token: 0x060000B5 RID: 181
		Task<bool> AddBookTag(Book book, Tag tag);

		// Token: 0x060000B6 RID: 182
		Task<bool> DeleteBookTag(Book book, Tag tag);

		// Token: 0x060000B7 RID: 183
		Task<byte[]> CheckBookMaterials(int bookId);

		// Token: 0x060000B8 RID: 184
		Task<IList<Tuple<int, IList<int>>>> CheckBookTagQuestionsComplete(int bookId);

		// Token: 0x060000B9 RID: 185
		Task<bool> CopyBookToTargetBook(int sourceBookId, int targetBookId);
	}
}
