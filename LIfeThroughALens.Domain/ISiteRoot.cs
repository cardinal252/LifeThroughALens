using Glass.Mapper.Sc.Fields;

namespace LifeThroughALens.Domain
{
    public interface ISiteRoot : IBreadCrumb, IPageTitleAndText
    {
        string SiteName { get; set; }

        Image Logo { get; set; }

        Image PoweredByImage { get; set; }

        Link PoweredByLink { get; set; }

        bool EnableGoogleFonts { get; set; }

        string GoogleFontType { get; set; }

        string StrapLine { get; set; }

        Theme Theme { get; set; }
    }
}
