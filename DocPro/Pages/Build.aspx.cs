﻿namespace DocPro.Pages
{
    using DocPro.Domain.Managers;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class Build : System.Web.UI.Page
    {
        private const string DOCX_MIME_TYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        private void Initialize()
        {
            // Do page initializations
            TemplatesLabel.Text = "Document";
        } // Initialize

        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
        } // Page_Load

        protected void TemplatesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                int id = Int32.Parse(TemplatesDropDownList.SelectedItem.Value);
                var inputs = FilesManager.Generate(id);

                foreach(var input in inputs)
                {
                    CreateTextBox(input);
                } // foreach
                TemplateInputsPanel.Wrap = true;
            }
        } // TemplatesDropDownList_SelectedIndexChanged

        private void CreateTextBox(KeyValuePair<int, string> input)
        {
            TextBox t1 = new TextBox();
            t1.Width = 481;
            t1.Style.Add("margin-top", "15px");
            t1.ID = "DynamicTextBox" + input.Key;

            Label l1 = new Label();
            l1.Style.Add("margin-top", "15px");
            l1.Width = 191;
            l1.ID = "DynamicLabel" + input.Key;
            l1.Text = input.Value;

            TemplateInputsPanel.Controls.Add(l1);
            TemplateInputsPanel.Controls.Add(t1);
        } // CreateTextBox

        public IEnumerable<DTO.Document> PopulateTemplatesDropDownList()
        {
            List<DTO.Document> templates = new List<DTO.Document>
            {
                new DTO.Document
                {
                    DocumentID = 0,
                    Name = "Select template.."
                }
            };
            templates.AddRange(FilesManager.GetAll());

            return templates;
        } // PopulateTemplatesDropDownList

        protected void GenerateBtn_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(TemplatesDropDownList.SelectedItem.Value);
            var inputs = FilesManager.Generate(id);

            Dictionary<string, string> replaceValues = new Dictionary<string, string>();
            int counter = 0;
            foreach(var input in inputs)
            {
                try
                {
                    replaceValues.Add($"<<<{input.Key}-{input.Value}>>>", GetTextBoxValue("DynamicTextBox" + input.Key));
                    counter++;
                }
                catch(Exception ex) { } // try-catch
            } // foreach

            if(inputs.Count == counter)
            {
                DownloadToBrowser(FilesManager.GetProcessed(id, replaceValues));
            } // if
        } // GenerateBtn_Click

        private void DownloadToBrowser(Tuple<string, byte[]> file)
        {
            string fileName = file.Item1;
            byte[] binary = file.Item2;
            Response.ContentType = DOCX_MIME_TYPE;
            Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", fileName));

            Response.BufferOutput = false;

            Response.OutputStream.Write(binary, 0, binary.Length);

            #region option2-physical
            //Response.TransmitFile(Server.MapPath(@"~/Задание_за_изработка - Copy.docx")); 
            #endregion

            Response.Flush();
            Response.End();
        } // DownloadToBrowser

        private string GetTextBoxValue(string textBoxID)
        {
            string[] ctrls = Request.Form.ToString().Split('&');

            for(int i = 0; i < ctrls.Length; i++)
            {
                if(ctrls[i].Contains(textBoxID)

                    && !ctrls[i].Contains("EVENTTARGET"))
                {
                    string textBoxValue = HttpUtility.UrlDecode(ctrls[i].Split('=')[1], Encoding.UTF8);

                    return textBoxValue;
                } // if
            } // for

            return string.Empty;
        } // GetTextBoxValue
    } // class Build
} // namespace