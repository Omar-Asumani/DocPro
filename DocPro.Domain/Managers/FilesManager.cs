using DocPro.Persistence.Exceptions;
using DocPro.Persistence.Repos;
using System.IO;
using System.Linq;
using Xceed.Words.NET;

namespace DocPro.Domain.Managers
{
    public class FilesManager
    {
        public static void Parse(Stream fileContent, string fileName, string path)
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
        } // Parse

        public static DTO.Document Generate(int documentID)
        {
            return DocumentsRepository.GetDocuments().FirstOrDefault(d => d.DocumentID == documentID);
        }

        public static DTO.Document Generate(string name)
        {
            return DocumentsRepository.GetDocuments().LastOrDefault(d => d.Name.Contains(name));
        }
    }
}
