
using Adsite.Data;

namespace Adsite.Web.Models
{
    public class IndexViewModel
    {
        public List<Ad> Ads { get; set; }
        public List<int> MyAds { get; set; } = new();
    }
}
