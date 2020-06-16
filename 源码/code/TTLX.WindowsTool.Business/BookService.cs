using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class BookService : IBookService
	{
		public async Task<Tuple<List<Book>, int>> GetBooksBySeries(Series s, int pageSize, int pageNum)
		{
			RestRequest request = new RestRequest("book/own/list", Method.GET);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			if (s.id.HasValue)
			{
				if (s.id.Value < 0)
				{
					return null;
				}
				dic.Add("seriesId", s.id.ToString());
			}
			dic.Add("pageSize", pageSize.ToString());
			dic.Add("pageNumber", pageNum.ToString());
			RequestUtility.AddParameter(request, dic);
			BookListResponse re = await RestService.StartRequestTask<BookListResponse>(request);
			if (re != null && re.TotalCount.HasValue)
			{
				return new Tuple<List<Book>, int>(re.Books, re.TotalCount.Value);
			}
			return null;
		}

		public async Task<List<Book>> GetAllBooks()
		{
			RestRequest req = new RestRequest("book/all/list", Method.GET);
			RequestUtility.AddParameter(req);
			BookListResponse re = await RestService.StartRequestTask<BookListResponse>(req);
			if (re != null)
			{
				foreach (Book book in re.Books)
				{
					book.Pinyin = PinyinHelper.GetPinyin(book.Name).ToLower();
				}
				return re.Books;
			}
			return null;
		}

		public async Task<List<Book>> GetMaterialBooks()
		{
			RestRequest req = new RestRequest("book/material_bank/list", Method.GET);
			RequestUtility.AddParameter(req);
			return (await RestService.StartRequestTask<BookListResponse>(req))?.Books;
		}

		public async Task<List<Book>> SearchBooks(string str)
		{
			RestRequest req = new RestRequest("book/own/search", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"info",
					str.Trim()
				}
			});
			return (await RestService.StartRequestTask<BookListResponse>(req))?.Books;
		}

		public async Task<Book> GetBookInfo(int bookId)
		{
			RestRequest req = new RestRequest("book/info", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"bookId",
					bookId.ToString()
				}
			});
			return (await RestService.StartRequestTask<BookInfoResponse>(req))?.book;
		}

		public async Task<int> CreateBook(Book book)
		{
			RestRequest restRequest = new RestRequest("book/create", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>
			{
				{
					"info",
					book.Name.Trim()
				},
				{
					"type",
					((byte)book.Type).ToString()
				}
			};
			if (book.BookSeriesId.HasValue && book.BookSeriesId != 0)
			{
				dic.Add("bookSeriesId", book.BookSeriesId.ToString());
			}
			dic.Add("brief", book.Brief);
			dic.Add("enableAutoEvaluation", book.EnableAutoEvaluation?.ToString());
			restRequest.AddFile("cover", book.CoverUrl, "image/jpeg");
			restRequest.AddFile("coverLandscape", book.CoverLandscapeUrl, "image/jpeg");
			RequestUtility.AddParameter(restRequest, dic, m: true);
			return (await RestService.StartRequestTask<EntityCreateResponse>(restRequest))?.id ?? (-1);
		}

		public async Task<bool> UpdateBook(Book book)
		{
			RestRequest request = new RestRequest("book/update", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("id", book.Id.ToString());
			dic.Add("info", book.Name.Trim());
			dic.Add("type", ((byte)book.Type).ToString());
			if (book.BookSeriesId.HasValue && book.BookSeriesId != 0)
			{
				dic.Add("bookSeriesId", book.BookSeriesId.ToString());
			}
			dic.Add("brief", book.Brief);
			dic.Add("enableAutoEvaluation", book.EnableAutoEvaluation?.ToString());
			if (!Helper.IsUrlPath(book.CoverUrl))
			{
				request.AddFile("cover", book.CoverUrl, "image/jpeg");
			}
			if (!Helper.IsUrlPath(book.CoverLandscapeUrl))
			{
				request.AddFile("coverLandscape", book.CoverLandscapeUrl, "image/jpeg");
			}
			RequestUtility.AddParameter(request, dic, m: true);
			return await RestService.StartRequestTask<BaseResponse>(request) != null;
		}

		public async Task<bool> DeleteBook(Book book)
		{
			RestRequest req = new RestRequest("book/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					book.Id.ToString()
				}
			});
			return await RestService.StartRequestTask<BaseResponse>(req) != null;
		}

		public async Task<bool> AddBookTag(Book book, Tag tag)
		{
			RestRequest req = new RestRequest("book/booktag/create", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"bookId",
					book.Id.ToString()
				},
				{
					"type",
					((byte)tag.Type).ToString()
				},
				{
					"name",
					tag.Name.Trim()
				}
			});
			return await RestService.StartRequestTask<BaseResponse>(req) != null;
		}

		public async Task<bool> DeleteBookTag(Book book, Tag tag)
		{
			RestRequest req = new RestRequest("book/booktag/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"bookId",
					book.Id.ToString()
				},
				{
					"tagId",
					tag.Id.ToString()
				}
			});
			return await RestService.StartRequestTask<BaseResponse>(req) != null;
		}

		public async Task<byte[]> CheckBookMaterials(int bookId)
		{
			RestRequest req = new RestRequest("question/book/media/check", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					bookId.ToString()
				}
			});
			return await RestService.StartByteRequestTask(req);
		}

		public async Task<IList<Tuple<int, IList<int>>>> CheckBookTagQuestionsComplete(int bookId)
		{
			RestRequest request = new RestRequest("question/book/question/type/check", Method.POST);
			dynamic param = new ExpandoObject();
			param.id = bookId;
			RequestUtility.AddDynamicParamter(request, param);
			BookQuestionCheckResponse re = await RestService.StartRequestTask_QuestionBankService<BookQuestionCheckResponse>(request);
			if (re != null)
			{
				List<Tuple<int, IList<int>>> lessonList = new List<Tuple<int, IList<int>>>();
				if (re.ProblemLessons != null)
				{
					foreach (BookQuestionCheckResponse.LessonTagCheckResult pl in re.ProblemLessons)
					{
						if (pl.Lesson != null)
						{
							List<int> tagList = new List<int>();
							foreach (BookQuestionCheckResponse.LessonTagCheckResult.ProblemTag pt in pl.ProblemTags)
							{
								tagList.Add(pt.Tag.Id);
							}
							lessonList.Add(new Tuple<int, IList<int>>(pl.Lesson.Id, tagList));
						}
					}
				}
				return lessonList;
			}
			return null;
		}

		public async Task<bool> CopyBookToTargetBook(int bookId, int targetBookId)
		{
			RestRequest request = new RestRequest("book/copy/windows/tool", Method.POST);
			dynamic param = new ExpandoObject();
			param.bookId = bookId;
			param.targetBookId = targetBookId;
			RequestUtility.AddDynamicParamter(request, param);
			return await RestService.StartRequestTask<BaseResponse>(request) != null;
		}
	}
}
