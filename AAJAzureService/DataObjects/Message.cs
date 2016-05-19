using Microsoft.WindowsAzure.Mobile.Service;
using System.Collections.Generic;

namespace AAJAzureService.DataObjects
{
    public class Message : EntityData
    {
        public string Text { get; set; }

        public string SenderId { get; set; }
        public virtual User Sender { get; set; }

        public virtual ICollection<User> Receivers { get; set; }
    }
}