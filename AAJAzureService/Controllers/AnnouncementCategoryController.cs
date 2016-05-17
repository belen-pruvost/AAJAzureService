using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using AAJAzureService.DataObjects;
using AAJAzureService.Models;

namespace AAJAzureService.Controllers
{
    public class AnnouncementCategoryController : TableController<AnnouncementCategory>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<AnnouncementCategory>(context, Request, Services);
        }

        // GET tables/AnnouncementCategory
        public IQueryable<AnnouncementCategory> GetAllAnnouncementCategories()
        {
            return Query();
        }

        // GET tables/AnnouncementCategory/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<AnnouncementCategory> GetAnnouncementCategory(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/AnnouncementCategory/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<AnnouncementCategory> PatchAnnouncementCategory(string id, Delta<AnnouncementCategory> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/AnnouncementCategory
        public async Task<IHttpActionResult> PostAnnouncementCategory(AnnouncementCategory item)
        {
            AnnouncementCategory current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/AnnouncementCategory/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAnnouncementCategory(string id)
        {
            return DeleteAsync(id);
        }
    }
}