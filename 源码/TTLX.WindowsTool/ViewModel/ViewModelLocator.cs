using System;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Features.Scanning;
using Microsoft.Practices.ServiceLocation;
using TTLX.WindowsTool.Business;

namespace TTLX.WindowsTool.ViewModel
{
	// Token: 0x02000082 RID: 130
	public class ViewModelLocator
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		public ViewModelLocator()
		{
			ContainerBuilder containerBuilder = new ContainerBuilder();
			from t in containerBuilder.RegisterAssemblyTypes(new Assembly[]
			{
				Assembly.GetExecutingAssembly()
			})
			where t.Name.EndsWith("ViewModel")
			select t;
			containerBuilder.RegisterType<UserService>().As<IUserService>().SingleInstance();
			containerBuilder.RegisterType<ConfigService>().As<IConfigService>().SingleInstance();
			containerBuilder.RegisterType<WordPronService>().As<IWordPronService>().SingleInstance();
			containerBuilder.RegisterType<SeriesService>().As<ISeriesService>().SingleInstance();
			containerBuilder.RegisterType<BookService>().As<IBookService>().SingleInstance();
			containerBuilder.RegisterType<LessonService>().As<ILessonService>().SingleInstance();
			containerBuilder.RegisterType<TopicService>().As<ITopicService>().SingleInstance();
			containerBuilder.RegisterType<QuestionService>().As<IQuestionService>().SingleInstance();
			containerBuilder.RegisterType<TencentTranslateService>().As<ITranslateService>().SingleInstance();
			containerBuilder.RegisterType<MaterialMediaService>().As<IMaterialMediaService>().SingleInstance();
			containerBuilder.RegisterType<KnowledgeService>().As<IKnowledgeService>().SingleInstance();
			containerBuilder.RegisterType<PackageQuestionService>().As<IPackageQuestionService>().SingleInstance();
			containerBuilder.RegisterType<KnowledgeTagService>().As<IKnowledgeTagService>().SingleInstance();
			IContainer container = containerBuilder.Build(ContainerBuildOptions.None);
			ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001D047 File Offset: 0x0001B247
		public static void Cleanup()
		{
		}
	}
}
