
namespace sacraldotnet
{
    public class ReportConfigurationModel
    {
        public int Id { get; set; }
        public string FileType { get; set; }
        public string Destination { get; set; }
        public string Frequency { get; set; }
    }

    public class VendorModel
    {
        public int Id { get; set; }
        public string Sector { get; set; }
    }

    public class ReportFileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }

    public class EmailAttachmentModel
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public ReportFileModel AttachedFile { get; set; }
    }

    public class FtpUploadModel
    {
        public int Id { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ReportFileModel FileToUpload { get; set; }
    }

    public class SharepointUploadModel
    {
        public int Id { get; set; }
        public string SiteUrl { get; set; }
        public string LibraryName { get; set; }
        public ReportFileModel FileToUpload { get; set; }
    }

    public class RequestModel
    {
        public int Id { get; set; }
        public string Information { get; set; }
        public ReportFileModel GeneratedFile { get; set; }
    }

    public class ScheduleModel
    {
        public int Id { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public RequestModel Request { get; set; }
    }

    public class TimerModel
    {
        public int Id { get; set; }
        public int Interval { get; set; }
        public ScheduleModel Schedule { get; set; }
    }
}
