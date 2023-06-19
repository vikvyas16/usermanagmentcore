namespace DemoApplication.BusinessEntity
{
    public class Notification
    {
        public int Id { get; set; }
        public string NotificationMessage { get; set; }
        public int UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdated 
        {
            get
            {
                return CreatedDate.ToString("ss") + " seconds ago...";
            }
        }
    }
}