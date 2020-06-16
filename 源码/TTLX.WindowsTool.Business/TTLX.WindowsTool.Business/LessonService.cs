using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class LessonService : ILessonService
	{
		public async Task<int> CreateLesson(Lesson lesson)
		{
			RestRequest req = new RestRequest("book/lesson/create", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>
			{
				{
					"bookId",
					lesson.BookId.ToString()
				},
				{
					"info",
					lesson.Name.Trim()
				}
			};
			if (lesson.Book.Type.Equals(BookTypeEnum.教研题包))
			{
				dic.Add("type", "2");
			}
			RequestUtility.AddFile(req, "cover", lesson.ImgPortraitUrl, "image/jpeg");
			RequestUtility.AddFile(req, "coverLandscape", lesson.ImgLandscapeUrl, "image/jpeg");
			RequestUtility.AddParameter(req, dic, m: true);
			return (await RestService.StartRequestTask<EntityCreateResponse>(req))?.id ?? (-1);
		}

		public async Task<bool> BatchCreateLessons(int bookId, string name, int count)
		{
			RestRequest req = new RestRequest("book/lesson/create/batch", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"bookId",
					bookId.ToString()
				},
				{
					"info",
					name.Trim()
				},
				{
					"type",
					"2"
				},
				{
					"batchSize",
					count.ToString()
				}
			}, m: true);
			return await RestService.StartRequestTask<BaseResponse>(req) != null;
		}

		public async Task<bool> UpdateLesson(Lesson lesson)
		{
			RestRequest request = new RestRequest("book/lesson/update", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("id", lesson.Id.ToString());
			dic.Add("bookId", lesson.BookId.ToString());
			dic.Add("info", lesson.Name.Trim());
			dic.Add("idx", lesson.Idx.ToString());
			if (!Helper.IsUrlPath(lesson.ImgPortraitUrl))
			{
				RequestUtility.AddFile(request, "cover", lesson.ImgPortraitUrl, "image/jpeg");
			}
			if (!Helper.IsUrlPath(lesson.ImgLandscapeUrl))
			{
				RequestUtility.AddFile(request, "coverLandscape", lesson.ImgLandscapeUrl, "image/jpeg");
			}
			RequestUtility.AddParameter(request, dic, m: true);
			return await RestService.StartRequestTask<BaseResponse>(request) != null;
		}

		public async Task<bool> DeleteLesson(Lesson lesson)
		{
			RestRequest req = new RestRequest("book/lesson/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					lesson.Id.ToString()
				}
			});
			return await RestService.StartRequestTask<BaseResponse>(req) != null;
		}

		public async Task<Lesson> GetLessonInfo(int lessonId)
		{
			RestRequest req = new RestRequest("book/lesson/info", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"lessonId",
					lessonId.ToString()
				}
			});
			return (await RestService.StartRequestTask<LessonInfoResponse>(req))?.lesson;
		}

		public async Task<bool> MoveLesson(Lesson lesson, int idx)
		{
			RestRequest req = new RestRequest("book/lesson/idx/jump", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					lesson.Id.ToString()
				},
				{
					"idx",
					idx.ToString()
				}
			});
			return await RestService.StartRequestTask<BaseResponse>(req) != null;
		}
	}
}
