USE [MotorDBNVS]
GO
/****** Object:  StoredProcedure [dbo].[GetAllRegistrationsWithSpecificVehicleId]    Script Date: 30-03-2022 14:25:46 ******/
DROP PROCEDURE [dbo].[GetAllRegistrationsWithSpecificVehicleId]
GO
ALTER TABLE [dbo].[Vehicle] DROP CONSTRAINT [FK_Vehicle_Fuel_FuelId]
GO
ALTER TABLE [dbo].[Vehicle] DROP CONSTRAINT [FK_Vehicle_Category_CategoryId]
GO
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [FK_Registration_Vehicle_VehicleId]
GO
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [FK_Registration_Customer_CustomerId]
GO
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_Address_AddressId]
GO
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_Zipcode_ZipCodeId]
GO
/****** Object:  Index [IX_Vehicle_FuelId]    Script Date: 30-03-2022 14:25:46 ******/
DROP INDEX [IX_Vehicle_FuelId] ON [dbo].[Vehicle]
GO
/****** Object:  Index [IX_Vehicle_CategoryId]    Script Date: 30-03-2022 14:25:46 ******/
DROP INDEX [IX_Vehicle_CategoryId] ON [dbo].[Vehicle]
GO
/****** Object:  Index [IX_Registration_VehicleId]    Script Date: 30-03-2022 14:25:46 ******/
DROP INDEX [IX_Registration_VehicleId] ON [dbo].[Registration]
GO
/****** Object:  Index [IX_Registration_CustomerId]    Script Date: 30-03-2022 14:25:46 ******/
DROP INDEX [IX_Registration_CustomerId] ON [dbo].[Registration]
GO
/****** Object:  Index [IX_Customer_AddressId]    Script Date: 30-03-2022 14:25:46 ******/
DROP INDEX [IX_Customer_AddressId] ON [dbo].[Customer]
GO
/****** Object:  Index [IX_Address_ZipCodeId]    Script Date: 30-03-2022 14:25:46 ******/
DROP INDEX [IX_Address_ZipCodeId] ON [dbo].[Address]
GO
/****** Object:  Table [dbo].[Zipcode]    Script Date: 30-03-2022 14:25:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Zipcode]') AND type in (N'U'))
DROP TABLE [dbo].[Zipcode]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 30-03-2022 14:25:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vehicle]') AND type in (N'U'))
DROP TABLE [dbo].[Vehicle]
GO
/****** Object:  Table [dbo].[Registration]    Script Date: 30-03-2022 14:25:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Registration]') AND type in (N'U'))
DROP TABLE [dbo].[Registration]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 30-03-2022 14:25:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Login]') AND type in (N'U'))
DROP TABLE [dbo].[Login]
GO
/****** Object:  Table [dbo].[Fuel]    Script Date: 30-03-2022 14:25:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fuel]') AND type in (N'U'))
DROP TABLE [dbo].[Fuel]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 30-03-2022 14:25:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 30-03-2022 14:25:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Category]') AND type in (N'U'))
DROP TABLE [dbo].[Category]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 30-03-2022 14:25:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
DROP TABLE [dbo].[Address]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 30-03-2022 14:25:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
USE [master]
GO
/****** Object:  Database [MotorDBNVS]    Script Date: 30-03-2022 14:25:46 ******/
DROP DATABASE [MotorDBNVS]
GO
/****** Object:  Database [MotorDBNVS]    Script Date: 30-03-2022 14:25:46 ******/
CREATE DATABASE [MotorDBNVS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MotorDBNVS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MotorDBNVS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MotorDBNVS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MotorDBNVS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MotorDBNVS] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MotorDBNVS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MotorDBNVS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MotorDBNVS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MotorDBNVS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MotorDBNVS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MotorDBNVS] SET ARITHABORT OFF 
GO
ALTER DATABASE [MotorDBNVS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MotorDBNVS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MotorDBNVS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MotorDBNVS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MotorDBNVS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MotorDBNVS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MotorDBNVS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MotorDBNVS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MotorDBNVS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MotorDBNVS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MotorDBNVS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MotorDBNVS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MotorDBNVS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MotorDBNVS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MotorDBNVS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MotorDBNVS] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MotorDBNVS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MotorDBNVS] SET RECOVERY FULL 
GO
ALTER DATABASE [MotorDBNVS] SET  MULTI_USER 
GO
ALTER DATABASE [MotorDBNVS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MotorDBNVS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MotorDBNVS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MotorDBNVS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MotorDBNVS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MotorDBNVS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MotorDBNVS', N'ON'
GO
ALTER DATABASE [MotorDBNVS] SET QUERY_STORE = OFF
GO
USE [MotorDBNVS]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StreetAndNo] [nvarchar](100) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[ZipCodeId] [int] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[IsActive] [nvarchar](5) NOT NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fuel]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fuel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FuelName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Fuel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registration]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationDate] [date] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[VehicleId] [int] NOT NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Make] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[FuelId] [int] NOT NULL,
	[IsActive] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zipcode]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zipcode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ZipcodeNo] [nvarchar](20) NULL,
	[City] [nvarchar](50) NULL,
 CONSTRAINT [PK_Zipcode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_ZipCodeId]    Script Date: 30-03-2022 14:25:47 ******/
CREATE NONCLUSTERED INDEX [IX_Address_ZipCodeId] ON [dbo].[Address]
(
	[ZipCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Customer_AddressId]    Script Date: 30-03-2022 14:25:47 ******/
CREATE NONCLUSTERED INDEX [IX_Customer_AddressId] ON [dbo].[Customer]
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Registration_CustomerId]    Script Date: 30-03-2022 14:25:47 ******/
CREATE NONCLUSTERED INDEX [IX_Registration_CustomerId] ON [dbo].[Registration]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Registration_VehicleId]    Script Date: 30-03-2022 14:25:47 ******/
CREATE NONCLUSTERED INDEX [IX_Registration_VehicleId] ON [dbo].[Registration]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicle_CategoryId]    Script Date: 30-03-2022 14:25:47 ******/
CREATE NONCLUSTERED INDEX [IX_Vehicle_CategoryId] ON [dbo].[Vehicle]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicle_FuelId]    Script Date: 30-03-2022 14:25:47 ******/
CREATE NONCLUSTERED INDEX [IX_Vehicle_FuelId] ON [dbo].[Vehicle]
(
	[FuelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Zipcode_ZipCodeId] FOREIGN KEY([ZipCodeId])
REFERENCES [dbo].[Zipcode] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Zipcode_ZipCodeId]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Address_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Address_AddressId]
GO
ALTER TABLE [dbo].[Registration]  WITH CHECK ADD  CONSTRAINT [FK_Registration_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Registration] CHECK CONSTRAINT [FK_Registration_Customer_CustomerId]
GO
ALTER TABLE [dbo].[Registration]  WITH CHECK ADD  CONSTRAINT [FK_Registration_Vehicle_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicle] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Registration] CHECK CONSTRAINT [FK_Registration_Vehicle_VehicleId]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vehicle_Category_CategoryId]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_Fuel_FuelId] FOREIGN KEY([FuelId])
REFERENCES [dbo].[Fuel] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vehicle_Fuel_FuelId]
GO
/****** Object:  StoredProcedure [dbo].[GetAllRegistrationsWithSpecificVehicleId]    Script Date: 30-03-2022 14:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicklas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetAllRegistrationsWithSpecificVehicleId] 
	-- Add the parameters for the stored procedure here
	@VehicleId int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	Registration.Id as rid, 
	Registration.RegistrationDate, 
	Customer.Id as cusid,
	Customer.FirstName,
	Customer.LastName,
	Customer.CreateDate,
	Customer.IsActive,
	Address.Id as aid,
	Address.StreetAndNo,
	Address.CreateDate,
	Zipcode.Id as zid,
	Zipcode.City,
	Zipcode.ZipcodeNo,
	Vehicle.Id as vid,
	Vehicle.Make,
	Vehicle.Model,
	Vehicle.CreateDate,
	Vehicle.IsActive,
	Category.Id as catid,
	Category.CategoryName,
	Fuel.Id as fid,
	Fuel.FuelName
	from Registration
	inner join Customer ON Registration.CustomerId=Customer.Id
	inner join Address ON Customer.AddressId=Address.Id
	inner join Zipcode ON Address.ZipCodeId=Zipcode.Id
	inner join Vehicle ON Registration.VehicleId=Vehicle.Id
	inner join Category ON Vehicle.CategoryId=Category.Id
	inner join Fuel ON Vehicle.FuelId=Fuel.Id
	where Registration.VehicleId = @VehicleId
END
GO
USE [master]
GO
ALTER DATABASE [MotorDBNVS] SET  READ_WRITE 
GO
