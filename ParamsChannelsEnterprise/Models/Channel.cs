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
    
    public partial class Channel
    {
        public long IDChannel { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Segmento { get; set; }
        public string PuntoEmision { get; set; }
        public Nullable<int> Ambiente { get; set; }
        public Nullable<int> Iva { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CategoriaCliente { get; set; }
        public string CuentaContable { get; set; }
        public Nullable<int> GrupoCredito { get; set; }
        public Nullable<int> DocumentoElectronico { get; set; }
        public Nullable<int> Relacionado { get; set; }
        public string VendedorSeccion { get; set; }
        public string ListaPrecioContado { get; set; }
        public string ListaPrecioCredito { get; set; }
        public Nullable<int> LimiteCredito { get; set; }
        public string Uge { get; set; }
        public string Bodega { get; set; }
        public string TipoFormaPago { get; set; }
        public string StatusChannel { get; set; }
        public string EnlaceInvoice { get; set; }
        public string EnlaceCreditNote { get; set; }
        public string EnlaceCotization { get; set; }
        public bool Status { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string AuthotizationNumber { get; set; }
        public Nullable<int> ProductItemGroupCode { get; set; }
        public string Declarable { get; set; }
    }
}
