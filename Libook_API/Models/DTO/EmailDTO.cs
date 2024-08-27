namespace Libook_API.Models.DTO
{
    public class EmailDTO
    {
    }
    public class ConfirmEmailDTO
    {
        public string Username { get; set; }
        public string ConfirmationLink { get; set; }
    }
}
