using Cardinal.Glass.Extensions.Mapping;
using Cardinal.Glass.Extensions.Mapping.Sitecore;
using Cardinal.IoC;
using Glass.Mapper;
using Glass.Mapper.Sc.Configuration.Fluent;
using LifeThroughALens.Domain;
using LifeThroughALens.Maps;
using NUnit.Framework;

namespace LifeThroughALens.IntegrationTests
{
    [TestFixture]
    public class ItemRetrievalTests : GlassTestBase
    {

        [TestFixtureSetUp]
        public void Setup()
        {           
            RegisterMapsWithContainer(ContainerManager);

            ConfigurationMap configurationMap = new ConfigurationMap(DependencyResolver);

            SitecoreFluentConfigurationLoader configloader = configurationMap.GetConfigurationLoader<SitecoreFluentConfigurationLoader>();

            SetupGlassService(configloader);
        }

        [Test]
        public void CanGetHomepageSuccessfully()
        {
            // Act
            ISiteRoot siteRoot = GlassService.GetItem<ISiteRoot>(SitecoreIds.HomePageId.ToGuid());

            // Assert
            Assert.IsNotNull(siteRoot);
            Assert.AreEqual("Home", siteRoot.Name);
            Assert.AreEqual("Welcome to Officecore", siteRoot.Title);
            Assert.AreEqual("/~/media/Images/Logos/poweredbysitecore.ashx", siteRoot.PoweredByImage.Src);

        }

        protected void RegisterMapsWithContainer(IContainerManager containerManager)
        {
            // this would usually be done using the containers own scanning functionality

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

            containerManager.Adapter.Register(DependencyResolver);

            containerManager.Adapter.Register<AbstractDataMapper, SitecoreDelegateMapper>();
            containerManager.Adapter.Register<ISitecoreTypeProvider, SitecoreTypeProvider>();
        }

    }
}
