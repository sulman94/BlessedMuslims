USE [master]
GO
/****** Object:  Database [BlessedMuslimDB]    Script Date: 15/03/2021 2:16:10 am ******/
CREATE DATABASE [BlessedMuslimDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BlessedMuslimDB', FILENAME = N'D:\MSSQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\BlessedMuslimDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BlessedMuslimDB_log', FILENAME = N'D:\MSSQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\BlessedMuslimDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BlessedMuslimDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BlessedMuslimDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BlessedMuslimDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BlessedMuslimDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BlessedMuslimDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BlessedMuslimDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BlessedMuslimDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BlessedMuslimDB] SET  MULTI_USER 
GO
ALTER DATABASE [BlessedMuslimDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BlessedMuslimDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BlessedMuslimDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BlessedMuslimDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BlessedMuslimDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BlessedMuslimDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BlessedMuslimDB] SET QUERY_STORE = OFF
GO
USE [BlessedMuslimDB]
GO
/****** Object:  Table [dbo].[City]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ContactPerson] [varchar](50) NULL,
	[ContactNumber] [varchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FixedExpanse]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FixedExpanse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[ExpanseMonth] [varchar](15) NULL,
	[StaffSalary] [int] NULL,
	[StaffBhatta] [int] NULL,
	[BhattaDetails] [varchar](max) NULL,
	[Donation] [int] NULL,
	[DriverRoomRent] [int] NULL,
	[FormanSalary] [int] NULL,
	[ExtraDriversSalary] [int] NULL,
	[ExtraExpense] [int] NULL,
	[ExtraExpenseDetails] [varchar](max) NULL,
	[EntryDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Installment]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Installment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[InstallmentsMonth] [varchar](15) NULL,
	[InstallmentDate] [date] NULL,
	[InstallmentDetail] [varchar](max) NULL,
	[Amount] [int] NULL,
	[EntryDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Maintenance]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Maintenance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[MaintenanceDate] [date] NULL,
	[StationId] [int] NULL,
	[DepartmentId] [int] NULL,
	[Description] [varchar](max) NULL,
	[Amount] [int] NULL,
	[EntryDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Station]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Station](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Location] [varchar](50) NOT NULL,
	[OwnerName] [varchar](50) NULL,
	[ContactNumber] [varchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[StationType] [varchar](50) NULL,
	[CityId] [int] NULL,
 CONSTRAINT [PK_Pump] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleNumber] [varchar](50) NOT NULL,
	[OwnerName] [varchar](50) NOT NULL,
	[OwnerContactNumber] [varchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleClaim]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[ClaimDate] [date] NULL,
	[Claim] [varchar](100) NULL,
	[Description] [varchar](max) NULL,
	[Amount] [int] NULL,
	[EntryDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleLoadingDetail]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleLoadingDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoadingId] [int] NULL,
	[VehicleId] [int] NULL,
	[LoadingDate] [date] NULL,
	[VehicleName] [varchar](100) NULL,
	[Description] [varchar](max) NULL,
	[EntryDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleNumber] [int] NULL,
	[Month] [varchar](50) NOT NULL,
	[UpDate] [date] NULL,
	[UpFrom] [int] NULL,
	[UpTo] [int] NULL,
	[UpAmount] [decimal](18, 0) NULL,
	[DownReturnDate] [date] NULL,
	[DownFrom] [int] NULL,
	[DownTo] [int] NULL,
	[DownDescription] [varchar](50) NULL,
	[DownAmount] [decimal](18, 0) NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [int] NULL,
	[OilShopId] [int] NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoucherDieselDetails]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoucherDieselDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SerialNo] [int] NOT NULL,
	[Date] [date] NULL,
	[StationId] [int] NULL,
	[Litre] [int] NULL,
	[Rate] [decimal](18, 0) NULL,
	[OilAndOthers] [decimal](18, 0) NULL,
	[Amount] [decimal](18, 0) NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[VoucherId] [int] NULL,
 CONSTRAINT [PK_VoucherDieselDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoucherOthersExpenses]    Script Date: 15/03/2021 2:16:10 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoucherOthersExpenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SerialNo] [int] NOT NULL,
	[OthersExpense] [varchar](50) NULL,
	[Amount] [decimal](18, 0) NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[VoucherId] [int] NULL,
 CONSTRAINT [PK_VoucherOthersExpenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[City] ON 
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (1, N'Karachi')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (2, N'Lahore')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (3, N'Islamabad')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (4, N'Hyderabad')
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Company] ON 
GO
INSERT [dbo].[Company] ([Id], [Name], [ContactPerson], [ContactNumber], [CreatedDate], [IsDeleted]) VALUES (1, N'AS Enterprises', N'Sulman CP', N'+923312309633', CAST(N'2021-03-10' AS Date), 0)
GO
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Department] ON 
GO
INSERT [dbo].[Department] ([Id], [DepartmentName]) VALUES (1, N'Maintenance')
GO
INSERT [dbo].[Department] ([Id], [DepartmentName]) VALUES (2, N'Cargo')
GO
INSERT [dbo].[Department] ([Id], [DepartmentName]) VALUES (3, N'Fleet')
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[FixedExpanse] ON 
GO
INSERT [dbo].[FixedExpanse] ([Id], [VehicleId], [ExpanseMonth], [StaffSalary], [StaffBhatta], [BhattaDetails], [Donation], [DriverRoomRent], [FormanSalary], [ExtraDriversSalary], [ExtraExpense], [ExtraExpenseDetails], [EntryDate]) VALUES (2, 1, N'1', 200, 555, N'de dye', 78987, 878979, 55550, 5000, 555, N'hello expanses', CAST(N'2021-03-15T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[FixedExpanse] OFF
GO
SET IDENTITY_INSERT [dbo].[Installment] ON 
GO
INSERT [dbo].[Installment] ([Id], [VehicleId], [InstallmentsMonth], [InstallmentDate], [InstallmentDetail], [Amount], [EntryDate]) VALUES (2, 1, N'2', CAST(N'2021-03-02' AS Date), N'No Detail added', 50000, CAST(N'2021-03-15T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Installment] OFF
GO
SET IDENTITY_INSERT [dbo].[Maintenance] ON 
GO
INSERT [dbo].[Maintenance] ([Id], [VehicleId], [MaintenanceDate], [StationId], [DepartmentId], [Description], [Amount], [EntryDate]) VALUES (1, 1, CAST(N'2021-03-12' AS Date), 3, 1, N'Battery Change', 5000, CAST(N'2021-03-12T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Maintenance] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (1, N'Admin')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Station] ON 
GO
INSERT [dbo].[Station] ([Id], [Name], [Location], [OwnerName], [ContactNumber], [CreatedDate], [IsDeleted], [StationType], [CityId]) VALUES (1, N'PSO', N'Karachi', N'Sulman', N'090078963', CAST(N'2021-03-10' AS Date), 0, N'Pump', 3)
GO
INSERT [dbo].[Station] ([Id], [Name], [Location], [OwnerName], [ContactNumber], [CreatedDate], [IsDeleted], [StationType], [CityId]) VALUES (2, N'Al Barkat Oil Depo', N'Johar Mor', N'Khan BABA', N'00000000', CAST(N'2021-03-10' AS Date), 0, N'OilShop', 1)
GO
INSERT [dbo].[Station] ([Id], [Name], [Location], [OwnerName], [ContactNumber], [CreatedDate], [IsDeleted], [StationType], [CityId]) VALUES (3, N'ABC Maintenance', N'Gulberg', N'Khan bhai', N'556565656', CAST(N'2021-03-12' AS Date), 0, N'MaintenanceShop', 2)
GO
SET IDENTITY_INSERT [dbo].[Station] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Phone], [CreatedDate], [IsDeleted], [RoleId]) VALUES (1, N'sulman', N'sulman.khurshid', N'a', N'03312309633', CAST(N'2021-03-10' AS Date), 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicle] ON 
GO
INSERT [dbo].[Vehicle] ([Id], [VehicleNumber], [OwnerName], [OwnerContactNumber], [CreatedDate], [IsDeleted]) VALUES (1, N'DHA-423', N'Sulman', N'03312309633', CAST(N'2021-03-10' AS Date), 0)
GO
SET IDENTITY_INSERT [dbo].[Vehicle] OFF
GO
SET IDENTITY_INSERT [dbo].[VehicleClaim] ON 
GO
INSERT [dbo].[VehicleClaim] ([Id], [VehicleId], [ClaimDate], [Claim], [Description], [Amount], [EntryDate]) VALUES (2, 1, CAST(N'2021-03-01' AS Date), N'Accident', N'Bumber Damage', 5000, CAST(N'2021-03-15T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[VehicleClaim] OFF
GO
SET IDENTITY_INSERT [dbo].[VehicleLoadingDetail] ON 
GO
INSERT [dbo].[VehicleLoadingDetail] ([Id], [LoadingId], [VehicleId], [LoadingDate], [VehicleName], [Description], [EntryDate]) VALUES (2, 255, 1, CAST(N'2021-03-08' AS Date), N'AAE290', N'Suzuki', CAST(N'2021-03-15T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[VehicleLoadingDetail] OFF
GO
ALTER TABLE [dbo].[FixedExpanse] ADD  DEFAULT (getdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[Installment] ADD  DEFAULT (getdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[Maintenance] ADD  DEFAULT (getdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[VehicleClaim] ADD  DEFAULT (getdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[VehicleLoadingDetail] ADD  DEFAULT (getdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[FixedExpanse]  WITH CHECK ADD  CONSTRAINT [FK_FixedExpanse_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicle] ([Id])
GO
ALTER TABLE [dbo].[FixedExpanse] CHECK CONSTRAINT [FK_FixedExpanse_Vehicle]
GO
ALTER TABLE [dbo].[Installment]  WITH CHECK ADD  CONSTRAINT [FK_Installment_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicle] ([Id])
GO
ALTER TABLE [dbo].[Installment] CHECK CONSTRAINT [FK_Installment_Vehicle]
GO
ALTER TABLE [dbo].[Maintenance]  WITH CHECK ADD  CONSTRAINT [FK_Maintenance_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Maintenance] CHECK CONSTRAINT [FK_Maintenance_Department]
GO
ALTER TABLE [dbo].[Maintenance]  WITH CHECK ADD  CONSTRAINT [FK_Maintenance_Station] FOREIGN KEY([StationId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[Maintenance] CHECK CONSTRAINT [FK_Maintenance_Station]
GO
ALTER TABLE [dbo].[Maintenance]  WITH CHECK ADD  CONSTRAINT [FK_Maintenance_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicle] ([Id])
GO
ALTER TABLE [dbo].[Maintenance] CHECK CONSTRAINT [FK_Maintenance_Vehicle]
GO
ALTER TABLE [dbo].[Station]  WITH CHECK ADD  CONSTRAINT [FK_Station_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Station] CHECK CONSTRAINT [FK_Station_City]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Role]
GO
ALTER TABLE [dbo].[VehicleClaim]  WITH CHECK ADD  CONSTRAINT [FK_VehicleClaim_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicle] ([Id])
GO
ALTER TABLE [dbo].[VehicleClaim] CHECK CONSTRAINT [FK_VehicleClaim_Vehicle]
GO
ALTER TABLE [dbo].[VehicleLoadingDetail]  WITH CHECK ADD  CONSTRAINT [FK_VehicleLoadingDetail_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicle] ([Id])
GO
ALTER TABLE [dbo].[VehicleLoadingDetail] CHECK CONSTRAINT [FK_VehicleLoadingDetail_Vehicle]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_City] FOREIGN KEY([UpTo])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_City]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_City1] FOREIGN KEY([DownFrom])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_City1]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_City2] FOREIGN KEY([DownTo])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_City2]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Company] FOREIGN KEY([UpFrom])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Company]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Station] FOREIGN KEY([OilShopId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Station]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Users] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Users]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Vehicle1] FOREIGN KEY([VehicleNumber])
REFERENCES [dbo].[Vehicle] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Vehicle1]
GO
ALTER TABLE [dbo].[VoucherDieselDetails]  WITH CHECK ADD  CONSTRAINT [FK_VoucherDieselDetails_Station] FOREIGN KEY([StationId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[VoucherDieselDetails] CHECK CONSTRAINT [FK_VoucherDieselDetails_Station]
GO
ALTER TABLE [dbo].[VoucherDieselDetails]  WITH CHECK ADD  CONSTRAINT [FK_VoucherDieselDetails_Voucher] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([Id])
GO
ALTER TABLE [dbo].[VoucherDieselDetails] CHECK CONSTRAINT [FK_VoucherDieselDetails_Voucher]
GO
ALTER TABLE [dbo].[VoucherOthersExpenses]  WITH CHECK ADD  CONSTRAINT [FK_VoucherOthersExpenses_Voucher] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([Id])
GO
ALTER TABLE [dbo].[VoucherOthersExpenses] CHECK CONSTRAINT [FK_VoucherOthersExpenses_Voucher]
GO
USE [master]
GO
ALTER DATABASE [BlessedMuslimDB] SET  READ_WRITE 
GO
