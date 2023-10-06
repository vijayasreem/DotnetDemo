using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Npgsql;

namespace sacraldotnet
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private string connectionString;

        public InvoiceRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<List<InvoiceModel>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new NpgsqlCommand("SELECT * FROM invoices", connection);
                var reader = await command.ExecuteReaderAsync();

                var invoices = new List<InvoiceModel>();

                while (reader.Read())
                {
                    var invoice = new InvoiceModel()
                    {
                        Id = reader.GetInt32(0),
                        Account = reader.GetString(1),
                        VendorAccount = reader.GetString(2),
                        VendorName = reader.GetString(3),
                        CustomerAccount = reader.GetString(4),
                        ClientName = reader.GetString(5),
                        AccountLocationCountry = reader.GetString(6),
                        Currency = reader.GetString(7),
                        MethodOfPayment = reader.GetString(8),
                        TermsOfPayment = reader.GetString(9),
                        SalesTaxGroup = reader.GetString(10),
                        CostCenter = reader.GetString(11),
                        MeterPointIdentificationNumber = reader.GetString(12),
                        PurchaseOrder = reader.GetString(13),
                        InvoiceId = reader.GetString(14)
                    };

                    invoices.Add(invoice);
                }

                return invoices;
            }
        }

        public async Task<InvoiceModel> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new NpgsqlCommand("SELECT * FROM invoices WHERE id = @id", connection);
                command.Parameters.AddWithValue("id", id);

                var reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    var invoice = new InvoiceModel()
                    {
                        Id = reader.GetInt32(0),
                        Account = reader.GetString(1),
                        VendorAccount = reader.GetString(2),
                        VendorName = reader.GetString(3),
                        CustomerAccount = reader.GetString(4),
                        ClientName = reader.GetString(5),
                        AccountLocationCountry = reader.GetString(6),
                        Currency = reader.GetString(7),
                        MethodOfPayment = reader.GetString(8),
                        TermsOfPayment = reader.GetString(9),
                        SalesTaxGroup = reader.GetString(10),
                        CostCenter = reader.GetString(11),
                        MeterPointIdentificationNumber = reader.GetString(12),
                        PurchaseOrder = reader.GetString(13),
                        InvoiceId = reader.GetString(14)
                    };

                    return invoice;
                }

                return null;
            }
        }

        public async Task<int> CreateAsync(InvoiceModel invoice)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new NpgsqlCommand("INSERT INTO invoices (account, vendor_account, vendor_name, customer_account, client_name, account_location_country, currency, method_of_payment, terms_of_payment, sales_tax_group, cost_center, meter_point_identification_number, purchase_order, invoice_id) VALUES (@account, @vendor_account, @vendor_name, @customer_account, @client_name, @account_location_country, @currency, @method_of_payment, @terms_of_payment, @sales_tax_group, @cost_center, @meter_point_identification_number, @purchase_order, @invoice_id) RETURNING id", connection);
                command.Parameters.AddWithValue("account", invoice.Account);
                command.Parameters.AddWithValue("vendor_account", invoice.VendorAccount);
                command.Parameters.AddWithValue("vendor_name", invoice.VendorName);
                command.Parameters.AddWithValue("customer_account", invoice.CustomerAccount);
                command.Parameters.AddWithValue("client_name", invoice.ClientName);
                command.Parameters.AddWithValue("account_location_country", invoice.AccountLocationCountry);
                command.Parameters.AddWithValue("currency", invoice.Currency);
                command.Parameters.AddWithValue("method_of_payment", invoice.MethodOfPayment);
                command.Parameters.AddWithValue("terms_of_payment", invoice.TermsOfPayment);
                command.Parameters.AddWithValue("sales_tax_group", invoice.SalesTaxGroup);
                command.Parameters.AddWithValue("cost_center", invoice.CostCenter);
                command.Parameters.AddWithValue("meter_point_identification_number", invoice.MeterPointIdentificationNumber);
                command.Parameters.AddWithValue("purchase_order", invoice.PurchaseOrder);
                command.Parameters.AddWithValue("invoice_id", invoice.InvoiceId);

                var id = await command.ExecuteScalarAsync();

                return (int)id;
            }
        }

        public async Task UpdateAsync(InvoiceModel invoice)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new NpgsqlCommand("UPDATE invoices SET account = @account, vendor_account = @vendor_account, vendor_name = @vendor_name, customer_account = @customer_account, client_name = @client_name, account_location_country = @account_location_country, currency = @currency, method_of_payment = @method_of_payment, terms_of_payment = @terms_of_payment, sales_tax_group = @sales_tax_group, cost_center = @cost_center, meter_point_identification_number = @meter_point_identification_number, purchase_order = @purchase_order, invoice_id = @invoice_id WHERE id = @id", connection);
                command.Parameters.AddWithValue("id", invoice.Id);
                command.Parameters.AddWithValue("account", invoice.Account);
                command.Parameters.AddWithValue("vendor_account", invoice.VendorAccount);
                command.Parameters.AddWithValue("vendor_name", invoice.VendorName);
                command.Parameters.AddWithValue("customer_account", invoice.CustomerAccount);
                command.Parameters.AddWithValue("client_name", invoice.ClientName);
                command.Parameters.AddWithValue("account_location_country", invoice.AccountLocationCountry);
                command.Parameters.AddWithValue("currency", invoice.Currency);
                command.Parameters.AddWithValue("method_of_payment", invoice.MethodOfPayment);
                command.Parameters.AddWithValue("terms_of_payment", invoice.TermsOfPayment);
                command.Parameters.AddWithValue("sales_tax_group", invoice.SalesTaxGroup);
                command.Parameters.AddWithValue("cost_center", invoice.CostCenter);
                command.Parameters.AddWithValue("meter_point_identification_number", invoice.MeterPointIdentificationNumber);
                command.Parameters.AddWithValue("purchase_order", invoice.PurchaseOrder);
                command.Parameters.AddWithValue("invoice_id", invoice.InvoiceId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new NpgsqlCommand("DELETE FROM invoices WHERE id = @id", connection);
                command.Parameters.AddWithValue("id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<InvoiceModel>> FetchVendorsListBySector(string sector)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new NpgsqlCommand("SELECT * FROM invoices WHERE sector = @sector", connection);
                command.Parameters.AddWithValue("sector", sector);

                var reader = await command.ExecuteReaderAsync();

                var vendors = new List<InvoiceModel>();

                while (reader.Read())
                {
                    var vendor = new InvoiceModel()
                    {
                        Id = reader.GetInt32(0),
                        Account = reader.GetString(1),
                        VendorAccount = reader.GetString(2),
                        VendorName = reader.GetString(3),
                        CustomerAccount = reader.GetString(4),
                        ClientName = reader.GetString(5),
                        AccountLocationCountry = reader.GetString(6),
                        Currency = reader.GetString(7),
                        MethodOfPayment = reader.GetString(8),
                        TermsOfPayment = reader.GetString(9),
                        SalesTaxGroup = reader.GetString(10),
                        CostCenter = reader.GetString(11),
                        MeterPointIdentificationNumber = reader.GetString(12),
                        PurchaseOrder = reader.GetString(13),
                        InvoiceId = reader.GetString(14)
                    };

                    vendors.Add(vendor);
                }

                return vendors;
            }
        }

        public byte[] GeneratePdf(List<InvoiceModel> vendors)
        {
            var document = new Document();
            var stream = new MemoryStream();

            var writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            foreach (var vendor in vendors)
            {
                document.Add(new Paragraph($"Vendor: {vendor.VendorName}"));
                document.Add(new Paragraph($"Account: {vendor.Account}"));
                document.Add(new Paragraph($"Vendor Account: {vendor.VendorAccount}"));
                // Add more fields here

                document.Add(new Paragraph("------------------------------------------"));
            }

            document.Close();

            return stream.ToArray();
        }

        public byte[] GenerateCsv(List<InvoiceModel> vendors)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Invoice, Account, Vendor Account, Vendor Name, Customer Account, Client Name, Account Location Country, Currency, Method of Payment, Terms of Payment, Sales Tax Group, Cost Center, Meter Point Identification Number, Purchase Order, Invoice ID");

            foreach (var vendor in vendors)
            {
                sb.AppendLine($"{vendor.InvoiceId}, {vendor.Account}, {vendor.VendorAccount}, {vendor.VendorName}, {vendor.CustomerAccount}, {vendor.ClientName}, {vendor.AccountLocationCountry}, {vendor.Currency}, {vendor.MethodOfPayment}, {vendor.TermsOfPayment}, {vendor.SalesTaxGroup}, {vendor.CostCenter}, {vendor.MeterPointIdentificationNumber}, {vendor.PurchaseOrder}, {vendor.InvoiceId}");
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public async Task<bool> SendEmail(string recipient, string subject, string body, byte[] attachment)
        {
            var smtpClient = new SmtpClient();
            var mailMessage = new MailMessage();

            // Configure SMTP client and mail message

            mailMessage.Attachments.Add(new Attachment(new MemoryStream(attachment), "attachment.pdf"));

            await smtpClient.SendMailAsync(mailMessage);

            return true;
        }

        public async Task<bool> UploadToFtpServer(byte[] fileContent)
        {
            // Connect to FTP server and upload file

            return true;
        }

        public async Task<bool> UploadToSharepointServer(byte[] fileContent)
        {
            // Connect to SharePoint server and upload file

            return true;
        }

        public async Task ProcessRequest()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new NpgsqlCommand("SELECT * FROM requests", connection);
                var reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    var fileType = reader.GetString(0);
                    var destinationType = reader.GetString(1);

                    if (fileType == "PDF")
                    {
                        var vendors = await FetchVendorsListBySector("SomeSector");
                        var fileContent = GeneratePdf(vendors);

                        if (destinationType == "EMAIL")
                        {
                            var recipient = reader.GetString(2);
                            var subject = reader.GetString(3);
                            var body = reader.GetString(4);

                            await SendEmail(recipient, subject, body, fileContent);
                        }
                        else if (destinationType == "FTP")
                        {
                            await UploadToFtpServer(fileContent);
                        }
                        else if (destinationType == "SHAREPOINT")
                        {
                            await UploadToSharepointServer(fileContent);
                        }
                    }
                    else if (fileType == "CSV")
                    {
                        var vendors = await FetchVendorsListBySector("SomeSector");
                        var fileContent = GenerateCsv(vendors);

                        if (destinationType == "EMAIL")
                        {
                            var recipient = reader.GetString(2);
                            var subject = reader.GetString(3);
                            var body = reader.GetString(4);

                            await SendEmail(recipient, subject, body, fileContent);
                        }
                        else if (destinationType == "FTP")
                        {
                            await UploadToFtpServer(fileContent);
                        }
                        else if (destinationType == "SHAREPOINT")
                        {
                            await UploadToSharepointServer(fileContent);
                        }
                    }
                }
            }
        }

        public async Task FetchScheduleInformation()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new NpgsqlCommand("SELECT * FROM schedule", connection);
                var reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    var scheduleDateTime = reader.GetDateTime(0);
                    var currentDate = DateTime.Now;

                    if (scheduleDateTime <= currentDate)
                    {
                        await ProcessRequest();
                    }
                }
            }
        }
    }
}