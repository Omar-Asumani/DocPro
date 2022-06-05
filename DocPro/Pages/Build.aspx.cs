using DocPro.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DocPro.Pages
{
    public partial class Build : System.Web.UI.Page
    {
        private void Initialize()
        {
            // Do page initializations
            TemplatesLabel.Text = "Document";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        protected void TemplatesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Int32.Parse(TemplatesDropDownList.SelectedItem.Value);
            var inputs = FilesManager.Generate(id);

            TextBox t1;
            Label l1;
            foreach(var input in inputs)
            {
                t1 = new TextBox();
                t1.Width = 481;
                t1.Style.Add("margin-top", "15px");
                t1.ID = "TextBox" + input.Key;

                l1 = new Label();
                l1.Style.Add("margin-top", "15px");
                l1.Width = 191;
                l1.ID = "Label" + input.Key;
                l1.Text = input.Value;

                TemplateInputsPanel.Controls.Add(l1);
                TemplateInputsPanel.Controls.Add(t1);

                TemplateInputsPanel.Wrap = true;
            }
        }

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
        }
    }
}