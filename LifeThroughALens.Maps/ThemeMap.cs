using Cardinal.Glass.Extensions.Mapping.Sitecore;
using LifeThroughALens.Domain;

namespace LifeThroughALens.Maps
{
    public class ThemeMap : SitecoreGlassMap<ITheme>
    {
        public ThemeMap(ISitecoreTypeProvider sitecoreTypeProvider) : base(sitecoreTypeProvider)
        {
        }

        public override void Configure()
        {
            Map(x => x.Field(y => y.CssFile).FieldName("Css File"),
                x => ImportType<ISitecoreItem>());
        }
    }
}
