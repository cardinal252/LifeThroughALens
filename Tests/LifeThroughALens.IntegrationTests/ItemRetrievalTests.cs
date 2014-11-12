using Cardinal.Glass.Extensions.Mapping;
using Cardinal.IoC;
using Cardinal.IoC.StructureMap;
using Glass.Mapper.Sc;
using LifeThroughALens.Domain;
using NUnit.Framework;
using StructureMap;

namespace LifeThroughALens.IntegrationTests
{
    [TestFixture]
    public class ItemRetrievalTests
    {
        private ISitecoreService sitecoreService;

        [TestFixtureSetUp]
        public void Setup()
        {
            IContainer container = new Container();
            // use the native container scanning functionality to register the maps
            container.Configure(x => x.Scan(y =>
            {
                y.Assembly("LifeThroughALens.Maps");
                y.AddAllTypesOf<IGlassMap>();
            }));

            IContainerManager containerManager = new ContainerManager(new StructureMapContainerAdapter(container));
            sitecoreService = new SitecoreService("master");
        }

        [Test]
        public void CanGetHomepageSuccessfully()
        {
            // Assign

            // Act
            ISiteRoot siteRoot = sitecoreService.GetItem<ISiteRoot>(SitecoreIds.JobHubId.ToGuid());

            // Assert
            Assert.IsNotNull(siteRoot);
        }

    }
}
