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
    public class VisibilityController : TableController<Visibility>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Visibility>(context, Request, Services);
        }

        // GET tables/Visibility
        public IQueryable<Visibility> GetAllVisibilities()
        {
            return Query();
        }

        // GET tables/Visibility/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Visibility> GetVisibility(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Visibility/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Visibility> PatchVisibility(string id, Delta<Visibility> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Visibility
        public async Task<IHttpActionResult> PostVisibility(Visibility item)
        {
            Visibility current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Visibility/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteVisibility(string id)
        {
            return DeleteAsync(id);
        }
    }
}