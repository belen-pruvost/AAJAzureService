using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using AAJAzureService.DataObjects;
using AAJAzureService.Models;
using AAJAzureService.Utilities;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using AAJAzureService.Filters;

namespace AAJAzureService.Controllers
{
    public class UserController : TableController<User>
    {
        public const string companyMail = "aajtech.com";
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
            var user = Query().Where(u => u.Email == item.Email).FirstOrDefault();
            if (user == null)
            {
                /* this validation has to be added to send code confirmation to people don't belongs to the company
                 string mail = item.Email.Split('@')[1].ToLower();
                 if (!mail.Equals(companyMail))
                 {
                     item.IsConfirmedMail = true;
                     item.CodeConfirmation=Common.GenerateRandomNo();
                 }*/
                item.IsConfirmedMail = true;
                item.CodeConfirmation = Common.GenerateRandomNo();
                User current = await InsertAsync(item);
                // if (item.IsConfirmedMail)
                //{
                MailsServices.SendCodeConfirmationEmail(current);
                // }
                return CreatedAtRoute("Tables", new { id = current.Id }, current);
            }
            else
            {
                string message = "Email already exists";

                var resp = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new StringContent(message),
                    ReasonPhrase = message
                };
                throw new HttpResponseException(resp);
            }
        }

        // DELETE tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUser(string id)
        {
            return DeleteAsync(id);
        }



    }
}