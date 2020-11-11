using ParamsChannelsEnterprise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ParamsChannelsEnterprise.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ManagementSequentialController : ApiController
    {
        private SwitchAtiscodeEntities db = new SwitchAtiscodeEntities();

        // GET: api/ManagementSequential
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                List<string> typeDocuments = new List<string>();
                Parameters parameter = new Parameters();

                await Task.Run(() =>
                { // no await here and function as a whole is not async
                    parameter = db.Parameters.SingleOrDefault(s => s.Code == "TYPESDOCUMENTS");
                });

                typeDocuments = parameter.Value.Split(',').ToList();

                var dict = typeDocuments.Select((s, i) => new DocumentsTypes { Id = Guid.NewGuid().ToString(), Name = s }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, dict);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/ManagementSequential/5
        public async Task<HttpResponseMessage> Get(string channel, string typeDocument, string sequential)
        {
            try
            {
                string documentNumber = string.Empty;
                await Task.Run(() =>
                { // no await here and function as a whole is not async
                    var infoDocument = db.AtisLogTran.FirstOrDefault(s => s.Canal.Contains(channel) && s.Tipo.Contains(typeDocument) && s.Secuencial.Contains(sequential) && !string.IsNullOrEmpty(s.NumeroDocumento));

                    if (infoDocument != null)
                    {
                        documentNumber = infoDocument.NumeroDocumento;
                        db.LiberarSecuencial(channel, sequential, typeDocument);
                    }
                });

                return Request.CreateResponse(HttpStatusCode.OK, new AtisLogTran { Canal = channel, Secuencial = sequential, Tipo = typeDocument, NumeroDocumento = documentNumber });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/ChannelEnterprises
        public async Task<HttpResponseMessage> Post(ManageSequential manage)
        {
            try
            {
                string documentNumber = string.Empty;
                HttpResponseMessage response = new HttpResponseMessage();
                await Task.Run(() =>
                { // no await here and function as a whole is not async
                    var infoDocument = db.AtisLogTran.FirstOrDefault(s => s.Canal.Contains(manage.Channel) && s.Tipo.Contains(manage.TypeDocument) && s.Secuencial.Contains(manage.Sequential) && !string.IsNullOrEmpty(s.NumeroDocumento));

                    if (infoDocument != null)
                    {
                        documentNumber = infoDocument.NumeroDocumento;
                        db.LiberarSecuencial(manage.Channel, manage.Sequential, manage.TypeDocument);
                        response = Request.CreateResponse(HttpStatusCode.OK, new AtisLogTran { Canal = manage.Channel, Secuencial = manage.Sequential, Tipo = manage.TypeDocument, NumeroDocumento = documentNumber });
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound, new AtisLogTran { Canal = manage.Channel, Secuencial = manage.Sequential, Tipo = manage.TypeDocument, NumeroDocumento = documentNumber });
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
