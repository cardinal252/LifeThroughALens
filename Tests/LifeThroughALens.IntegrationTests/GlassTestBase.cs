using System;
using Autofac;
using Cardinal.Glass.Extensions.AgnosticIoC;
using Cardinal.IoC;
using Cardinal.Ioc.Autofac;
using Glass.Mapper;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration.Fluent;
using Sitecore.Configuration;
using Sitecore.Links;

namespace LifeThroughALens.IntegrationTests
{
    public class GlassTestBase
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

        /// <summary>
        /// Gets the container manager
        /// </summary>
        /// <returns></returns>
        private void PopulateContainerManager()
        {
            ContainerBuilder builder = new ContainerBuilder();

            IContainer container = builder.Build();

            Config config = new Config();

            containerManager = new ContainerManager(new AutofacContainerAdapter(container));
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
    }
}
