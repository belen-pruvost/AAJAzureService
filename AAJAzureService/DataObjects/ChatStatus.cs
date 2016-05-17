using Microsoft.WindowsAzure.Mobile.Service;

namespace AAJAzureService.DataObjects
{
    public class ChatStatus : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}