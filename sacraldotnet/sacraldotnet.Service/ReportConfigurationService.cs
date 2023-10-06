using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ReportService
{
    public class ReportConfigurationService : IReportConfigurationService
    {
        private readonly string emailHost;
        private readonly int emailPort;
        private readonly string emailUsername;
        private readonly string emailPassword;
        private readonly string ftpServer;
        private readonly string ftpUsername;
        private readonly string ftpPassword;
        private readonly string sharepointUrl;

        public ReportConfigurationService(string emailHost, int emailPort, string emailUsername, string emailPassword, string ftpServer, string ftpUsername, string ftpPassword, string sharepointUrl)
        {
            this.emailHost = emailHost;
            this.emailPort = emailPort;
            this.emailUsername = emailUsername;
            this.emailPassword = emailPassword;
            this.ftpServer = ftpServer;
            this.ftpUsername = ftpUsername;
            this.ftpPassword = ftpPassword;
            this.sharepointUrl = sharepointUrl;
        }

        public async Task ConfigureReport(string fileType, string destination, string frequency)
        {
            var vendors = await FetchVendorsList();

            byte[] fileBytes = fileType.ToLower() switch
            {
                "pdf" => GeneratePdfFile(vendors),
                "csv" => GenerateCsvFile(vendors),
                _ => throw new ArgumentException("Invalid file type.")
            };

            if (destination.ToLower() == "email")
            {
                await SendEmail(fileBytes, frequency);
            }
            else if (destination.ToLower() == "ftp")
            {
                await UploadToFtp(fileBytes);
            }
            else if (destination.ToLower() == "sharepoint")
            {
                await UploadToSharepoint(fileBytes);
            }
            else
            {
                throw new ArgumentException("Invalid destination.");
            }
        }

        private async Task<Vendor[]> FetchVendorsList()
        {
            throw new NotImplementedException();
        }

        private byte[] GeneratePdfFile(Vendor[] vendors)
        {
            using (var document = new Document())
            {
                throw new NotImplementedException();
            }
        }

        private byte[] GenerateCsvFile(Vendor[] vendors)
        {
            var csvData = new StringBuilder();

            byte[] csvBytes = Encoding.UTF8.GetBytes(csvData.ToString());
            return csvBytes;
        }

        private async Task SendEmail(byte[] fileBytes, string frequency)
        {
            using (var client = new SmtpClient(emailHost, emailPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailUsername, emailPassword);
                client.EnableSsl = true;

                var attachment = new Attachment(new MemoryStream(fileBytes), "Report." + GetFileTypeExtension());

                await client.SendMailAsync(emailMessage);
            }
        }

        private async Task UploadToFtp(byte[] fileBytes)
        {
            throw new NotImplementedException();
        }

        private async Task UploadToSharepoint(byte[] fileBytes)
        {
            throw new NotImplementedException();
        }

        private string GetFileTypeExtension()
        {
            throw new NotImplementedException();
        }
    }

    public class Vendor
    {
        
    }
}