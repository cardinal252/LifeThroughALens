using Cardinal.Glass.Extensions.Mapping.Sitecore;
using LifeThroughALens.Domain;

namespace LifeThroughALens.Maps
{
    public class ConcreteThemeMap : SitecoreGlassMap<Theme>
    {
        public ConcreteThemeMap(ISitecoreTypeProvider sitecoreTypeProvider) : base(sitecoreTypeProvider)
        {
        }

        public override void Configure()
        {
            Map(x => ImportType<ITheme>());
        }
    }
}
