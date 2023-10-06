using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using sacraldotnet.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace sacraldotnet
{
    public class VendorRepository : IVendorService
    {
        private readonly string connectionString;

        public VendorRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;
        }

        public async Task<List<VendorModel>> GetAllVendorsAsync()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("SELECT * FROM vendors", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var vendors = new List<VendorModel>();

                        while (await reader.ReadAsync())
                        {
                            var vendor = new VendorModel
                            {
                                Id = reader.GetInt32(0),
                                Invoice = reader.GetString(1),
                                Account = reader.GetString(2),
                                VendorAccount = reader.GetString(3),
                                VendorName = reader.GetString(4),
                                CustomerAccount = reader.GetString(5),
                                ClientName = reader.GetString(6),
                                AccountLocationCountry = reader.GetString(7),
                                Currency = reader.GetString(8),
                                MethodOfPayment = reader.GetString(9),
                                TermsOfPayment = reader.GetString(10),
                                SalesTaxGroup = reader.GetString(11),
                                CostCenter = reader.GetString(12),
                                MeterPointIdentificationNumber = reader.GetString(13),
                                PurchaseOrder = reader.GetString(14),
                                InvoiceId = reader.GetInt32(15)
                            };

                            vendors.Add(vendor);
                        }

                        return vendors;
                    }
                }
            }
        }

        private async Task<List<VendorModel>> GetVendorsBySectorAsync(string sector)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("SELECT * FROM vendors WHERE sector = @sector", connection))
                {
                    command.Parameters.AddWithValue("@sector", sector);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var vendors = new List<VendorModel>();

                        while (await reader.ReadAsync())
                        {
                            var vendor = new VendorModel
                            {
                                Id = reader.GetInt32(0),
                                Invoice = reader.GetString(1),
                                Account = reader.GetString(2),
                                VendorAccount = reader.GetString(3),
                                VendorName = reader.GetString(4),
                                CustomerAccount = reader.GetString(5),
                                ClientName = reader.GetString(6),
                                AccountLocationCountry = reader.GetString(7),
                                Currency = reader.GetString(8),
                                MethodOfPayment = reader.GetString(9),
                                TermsOfPayment = reader.GetString(10),
                                SalesTaxGroup = reader.GetString(11),
                                CostCenter = reader.GetString(12),
                                MeterPointIdentificationNumber = reader.GetString(13),
                                PurchaseOrder = reader.GetString(14),
                                InvoiceId = reader.GetInt32(15)
                            };

                            vendors.Add(vendor);
                        }

                        return vendors;
                    }
                }
            }
        }

        public async Task<byte[]> GeneratePdfAsync(List<VendorModel> vendors)
        {
            var document = new Document();

            using (var memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Add vendors data to the document

                document.Close();
                return memoryStream.ToArray();
            }
        }

        private async Task<string> GenerateCsvAsync(List<VendorModel> vendors)
        {
            var csv = new StringBuilder();

            // Generate CSV content

            return csv.ToString();
        }

        private async Task<bool> SendEmailAsync(string recipient, string subject, string body, byte[] attachment)
        {
            var smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            var smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            var smtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
            var smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add(recipient);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    // Add attachment

                    client.Send(mailMessage);
                }
            }

            return true;
        }

        private async Task<bool> UploadToFtpServerAsync(byte[] fileContent)
        {
            var ftpServer = ConfigurationManager.AppSettings["FtpServer"];
            var ftpUsername = ConfigurationManager.AppSettings["FtpUsername"];
            var ftpPassword = ConfigurationManager.AppSettings["FtpPassword"];

            // Upload file content to FTP server

            return true;
        }

        private async Task<bool> UploadToSharepointServerAsync(byte[] fileContent)
        {
            var sharepointUrl = ConfigurationManager.AppSettings["SharepointUrl"];
            var sharepointUsername = ConfigurationManager.AppSettings["SharepointUsername"];
            var sharepointPassword = ConfigurationManager.AppSettings["SharepointPassword"];

            // Upload file content to Sharepoint server

            return true;
        }

        private async Task ProcessRequestAsync(int requestId)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("SELECT * FROM requests WHERE id = @requestId", connection))
                {
                    command.Parameters.AddWithValue("@requestId", requestId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var fileType = reader.GetString(1);
                            var destinationType = reader.GetString(2);

                            List<VendorModel> vendors;

                            if (destinationType == "EMAIL")
                            {
                                vendors = await GetVendorsBySectorAsync(reader.GetString(3));
                                var pdfContent = await GeneratePdfAsync(vendors);
                                await SendEmailAsync(reader.GetString(4), "Vendors Report", "Please find attached the vendors report.", pdfContent);
                            }
                            else if (destinationType == "FTP")
                            {
                                vendors = await GetVendorsBySectorAsync(reader.GetString(3));
                                var csvContent = await GenerateCsvAsync(vendors);
                                await UploadToFtpServerAsync(Encoding.UTF8.GetBytes(csvContent));
                            }
                            else if (destinationType == "SHAREPOINT")
                            {
                                vendors = await GetVendorsBySectorAsync(reader.GetString(3));
                                var pdfContent = await GeneratePdfAsync(vendors);
                                await UploadToSharepointServerAsync(pdfContent);
                            }
                        }
                    }
                }
            }
        }

        public async Task FetchScheduleInformationAsync()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("SELECT * FROM schedules", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var scheduleDateTime = reader.GetDateTime(1);

                            if (scheduleDateTime <= DateTime.Now)
                            {
                                var requestId = reader.GetInt32(0);
                                await ProcessRequestAsync(requestId);
                            }
                        }
                    }
                }
            }
        }
    }
}