using Microsoft.WindowsAzure.Mobile.Service;

namespace AAJAzureService.DataObjects
{
    public class Announcement : EntityData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int LikesCount { get; set; }

        public string VisibilityId { get; set; }
        public virtual Visibility Visibility { get; set; }

        public string AnnouncementCategoryId { get; set; }
        public virtual AnnouncementCategory Category { get; set; }
        
    }
}