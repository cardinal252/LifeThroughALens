using System;
using Cardinal.Glass.Extensions.Mapping.Sitecore;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration;
using LifeThroughALens.Domain;
using Sitecore.Links;

namespace LifeThroughALens.Maps
{
    public class SitecoreItemMap : SitecoreGlassMap<ISitecoreItem>
    {
        public SitecoreItemMap(ISitecoreTypeProvider sitecoreTypeProvider) : base(sitecoreTypeProvider)
        {
        }

        public override void Configure()
        {
            Map(x =>
            {
                x.Id(y => y.Id);
                x.Info(y => y.Name).InfoType(SitecoreInfoType.Name);
                x.Delegate(y => y.Url).GetValue(GetItemUrl);
            });
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
