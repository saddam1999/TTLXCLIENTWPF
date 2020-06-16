using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public interface IBookService
	{
		Task<Tuple<List<Book>, int>> GetBooksBySeries(Series s, int pageSize, int pageNum);

		Task<List<Book>> GetAllBooks();

		Task<List<Book>> GetMaterialBooks();

		Task<List<Book>> SearchBooks(string str);

		Task<Book> GetBookInfo(int bookId);

		Task<int> CreateBook(Book book);

		Task<bool> UpdateBook(Book book);

		Task<bool> DeleteBook(Book book);

		Task<bool> AddBookTag(Book book, Tag tag);

		Task<bool> DeleteBookTag(Book book, Tag tag);

		Task<byte[]> CheckBookMaterials(int bookId);

		Task<IList<Tuple<int, IList<int>>>> CheckBookTagQuestionsComplete(int bookId);

		Task<bool> CopyBookToTargetBook(int sourceBookId, int targetBookId);
	}
}
