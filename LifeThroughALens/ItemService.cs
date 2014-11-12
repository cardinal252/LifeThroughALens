using System;
using System.Collections.Generic;
using Glass.Mapper.Sc;
using LifeThroughALens.Domain;

namespace LifeThroughALens
{
    public class ItemService
    {
        public ItemService(ISitecoreService sitecoreService)
        {
            Service = sitecoreService;
        }

        protected ISitecoreService Service { get; private set; }

        public IEnumerable<IJob> GetJobs()
        {
            try
            {
                var jobHub = Service.GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid());
                return jobHub == null ? null : jobHub.Jobs;
            }
            catch (Exception ex)
            {
                // do something
                return null;
            }
        }
    }
}
