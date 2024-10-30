using HelpyWebApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace HelpyWebApi.ViewModel
{
    public class GetUserDetailsCiewModal
    {
        public Users Users { get; set; }
        public UserDetails UserDetails { get; set; }
        public Images Image { get; set; }
        public Subscriptions Subscription { get; set; }
    }
}
