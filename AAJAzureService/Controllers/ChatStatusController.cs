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
    public class ChatStatusController : TableController<ChatStatus>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ChatStatus>(context, Request, Services);
        }

        // GET tables/ChatStatus
        public IQueryable<ChatStatus> GetAllChatStatuses()
        {
            return Query();
        }

        // GET tables/ChatStatus/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ChatStatus> GetChatStatus(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ChatStatus/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ChatStatus> PatchChatStatus(string id, Delta<ChatStatus> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/ChatStatus
        public async Task<IHttpActionResult> PostChatStatus(ChatStatus item)
        {
            ChatStatus current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ChatStatus/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteChatStatus(string id)
        {
            return DeleteAsync(id);
        }
    }
}