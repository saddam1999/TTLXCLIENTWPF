using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200001E RID: 30
	public class LessonService : ILessonService
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00004E30 File Offset: 0x00003030
		public async Task<int> CreateLesson(Lesson lesson)
		{
			RestRequest req = new RestRequest("book/lesson/create", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("bookId", lesson.BookId.ToString());
			dic.Add("info", lesson.Name.Trim());
			if (lesson.Book.Type.Equals(BookTypeEnum.教研题包))
			{
				dic.Add("type", "2");
			}
			RequestUtility.AddFile(req, "cover", lesson.ImgPortraitUrl, "image/jpeg");
			RequestUtility.AddFile(req, "coverLandscape", lesson.ImgLandscapeUrl, "image/jpeg");
			RequestUtility.AddParameter(req, dic, true);
			EntityCreateResponse re = await RestService.StartRequestTask<EntityCreateResponse>(req, null, null);
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

		// Token: 0x06000123 RID: 291 RVA: 0x00004E78 File Offset: 0x00003078
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
			}, true);
			return await RestService.StartRequestTask<BaseResponse>(req, null, null) != null;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004ED0 File Offset: 0x000030D0
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
			RequestUtility.AddParameter(request, dic, true);
			return await RestService.StartRequestTask<BaseResponse>(request, null, null) != null;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004F18 File Offset: 0x00003118
		public async Task<bool> DeleteLesson(Lesson lesson)
		{
			RestRequest req = new RestRequest("book/lesson/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					lesson.Id.ToString()
				}
			}, false);
			return await RestService.StartRequestTask<BaseResponse>(req, null, null) != null;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004F60 File Offset: 0x00003160
		public async Task<Lesson> GetLessonInfo(int lessonId)
		{
			RestRequest req = new RestRequest("book/lesson/info", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"lessonId",
					lessonId.ToString()
				}
			}, false);
			LessonInfoResponse re = await RestService.StartRequestTask<LessonInfoResponse>(req, null, null);
			Lesson result;
			if (re != null)
			{
				result = re.lesson;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004FA8 File Offset: 0x000031A8
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
			}, false);
			return await RestService.StartRequestTask<BaseResponse>(req, null, null) != null;
		}
	}
}
