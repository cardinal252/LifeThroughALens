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
    }
}
