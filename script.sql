USE [master]
GO
/****** Object:  Database [TrackerDB]    Script Date: 12.11.2015 17:02:25 ******/
CREATE DATABASE [TrackerDB] ON  PRIMARY 
( NAME = N'TrackerDB', FILENAME = N'C:\PROGRAM FILES\MICROSOFT SQL SERVER\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\TrackerDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TrackerDB_log', FILENAME = N'C:\PROGRAM FILES\MICROSOFT SQL SERVER\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\TrackerDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TrackerDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TrackerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TrackerDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TrackerDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TrackerDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TrackerDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TrackerDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TrackerDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TrackerDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TrackerDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TrackerDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TrackerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TrackerDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TrackerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TrackerDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TrackerDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TrackerDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TrackerDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TrackerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TrackerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TrackerDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TrackerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TrackerDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TrackerDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TrackerDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TrackerDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TrackerDB] SET  MULTI_USER 
GO
ALTER DATABASE [TrackerDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TrackerDB] SET DB_CHAINING OFF 
GO
USE [TrackerDB]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12.11.2015 17:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NULL,
	[Salary] [float] NULL,
	[DOB] [smalldatetime] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customers222]    Script Date: 12.11.2015 17:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers222](
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NULL,
	[Salary] [float] NULL,
	[DOB] [smalldatetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Departments]    Script Date: 12.11.2015 17:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Department] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 12.11.2015 17:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusGroup]    Script Date: 12.11.2015 17:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StatusGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 12.11.2015 17:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Subject] [nvarchar](50) NULL,
	[Body] [nvarchar](max) NULL,
	[DataMail] [smalldatetime] NULL,
	[Reference] [nvarchar](10) NULL,
	[Responce] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 12.11.2015 17:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Cookies] [nvarchar](100) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([Id], [Department]) VALUES (1, N'-')
INSERT [dbo].[Departments] ([Id], [Department]) VALUES (2, N'Hardware')
INSERT [dbo].[Departments] ([Id], [Department]) VALUES (3, N'Software')
INSERT [dbo].[Departments] ([Id], [Department]) VALUES (4, N'Issue for staff')
SET IDENTITY_INSERT [dbo].[Departments] OFF
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([Id], [Status], [GroupId]) VALUES (0, N'-', 0)
INSERT [dbo].[Statuses] ([Id], [Status], [GroupId]) VALUES (1, N'-', 0)
INSERT [dbo].[Statuses] ([Id], [Status], [GroupId]) VALUES (2, N'Waiting for Staff Response', 0)
INSERT [dbo].[Statuses] ([Id], [Status], [GroupId]) VALUES (3, N'?Waiting for Customer', 1)
INSERT [dbo].[Statuses] ([Id], [Status], [GroupId]) VALUES (4, N'On Hold', 2)
INSERT [dbo].[Statuses] ([Id], [Status], [GroupId]) VALUES (5, N'Cancelled', 3)
INSERT [dbo].[Statuses] ([Id], [Status], [GroupId]) VALUES (6, N'Completed', 3)
SET IDENTITY_INSERT [dbo].[Statuses] OFF
SET IDENTITY_INSERT [dbo].[StatusGroup] ON 

INSERT [dbo].[StatusGroup] ([Id], [GroupName]) VALUES (0, N'New unassigned tickets')
INSERT [dbo].[StatusGroup] ([Id], [GroupName]) VALUES (1, N'Open Tickets')
INSERT [dbo].[StatusGroup] ([Id], [GroupName]) VALUES (2, N'On hold tickets')
INSERT [dbo].[StatusGroup] ([Id], [GroupName]) VALUES (3, N'Closed Tickets')
SET IDENTITY_INSERT [dbo].[StatusGroup] OFF
SET IDENTITY_INSERT [dbo].[Tickets] ON 

INSERT [dbo].[Tickets] ([Id], [DepartmentId], [UserId], [StatusId], [Email], [Subject], [Body], [DataMail], [Reference], [Responce]) VALUES (1, 4, 1, 0, N'aaa@mail.ru', N'ghhfg', N'asddasd', CAST(N'2015-05-11 00:00:00' AS SmallDateTime), N'AAA-000001', NULL)
INSERT [dbo].[Tickets] ([Id], [DepartmentId], [UserId], [StatusId], [Email], [Subject], [Body], [DataMail], [Reference], [Responce]) VALUES (2, 3, 1, 3, N'aaa@mail.ru', NULL, N'asdasddasasd', NULL, N'AAA-000002', NULL)
INSERT [dbo].[Tickets] ([Id], [DepartmentId], [UserId], [StatusId], [Email], [Subject], [Body], [DataMail], [Reference], [Responce]) VALUES (3, 2, 1, 1, N'aaa@mail.ru', NULL, N'sdasdasdasd
sdasdasdasd
sdasdasdasd
sdasdasdasdsdasdasdasd', CAST(N'2015-11-05 00:00:00' AS SmallDateTime), N'AAA-000003', NULL)
INSERT [dbo].[Tickets] ([Id], [DepartmentId], [UserId], [StatusId], [Email], [Subject], [Body], [DataMail], [Reference], [Responce]) VALUES (35, 2, 1, 4, N'asdasasd@mail.ru', N'sadsdaasd', N'asdaasd', NULL, N'AAA-000004', NULL)
INSERT [dbo].[Tickets] ([Id], [DepartmentId], [UserId], [StatusId], [Email], [Subject], [Body], [DataMail], [Reference], [Responce]) VALUES (37, 2, 1, 6, N'asdasdaasdasd@mail.ru', N'asdasdasd', N'asdasdasd', NULL, N'AAA-000005', N'asdfadfadfasdfasdfasdfasdf')
SET IDENTITY_INSERT [dbo].[Tickets] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Password], [Cookies]) VALUES (0, N'-', N'', NULL)
INSERT [dbo].[Users] ([Id], [Name], [Password], [Cookies]) VALUES (1, N'Ivanov', N'123qwe', NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Departments]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Statuses]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Users]
GO
USE [master]
GO
ALTER DATABASE [TrackerDB] SET  READ_WRITE 
GO
