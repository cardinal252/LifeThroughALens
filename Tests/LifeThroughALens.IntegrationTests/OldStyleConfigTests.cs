using System;
using Autofac;
using Cardinal.Glass.Extensions.AgnosticIoC;
using Cardinal.Glass.Extensions.Mapping.Sitecore;
using Cardinal.IoC;
using Cardinal.Ioc.Autofac;
using Glass.Mapper;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Fluent;
using LifeThroughALens.Domain;
using NUnit.Framework;
using Sitecore.Configuration;
using Sitecore.Links;

namespace LifeThroughALens.IntegrationTests
{
    [TestFixture]
    public class OldStyleConfigTests
    {
        private ISitecoreService sitecoreService;

        [TestFixtureSetUp]
        public void Setup()
        {
            // use the native container scanning functionality to register the maps
            IContainerManager containerManager = GetContainerManager();

            Config config = new Config();
            IDependencyResolver dependencyResolver = new DependencyResolver(containerManager.Adapter);
            containerManager.Adapter.Register(dependencyResolver);
            containerManager.Adapter.RegisterGroup(new SitecoreInstaller(config));
            containerManager.Adapter.Register<AbstractDataMapper, SitecoreDelegateMapper>();

            SitecoreFluentConfigurationLoader configloader = new SitecoreFluentConfigurationLoader();
            SetupGlass(configloader);

            Context context = Context.Create(dependencyResolver);
            context.Load(configloader);

            sitecoreService = new SitecoreService(Factory.GetDatabase("master"), context);
        }

        private void SetupGlass(SitecoreFluentConfigurationLoader configloader)
        {
            SitecoreType<ISitecoreItem> sitecoreItemType = new SitecoreType<ISitecoreItem>();
            sitecoreItemType.Id(y => y.Id);
            sitecoreItemType.Info(y => y.Name).InfoType(SitecoreInfoType.Name);
            // NOT DEFAULT GLASS
            sitecoreItemType.Delegate(y => y.Url).GetValue(GetItemUrl);
            configloader.Add(sitecoreItemType);

            SitecoreType<IBreadCrumb> breadcrumbType = new SitecoreType<IBreadCrumb>();
            breadcrumbType.Field(y => y.BreadcrumbTitle).FieldName("Breadcrumb Title");
            breadcrumbType.Import(sitecoreItemType);
            configloader.Add(sitecoreItemType);

            SitecoreType<IPageTitleAndText> pageTitleAndTextType = new SitecoreType<IPageTitleAndText>();
            pageTitleAndTextType.Field(y => y.Title).FieldName("Title");
            pageTitleAndTextType.Field(y => y.SectionImage).FieldName("Section Image");
            pageTitleAndTextType.Import(sitecoreItemType);
            configloader.Add(pageTitleAndTextType);

            SitecoreType<ISiteRoot> siteRootType = new SitecoreType<ISiteRoot>();
            siteRootType.Field(y => y.EnableGoogleFonts).FieldName("Enable Google Fonts");
            siteRootType.Field(y => y.GoogleFontType).FieldName("Google Font Type");
            siteRootType.Field(y => y.PoweredByImage).FieldName("Powered By Image");
            siteRootType.Field(y => y.PoweredByLink).FieldName("Powered By Link");
            siteRootType.Field(y => y.SiteName).FieldName("Site Name");
            siteRootType.Field(y => y.StrapLine).FieldName("Strap Line");
            /*
             * Can't do this cause it throws a duplicate mapping exception
             * this is to be fixed in future versions of glass
            siteRootType.Import(pageTitleAndTextType);
            siteRootType.Import(breadcrumbType);
             * so instead you have to do:
             */

            siteRootType.Field(y => y.Title).FieldName("Title");
            siteRootType.Field(y => y.SectionImage).FieldName("Section Image");
            siteRootType.Field(y => y.BreadcrumbTitle).FieldName("Breadcrumb Title");
            siteRootType.Import(sitecoreItemType);
            configloader.Add(siteRootType);
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

        private string GetItemUrl(SitecoreDataMappingContext arg)
        {
            if (arg.Item == null)
            {
                return String.Empty;
            }

            return LinkManager.GetItemUrl(arg.Item);
        }

    }
}
