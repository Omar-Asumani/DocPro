using DocPro.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocPro.Persistence.Repos
{
    public class DocumentsRepository
    {
        public static List<DTO.Document> GetDocuments()
        {
            DocProEntities db = new DocProEntities();
            List<Document> dbDocuments = db.Documents.OrderBy(c => c.Name).ToList();
            List<DTO.Document> dtoDocuments = new List<DTO.Document>();
            foreach(Document item in dbDocuments)
            {
                var Document = new DTO.Document();
                Document.DocumentID = item.DocumentID;
                Document.Name = item.Name;
                Document.Binary = item.Binary;
                Document.DateCreated = item.DateCreated;
                Document.KindID = item.KindID;
                dtoDocuments.Add(Document);
            }

            return dtoDocuments;
        }

        public static void AddDocument(Document newDocument)
        {
            DocProEntities db = new DocProEntities();
            if(String.IsNullOrWhiteSpace(newDocument.Name))
            {
                throw new CustomException("Document requires Name.");
            }
            if(newDocument.Binary == null || newDocument.Binary.Length == 0)
            {
                throw new CustomException("Document requires Binary.");
            }
            if(newDocument.DateCreated.Year < 2021)
            {
                throw new CustomException("Date created can not be before 2021.");
            }
            //if(DoesDocumentExist(newDocument, db))
            //{
            //    throw new CustomException("Document already exists.");
            //}

            var document = new Document();
            document.Name = newDocument.Name;
            document.KindID = newDocument.KindID;
            document.DateCreated = newDocument.DateCreated;
            document.Binary = newDocument.Binary;
            db.Documents.Add(document);
            db.SaveChanges();
        }

        private static bool DoesDocumentExist(Document newDocument, DocProEntities db)
        {
            return db.Documents.Any(c => c.Name == newDocument.Name);
        }
    }
}
