using System;
using Autofac;
using Cardinal.Glass.Extensions.AgnosticIoC;
using Cardinal.Glass.Extensions.Mapping;
using Cardinal.Glass.Extensions.Mapping.Sitecore;
using Cardinal.IoC;
using Cardinal.Ioc.Autofac;
using Glass.Mapper;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration.Fluent;
using LifeThroughALens.Domain;
using LifeThroughALens.Maps;
using NUnit.Framework;
using Sitecore.Configuration;

namespace LifeThroughALens.IntegrationTests
{
    [TestFixture]
    public class ItemRetrievalTests
    {
        private ISitecoreService sitecoreService;

        [TestFixtureSetUp]
        public void Setup()
        {
            // use the native container scanning functionality to register the maps
            IContainerManager containerManager = GetContainerManager();

            var dependencyResolver = RegisterMaps(containerManager);

            SitecoreTypeProvider provider = new SitecoreTypeProvider(dependencyResolver);
            Console.WriteLine(provider.GetSitecoreType<IBreadCrumb>().GetType());

            ConfigurationMap configurationMap = new ConfigurationMap(dependencyResolver);
            SitecoreFluentConfigurationLoader configloader = configurationMap.GetConfigurationLoader<SitecoreFluentConfigurationLoader>();

            Context context = Context.Create(dependencyResolver);
            context.Load(configloader);

            sitecoreService = new SitecoreService(Factory.GetDatabase("master"), context);
        }

        [Test]
        public void CanGetHomepageSuccessfully()
        {
            // Act
            ISiteRoot siteRoot = sitecoreService.GetItem<ISiteRoot>(SitecoreIds.HomePageId.ToGuid());

            // Assert
            Assert.IsNotNull(siteRoot);
            Assert.AreEqual("Home", siteRoot.Name);
            Assert.AreEqual("Welcome to Officecore", siteRoot.Title);
            Assert.AreEqual("/~/media/Images/Logos/poweredbysitecore.ashx", siteRoot.PoweredByImage.Src);

        }

        private static IContainerManager GetContainerManager()
        {
            ContainerBuilder builder = new ContainerBuilder();

            IContainer container = builder.Build();

            return new ContainerManager(new AutofacContainerAdapter(container));
        }

        private static IDependencyResolver RegisterMaps(IContainerManager containerManager)
        {
            var config = new Config();
            containerManager.Adapter.Register<IGlassMap, SitecoreItemMap>();
            containerManager.Adapter.Register<ISitecoreGlassMap<ISitecoreItem>, SitecoreItemMap>();

            containerManager.Adapter.Register<IGlassMap, BreadCrumbMap>();
            containerManager.Adapter.Register<ISitecoreGlassMap<IBreadCrumb>, BreadCrumbMap>();

            containerManager.Adapter.Register<IGlassMap, PageTitleAndTextMap>();
            containerManager.Adapter.Register<ISitecoreGlassMap<IPageTitleAndText>, PageTitleAndTextMap>();

            containerManager.Adapter.Register<IGlassMap, SiteRootMap>();
            containerManager.Adapter.Register<ISitecoreGlassMap<ISiteRoot>, SiteRootMap>();

            containerManager.Adapter.Register<IGlassMap, ThemeMap>();
            containerManager.Adapter.Register<ISitecoreGlassMap<ITheme>, ThemeMap>();

            containerManager.Adapter.Register<IGlassMap, ConcreteThemeMap>();
            containerManager.Adapter.Register<ISitecoreGlassMap<Theme>, ConcreteThemeMap>();

            IDependencyResolver dependencyResolver = new DependencyResolver(containerManager.Adapter);
            containerManager.Adapter.Register(dependencyResolver);
            containerManager.Adapter.RegisterGroup(new SitecoreInstaller(config));
            containerManager.Adapter.Register<ISitecoreTypeProvider, SitecoreTypeProvider>();
            containerManager.Adapter.Register<AbstractDataMapper, SitecoreDelegateMapper>();
            return dependencyResolver;
        }

    }
}
