using System;
using System.Collections.Generic;
using System.Linq;
using Glass.Mapper.Sc;
using LifeThroughALens.Domain;

namespace LifeThroughALens
{
    public class JobsService
    {
        public JobsService(ISitecoreService sitecoreService, ILog log)
        {
            Service = sitecoreService;
            Log = log;
        }

        protected ISitecoreService Service { get; private set; }

        protected ILog Log { get; set; }

        public IEnumerable<IJob> GetLiveJobs()
        {
            try
            {
                var jobHub = Service.GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid());
                if (jobHub == null || jobHub.Jobs == null)
                {
                    return null;
                }

                return jobHub.Jobs.Where(job => job.DeadlineDateTime.CompareTo(DateTime.Today) > 0);;
            }
            catch (Exception ex)
            {
                Log.LogException("JobsService.GetLiveJobs() - An error occurred whilst attempting to retrieve the jobs", ex);
                return null;
            }
        }
    }
}
