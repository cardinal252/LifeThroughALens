using Cardinal.Glass.Extensions.Mapping.Sitecore;
using LifeThroughALens.Domain;

namespace LifeThroughALens.Maps
{
    public class JobMap : SitecoreGlassMap<IJob>
    {
        public JobMap(ISitecoreTypeProvider sitecoreTypeProvider) : base(sitecoreTypeProvider)
        {
        }

        public override void Configure()
        {
            Map(x =>
            {
                ImportType<ISitecoreItem>();
                x.Field(y => y.Contact).FieldName("Contact");
                x.Field(y => y.DeadlineDateTime).FieldName("Deadline Date");
            });
        }
    }
}
