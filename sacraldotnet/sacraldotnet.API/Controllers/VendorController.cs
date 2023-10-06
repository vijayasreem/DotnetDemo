
using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sacraldotnet.DTO;
using sacraldotnet.Service;

namespace sacraldotnet.API
{
    [ApiController]
    [Route("api/vendors")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        private readonly string _sharepointUrl;
        private readonly string _sharepointUsername;
        private readonly string _sharepointPassword;
        private readonly string _emailUsername;
        private readonly string _emailPassword;
        private readonly string _ftpHost;
        private readonly string _ftpUsername;
        private readonly string _ftpPassword;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
            _sharepointUrl = Configuration["SharepointUrl"];
            _sharepointUsername = Configuration["SharepointUsername"];
            _sharepointPassword = Configuration["SharepointPassword"];
            _emailUsername = Configuration["EmailUsername"];
            _emailPassword = Configuration["EmailPassword"];
            _ftpHost = Configuration["FtpHost"];
            _ftpUsername = Configuration["FtpUsername"];
            _ftpPassword = Configuration["FtpPassword"];
        }

        [HttpPost("fetch/{sector}")]
        public async Task<IActionResult> FetchVendorsBySector(string sector)
        {
            await _vendorService.FetchVendorsBySector(sector);
            return Ok();
        }

        [HttpGet("schedule")]
        public async Task<IActionResult> FetchScheduleInformation()
        {
            await _vendorService.FetchScheduleInformation();
            return Ok();
        }

        [HttpGet("start-timer")]
        public async Task<IActionResult> StartTimer()
        {
            await _vendorService.StartTimer();
            return Ok();
        }

        private async Task<bool> FetchDataFromDatabase()
        {
            // ADO.NET code to fetch data from the database
            // Use the fetched data to generate vendors list

            return true;
        }

        private byte[] GeneratePdfFile(VendorData vendorData)
        {
            // iTextSharp code to generate PDF file with vendor data
            // Return the PDF file content as byte array

            return null;
        }

        private byte[] GenerateCsvFile(VendorData vendorData)
        {
            // StringBuilder code to generate CSV file with vendor data
            // Return the CSV file content as byte array

            return null;
        }

        private async Task<bool> SendEmailWithAttachment(string recipient, byte[] attachmentContent, string attachmentName)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_emailUsername);
                mailMessage.To.Add(recipient);
                mailMessage.Subject = "Vendor Data";
                mailMessage.Body = "Please find the attached vendor data.";

                using (MemoryStream memoryStream = new MemoryStream(attachmentContent))
                {
                    mailMessage.Attachments.Add(new Attachment(memoryStream, attachmentName));

                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.Port = 587;
                        smtpClient.Credentials = new System.Net.NetworkCredential(_emailUsername, _emailPassword);
                        smtpClient.EnableSsl = true;
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Handle exception here
                return false;
            }
        }

        private async Task<bool> UploadFileToFtpServer(byte[] fileContent, string fileName)
        {
            try
            {
                using (FtpClient ftpClient = new FtpClient(_ftpHost, _ftpUsername, _ftpPassword))
                {
                    await ftpClient.UploadFileAsync(fileContent, fileName);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Handle exception here
                return false;
            }
        }

        private async Task<bool> UploadFileToSharepoint(byte[] fileContent, string fileName)
        {
            try
            {
                using (SharepointClient sharepointClient = new SharepointClient(_sharepointUrl, _sharepointUsername, _sharepointPassword))
                {
                    await sharepointClient.UploadFileAsync(fileContent, fileName);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Handle exception here
                return false;
            }
        }

        private async Task<VendorRequest> GetRequestFromDatabase()
        {
            // ADO.NET code to fetch request information from the database

            return null;
        }

        public async Task FetchScheduleInformation()
        {
            // ADO.NET code to fetch schedule information from the database

            // Check if schedule date and time crossed the current date and time
            // If yes, call the request fetch method
        }

        public void TimerCallback(object state)
        {
            FetchScheduleInformation().Wait();
        }
    }
}
