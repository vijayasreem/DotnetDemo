
using System;

namespace sacraldotnet
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string VendorAccount { get; set; }
        public string VendorName { get; set; }
        public string CustomerAccount { get; set; }
        public string ClientName { get; set; }
        public string AccountLocationCountry { get; set; }
        public string Currency { get; set; }
        public string MethodOfPayment { get; set; }
        public string TermsOfPayment { get; set; }
        public string SalesTaxGroup { get; set; }
        public string CostCenter { get; set; }
        public string MeterPointIdentificationNumber { get; set; }
        public string PurchaseOrder { get; set; }
        public string InvoiceId { get; set; }
    }
}
