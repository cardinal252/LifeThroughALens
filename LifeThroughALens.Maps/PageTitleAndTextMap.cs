using Cardinal.Glass.Extensions.Mapping.Sitecore;
using LifeThroughALens.Domain;

namespace LifeThroughALens.Maps
{
    public class PageTitleAndTextMap : SitecoreGlassMap<IPageTitleAndText>
    {
        public PageTitleAndTextMap(ISitecoreTypeProvider sitecoreTypeProvider) : base(sitecoreTypeProvider)
        {
        }

        public override void Configure()
        {
            Map(
                x => x.Field(y => y.Title).FieldName("Title"),
                x => x.Field(y => y.SectionImage).FieldName("Section Image"),
                x => ImportType<ISitecoreItem>());
        }
    }
}
