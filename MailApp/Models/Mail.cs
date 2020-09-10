using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MailApp.Models
{
    public class Mail
    {
        public int Id { get; set; }

        /// <summary>
        /// message subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// message text
        /// </summary>
        public string Body { get; set; }       
        
        /// <summary>
        /// creation date of a message
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// result of the sending, true = ok, false = failed
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// text of a sending error, empty if sent successfully
        /// </summary>
        public string FailedMessage { get; set; }

        /// <summary>
        /// list of recipients mails in string format
        /// </summary>
        [NotMapped]
        public ICollection<string> Recipients { get; set; }

        /// <summary>
        /// navigation property of this mail's recipients
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Recipient> RecipientList { get; set; }
    }
}
