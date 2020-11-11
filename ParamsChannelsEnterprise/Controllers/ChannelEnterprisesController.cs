using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ParamsChannelsEnterprise.Models;

namespace ParamsChannelsEnterprise.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ChannelEnterprisesController : ApiController
    {
        private AdministrationSwitchEntities db = new AdministrationSwitchEntities();

        // GET: api/ChannelEnterprises
        public async Task<HttpResponseMessage> GetChannelEnterprise()
        {
            try
            {
                List<ChannelEnterpriseInfo> channelEnterprise = new List<ChannelEnterpriseInfo>();

                await Task.Run(() =>
                { // no await here and function as a whole is not async
                    channelEnterprise = db.GetChannel(null).ToList();
                });

                return Request.CreateResponse(HttpStatusCode.OK, channelEnterprise);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/ChannelEnterprises/5
        [ResponseType(typeof(ChannelEnterprise))]
        public async Task<HttpResponseMessage> GetChannelEnterprise(string channel)
        {
            try
            {
                ChannelEnterpriseInfo channelEnterprise = new ChannelEnterpriseInfo();

                await Task.Run(() =>
                { // no await here and function as a whole is not async
                    channelEnterprise = db.GetChannel(channel).SingleOrDefault();
                });

                if (channelEnterprise == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, channelEnterprise);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT: api/ChannelEnterprises/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChannelEnterprise(int id, ChannelEnterprise channelEnterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != channelEnterprise.IDChannelEnterprise)
            {
                return BadRequest();
            }

            db.Entry(channelEnterprise).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChannelEnterpriseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ChannelEnterprises
        [ResponseType(typeof(ChannelEnterprise))]
        public async Task<IHttpActionResult> PostChannelEnterprise(ChannelEnterprise channelEnterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChannelEnterprise.Add(channelEnterprise);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = channelEnterprise.IDChannelEnterprise }, channelEnterprise);
        }

        // DELETE: api/ChannelEnterprises/5
        [ResponseType(typeof(ChannelEnterprise))]
        public async Task<IHttpActionResult> DeleteChannelEnterprise(int id)
        {
            ChannelEnterprise channelEnterprise = await db.ChannelEnterprise.FindAsync(id);
            if (channelEnterprise == null)
            {
                return NotFound();
            }

            db.ChannelEnterprise.Remove(channelEnterprise);
            await db.SaveChangesAsync();

            return Ok(channelEnterprise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChannelEnterpriseExists(int id)
        {
            return db.ChannelEnterprise.Count(e => e.IDChannelEnterprise == id) > 0;
        }
    }
}