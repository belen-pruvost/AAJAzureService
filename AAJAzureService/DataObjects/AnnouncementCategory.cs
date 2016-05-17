using Microsoft.WindowsAzure.Mobile.Service;

namespace AAJAzureService.DataObjects
{
    public class AnnouncementCategory : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}