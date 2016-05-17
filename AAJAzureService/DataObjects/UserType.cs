using Microsoft.WindowsAzure.Mobile.Service;

namespace AAJAzureService.DataObjects
{
    public class UserType : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}