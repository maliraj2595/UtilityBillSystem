-- Script to delete and insert records for Admin and User tables in UtilityMvcDB

-- Deletion script to clear all dependent tables first
--DELETE FROM [dbo].[TicketDetail];
--DELETE FROM [dbo].[BillDetail];
--DELETE FROM [dbo].[User];
--DELETE FROM [dbo].[ApplicationDetail];
--DELETE FROM [dbo].[MeterDetail];
--DELETE FROM [dbo].[Address];

-- Reset identity values for all tables
--DBCC CHECKIDENT ('[dbo].[TicketDetail]', RESEED, 0);
--DBCC CHECKIDENT ('[dbo].[BillDetail]', RESEED, 0);
--DBCC CHECKIDENT ('[dbo].[User]', RESEED, 0);
--DBCC CHECKIDENT ('[dbo].[ApplicationDetail]', RESEED, 0);
--DBCC CHECKIDENT ('[dbo].[MeterDetail]', RESEED, 0);
--DBCC CHECKIDENT ('[dbo].[Address]', RESEED, 0);

-- 1. Address Table (Independent Table)
INSERT INTO [dbo].[Address]
           ([Street]
           ,[City]
           ,[PinCode]
           ,[State])
     VALUES
           ('123 Main Street', 'New York', 10001, 'New York'),
           ('456 Elm Street', 'Los Angeles', 90001, 'California');

-- 2. MeterDetail Table (Independent Table)
INSERT INTO [dbo].[MeterDetail]
           ([InstallationDate]
           ,[Status])
     VALUES
           ('2025-01-10T12:00:00', 'Active'),
           ('2025-01-18T10:30:00', 'Active');

-- 3. ApplicationDetail Table (Dependent on MeterDetail)
INSERT INTO [dbo].[ApplicationDetail]
           ([ApplicationDate]
           ,[ApplicationStatus]
           ,[ConnectionType]
           ,[RequiredLoad]
           ,[MeterDetailMeterId])
     VALUES
           ('2025-01-15T16:00:00', 'Approved', 'Residential', '5 kW', 1),
           ('2025-01-19T16:00:00', 'Pending', 'Commercial', '10 kW', 2);

-- 4. User Table (Dependent on Address and ApplicationDetail)
INSERT INTO [dbo].[User]
           ([Username]
           ,[Email]
           ,[Password]
           ,[Gender]
           ,[DateOfBirth]
           ,[MobileNumber]
           ,[Role]
           ,[IsDeleted]
           ,[AddressId]
           ,[ApplicationDetailId])
     VALUES
           ('Admin', 'admin@example.com', 'Admin@123', 'Male', '1990-05-15T00:00:00', '9876543210', 'Admin', 0, 1, 1),
           ('User', 'user@example.com', 'User@123', 'Female', '1995-06-20T00:00:00', '9876543211', 'User', 0, 2, 2);

-- 5. BillDetail Table (Dependent on MeterDetail and User)
INSERT INTO [dbo].[BillDetail]
           ([UnitsConsumed]
           ,[TotalBill]
           ,[Rate]
           ,[BillDate]
           ,[BillStatus]
           ,[MeterDetailMeterId]
           ,[UserId])
     VALUES
           (150, 2250.00, 15.00, '2025-01-10T00:00:00', 'Paid', 1, 1),
           (200, 3000.00, 15.00, '2025-01-20T00:00:00', 'Unpaid', 2, 2);

-- 6. TicketDetail Table (Dependent on User)
INSERT INTO [dbo].[TicketDetail]
           ([Type]
           ,[Description]
           ,[status]
           ,[UserId])
     VALUES
           ('Complaint', 'Power outage in my area.', 'Open', 1),
           ('Request', 'Request for meter check.', 'Open', 2);
