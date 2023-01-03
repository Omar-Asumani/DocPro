namespace DocPro.Pages
{
    using DocPro.Domain.Managers;
    using System;

    public partial class Import : System.Web.UI.Page
    {

        private const string DOCX_MIME_TYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        private void Initialize()
        {
            // Do page initializations
            Title = "Select template to upload";
            ErrorLabel.Text = string.Empty;
        } // Initialize

        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
        } // Page_Load

        protected void SubmitTemplateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.Stream fileStream = FileUpload1.FileContent;
                FilesManager.Parse(fileStream, FileUpload1.FileName);
                Response.Redirect("Build.aspx");
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = ex.ToString();
            } // try-catch
        } // SubmitTemplateBtn_Click

        private void DownloadToBrowser(byte[] binary)
        {
            Response.ContentType = DOCX_MIME_TYPE;
            Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=DocPro - {0}-utc.docx", DateTime.UtcNow.ToString()));

            Response.BufferOutput = false;

            Response.OutputStream.Write(binary, 0, binary.Length);

            #region option2-physical
            //Response.TransmitFile(Server.MapPath(@"~/Задание_за_изработка - Copy.docx")); 
            #endregion

            Response.Flush();
            Response.End();
        } // DownloadToBrowser
    } // class Import
} // namespace