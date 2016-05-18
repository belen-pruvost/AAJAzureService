﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using AAJAzureService.DataObjects;
using AAJAzureService.Models;
using AAJAzureService.Filters;

namespace AAJAzureService.Controllers
{
    public class AnnouncementController : TableController<Announcement>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Announcement>(context, Request, Services);
        }

        // GET tables/Announcement
        [QueryableExpand("Visibility,Category")]
        public IQueryable<Announcement> GetAllAnnouncements()
        {
            return Query();
        }

        // GET tables/Announcement/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [QueryableExpand("Visibility,Category")]
        public SingleResult<Announcement> GetAnnouncement(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Announcement/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Announcement> PatchAnnouncement(string id, Delta<Announcement> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Announcement
        public async Task<IHttpActionResult> PostAnnouncement(Announcement item)
        {
            Announcement current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Announcement/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAnnouncement(string id)
        {
            return DeleteAsync(id);
        }
    }
}