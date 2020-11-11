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
using System.Web.Http.Description;
using ParamsChannelsEnterprise.Models;

namespace ParamsChannelsEnterprise.Controllers
{
    public class QueryManagerController : ApiController
    {
        private AdministrationSwitchEntities db = new AdministrationSwitchEntities();

        // GET: api/QueryManager
        public IQueryable<QueryManager> GetQueryManager()
        {
            return db.QueryManager;
        }

        // GET: api/ChannelEnterprises/5
        [ResponseType(typeof(ChannelEnterprise))]
        public async Task<HttpResponseMessage> GetQueryManager(string code)
        {
            try
            {
                QueryInfo query = new QueryInfo();

                await Task.Run(() =>
                { // no await here and function as a whole is not async
                    query = db.GetQuery(code).SingleOrDefault();
                });

                if (query == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, query);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT: api/QueryManager/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQueryManager(int id, QueryManager queryManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != queryManager.IDQueryManager)
            {
                return BadRequest();
            }

            db.Entry(queryManager).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueryManagerExists(id))
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

        // POST: api/QueryManager
        [ResponseType(typeof(QueryManager))]
        public async Task<IHttpActionResult> PostQueryManager(QueryManager queryManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.QueryManager.Add(queryManager);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = queryManager.IDQueryManager }, queryManager);
        }

        // DELETE: api/QueryManager/5
        [ResponseType(typeof(QueryManager))]
        public async Task<IHttpActionResult> DeleteQueryManager(int id)
        {
            QueryManager queryManager = await db.QueryManager.FindAsync(id);
            if (queryManager == null)
            {
                return NotFound();
            }

            db.QueryManager.Remove(queryManager);
            await db.SaveChangesAsync();

            return Ok(queryManager);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QueryManagerExists(int id)
        {
            return db.QueryManager.Count(e => e.IDQueryManager == id) > 0;
        }
    }
}