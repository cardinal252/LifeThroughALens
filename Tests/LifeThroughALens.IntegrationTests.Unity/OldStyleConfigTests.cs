using System;
using Cardinal.Glass.Extensions.Mapping.Sitecore;
using Cardinal.IoC;
using Cardinal.IoC.Unity;
using Glass.Mapper;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Fluent;
using LifeThroughALens.Domain;
using LifeThroughALens.IntegrationTests.Common;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace LifeThroughALens.IntegrationTests.Unity
{
    [TestFixture]
    public class OldStyleConfigTests : GlassTestBase
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            ContainerManager.Adapter.Register<AbstractDataMapper, SitecoreDelegateMapper>();

            SitecoreFluentConfigurationLoader configloader = new SitecoreFluentConfigurationLoader();
            
            SetupGlass(configloader);

            SetupGlassService(configloader);
        }

        [Test]
        public void CanGetSiteRootSuccessfully()
        {
            GetHomeItem();
        }

        protected void SetupGlass(SitecoreFluentConfigurationLoader configloader)
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

        protected override IContainerAdapter GetContainerAdapter()
        {
            IUnityContainer container = new UnityContainer();
            return new UnityContainerAdapter(Guid.NewGuid().ToString(), container);
        }
    }
}
