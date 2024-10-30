namespace HelpyWebApi.Models
{
    public class Follow
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowUserId { get; set; }
    }
}
