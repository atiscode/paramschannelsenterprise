//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParamsChannelsEnterprise.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AtisLogTran
    {
        public long IdLogTran { get; set; }
        public string NumeroDocumento { get; set; }
        public string TramaEntrada { get; set; }
        public Nullable<System.DateTime> FechaEntrada { get; set; }
        public string Estado { get; set; }
        public string TramaRespuesta { get; set; }
        public Nullable<System.DateTime> FechaSalida { get; set; }
        public string Tipo { get; set; }
        public string Secuencial { get; set; }
        public string Canal { get; set; }
        public Nullable<int> TipoSolicitud { get; set; }
    }
}
