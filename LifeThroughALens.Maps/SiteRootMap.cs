using Cardinal.Glass.Extensions.Mapping.Sitecore;
using LifeThroughALens.Domain;

namespace LifeThroughALens.Maps
{
    public class SiteRootMap : SitecoreGlassMap<ISiteRoot>
    {
        public SiteRootMap(ISitecoreTypeProvider sitecoreTypeProvider) : base(sitecoreTypeProvider)
        {
        }

        public override void Configure()
        {
            Map(
                x => ImportType<IBreadCrumb>(),
                x => ImportType<IPageTitleAndText>(),
                x => x.Field(y => y.EnableGoogleFonts).FieldName("Enable Google Fonts"),
                x => x.Field(y => y.GoogleFontType).FieldName("Google Font Type"),
                x => x.Field(y => y.PoweredByImage).FieldName("Powered By Image"),
                x => x.Field(y => y.PoweredByLink).FieldName("Powered By Link"),
                x => x.Field(y => y.SiteName).FieldName("Site Name"),
                x => x.Field(y => y.StrapLine).FieldName("Strap Line"));
        }
    }
}
