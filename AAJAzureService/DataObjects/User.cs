using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Mobile.Service;

namespace AAJAzureService.DataObjects
{
    public class User : EntityData
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public DateTime BirthDay { get; set; }


        public string ChatStatusId { get; set; }
        public virtual ChatStatus ChatStatus { get; set; }

        public string UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        
        public bool IsConfirmedMail { get; set; }

        public int CodeConfirmation { get; set; }
    }
}