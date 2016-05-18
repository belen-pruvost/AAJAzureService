using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using AAJAzureService.DataObjects;
using AAJAzureService.Models;
using System.Net.Mail;
using AAJAzureService.Filters;

namespace AAJAzureService.Controllers
{
    public class UserController : TableController<User>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<User>(context, Request, Services);
        }

        // GET tables/User
        [QueryableExpand("ChatStatus,UserType")]
        public IQueryable<User> GetAllUsers()
        {
            //Send Email
            // Create a mail message
            var message = new MailMessage();

            // Add recipient
            message.To.Add(new MailAddress("martin.aybar@aajtech.com", "Martin Aybar"));
            message.To.Add(new MailAddress("ezequiel.echeveste@aajtech.com", "Ezequiel Echeveste"));

            // Set subject
            message.Subject = "Welcome to AAJ Mobile Service";

            // Set HTML body
            message.Body = "Your email test works great...!!!!";
            message.IsBodyHtml = true;

            // Create the smtp client
            var smtpClient = new SmtpClient();

            // Send the message
            smtpClient.Send(message);


            return Query();
        }

        // GET tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [QueryableExpand("ChatStatus,UserType")]
        public SingleResult<User> GetUser(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<User> PatchUser(string id, Delta<User> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/User
        public async Task<IHttpActionResult> PostUser(User item)
        {
            User current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUser(string id)
        {
            return DeleteAsync(id);
        }
    }
}