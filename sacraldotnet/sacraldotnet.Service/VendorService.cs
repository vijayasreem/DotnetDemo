using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Timers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading.Tasks;

public class VendorService : IVendorService
{
    private string connectionString;
    private string ftpServer;
    private string ftpUsername;
    private string ftpPassword;
    private string sharepointUrl;
    private string sharepointUsername;
    private string sharepointPassword;

    public VendorService()
    {
        connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
        ftpServer = ConfigurationManager.AppSettings["FTPServer"];
        ftpUsername = ConfigurationManager.AppSettings["FTPUsername"];
        ftpPassword = ConfigurationManager.AppSettings["FTPPassword"];
        sharepointUrl = ConfigurationManager.AppSettings["SharepointUrl"];
        sharepointUsername = ConfigurationManager.AppSettings["SharepointUsername"];
        sharepointPassword = ConfigurationManager.AppSettings["SharepointPassword"];
    }

    public async Task<List<Vendor>> FetchVendorsBySector(string sector)
    {
        List<Vendor> vendors = new List<Vendor>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Vendors WHERE Sector = @Sector", connection))
            {
                command.Parameters.AddWithValue("@Sector", sector);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        Vendor vendor = new Vendor
                        {
                            Invoice = reader.GetString(0),
                            Account = reader.GetString(1),
                            VendorAccount = reader.GetString(2),
                            VendorName = reader.GetString(3),
                            CustomerAccount = reader.GetString(4),
                            ClientName = reader.GetString(5),
                            AccountLocation = reader.GetString(6),
                            Country = reader.GetString(7),
                            Currency = reader.GetString(8),
                            PaymentMethod = reader.GetString(9),
                            PaymentTerms = reader.GetString(10),
                            SalesTaxGroup = reader.GetString(11),
                            CostCenter = reader.GetString(12),
                            MeterPointIdentificationNumber = reader.GetString(13),
                            PurchaseOrder = reader.GetString(14),
                            InvoiceId = reader.GetInt32(15)
                        };

                        vendors.Add(vendor);
                    }
                }
            }
        }

        return vendors;
    }

    private byte[] GeneratePdf(List<Vendor> vendors)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Add vendors data to the PDF document

            document.Close();
            return memoryStream.ToArray();
        }
    }

    private byte[] GenerateCsv(List<Vendor> vendors)
    {
        StringBuilder csvContent = new StringBuilder();

        // Generate CSV content from vendors data

        return Encoding.UTF8.GetBytes(csvContent.ToString());
    }

    private bool SendEmail(string recipient, string subject, string body, byte[] attachment)
    {
        try
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
                mailMessage.To.Add(recipient);
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                if (attachment != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream(attachment))
                    {
                        mailMessage.Attachments.Add(new Attachment(memoryStream, "VendorsData.pdf"));
                        // For CSV attachment, change the file name and content type accordingly
                    }
                }

                using (SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]))
                {
                    smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                    smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsername"], ConfigurationManager.AppSettings["SMTPPassword"]);
                    smtpClient.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);
                    smtpClient.Send(mailMessage);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send email: " + ex.Message);
            return false;
        }
    }

    private bool UploadToFtp(byte[] fileContent)
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                client.UploadData(ftpServer, "STOR", fileContent);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to upload file to FTP server: " + ex.Message);
            return false;
        }
    }

    private bool UploadToSharepoint(byte[] fileContent)
    {
        try
        {
            // Implement SharePoint upload code here using sharepointUrl, sharepointUsername, sharepointPassword

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to upload file to SharePoint server: " + ex.Message);
            return false;
        }
    }

    private async Task ProcessRequest()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (SqlCommand command = new SqlCommand("SELECT FileType, DestinationType FROM Requests", connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        string fileType = reader.GetString(0);
                        string destinationType = reader.GetString(1);

                        List<Vendor> vendors = await FetchVendorsBySector("SomeSector");
                        byte[] fileContent = null;

                        if (fileType == "PDF")
                        {
                            fileContent = GeneratePdf(vendors);
                        }
                        else if (fileType == "CSV")
                        {
                            fileContent = GenerateCsv(vendors);
                        }

                        if (destinationType == "EMAIL")
                        {
                            SendEmail("recipient@example.com", "Vendors Data", "Please find attached the vendors data.", fileContent);
                        }
                        else if (destinationType == "FTP")
                        {
                            UploadToFtp(fileContent);
                        }
                        else if (destinationType == "SHAREPOINT")
                        {
                            UploadToSharepoint(fileContent);
                        }
                    }
                }
            }
        }
    }

    public void StartSchedule()
    {
        Timer timer = new Timer();
        timer.Interval = 60000; // 1 minute interval
        timer.Elapsed += async (sender, e) => await FetchScheduleInformation();
        timer.Start();
    }

    private async Task FetchScheduleInformation()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (SqlCommand command = new SqlCommand("SELECT ScheduleDate, ScheduleTime FROM Schedule", connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        DateTime scheduleDateTime = reader.GetDateTime(0).Add(reader.GetTimeSpan(1));

                        if (scheduleDateTime <= DateTime.Now)
                        {
                            await ProcessRequest();
                        }
                    }
                }
            }
        }
    }
}

public class Vendor
{
    public string Invoice { get; set; }
    public string Account { get; set; }
    public string VendorAccount { get; set; }
    public string VendorName { get; set; }
    public string CustomerAccount { get; set; }
    public string ClientName { get; set; }
    public string AccountLocation { get; set; }
    public string Country { get; set; }
    public string Currency { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentTerms { get; set; }
    public string SalesTaxGroup { get; set; }
    public string CostCenter { get; set; }
    public string MeterPointIdentificationNumber { get; set; }
    public string PurchaseOrder { get; set; }
    public int InvoiceId { get; set; }
}