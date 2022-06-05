using DocPro.DTO;
using DocPro.Persistence.Exceptions;
using DocPro.Persistence.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xceed.Words.NET;

namespace DocPro.Domain.Managers
{
    public class FilesManager
    {
        public static void Parse(Stream fileContent, string fileName)
        {
            try
            {
                // Load the document.
                using(MemoryStream ms = new MemoryStream())
                {
                    using(DocX document = DocX.Load(fileContent))
                    {
                        // Replace text in this document.
                        document.ReplaceText("<<<задание>>>", "WTF");
                        // Save changes made to this document.
                        document.SaveAs(ms);
                        using(var reader = new BinaryReader(ms))
                        {
                            ms.Position = 0;
                            DocumentsRepository.AddDocument(new Persistence.Document
                            {
                                Binary = reader.ReadBytes((int)ms.Length),
                                Name = fileName,
                                DateCreated = System.DateTime.UtcNow
                            });
                        } // using
                    } // using
                } // using
            } // try
            catch(System.Exception ex)
            {
                throw new CustomException("Problem reading the file.");
            } // try-catch
        }

        public static IEnumerable<Document> GetAll()
        {
            return DocumentsRepository.GetDocuments().OrderByDescending(d => d.DocumentID);
        }

        public static Document Generate(string name)
        {
            return DocumentsRepository.GetDocuments().LastOrDefault(d => d.Name.Contains(name));
        }

        public static Dictionary<int, string> Generate(int documentID)
        {
            Document selectedDocument = DocumentsRepository.GetDocuments().FirstOrDefault(d => d.DocumentID == documentID);
            Dictionary<int, string> placeholders = new Dictionary<int, string>();
            try
            {
                // Load the document.
                using(MemoryStream ms = new MemoryStream(selectedDocument.Binary))
                {
                    using(DocX document = DocX.Load(ms))
                    {
                        string placeholderPattern = @"<<<(?<ID>\d)-(?<Caption>[\w\d ]+)>>>";
                        Regex placeholderRegex = new Regex(placeholderPattern);
                        foreach(var paragraph in document.Sections.SelectMany(s => s.SectionParagraphs))
                        {
                            foreach(Match itemMatch in placeholderRegex.Matches(paragraph.Text))
                            {
                                int placeholderID = 0;
                                try
                                {
                                    placeholders.Add(
                                        Int32.TryParse(itemMatch.Groups["ID"].ToString(), out placeholderID) ? placeholderID : 0,
                                        itemMatch.Groups["Caption"].ToString()
                                    );
                                }
                                catch(ArgumentException) { }
                            }
                        }
                    } // using
                } // using
            }
            catch(Exception)
            {
                throw new CustomException("Problem reading the file.");
            } // try-catch

            return placeholders;
        }

        //public static void Parse(Stream fileContent, string fileName, string path)
        //{
        //    try
        //    {
        //        // Load the document.
        //        using(MemoryStream ms = new MemoryStream())
        //        {
        //            using(DocX document = DocX.Load(fileContent))
        //            {
        //                // Replace text in this document.
        //                document.ReplaceText("<<<задание>>>", "WTF");
        //                // Save changes made to this document.
        //                document.SaveAs(ms);
        //                using(var reader = new BinaryReader(ms))
        //                {
        //                    ms.Position = 0;
        //                    DocumentsRepository.AddDocument(new Persistence.Document
        //                    {
        //                        Binary = reader.ReadBytes((int)ms.Length),
        //                        Name = fileName,
        //                        DateCreated = System.DateTime.UtcNow
        //                    });
        //                } // using
        //            } // using
        //        } // using
        //    } // try
        //    catch(System.Exception ex)
        //    {
        //        throw new CustomException("Problem reading the file.");
        //    } // try-catch
        //} // Parse
    }
}
