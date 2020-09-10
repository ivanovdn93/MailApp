namespace MailApp.Models
{
    public class Recipient
    {
        public int Id { get; set; }

        /// <summary>
        /// recipient's email
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// navigation property for this recipient's mail id
        /// </summary>
        public int MailId { get; set; }

        /// <summary>
        /// navigation property for this recipient's mail
        /// </summary>
        public virtual Mail Mail { get; set; }        
    }
}
