using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000006 RID: 6
	public class AppData : ObservableObject
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024C2 File Offset: 0x000006C2
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024B9 File Offset: 0x000006B9
		public ObservableCollection<Book> BooksCollection { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000024CA File Offset: 0x000006CA
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000024D2 File Offset: 0x000006D2
		public Book CurrentBook
		{
			get
			{
				return this._currentBook;
			}
			set
			{
				this._currentBook = value;
				this.RaisePropertyChanged<Book>(() => this.CurrentBook);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002510 File Offset: 0x00000710
		private void InitBook()
		{
			this.BooksCollection = new ObservableCollection<Book>();
			this._bookService = ServiceLocator.Current.GetInstance<IBookService>();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000252D File Offset: 0x0000072D
		public void NewBook()
		{
			this.CurrentBook = new Book();
			this.CurrentBook.BookSeries = this.GetCurrentBookSeries();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000254B File Offset: 0x0000074B
		public void ClearBook()
		{
			this.ClearLesson();
			this.CurrentBook = null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000255C File Offset: 0x0000075C
		public async Task<List<Book>> GetAllBooks()
		{
			return await this._bookService.GetAllBooks();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025A4 File Offset: 0x000007A4
		public async Task SearchBooks(string str)
		{
			List<Book> re = await this._bookService.SearchBooks(str);
			if (re != null)
			{
				this.BooksCollection.Clear();
				this.BooksCollection.AddRange(re);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025F4 File Offset: 0x000007F4
		public async Task<int> GetMaterialBooksCount()
		{
			List<Book> re = await this._bookService.GetMaterialBooks();
			int result;
			if (re != null)
			{
				result = re.Count<Book>();
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000263C File Offset: 0x0000083C
		public async Task GetMaterialBooks()
		{
			List<Book> re = await this._bookService.GetMaterialBooks();
			if (re != null)
			{
				this.BooksCollection.Clear();
				this.BooksCollection.AddRange(re);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002684 File Offset: 0x00000884
		public async Task GetBooksBySeries()
		{
			if (this.CurrentSeries != null)
			{
				Tuple<List<Book>, int> re = await this._bookService.GetBooksBySeries(this.CurrentSeries, 15, 1);
				this.BooksCollection.Clear();
				this._bookTotalCount = 0;
				if (re != null)
				{
					this._bookTotalCount = re.Item2;
					foreach (Book b in re.Item1)
					{
						this.UpdateBookCreator(b);
					}
					this.BooksCollection.AddRange(re.Item1);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026CC File Offset: 0x000008CC
		public async Task GetMoreBooksBySeries()
		{
			if (this.CurrentSeries != null)
			{
				if (this._bookTotalCount > this.BooksCollection.Count)
				{
					int pageNum = this.BooksCollection.Count / 15 + 1;
					Tuple<List<Book>, int> re = await this._bookService.GetBooksBySeries(this.CurrentSeries, 15, pageNum);
					if (re != null)
					{
						this._bookTotalCount = re.Item2;
						foreach (Book b in re.Item1)
						{
							this.UpdateBookCreator(b);
						}
						this.BooksCollection.AddRange(re.Item1);
					}
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002714 File Offset: 0x00000914
		private void UpdateBookCreator(Book b)
		{
			string creator = "【乐学课本】";
			if (AppUser.Instance().CurrentUser.IsAdmin)
			{
				if (b.companyId != null)
				{
					creator = b.companyId.Value.ToString();
				}
			}
			else if (b.companyId != null)
			{
				creator = AppUser.Instance().CurrentUser.Name;
			}
			b.CreatorStr = creator;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002788 File Offset: 0x00000988
		public async Task<bool> DeleteBook(Book b)
		{
			bool flag = await this._bookService.DeleteBook(b);
			bool r = flag;
			if (r)
			{
				await this.GetBooksBySeries();
			}
			return r;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027D8 File Offset: 0x000009D8
		public async Task<bool> GetBook(int id)
		{
			Book re = await this._bookService.GetBookInfo(id);
			if (re != null)
			{
				this.CurrentBook = re;
				this.CurrentBook.BookSeries = this.GetCurrentBookSeries();
			}
			return re != null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002828 File Offset: 0x00000A28
		private Series GetCurrentBookSeries()
		{
			Series currentSeries = this.CurrentSeries;
			if (currentSeries != null && currentSeries.id != null && this.CurrentSeries.id.Value > 0)
			{
				return this.CurrentSeries;
			}
			if (this.CurrentBook.BookSeriesId != null)
			{
				Series re = this.SeriesCollection.FirstOrDefault((Series s) => s.id != null && s.id.Value.Equals(this.CurrentBook.BookSeriesId.Value));
				if (re != null)
				{
					return re;
				}
			}
			return this.SeriesCollection.FirstOrDefault((Series s) => s.id == null);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028CC File Offset: 0x00000ACC
		public async Task RefreshBookInfo()
		{
			if (this.CurrentBook != null)
			{
				Book re = await this._bookService.GetBookInfo(this.CurrentBook.Id);
				if (re != null)
				{
					this.CurrentBook = re;
					this.CurrentBook.BookSeries = this.GetCurrentBookSeries();
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002914 File Offset: 0x00000B14
		public async Task<bool> CreateBook()
		{
			int num = await this._bookService.CreateBook(this.CurrentBook);
			int id = num;
			if (id != -1)
			{
				this.CurrentBook.Id = id;
				await this.GetBooksBySeries();
			}
			return id != -1;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000295C File Offset: 0x00000B5C
		public async Task<bool> UpdateBook()
		{
			bool flag = await this._bookService.UpdateBook(this.CurrentBook);
			bool re = flag;
			if (re)
			{
				await this.GetBooksBySeries();
			}
			return re;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029A4 File Offset: 0x00000BA4
		public async Task<bool> AddBookTag(Tag tag)
		{
			bool result;
			if (!this.CurrentBook.IsSaved)
			{
				result = false;
			}
			else
			{
				bool flag = await this._bookService.AddBookTag(this.CurrentBook, tag);
				bool re = flag;
				if (re)
				{
					await this.RefreshBookInfo();
				}
				result = re;
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029F4 File Offset: 0x00000BF4
		public async Task<bool> DeleteBookTag(Tag tag)
		{
			bool flag = await this._bookService.DeleteBookTag(this.CurrentBook, tag);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
			}
			return re;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A44 File Offset: 0x00000C44
		public async Task<bool> CheckBookMaterials(Book book, string path)
		{
			try
			{
				byte[] byteArray = await this._bookService.CheckBookMaterials(book.Id);
				if (byteArray != null)
				{
					using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
					{
						fs.Write(byteArray, 0, byteArray.Length);
					}
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A9C File Offset: 0x00000C9C
		public async Task<IList<Tuple<int, IList<int>>>> CheckBookTagQuestionsComplete(Book book)
		{
			return await this._bookService.CheckBookTagQuestionsComplete(book.Id);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002AEC File Offset: 0x00000CEC
		public async Task<bool> CopyBookToTargetBook(int bookId, int targetBookId)
		{
			bool result;
			if (await this._bookService.CopyBookToTargetBook(bookId, targetBookId))
			{
				await this.RefreshBookInfo();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002B41 File Offset: 0x00000D41
		public static AppData Current
		{
			get
			{
				AppData result;
				if ((result = AppData._instance) == null)
				{
					result = (AppData._instance = new AppData());
				}
				return result;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B5C File Offset: 0x00000D5C
		private AppData()
		{
			this.InitSeries();
			this.InitBook();
			this.InitLesson();
			this.InitTopic();
			this.InitQuestion();
			this.InitTopicPackageLesson();
			this.InitTopicPackageQuestion();
			this.AudioFilePathCollection = new ObservableCollection<AudioFile>();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BCC File Offset: 0x00000DCC
		public void Clear()
		{
			AppData._instance = null;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002BDF File Offset: 0x00000DDF
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public ObservableCollection<AudioFile> AudioFilePathCollection { get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x00002BE7 File Offset: 0x00000DE7
		public void ClearAudioFilePath()
		{
			this.AudioFilePathCollection.Clear();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public async Task<bool> Upload(UploadData[] uploadDatas)
		{
			return await UploadService.IsUploadFilesAsync(uploadDatas);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C39 File Offset: 0x00000E39
		private void InitLesson()
		{
			this._lessonService = ServiceLocator.Current.GetInstance<ILessonService>();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C4B File Offset: 0x00000E4B
		public void NewLesson()
		{
			this.CurrentLesson = new Lesson
			{
				Book = this.CurrentBook
			};
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C64 File Offset: 0x00000E64
		public void ClearLesson()
		{
			this.ClearTopic();
			this.CurrentLesson = null;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002C73 File Offset: 0x00000E73
		public ObservableCollection<Lesson> LessonsCollection
		{
			get
			{
				Book currentBook = this.CurrentBook;
				if (currentBook == null)
				{
					return null;
				}
				return currentBook.Lessons;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002C86 File Offset: 0x00000E86
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002C8E File Offset: 0x00000E8E
		public Lesson CurrentLesson
		{
			get
			{
				return this._currentLesson;
			}
			set
			{
				this._currentLesson = value;
				this.RaisePropertyChanged<Lesson>(() => this.CurrentLesson);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CCC File Offset: 0x00000ECC
		public async Task<bool> GetLessonInfo(int id)
		{
			Lesson re = await this._lessonService.GetLessonInfo(id);
			if (re != null)
			{
				this.CurrentLesson = re;
				this.CurrentLesson.Book = this.CurrentBook;
			}
			return re != null;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D1C File Offset: 0x00000F1C
		public async Task RefreshLessonInfo()
		{
			if (this.CurrentLesson != null)
			{
				Lesson re = await this._lessonService.GetLessonInfo(this.CurrentLesson.Id);
				if (re != null)
				{
					this.CurrentLesson = re;
					this.CurrentLesson.Book = this.CurrentBook;
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D64 File Offset: 0x00000F64
		public async Task<bool> CreateLesson()
		{
			int num = await this._lessonService.CreateLesson(this.CurrentLesson);
			int id = num;
			if (id != -1)
			{
				this.CurrentLesson.Id = id;
				await this.RefreshBookInfo();
			}
			return id != -1;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002DAC File Offset: 0x00000FAC
		public async Task<bool> BatchCreateLessons(string name, int count)
		{
			bool result;
			if (await this._lessonService.BatchCreateLessons(this.CurrentBook.Id, name, count))
			{
				await this.RefreshBookInfo();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002E04 File Offset: 0x00001004
		public async Task<bool> UpdateLesson()
		{
			bool flag = await this._lessonService.UpdateLesson(this.CurrentLesson);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
			}
			return re;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002E4C File Offset: 0x0000104C
		public async Task<bool> UpdateLesson(Lesson lesson)
		{
			bool flag = await this._lessonService.UpdateLesson(lesson);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
			}
			return re;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E9C File Offset: 0x0000109C
		public async Task<bool> DeleteLesson(Lesson l)
		{
			bool flag = await this._lessonService.DeleteLesson(l);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
			}
			return re;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002EEC File Offset: 0x000010EC
		public async Task<bool> MoveLesson(Lesson lesson, int idx)
		{
			bool flag = await this._lessonService.MoveLesson(lesson, idx);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
			}
			return re;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F41 File Offset: 0x00001141
		private void InitQuestion()
		{
			this._questionService = ServiceLocator.Current.GetInstance<IQuestionService>();
			this._translateService = ServiceLocator.Current.GetInstance<ITranslateService>();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F64 File Offset: 0x00001164
		public async Task<bool> CreateQuestion(Question q)
		{
			int id = await this._questionService.CreateQuestion(q);
			if (id != -1)
			{
				q.Id = id;
			}
			return id != -1;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002FB4 File Offset: 0x000011B4
		public async Task<bool> UpdateQuestion(Question q)
		{
			return await this._questionService.UpdateQuestion(q);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003004 File Offset: 0x00001204
		public async Task<bool> DeleteQuestion(Question q)
		{
			return await this._questionService.DeleteQuestion(q);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003054 File Offset: 0x00001254
		public async Task AutoTranslate(Question q)
		{
			Question question = q;
			string nativeText = await this._translateService.GetTranslationOf(q.ForeignText);
			question.NativeText = nativeText;
			question = null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000030A1 File Offset: 0x000012A1
		private void InitSeries()
		{
			this.SeriesCollection = new ObservableCollection<Series>();
			this._seriesService = ServiceLocator.Current.GetInstance<ISeriesService>();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000030BE File Offset: 0x000012BE
		public void ClearSeries()
		{
			this.ClearBook();
			this.CurrentSeries = null;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000030D6 File Offset: 0x000012D6
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000030CD File Offset: 0x000012CD
		public ObservableCollection<Series> SeriesCollection { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000030DE File Offset: 0x000012DE
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000030E6 File Offset: 0x000012E6
		public Series CurrentSeries
		{
			get
			{
				return this._currentSeries;
			}
			set
			{
				this._currentSeries = value;
				this.RaisePropertyChanged<Series>(() => this.CurrentSeries);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003124 File Offset: 0x00001324
		public async Task GetSeries(bool isInitSelectedSeries = true)
		{
			List<Series> seriesItems = await this._seriesService.GetSeries();
			if (seriesItems != null)
			{
				this.SeriesCollection.Clear();
				foreach (Series item in seriesItems)
				{
					item.Pinyin = PinyinHelper.GetPinyin(item.Name).ToLower();
					this.SeriesCollection.Add(item);
				}
				int bcount = await this.GetMaterialBooksCount();
				this.SeriesCollection.Add(new Series
				{
					id = new int?(-1),
					Name = "教师素材",
					Pinyin = "jssc",
					bookCount = (long)bcount
				});
				if (isInitSelectedSeries)
				{
					this.CurrentSeries = this.SeriesCollection.FirstOrDefault<Series>();
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003174 File Offset: 0x00001374
		public async Task<bool> DeleteSeries(Series series)
		{
			bool flag = await this._seriesService.DeleteSeries(series);
			if (flag)
			{
				this.SeriesCollection.Remove(series);
				this.CurrentSeries = this.SeriesCollection.FirstOrDefault<Series>();
			}
			return flag;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000031C4 File Offset: 0x000013C4
		public async Task<bool> UpdateSeries()
		{
			return await this._seriesService.UpdateSeries(this.CurrentSeries);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000320C File Offset: 0x0000140C
		public async Task<bool> CreateSeries(Series ns)
		{
			AppData.<>c__DisplayClass82_0 CS$<>8__locals1 = new AppData.<>c__DisplayClass82_0();
			AppData.<>c__DisplayClass82_0 CS$<>8__locals2 = CS$<>8__locals1;
			int sid = CS$<>8__locals2.sid;
			int sid2 = await this._seriesService.CreateSeries(ns.Name);
			CS$<>8__locals2.sid = sid2;
			CS$<>8__locals2 = null;
			if (CS$<>8__locals1.sid != -1)
			{
				await this.GetSeries(false);
				this.CurrentSeries = this.SeriesCollection.FirstOrDefault((Series s) => s.id.Equals(CS$<>8__locals1.sid));
			}
			return CS$<>8__locals1.sid != -1;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003259 File Offset: 0x00001459
		private void InitTopic()
		{
			this._topicService = ServiceLocator.Current.GetInstance<ITopicService>();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000326B File Offset: 0x0000146B
		public void NewTopic(TopicTypeEnum type)
		{
			this.CurrentTopic = Topic.NewTopicByType(type);
			this.CurrentTopic.Lesson = this.CurrentLesson;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000328A File Offset: 0x0000148A
		public void ClearTopic()
		{
			this.CurrentTopic = null;
			this._initTopic = null;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000329A File Offset: 0x0000149A
		public IList<Topic> TopicsCollection
		{
			get
			{
				Lesson currentLesson = this.CurrentLesson;
				if (currentLesson == null)
				{
					return null;
				}
				return currentLesson.Topics;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000032B0 File Offset: 0x000014B0
		public async Task InitTopicInfo()
		{
			AppData appData = this;
			Topic initTopic = appData._initTopic;
			Topic initTopic2 = await ObjectHelper.DeepCopyAsync<Topic>(this.CurrentTopic);
			appData._initTopic = initTopic2;
			appData = null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000032F5 File Offset: 0x000014F5
		public bool TopicHasChanged()
		{
			if (this.CurrentTopic == null)
			{
				LogHelper.Error("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@返回前CurrentTopic为空@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
				return true;
			}
			return this.CurrentTopic.HasChanged(this._initTopic);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000331C File Offset: 0x0000151C
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003324 File Offset: 0x00001524
		public Topic CurrentTopic
		{
			get
			{
				return this._currentTopic;
			}
			set
			{
				this._currentTopic = value;
				this.RaisePropertyChanged<Topic>(() => this.CurrentTopic);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003364 File Offset: 0x00001564
		public async Task<bool> GetTopic(int id)
		{
			Topic re = await this._topicService.GetTopic(id);
			if (re != null)
			{
				this.CurrentTopic = re;
				this.CurrentTopic.Lesson = this.CurrentLesson;
			}
			return re != null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000033B4 File Offset: 0x000015B4
		public async Task RefreshTopicInfo()
		{
			if (this.CurrentTopic != null)
			{
				Topic re = await this._topicService.GetTopic(this.CurrentTopic.Id);
				if (re != null)
				{
					this.CurrentTopic = re;
					this.CurrentTopic.Lesson = this.CurrentLesson;
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000033FC File Offset: 0x000015FC
		public async Task<bool> MoveTopic(Topic topic, int idx)
		{
			bool flag = await this._topicService.MoveTopic(topic, idx);
			bool re = flag;
			if (re)
			{
				await this.RefreshLessonInfo();
			}
			return re;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003454 File Offset: 0x00001654
		public async Task<bool> DeleteTopic(Topic t)
		{
			bool flag = await this._topicService.DeleteTopic(t);
			bool re = flag;
			if (re)
			{
				await this.RefreshLessonInfo();
			}
			return re;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000034A4 File Offset: 0x000016A4
		public async Task<bool> CreateTopic()
		{
			int num = await this._topicService.CreateTopic(this.CurrentTopic);
			int id = num;
			if (id != -1)
			{
				this.CurrentTopic.Id = id;
				await this.RefreshLessonInfo();
			}
			return id != -1;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000034EC File Offset: 0x000016EC
		public async Task<bool> UpdateTopic()
		{
			bool flag = await this._topicService.UpdateTopic(this.CurrentTopic);
			bool re = flag;
			if (re)
			{
				await this.RefreshLessonInfo();
			}
			return re;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003534 File Offset: 0x00001734
		public async Task<bool> MoveTopicToLesson(Topic topic, Lesson lesson)
		{
			RequestInfo req = new RequestInfo();
			req.AddQueryStringInfo("id", topic.Id.ToString());
			req.AddQueryStringInfo("lessonId", lesson.Id.ToString());
			topic.TopicRequestInfo = req;
			bool flag = await this._topicService.UpdateTopic(topic);
			bool re = flag;
			if (re)
			{
				await this.RefreshLessonInfo();
			}
			return re;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003592 File Offset: 0x00001792
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003589 File Offset: 0x00001789
		public ObservableCollection<TopicPackageQuestion> AfterQuestions { get; set; } = new ObservableCollection<TopicPackageQuestion>();

		// Token: 0x06000066 RID: 102 RVA: 0x0000359A File Offset: 0x0000179A
		private void InitTopicPackageQuestion()
		{
			this._packageQuestionService = ServiceLocator.Current.GetInstance<IPackageQuestionService>();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000035AC File Offset: 0x000017AC
		public void ClearPackageQuestion()
		{
			this.AfterQuestions.Clear();
			this.BeforeLessonPackageGroup.Clear();
			this.BeforeLessonPackageGroupId = -1;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035CC File Offset: 0x000017CC
		public async Task<Tuple<IList<TopicPackageQuestion>, int>> GetPackageQuestionsBy(int tagId, int pageSize, int pageNum, TopicPackageQuestionTypeEnum type = TopicPackageQuestionTypeEnum.全部, string title = null)
		{
			return await this._packageQuestionService.GetPackageQuestionsBy(type, tagId, pageSize, pageNum, title);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000363C File Offset: 0x0000183C
		public async Task<TopicPackageQuestion> GetPackageQuestionBy(int questionId)
		{
			return await this._packageQuestionService.GetPackageQuestionById(questionId);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000368C File Offset: 0x0000188C
		public async Task GetPackageQuestionsByLesson()
		{
			if (this.CurrentLesson != null)
			{
				IList<TopicPackageQuestion> qs = await this._packageQuestionService.GetAfterPackageQuestionsByLessonId(this.CurrentLesson.Id);
				if (qs != null)
				{
					this.AfterQuestions.Clear();
					foreach (TopicPackageQuestion q in qs)
					{
						this.AfterQuestions.Add(q);
					}
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000036D4 File Offset: 0x000018D4
		public async Task<bool> DeletePackageQuestion(TopicPackageQuestion question)
		{
			int lessonId = this.CurrentLesson.Id;
			bool flag = await this._packageQuestionService.DeletePackageQuestion(lessonId, question.Id);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
			}
			return re;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003724 File Offset: 0x00001924
		public async Task<bool> AddPackageQuestionFromDatabase(int questionId)
		{
			int bookId = this.CurrentBook.Id;
			int lessonId = this.CurrentLesson.Id;
			bool flag = await this._packageQuestionService.AddPackageQuestion(bookId, lessonId, questionId);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
				await this.GetPackageQuestionsByLesson();
			}
			return re;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003774 File Offset: 0x00001974
		public async Task<bool> CreatePackageQuestion(TopicPackageQuestion question)
		{
			int num = await this._packageQuestionService.CreatePackageQuestion(question);
			int id = num;
			if (id != -1)
			{
				question.Id = id;
				await this.RefreshBookInfo();
			}
			return id != -1;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000037C4 File Offset: 0x000019C4
		public async Task<bool> UpdatePackageQuestion(TopicPackageQuestion question)
		{
			return await this._packageQuestionService.UpdatePackageQuestion(question);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003814 File Offset: 0x00001A14
		public async Task<bool> SavePackageQuestion(TopicPackageQuestion question)
		{
			bool result;
			if (question.IsUnsupported)
			{
				result = true;
			}
			else if (question.IsSaved)
			{
				if (question.HasChanged)
				{
					bool flag = await this._packageQuestionService.UpdatePackageQuestion(question);
					if (flag)
					{
						question.HasChanged = false;
					}
					result = flag;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				int id = await this._packageQuestionService.CreatePackageQuestion(question);
				if (id != -1)
				{
					question.Id = id;
					question.HasChanged = false;
					await this.RefreshBookInfo();
				}
				result = (id != -1);
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003864 File Offset: 0x00001A64
		public async Task<bool> CheckPackageQuestion(TopicPackageQuestion q)
		{
			return await this._packageQuestionService.ChangeQuestionStatus(q.Id);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000038B1 File Offset: 0x00001AB1
		private void InitTopicPackageLesson()
		{
			this._knowledgeService = ServiceLocator.Current.GetInstance<IKnowledgeService>();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000038C3 File Offset: 0x00001AC3
		public void NewTopicPackageLesson()
		{
			this.NewLesson();
			this.CurrentTopicPackageLesson = new TopicPackageLesson
			{
				LessonInfo = this.CurrentLesson
			};
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000038E2 File Offset: 0x00001AE2
		public void ClearTopicPackageLesson()
		{
			this.ClearLesson();
			this.ClearPackageQuestion();
			this.CurrentTopicPackageLesson = null;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003935 File Offset: 0x00001B35
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000038F7 File Offset: 0x00001AF7
		public TopicPackageLesson CurrentTopicPackageLesson
		{
			get
			{
				return this._currentTopicPackageLesson;
			}
			set
			{
				this._currentTopicPackageLesson = value;
				this.RaisePropertyChanged<TopicPackageLesson>(() => this.CurrentTopicPackageLesson);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003940 File Offset: 0x00001B40
		public async Task<bool> CreateTopicPackageLesson(IEnumerable<QuestionTag> tags, bool auto)
		{
			bool result;
			if (await this.CreateLesson() && await this._knowledgeService.AddLessonKnowledges(this.CurrentLesson.Id, auto, tags))
			{
				await this.RefreshBookInfo();
				await this.RefreshTopicPackageLesson();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003998 File Offset: 0x00001B98
		public async Task<bool> UpdateTopicPackageLesson(IEnumerable<QuestionTag> tags, bool auto)
		{
			bool result;
			if (await this.UpdateLesson() && await this._knowledgeService.AddLessonKnowledges(this.CurrentLesson.Id, auto, tags))
			{
				await this.RefreshBookInfo();
				await this.RefreshTopicPackageLesson();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000039F0 File Offset: 0x00001BF0
		public async Task<bool> AddLessonKnowledgeIds(bool auto, params int[] tagIds)
		{
			bool result;
			if (await this._knowledgeService.AddLessonKnowledgeIds(this.CurrentLesson.Id, auto, tagIds))
			{
				await this.RefreshBookInfo();
				await this.RefreshTopicPackageLesson();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003A48 File Offset: 0x00001C48
		public async Task<IList<TopicPackageLesson>> GetTopicPackageLessons(IList<Lesson> lessons)
		{
			if (lessons != null && lessons.Any<Lesson>())
			{
				IList<TopicPackageLesson> topicPackageLessons = await this._knowledgeService.GetTopicPackageLessonsByLessons(lessons);
				if (topicPackageLessons != null)
				{
					List<TopicPackageLesson> re = new List<TopicPackageLesson>();
					using (IEnumerator<Lesson> enumerator = lessons.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Lesson lesson = enumerator.Current;
							TopicPackageLesson i = topicPackageLessons.FirstOrDefault((TopicPackageLesson tl) => tl.Id.Equals(lesson.Id));
							if (i != null)
							{
								i.LessonInfo = lesson;
								re.Add(i);
							}
						}
					}
					return re;
				}
			}
			return null;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003A98 File Offset: 0x00001C98
		public async Task<bool> DeleteTopicPackageLessonKnowledge(QuestionTag tag)
		{
			bool flag = await this._knowledgeService.DeleteLessonKnowledge(this.CurrentLesson.Id, tag.Id);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
				await this.RefreshTopicPackageLesson();
			}
			return re;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public async Task<bool> UpdateTopicPackageLessonKnowledge(QuestionTag tag, string name)
		{
			bool result;
			if (string.IsNullOrWhiteSpace(name))
			{
				result = false;
			}
			else
			{
				result = await this._knowledgeService.UpdateLessonKnowledge(tag.Id, name);
			}
			return result;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003B46 File Offset: 0x00001D46
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003B3D File Offset: 0x00001D3D
		public ObservableCollection<QuestionTag> KnowledgeTags { get; set; } = new ObservableCollection<QuestionTag>();

		// Token: 0x0600007E RID: 126 RVA: 0x00003B50 File Offset: 0x00001D50
		public async Task RefreshTopicPackageLesson()
		{
			if (this.CurrentLesson != null && await this.GetLessonInfo(this.CurrentLesson.Id))
			{
				this.CurrentTopicPackageLesson = await this._knowledgeService.GetTopicPackageLessonByLesson(this.CurrentLesson);
				if (this.CurrentTopicPackageLesson != null)
				{
					this.KnowledgeTags.Clear();
					this.KnowledgeTags.AddRange(this.CurrentTopicPackageLesson.Knowledges);
					this.CurrentTopicPackageLesson.LessonInfo = this.CurrentLesson;
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003B98 File Offset: 0x00001D98
		public async Task<bool> GetTopicPackageLesson(int id)
		{
			if (await this.GetLessonInfo(id))
			{
				this.CurrentTopicPackageLesson = await this._knowledgeService.GetTopicPackageLessonByLesson(this.CurrentLesson);
				if (this.CurrentTopicPackageLesson != null)
				{
					this.KnowledgeTags.Clear();
					this.KnowledgeTags.AddRange(this.CurrentTopicPackageLesson.Knowledges);
					this.CurrentTopicPackageLesson.LessonInfo = this.CurrentLesson;
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003BEE File Offset: 0x00001DEE
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003BE5 File Offset: 0x00001DE5
		public ObservableCollection<BeforeLessonPackageGroup> BeforeLessonPackageGroup { get; set; } = new ObservableCollection<BeforeLessonPackageGroup>();

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003BFF File Offset: 0x00001DFF
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public int BeforeLessonPackageGroupId { get; set; } = -1;

		// Token: 0x06000084 RID: 132 RVA: 0x00003C08 File Offset: 0x00001E08
		public async Task GetBeforePackageQuestionsByLesson()
		{
			if (this.CurrentLesson != null)
			{
				BeforeLessonPackage re = await this._packageQuestionService.GetBeforePackageQuestionsByLessonId(this.CurrentLesson.Id);
				if (re != null)
				{
					this.BeforeLessonPackageGroupId = re.Id;
					this.BeforeLessonPackageGroup.Clear();
					this.BeforeLessonPackageGroup.AddRange(re.Groups);
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003C50 File Offset: 0x00001E50
		public async Task<bool> SaveBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups)
		{
			int bookId = this.CurrentBook.Id;
			int lessonId = this.CurrentLesson.Id;
			bool re = false;
			if (this.BeforeLessonPackageGroupId == -1)
			{
				int id = await this._packageQuestionService.CreateBeforePackageQuestionGroups(groups, bookId, lessonId);
				if (id != -1)
				{
					this.BeforeLessonPackageGroupId = id;
					re = true;
				}
			}
			else
			{
				re = await this._packageQuestionService.UpdateBeforePackageQuestionGroups(groups, bookId, lessonId, this.BeforeLessonPackageGroupId);
			}
			return re;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public async Task<bool> AddBeforePackageQuestionFromDatabase(IEnumerable<BeforeLessonPackageGroup> groups)
		{
			bool flag = await this.SaveBeforePackageQuestionGroups(groups);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
				await this.GetBeforePackageQuestionsByLesson();
			}
			return re;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public async Task<bool> DeleteBeforePackageQuestion(IEnumerable<BeforeLessonPackageGroup> groups)
		{
			bool flag = await this.SaveBeforePackageQuestionGroups(groups);
			bool re = flag;
			if (re)
			{
				await this.RefreshBookInfo();
				await this.GetBeforePackageQuestionsByLesson();
			}
			return re;
		}

		// Token: 0x04000008 RID: 8
		private const int PageSize = 15;

		// Token: 0x04000009 RID: 9
		private int _bookTotalCount;

		// Token: 0x0400000B RID: 11
		private Book _currentBook;

		// Token: 0x0400000C RID: 12
		private IBookService _bookService;

		// Token: 0x0400000D RID: 13
		private static volatile AppData _instance;

		// Token: 0x0400000F RID: 15
		private ILessonService _lessonService;

		// Token: 0x04000010 RID: 16
		private Lesson _currentLesson;

		// Token: 0x04000011 RID: 17
		private IQuestionService _questionService;

		// Token: 0x04000012 RID: 18
		private ITranslateService _translateService;

		// Token: 0x04000013 RID: 19
		private ISeriesService _seriesService;

		// Token: 0x04000015 RID: 21
		private Series _currentSeries;

		// Token: 0x04000016 RID: 22
		private ITopicService _topicService;

		// Token: 0x04000017 RID: 23
		private Topic _initTopic;

		// Token: 0x04000018 RID: 24
		private Topic _currentTopic;

		// Token: 0x0400001A RID: 26
		private IPackageQuestionService _packageQuestionService;

		// Token: 0x0400001B RID: 27
		private IKnowledgeService _knowledgeService;

		// Token: 0x0400001C RID: 28
		private TopicPackageLesson _currentTopicPackageLesson;
	}
}
