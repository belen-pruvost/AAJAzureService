using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using AAJAzureService.DataObjects;
using AAJAzureService.Models;
using AAJAzureService.Filters;
using System.Collections.Generic;

namespace AAJAzureService.Controllers
{
    public class MessageController : TableController<Message>
    {
        private MobileServiceContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Message>(context, Request, Services);
        }

        // GET tables/Message
        [QueryableExpand("Sender,Receivers")]
        public IQueryable<Message> GetAllMessages()
        {
            return Query();
        }

        // GET tables/Message/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [QueryableExpand("Sender,Receivers")]
        public SingleResult<Message> GetMessage(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Message/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Message> PatchMessage(string id, Delta<Message> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Message
        public async Task<IHttpActionResult> PostMessage(Message item)
        {
            List<User> receivers = item.Receivers.ToList();
            List<User> newReceivers = new List<User>();
        
            for (int i = item.Receivers.Count; i > 0;  i--)
            {
                string id = receivers[i-1].Id;
                User existingUser = context.Users.Where(u => u.Id == id).FirstOrDefault();
                if (existingUser != null)
                {
                    item.Receivers.Remove(receivers[i-1]);
                    newReceivers.Add(existingUser);
                }
            }
            foreach (User newUser in newReceivers)
            {
                item.Receivers.Add(newUser);
            }
            Message current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Message/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMessage(string id)
        {
            return DeleteAsync(id);
        }
        
    }
}