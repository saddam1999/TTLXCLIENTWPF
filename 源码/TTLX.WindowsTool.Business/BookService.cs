using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using RestSharp;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200000F RID: 15
	public class BookService : IBookService
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000402C File Offset: 0x0000222C
		public async Task<Tuple<List<Book>, int>> GetBooksBySeries(Series s, int pageSize, int pageNum)
		{
			RestRequest request = new RestRequest("book/own/list", Method.GET);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			if (s.id != null)
			{
				if (s.id.Value < 0)
				{
					return null;
				}
				dic.Add("seriesId", s.id.ToString());
			}
			dic.Add("pageSize", pageSize.ToString());
			dic.Add("pageNumber", pageNum.ToString());
			RequestUtility.AddParameter(request, dic, false);
			BookListResponse re = await RestService.StartRequestTask<BookListResponse>(request, null, null);
			Tuple<List<Book>, int> result;
			if (re != null && re.TotalCount != null)
			{
				result = new Tuple<List<Book>, int>(re.Books, re.TotalCount.Value);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004084 File Offset: 0x00002284
		public async Task<List<Book>> GetAllBooks()
		{
			RestRequest req = new RestRequest("book/all/list", Method.GET);
			RequestUtility.AddParameter(req, null, false);
			BookListResponse re = await RestService.StartRequestTask<BookListResponse>(req, null, null);
			List<Book> result;
			if (re != null)
			{
				foreach (Book book in re.Books)
				{
					book.Pinyin = PinyinHelper.GetPinyin(book.Name).ToLower();
				}
				result = re.Books;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000040C4 File Offset: 0x000022C4
		public async Task<List<Book>> GetMaterialBooks()
		{
			RestRequest req = new RestRequest("book/material_bank/list", Method.GET);
			RequestUtility.AddParameter(req, null, false);
			BookListResponse re = await RestService.StartRequestTask<BookListResponse>(req, null, null);
			List<Book> result;
			if (re != null)
			{
				result = re.Books;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004104 File Offset: 0x00002304
		public async Task<List<Book>> SearchBooks(string str)
		{
			RestRequest req = new RestRequest("book/own/search", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"info",
					str.Trim()
				}
			}, false);
			BookListResponse re = await RestService.StartRequestTask<BookListResponse>(req, null, null);
			List<Book> result;
			if (re != null)
			{
				result = re.Books;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000414C File Offset: 0x0000234C
		public async Task<Book> GetBookInfo(int bookId)
		{
			RestRequest req = new RestRequest("book/info", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"bookId",
					bookId.ToString()
				}
			}, false);
			BookInfoResponse re = await RestService.StartRequestTask<BookInfoResponse>(req, null, null);
			Book result;
			if (re != null)
			{
				result = re.book;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004194 File Offset: 0x00002394
		public async Task<int> CreateBook(Book book)
		{
			RestRequest restRequest = new RestRequest("book/create", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("info", book.Name.Trim());
			dic.Add("type", ((byte)book.Type).ToString());
			if (book.BookSeriesId != null && book.BookSeriesId != 0)
			{
				dic.Add("bookSeriesId", book.BookSeriesId.ToString());
			}
			dic.Add("brief", book.Brief);
			bool? flag;
			dic.Add("enableAutoEvaluation", (book.EnableAutoEvaluation != null) ? flag.GetValueOrDefault().ToString() : null);
			restRequest.AddFile("cover", book.CoverUrl, "image/jpeg");
			restRequest.AddFile("coverLandscape", book.CoverLandscapeUrl, "image/jpeg");
			RequestUtility.AddParameter(restRequest, dic, true);
			EntityCreateResponse re = await RestService.StartRequestTask<EntityCreateResponse>(restRequest, null, null);
			int result;
			if (re != null)
			{
				result = re.id;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000041DC File Offset: 0x000023DC
		public async Task<bool> UpdateBook(Book book)
		{
			RestRequest request = new RestRequest("book/update", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("id", book.Id.ToString());
			dic.Add("info", book.Name.Trim());
			dic.Add("type", ((byte)book.Type).ToString());
			if (book.BookSeriesId != null && book.BookSeriesId != 0)
			{
				dic.Add("bookSeriesId", book.BookSeriesId.ToString());
			}
			dic.Add("brief", book.Brief);
			bool? flag;
			dic.Add("enableAutoEvaluation", (book.EnableAutoEvaluation != null) ? flag.GetValueOrDefault().ToString() : null);
			if (!Helper.IsUrlPath(book.CoverUrl))
			{
				request.AddFile("cover", book.CoverUrl, "image/jpeg");
			}
			if (!Helper.IsUrlPath(book.CoverLandscapeUrl))
			{
				request.AddFile("coverLandscape", book.CoverLandscapeUrl, "image/jpeg");
			}
			RequestUtility.AddParameter(request, dic, true);
			return await RestService.StartRequestTask<BaseResponse>(request, null, null) != null;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004224 File Offset: 0x00002424
		public async Task<bool> DeleteBook(Book book)
		{
			RestRequest req = new RestRequest("book/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					book.Id.ToString()
				}
			}, false);
			return await RestService.StartRequestTask<BaseResponse>(req, null, null) != null;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000426C File Offset: 0x0000246C
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
			}, false);
			return await RestService.StartRequestTask<BaseResponse>(req, null, null) != null;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000042BC File Offset: 0x000024BC
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
			}, false);
			return await RestService.StartRequestTask<BaseResponse>(req, null, null) != null;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000430C File Offset: 0x0000250C
		public async Task<byte[]> CheckBookMaterials(int bookId)
		{
			RestRequest req = new RestRequest("question/book/media/check", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					bookId.ToString()
				}
			}, false);
			return await RestService.StartByteRequestTask(req);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004354 File Offset: 0x00002554
		public async Task<IList<Tuple<int, IList<int>>>> CheckBookTagQuestionsComplete(int bookId)
		{
			RestRequest request = new RestRequest("question/book/question/type/check", Method.POST);
			object param = new ExpandoObject();
			if (BookService.<>o__11.<>p__0 == null)
			{
				BookService.<>o__11.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "id", typeof(BookService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			BookService.<>o__11.<>p__0.Target(BookService.<>o__11.<>p__0, param, bookId);
			if (BookService.<>o__11.<>p__1 == null)
			{
				BookService.<>o__11.<>p__1 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(BookService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			BookService.<>o__11.<>p__1.Target(BookService.<>o__11.<>p__1, typeof(RequestUtility), request, param);
			BookQuestionCheckResponse re = await RestService.StartRequestTask_QuestionBankService<BookQuestionCheckResponse>(request);
			IList<Tuple<int, IList<int>>> result;
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
							foreach (BookQuestionCheckResponse.LessonTagCheckResult.ProblemTag problemTag in pl.ProblemTags)
							{
								tagList.Add(problemTag.Tag.Id);
							}
							lessonList.Add(new Tuple<int, IList<int>>(pl.Lesson.Id, tagList));
						}
					}
				}
				result = lessonList;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000439C File Offset: 0x0000259C
		public async Task<bool> CopyBookToTargetBook(int bookId, int targetBookId)
		{
			RestRequest request = new RestRequest("book/copy/windows/tool", Method.POST);
			object param = new ExpandoObject();
			if (BookService.<>o__12.<>p__0 == null)
			{
				BookService.<>o__12.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "bookId", typeof(BookService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			BookService.<>o__12.<>p__0.Target(BookService.<>o__12.<>p__0, param, bookId);
			if (BookService.<>o__12.<>p__1 == null)
			{
				BookService.<>o__12.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "targetBookId", typeof(BookService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			BookService.<>o__12.<>p__1.Target(BookService.<>o__12.<>p__1, param, targetBookId);
			if (BookService.<>o__12.<>p__2 == null)
			{
				BookService.<>o__12.<>p__2 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(BookService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			BookService.<>o__12.<>p__2.Target(BookService.<>o__12.<>p__2, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask<BaseResponse>(request, null, null) != null;
		}
	}
}
