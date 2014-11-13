using System.Collections.Generic;
using Glass.Mapper.Sc.Fields;

namespace LifeThroughALens.Domain
{
    public class JobHub : SitecoreItem, IPageTitleAndText, IBreadCrumb
    {
        public IEnumerable<IJob> Jobs { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public Image SectionImage { get; set; }

        public string BreadcrumbTitle { get; set; }
    }
}
