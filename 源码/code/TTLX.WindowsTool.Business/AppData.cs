using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	public class AppData : ObservableObject
	{
		private const int PageSize = 15;

		private int _bookTotalCount;

		private Book _currentBook;

		private IBookService _bookService;

		private static volatile AppData _instance;

		private ILessonService _lessonService;

		private Lesson _currentLesson;

		private IQuestionService _questionService;

		private ITranslateService _translateService;

		private ISeriesService _seriesService;

		private Series _currentSeries;

		private ITopicService _topicService;

		private Topic _initTopic;

		private Topic _currentTopic;

		private IPackageQuestionService _packageQuestionService;

		private IKnowledgeService _knowledgeService;

		private TopicPackageLesson _currentTopicPackageLesson;

		public ObservableCollection<Book> BooksCollection
		{
			get;
			set;
		}

		public Book CurrentBook
		{
			get
			{
				return _currentBook;
			}
			set
			{
				_currentBook = value;
				RaisePropertyChanged(() => CurrentBook);
			}
		}

		public static AppData Current => _instance ?? (_instance = new AppData());

		public ObservableCollection<AudioFile> AudioFilePathCollection
		{
			get;
			set;
		}

		public ObservableCollection<Lesson> LessonsCollection => CurrentBook?.Lessons;

		public Lesson CurrentLesson
		{
			get
			{
				return _currentLesson;
			}
			set
			{
				_currentLesson = value;
				RaisePropertyChanged(() => CurrentLesson);
			}
		}

		public ObservableCollection<Series> SeriesCollection
		{
			get;
			set;
		}

		public Series CurrentSeries
		{
			get
			{
				return _currentSeries;
			}
			set
			{
				_currentSeries = value;
				RaisePropertyChanged(() => CurrentSeries);
			}
		}

		public IList<Topic> TopicsCollection => CurrentLesson?.Topics;

		public Topic CurrentTopic
		{
			get
			{
				return _currentTopic;
			}
			set
			{
				_currentTopic = value;
				RaisePropertyChanged(() => CurrentTopic);
			}
		}

		public ObservableCollection<TopicPackageQuestion> AfterQuestions
		{
			get;
			set;
		} = new ObservableCollection<TopicPackageQuestion>();


		public TopicPackageLesson CurrentTopicPackageLesson
		{
			get
			{
				return _currentTopicPackageLesson;
			}
			set
			{
				_currentTopicPackageLesson = value;
				RaisePropertyChanged(() => CurrentTopicPackageLesson);
			}
		}

		public ObservableCollection<QuestionTag> KnowledgeTags
		{
			get;
			set;
		} = new ObservableCollection<QuestionTag>();


		public ObservableCollection<BeforeLessonPackageGroup> BeforeLessonPackageGroup
		{
			get;
			set;
		} = new ObservableCollection<BeforeLessonPackageGroup>();


		public int BeforeLessonPackageGroupId
		{
			get;
			set;
		} = -1;


		private void InitBook()
		{
			BooksCollection = new ObservableCollection<Book>();
			_bookService = ServiceLocator.Current.GetInstance<IBookService>();
		}

		public void NewBook()
		{
			CurrentBook = new Book();
			CurrentBook.BookSeries = GetCurrentBookSeries();
		}

		public void ClearBook()
		{
			ClearLesson();
			CurrentBook = null;
		}

		public async Task<List<Book>> GetAllBooks()
		{
			return await _bookService.GetAllBooks();
		}

		public async Task SearchBooks(string str)
		{
			List<Book> re = await _bookService.SearchBooks(str);
			if (re != null)
			{
				BooksCollection.Clear();
				BooksCollection.AddRange(re);
			}
		}

		public async Task<int> GetMaterialBooksCount()
		{
			List<Book> re = await _bookService.GetMaterialBooks();
			if (re != null)
			{
				return Enumerable.Count(re);
			}
			return 0;
		}

		public async Task GetMaterialBooks()
		{
			List<Book> re = await _bookService.GetMaterialBooks();
			if (re != null)
			{
				BooksCollection.Clear();
				BooksCollection.AddRange(re);
			}
		}

		public async Task GetBooksBySeries()
		{
			if (CurrentSeries != null)
			{
				Tuple<List<Book>, int> re = await _bookService.GetBooksBySeries(CurrentSeries, 15, 1);
				BooksCollection.Clear();
				_bookTotalCount = 0;
				if (re != null)
				{
					_bookTotalCount = re.Item2;
					foreach (Book book in re.Item1)
					{
						UpdateBookCreator(book);
					}
					BooksCollection.AddRange(re.Item1);
				}
			}
		}

		public async Task GetMoreBooksBySeries()
		{
			if (CurrentSeries != null && _bookTotalCount > BooksCollection.Count)
			{
				int pageNum = BooksCollection.Count / 15 + 1;
				Tuple<List<Book>, int> re = await _bookService.GetBooksBySeries(CurrentSeries, 15, pageNum);
				if (re != null)
				{
					_bookTotalCount = re.Item2;
					foreach (Book book in re.Item1)
					{
						UpdateBookCreator(book);
					}
					BooksCollection.AddRange(re.Item1);
				}
			}
		}

		private void UpdateBookCreator(Book b)
		{
			string creator = "【乐学课本】";
			if (AppUser.Instance().CurrentUser.IsAdmin)
			{
				if (b.companyId.HasValue)
				{
					creator = b.companyId.Value.ToString();
				}
			}
			else if (b.companyId.HasValue)
			{
				creator = AppUser.Instance().CurrentUser.Name;
			}
			b.CreatorStr = creator;
		}

		public async Task<bool> DeleteBook(Book b)
		{
			bool r = await _bookService.DeleteBook(b);
			if (r)
			{
				await GetBooksBySeries();
			}
			return r;
		}

		public async Task<bool> GetBook(int id)
		{
			Book re = await _bookService.GetBookInfo(id);
			if (re != null)
			{
				CurrentBook = re;
				CurrentBook.BookSeries = GetCurrentBookSeries();
			}
			return re != null;
		}

		private Series GetCurrentBookSeries()
		{
			if ((CurrentSeries?.id.HasValue ?? false) && CurrentSeries.id.Value > 0)
			{
				return CurrentSeries;
			}
			if (CurrentBook.BookSeriesId.HasValue)
			{
				Series re = Enumerable.FirstOrDefault(SeriesCollection, (Series s) => s.id.HasValue && s.id.Value.Equals(CurrentBook.BookSeriesId.Value));
				if (re != null)
				{
					return re;
				}
			}
			return Enumerable.FirstOrDefault(SeriesCollection, (Series s) => !s.id.HasValue);
		}

		public async Task RefreshBookInfo()
		{
			if (CurrentBook != null)
			{
				Book re = await _bookService.GetBookInfo(CurrentBook.Id);
				if (re != null)
				{
					CurrentBook = re;
					CurrentBook.BookSeries = GetCurrentBookSeries();
				}
			}
		}

		public async Task<bool> CreateBook()
		{
			int id = await _bookService.CreateBook(CurrentBook);
			if (id != -1)
			{
				CurrentBook.Id = id;
				await GetBooksBySeries();
			}
			return id != -1;
		}

		public async Task<bool> UpdateBook()
		{
			bool re = await _bookService.UpdateBook(CurrentBook);
			if (re)
			{
				await GetBooksBySeries();
			}
			return re;
		}

		public async Task<bool> AddBookTag(Tag tag)
		{
			if (!CurrentBook.IsSaved)
			{
				return false;
			}
			bool re = await _bookService.AddBookTag(CurrentBook, tag);
			if (re)
			{
				await RefreshBookInfo();
			}
			return re;
		}

		public async Task<bool> DeleteBookTag(Tag tag)
		{
			bool re = await _bookService.DeleteBookTag(CurrentBook, tag);
			if (re)
			{
				await RefreshBookInfo();
			}
			return re;
		}

		public async Task<bool> CheckBookMaterials(Book book, string path)
		{
			try
			{
				byte[] byteArray = await _bookService.CheckBookMaterials(book.Id);
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

		public async Task<IList<Tuple<int, IList<int>>>> CheckBookTagQuestionsComplete(Book book)
		{
			return await _bookService.CheckBookTagQuestionsComplete(book.Id);
		}

		public async Task<bool> CopyBookToTargetBook(int bookId, int targetBookId)
		{
			if (await _bookService.CopyBookToTargetBook(bookId, targetBookId))
			{
				await RefreshBookInfo();
				return true;
			}
			return false;
		}

		private AppData()
		{
			InitSeries();
			InitBook();
			InitLesson();
			InitTopic();
			InitQuestion();
			InitTopicPackageLesson();
			InitTopicPackageQuestion();
			AudioFilePathCollection = new ObservableCollection<AudioFile>();
		}

		public void Clear()
		{
			_instance = null;
		}

		public void ClearAudioFilePath()
		{
			AudioFilePathCollection.Clear();
		}

		public async Task<bool> Upload(UploadData[] uploadDatas)
		{
			return await UploadService.IsUploadFilesAsync(uploadDatas);
		}

		private void InitLesson()
		{
			_lessonService = ServiceLocator.Current.GetInstance<ILessonService>();
		}

		public void NewLesson()
		{
			CurrentLesson = new Lesson
			{
				Book = CurrentBook
			};
		}

		public void ClearLesson()
		{
			ClearTopic();
			CurrentLesson = null;
		}

		public async Task<bool> GetLessonInfo(int id)
		{
			Lesson re = await _lessonService.GetLessonInfo(id);
			if (re != null)
			{
				CurrentLesson = re;
				CurrentLesson.Book = CurrentBook;
			}
			return re != null;
		}

		public async Task RefreshLessonInfo()
		{
			if (CurrentLesson != null)
			{
				Lesson re = await _lessonService.GetLessonInfo(CurrentLesson.Id);
				if (re != null)
				{
					CurrentLesson = re;
					CurrentLesson.Book = CurrentBook;
				}
			}
		}

		public async Task<bool> CreateLesson()
		{
			int id = await _lessonService.CreateLesson(CurrentLesson);
			if (id != -1)
			{
				CurrentLesson.Id = id;
				await RefreshBookInfo();
			}
			return id != -1;
		}

		public async Task<bool> BatchCreateLessons(string name, int count)
		{
			if (await _lessonService.BatchCreateLessons(CurrentBook.Id, name, count))
			{
				await RefreshBookInfo();
				return true;
			}
			return false;
		}

		public async Task<bool> UpdateLesson()
		{
			bool re = await _lessonService.UpdateLesson(CurrentLesson);
			if (re)
			{
				await RefreshBookInfo();
			}
			return re;
		}

		public async Task<bool> UpdateLesson(Lesson lesson)
		{
			bool re = await _lessonService.UpdateLesson(lesson);
			if (re)
			{
				await RefreshBookInfo();
			}
			return re;
		}

		public async Task<bool> DeleteLesson(Lesson l)
		{
			bool re = await _lessonService.DeleteLesson(l);
			if (re)
			{
				await RefreshBookInfo();
			}
			return re;
		}

		public async Task<bool> MoveLesson(Lesson lesson, int idx)
		{
			bool re = await _lessonService.MoveLesson(lesson, idx);
			if (re)
			{
				await RefreshBookInfo();
			}
			return re;
		}

		private void InitQuestion()
		{
			_questionService = ServiceLocator.Current.GetInstance<IQuestionService>();
			_translateService = ServiceLocator.Current.GetInstance<ITranslateService>();
		}

		public async Task<bool> CreateQuestion(Question q)
		{
			int id = await _questionService.CreateQuestion(q);
			if (id != -1)
			{
				q.Id = id;
			}
			return id != -1;
		}

		public async Task<bool> UpdateQuestion(Question q)
		{
			return await _questionService.UpdateQuestion(q);
		}

		public async Task<bool> DeleteQuestion(Question q)
		{
			return await _questionService.DeleteQuestion(q);
		}

		public async Task AutoTranslate(Question q)
		{
			string text2 = q.NativeText = await _translateService.GetTranslationOf(q.ForeignText);
		}

		private void InitSeries()
		{
			SeriesCollection = new ObservableCollection<Series>();
			_seriesService = ServiceLocator.Current.GetInstance<ISeriesService>();
		}

		public void ClearSeries()
		{
			ClearBook();
			CurrentSeries = null;
		}

		public async Task GetSeries(bool isInitSelectedSeries = true)
		{
			List<Series> seriesItems = await _seriesService.GetSeries();
			if (seriesItems != null)
			{
				SeriesCollection.Clear();
				foreach (Series item in seriesItems)
				{
					item.Pinyin = PinyinHelper.GetPinyin(item.Name).ToLower();
					SeriesCollection.Add(item);
				}
				int bcount = await GetMaterialBooksCount();
				SeriesCollection.Add(new Series
				{
					id = -1,
					Name = "教师素材",
					Pinyin = "jssc",
					bookCount = bcount
				});
				if (isInitSelectedSeries)
				{
					CurrentSeries = Enumerable.FirstOrDefault(SeriesCollection);
				}
			}
		}

		public async Task<bool> DeleteSeries(Series series)
		{
			bool num = await _seriesService.DeleteSeries(series);
			if (num)
			{
				SeriesCollection.Remove(series);
				CurrentSeries = Enumerable.FirstOrDefault(SeriesCollection);
			}
			return num;
		}

		public async Task<bool> UpdateSeries()
		{
			return await _seriesService.UpdateSeries(CurrentSeries);
		}

		public async Task<bool> CreateSeries(Series ns)
		{
			_003C_003Ec__DisplayClass82_0 _003C_003Ec__DisplayClass82_;
			_ = _003C_003Ec__DisplayClass82_.sid;
			int sid = await _seriesService.CreateSeries(ns.Name);
			if (sid != -1)
			{
				await GetSeries(isInitSelectedSeries: false);
				CurrentSeries = Enumerable.FirstOrDefault(SeriesCollection, (Series s) => s.id.Equals(sid));
			}
			return sid != -1;
		}

		private void InitTopic()
		{
			_topicService = ServiceLocator.Current.GetInstance<ITopicService>();
		}

		public void NewTopic(TopicTypeEnum type)
		{
			CurrentTopic = Topic.NewTopicByType(type);
			CurrentTopic.Lesson = CurrentLesson;
		}

		public void ClearTopic()
		{
			CurrentTopic = null;
			_initTopic = null;
		}

		public async Task InitTopicInfo()
		{
			_ = _initTopic;
			Topic topic = _initTopic = await ObjectHelper.DeepCopyAsync(CurrentTopic);
		}

		public bool TopicHasChanged()
		{
			if (CurrentTopic == null)
			{
				LogHelper.Error("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@返回前CurrentTopic为空@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
				return true;
			}
			return CurrentTopic.HasChanged(_initTopic);
		}

		public async Task<bool> GetTopic(int id)
		{
			Topic re = await _topicService.GetTopic(id);
			if (re != null)
			{
				CurrentTopic = re;
				CurrentTopic.Lesson = CurrentLesson;
			}
			return re != null;
		}

		public async Task RefreshTopicInfo()
		{
			if (CurrentTopic != null)
			{
				Topic re = await _topicService.GetTopic(CurrentTopic.Id);
				if (re != null)
				{
					CurrentTopic = re;
					CurrentTopic.Lesson = CurrentLesson;
				}
			}
		}

		public async Task<bool> MoveTopic(Topic topic, int idx)
		{
			bool re = await _topicService.MoveTopic(topic, idx);
			if (re)
			{
				await RefreshLessonInfo();
			}
			return re;
		}

		public async Task<bool> DeleteTopic(Topic t)
		{
			bool re = await _topicService.DeleteTopic(t);
			if (re)
			{
				await RefreshLessonInfo();
			}
			return re;
		}

		public async Task<bool> CreateTopic()
		{
			int id = await _topicService.CreateTopic(CurrentTopic);
			if (id != -1)
			{
				CurrentTopic.Id = id;
				await RefreshLessonInfo();
			}
			return id != -1;
		}

		public async Task<bool> UpdateTopic()
		{
			bool re = await _topicService.UpdateTopic(CurrentTopic);
			if (re)
			{
				await RefreshLessonInfo();
			}
			return re;
		}

		public async Task<bool> MoveTopicToLesson(Topic topic, Lesson lesson)
		{
			RequestInfo req = new RequestInfo();
			req.AddQueryStringInfo("id", topic.Id.ToString());
			req.AddQueryStringInfo("lessonId", lesson.Id.ToString());
			topic.TopicRequestInfo = req;
			bool re = await _topicService.UpdateTopic(topic);
			if (re)
			{
				await RefreshLessonInfo();
			}
			return re;
		}

		private void InitTopicPackageQuestion()
		{
			_packageQuestionService = ServiceLocator.Current.GetInstance<IPackageQuestionService>();
		}

		public void ClearPackageQuestion()
		{
			AfterQuestions.Clear();
			BeforeLessonPackageGroup.Clear();
			BeforeLessonPackageGroupId = -1;
		}

		public async Task<Tuple<IList<TopicPackageQuestion>, int>> GetPackageQuestionsBy(int tagId, int pageSize, int pageNum, TopicPackageQuestionTypeEnum type = TopicPackageQuestionTypeEnum.全部, string title = null)
		{
			return await _packageQuestionService.GetPackageQuestionsBy(type, tagId, pageSize, pageNum, title);
		}

		public async Task<TopicPackageQuestion> GetPackageQuestionBy(int questionId)
		{
			return await _packageQuestionService.GetPackageQuestionById(questionId);
		}

		public async Task GetPackageQuestionsByLesson()
		{
			if (CurrentLesson != null)
			{
				IList<TopicPackageQuestion> qs = await _packageQuestionService.GetAfterPackageQuestionsByLessonId(CurrentLesson.Id);
				if (qs != null)
				{
					AfterQuestions.Clear();
					foreach (TopicPackageQuestion q in qs)
					{
						AfterQuestions.Add(q);
					}
				}
			}
		}

		public async Task<bool> DeletePackageQuestion(TopicPackageQuestion question)
		{
			int lessonId = CurrentLesson.Id;
			bool re = await _packageQuestionService.DeletePackageQuestion(lessonId, question.Id);
			if (re)
			{
				await RefreshBookInfo();
			}
			return re;
		}

		public async Task<bool> AddPackageQuestionFromDatabase(int questionId)
		{
			int bookId = CurrentBook.Id;
			int lessonId = CurrentLesson.Id;
			bool re = await _packageQuestionService.AddPackageQuestion(bookId, lessonId, questionId);
			if (re)
			{
				await RefreshBookInfo();
				await GetPackageQuestionsByLesson();
			}
			return re;
		}

		public async Task<bool> CreatePackageQuestion(TopicPackageQuestion question)
		{
			int id = await _packageQuestionService.CreatePackageQuestion(question);
			if (id != -1)
			{
				question.Id = id;
				await RefreshBookInfo();
			}
			return id != -1;
		}

		public async Task<bool> UpdatePackageQuestion(TopicPackageQuestion question)
		{
			return await _packageQuestionService.UpdatePackageQuestion(question);
		}

		public async Task<bool> SavePackageQuestion(TopicPackageQuestion question)
		{
			if (question.IsUnsupported)
			{
				return true;
			}
			if (question.IsSaved)
			{
				if (question.HasChanged)
				{
					bool num = await _packageQuestionService.UpdatePackageQuestion(question);
					if (num)
					{
						question.HasChanged = false;
					}
					return num;
				}
				return true;
			}
			int id = await _packageQuestionService.CreatePackageQuestion(question);
			if (id != -1)
			{
				question.Id = id;
				question.HasChanged = false;
				await RefreshBookInfo();
			}
			return id != -1;
		}

		public async Task<bool> CheckPackageQuestion(TopicPackageQuestion q)
		{
			return await _packageQuestionService.ChangeQuestionStatus(q.Id);
		}

		private void InitTopicPackageLesson()
		{
			_knowledgeService = ServiceLocator.Current.GetInstance<IKnowledgeService>();
		}

		public void NewTopicPackageLesson()
		{
			NewLesson();
			CurrentTopicPackageLesson = new TopicPackageLesson
			{
				LessonInfo = CurrentLesson
			};
		}

		public void ClearTopicPackageLesson()
		{
			ClearLesson();
			ClearPackageQuestion();
			CurrentTopicPackageLesson = null;
		}

		public async Task<bool> CreateTopicPackageLesson(IEnumerable<QuestionTag> tags, bool auto)
		{
			if (await CreateLesson() && await _knowledgeService.AddLessonKnowledges(CurrentLesson.Id, auto, tags))
			{
				await RefreshBookInfo();
				await RefreshTopicPackageLesson();
				return true;
			}
			return false;
		}

		public async Task<bool> UpdateTopicPackageLesson(IEnumerable<QuestionTag> tags, bool auto)
		{
			if (await UpdateLesson() && await _knowledgeService.AddLessonKnowledges(CurrentLesson.Id, auto, tags))
			{
				await RefreshBookInfo();
				await RefreshTopicPackageLesson();
				return true;
			}
			return false;
		}

		public async Task<bool> AddLessonKnowledgeIds(bool auto, params int[] tagIds)
		{
			if (await _knowledgeService.AddLessonKnowledgeIds(CurrentLesson.Id, auto, tagIds))
			{
				await RefreshBookInfo();
				await RefreshTopicPackageLesson();
				return true;
			}
			return false;
		}

		public async Task<IList<TopicPackageLesson>> GetTopicPackageLessons(IList<Lesson> lessons)
		{
			if (lessons != null && Enumerable.Any(lessons))
			{
				IList<TopicPackageLesson> topicPackageLessons = await _knowledgeService.GetTopicPackageLessonsByLessons(lessons);
				if (topicPackageLessons != null)
				{
					List<TopicPackageLesson> re = new List<TopicPackageLesson>();
					foreach (Lesson lesson in lessons)
					{
						TopicPackageLesson i = Enumerable.FirstOrDefault(topicPackageLessons, (TopicPackageLesson tl) => tl.Id.Equals(lesson.Id));
						if (i != null)
						{
							i.LessonInfo = lesson;
							re.Add(i);
						}
					}
					return re;
				}
			}
			return null;
		}

		public async Task<bool> DeleteTopicPackageLessonKnowledge(QuestionTag tag)
		{
			bool re = await _knowledgeService.DeleteLessonKnowledge(CurrentLesson.Id, tag.Id);
			if (re)
			{
				await RefreshBookInfo();
				await RefreshTopicPackageLesson();
			}
			return re;
		}

		public async Task<bool> UpdateTopicPackageLessonKnowledge(QuestionTag tag, string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return false;
			}
			return await _knowledgeService.UpdateLessonKnowledge(tag.Id, name);
		}

		public async Task RefreshTopicPackageLesson()
		{
			if (CurrentLesson != null && await GetLessonInfo(CurrentLesson.Id))
			{
				TopicPackageLesson topicPackageLesson2 = CurrentTopicPackageLesson = await _knowledgeService.GetTopicPackageLessonByLesson(CurrentLesson);
				if (CurrentTopicPackageLesson != null)
				{
					KnowledgeTags.Clear();
					KnowledgeTags.AddRange(CurrentTopicPackageLesson.Knowledges);
					CurrentTopicPackageLesson.LessonInfo = CurrentLesson;
				}
			}
		}

		public async Task<bool> GetTopicPackageLesson(int id)
		{
			if (await GetLessonInfo(id))
			{
				TopicPackageLesson topicPackageLesson2 = CurrentTopicPackageLesson = await _knowledgeService.GetTopicPackageLessonByLesson(CurrentLesson);
				if (CurrentTopicPackageLesson != null)
				{
					KnowledgeTags.Clear();
					KnowledgeTags.AddRange(CurrentTopicPackageLesson.Knowledges);
					CurrentTopicPackageLesson.LessonInfo = CurrentLesson;
					return true;
				}
			}
			return false;
		}

		public async Task GetBeforePackageQuestionsByLesson()
		{
			if (CurrentLesson != null)
			{
				BeforeLessonPackage re = await _packageQuestionService.GetBeforePackageQuestionsByLessonId(CurrentLesson.Id);
				if (re != null)
				{
					BeforeLessonPackageGroupId = re.Id;
					BeforeLessonPackageGroup.Clear();
					BeforeLessonPackageGroup.AddRange(re.Groups);
				}
			}
		}

		public async Task<bool> SaveBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups)
		{
			int bookId = CurrentBook.Id;
			int lessonId = CurrentLesson.Id;
			bool re = false;
			if (BeforeLessonPackageGroupId == -1)
			{
				int id = await _packageQuestionService.CreateBeforePackageQuestionGroups(groups, bookId, lessonId);
				if (id != -1)
				{
					BeforeLessonPackageGroupId = id;
					re = true;
				}
			}
			else
			{
				re = await _packageQuestionService.UpdateBeforePackageQuestionGroups(groups, bookId, lessonId, BeforeLessonPackageGroupId);
			}
			return re;
		}

		public async Task<bool> AddBeforePackageQuestionFromDatabase(IEnumerable<BeforeLessonPackageGroup> groups)
		{
			bool re = await SaveBeforePackageQuestionGroups(groups);
			if (re)
			{
				await RefreshBookInfo();
				await GetBeforePackageQuestionsByLesson();
			}
			return re;
		}

		public async Task<bool> DeleteBeforePackageQuestion(IEnumerable<BeforeLessonPackageGroup> groups)
		{
			bool re = await SaveBeforePackageQuestionGroups(groups);
			if (re)
			{
				await RefreshBookInfo();
				await GetBeforePackageQuestionsByLesson();
			}
			return re;
		}
	}
}
