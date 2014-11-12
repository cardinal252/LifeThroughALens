using Cardinal.Glass.Extensions.Mapping.Sitecore;
using LifeThroughALens.Domain;

namespace LifeThroughALens.Maps
{
    public class BreadCrumbMap : SitecoreGlassMap<IBreadCrumb>
    {
        public BreadCrumbMap(ISitecoreTypeProvider sitecoreTypeProvider) : base(sitecoreTypeProvider)
        {
        }

        public override void Configure()
        {
            Map(
                x => x.Field(y => y.BreadcrumbTitle).FieldName("Breadcrumb Title"),
                x => ImportType<ISitecoreItem>());
        }
    }
}
