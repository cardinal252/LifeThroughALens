using Cardinal.Glass.Extensions.Mapping.Sitecore;
using LifeThroughALens.Domain;

namespace LifeThroughALens.Maps
{
    public class JobHubMap : SitecoreGlassMap<JobHub>
    {
        public JobHubMap(ISitecoreTypeProvider sitecoreTypeProvider) : base(sitecoreTypeProvider)
        {
        }

        public override void Configure()
        {
            Map(
                x =>
                {
                    x.Children(y => y.Jobs);
                    ImportType<IBreadCrumb>();
                    ImportType<IPageTitleAndText>();
                });
        }
    }
}
