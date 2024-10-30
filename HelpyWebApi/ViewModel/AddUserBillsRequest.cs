namespace HelpyWebApi.ViewModel
{
    public class AddUserBillsRequest
    {
        public int UserId { get; set; }
        public List<int> SelectedItems { get; set; }
    }
}
