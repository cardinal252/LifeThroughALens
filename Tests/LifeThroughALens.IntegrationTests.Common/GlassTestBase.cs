using System;
using Cardinal.Glass.Extensions.AgnosticIoC;
using Cardinal.Glass.Extensions.Mapping;
using Cardinal.Glass.Extensions.Mapping.Sitecore;
using Cardinal.IoC;
using Glass.Mapper;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration.Fluent;
using LifeThroughALens.Domain;
using LifeThroughALens.Maps;
using NUnit.Framework;
using Sitecore.Configuration;
using Sitecore.Links;

namespace LifeThroughALens.IntegrationTests.Common
{
    public abstract class GlassTestBase
    {
        #region [ Fields ]

        private IContainerManager containerManager;

        private IDependencyResolver dependencyResolver;

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the glass sitecore service
        /// </summary>
        protected ISitecoreService GlassService { get; set; }

        /// <summary>
        /// Gets the container manager
        /// </summary>
        protected IContainerManager ContainerManager 
        {
            get
            {
                if (containerManager == null)
                {
                    PopulateContainerManager();
                }

                return containerManager;
            }
        }

        /// <summary>
        /// Gets the dependency resolver
        /// </summary>
        protected IDependencyResolver DependencyResolver
        {
            get
            {
                return dependencyResolver ?? (dependencyResolver = new DependencyResolver(ContainerManager.Adapter));
            }
        }

        #endregion

        protected void GetHomeItem()
        {
            // Act
            ISiteRoot siteRoot = GlassService.GetItem<ISiteRoot>(SitecoreIds.HomePageId.ToGuid());

            // Assert
            Assert.IsNotNull(siteRoot);
            Assert.AreEqual("Home", siteRoot.Name);
            Assert.AreEqual("Welcome to Officecore", siteRoot.Title);
            Assert.AreEqual("/~/media/Images/Logos/poweredbysitecore.ashx", siteRoot.PoweredByImage.Src);
        }


        protected abstract IContainerAdapter GetContainerAdapter();

        /// <summary>
        /// Gets the container manager
        /// </summary>
        /// <returns></returns>
        private void PopulateContainerManager()
        {
            Config config = new Config();

            containerManager = new ContainerManager(GetContainerAdapter());
            containerManager.Adapter.RegisterGroup(new SitecoreInstaller(config));
        }

        protected string GetItemUrl(SitecoreDataMappingContext arg)
        {
            if (arg.Item == null)
            {
                return String.Empty;
            }

            return LinkManager.GetItemUrl(arg.Item);
        }

        protected void SetupGlassService(SitecoreFluentConfigurationLoader configloader)
        {
            Context context = Context.Create(DependencyResolver);
            context.Load(configloader);

            GlassService = new SitecoreService(Factory.GetDatabase("master"), context);
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
