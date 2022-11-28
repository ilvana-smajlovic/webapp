USE Trackster
INSERT INTO dbo.Pictures([Name], [File])
SELECT 'MichaelPotts', 
	BulkColumn FROM OPENROWSET(BULK N'C:\Users\ilvana\Desktop\Webapp\dbBackup\Pictures\People\MichaelPotts.jpg', SINGLE_BLOB) image;