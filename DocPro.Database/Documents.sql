CREATE TABLE [dbo].[Documents]
(
	[DocumentID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Binary] VARBINARY(MAX) NOT NULL, 
    [KindID] INT NULL, 
    [DateCreated] DATETIME NOT NULL
)
