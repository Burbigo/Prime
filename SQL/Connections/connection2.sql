use Prime
/*
   9 травня 2016 р.18:18:36
   User: 
   Server: Я-ПК\SQLEXPRESS
   Database: Prime
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Subnets SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Devices SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Operations ADD CONSTRAINT
	FK_Operations_Devices FOREIGN KEY
	(
	Device_name
	) REFERENCES dbo.Devices
	(
	Name
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Operations ADD CONSTRAINT
	FK_Operations_Subnets FOREIGN KEY
	(
	Subnet_name
	) REFERENCES dbo.Subnets
	(
	Name
	) ON UPDATE  CASCADE 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.Operations ADD CONSTRAINT
	FK_Operations_Users FOREIGN KEY
	(
	Engineer
	) REFERENCES dbo.Users
	(
	Username
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Operations SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
