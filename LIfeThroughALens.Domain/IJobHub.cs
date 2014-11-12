using System.Collections.Generic;

namespace LifeThroughALens.Domain
{
    public class JobHub : SitecoreItem
    {
        public IEnumerable<IJob> Jobs { get; set; } 
    }
}
