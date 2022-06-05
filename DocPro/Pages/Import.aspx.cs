using DocPro.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Navigation;

namespace DocPro.Pages
{
    public partial class Import : System.Web.UI.Page
    {

        private const string DOCX_MIME_TYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        private void Initialize()
        {
            // Do page initializations
            Title = "Select template to upload";
            ErrorLabel.Text = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            //{
            Initialize();
            //}
        }

        protected void SubmitTemplateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.Stream fileStream = FileUpload1.FileContent;
                //string path = Server.MapPath(@"~/Задание_за_изработка - Copy.docx");
                //FilesManager.Parse(fileStream, "DocPro", path);
                FilesManager.Parse(fileStream, "DocPro");
                //DownloadToBrowser(FilesManager.Generate("DocPro").Binary);
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = ex.Message;
            }
        }

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
        }

        //private void DownloadToBrowser(System.IO.Stream fileStream)
        //{
        //    Response.ContentType = DOCX_MIME_TYPE;
        //    Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=DocPro - {0}-utc.docx", DateTime.UtcNow.ToString()));

        //    #region option1-filestream
        //    Response.BufferOutput = false;

        //    Int16 bufferSize = 1024;
        //    byte[] buffer = new byte[bufferSize + 1];
        //    fileStream.Position = 0;
        //    int count = fileStream.Read(buffer, 0, bufferSize);

        //    while(count > 0)
        //    {
        //        Response.OutputStream.Write(buffer, 0, count);
        //        count = fileStream.Read(buffer, 0, bufferSize);
        //    }
        //    #endregion

        //    #region option2-physical
        //    //Response.TransmitFile(Server.MapPath(@"~/Задание_за_изработка - Copy.docx")); 
        //    #endregion

        //    Response.Flush();
        //    Response.End();
        //}
    }
}