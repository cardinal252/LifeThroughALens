using System;
using Cardinal.Glass.Extensions.Mapping;
using Cardinal.IoC;
using Cardinal.IoC.Unity;
using Glass.Mapper.Sc.Configuration.Fluent;
using LifeThroughALens.IntegrationTests.Common;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace LifeThroughALens.IntegrationTests.Unity
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
            GetHomeItem();
        }

        protected override IContainerAdapter GetContainerAdapter()
        {
            IUnityContainer container = new UnityContainer();
            return new UnityContainerAdapter(Guid.NewGuid().ToString(), container);
        }
    }
}
