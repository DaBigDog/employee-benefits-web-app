USE [master]
GO
/********************************************************************************************************************************* 
*	Object:  Database [EmployeeBenefitsDatabase]    
*
*	Note: You may need to change the paths below to your local instance if using MSSQLExpress.
*   If you're using MS SQL Server then you can comment out the lines between 'CREATE DATABASE [EmployeeBenefitsDatabase]' and 'GO'
*
***********************************************************************************************************************************/
CREATE DATABASE [EmployeeBenefitsDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmployeeBenefitsDatabase', FILENAME = N'C:\Users\Jim\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\Employees.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EmployeeBenefitsDatabase', FILENAME = N'C:\Users\Jim\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\Employees.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeeBenefitsDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET ANSI_NULLS ON 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET ANSI_PADDING ON 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET ARITHABORT ON 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
USE [EmployeeBenefitsDatabase]
GO
/****** Object:  Table [dbo].[Benefits]    Script Date: 1/28/21 5:15:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Benefits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[EmpYearlyBenefitCost] [decimal](18, 4) NOT NULL,
	[DepYearlyBenefitCost] [decimal](18, 4) NOT NULL,
	[PercentDiscount] [int] NULL,
	[DiscountMatch] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 1/28/21 5:15:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Dependent]    Script Date: 1/28/21 5:15:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dependent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[FirstName] [nvarchar](500) NOT NULL,
	[MiddleName] [nvarchar](500) NULL,
	[LastName] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 1/28/21 5:15:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[FirstName] [nvarchar](500) NOT NULL,
	[MiddleName] [nvarchar](500) NULL,
	[LastName] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Benefits]  WITH CHECK ADD  CONSTRAINT [FK_CompanyBenefits] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Benefits] CHECK CONSTRAINT [FK_CompanyBenefits]
GO
ALTER TABLE [dbo].[Dependent]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDependent] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Dependent] CHECK CONSTRAINT [FK_EmployeeDependent]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_CompanyEmployee] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_CompanyEmployee]
GO
USE [master]
GO
ALTER DATABASE [EmployeeBenefitsDatabase] SET  READ_WRITE 
GO



/********  Add ititial data to tables  **********/

GO
SET IDENTITY_INSERT [dbo].[Company] ON 

GO
INSERT [dbo].[Company] ([Id], [Name]) VALUES (1, N'The Company')
GO
INSERT [dbo].[Company] ([Id], [Name]) VALUES (2, N'Your Company')
GO
INSERT [dbo].[Company] ([Id], [Name]) VALUES (3, N'Our Company')
GO
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

GO
INSERT [dbo].[Employee] ([Id], [CompanyId], [FirstName], [MiddleName], [LastName]) VALUES (1, 1, N'Albert', NULL, N'Smith')
GO
INSERT [dbo].[Employee] ([Id], [CompanyId], [FirstName], [MiddleName], [LastName]) VALUES (3, 1, N'Nancy', N'J', N'Jones')
GO
INSERT [dbo].[Employee] ([Id], [CompanyId], [FirstName], [MiddleName], [LastName]) VALUES (2002, 1, N'Alphonse', NULL, N'Capone')
GO
INSERT [dbo].[Employee] ([Id], [CompanyId], [FirstName], [MiddleName], [LastName]) VALUES (3002, 1, N'Aaron', NULL, N'Rogers')
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Dependent] ON 

GO
INSERT [dbo].[Dependent] ([Id], [EmployeeId], [FirstName], [MiddleName], [LastName]) VALUES (1, 3, N'James', N'P', N'Jones')
GO
INSERT [dbo].[Dependent] ([Id], [EmployeeId], [FirstName], [MiddleName], [LastName]) VALUES (2, 1, N'Liam', N'A', N'Smith')
GO
INSERT [dbo].[Dependent] ([Id], [EmployeeId], [FirstName], [MiddleName], [LastName]) VALUES (3, 1, N'Betsy', N'B', N'Smith')
GO
INSERT [dbo].[Dependent] ([Id], [EmployeeId], [FirstName], [MiddleName], [LastName]) VALUES (2002, 2002, N'Lucy', NULL, N'Capone')
GO
SET IDENTITY_INSERT [dbo].[Dependent] OFF
GO
SET IDENTITY_INSERT [dbo].[Benefits] ON 

GO
INSERT [dbo].[Benefits] ([Id], [CompanyId], [EmpYearlyBenefitCost], [DepYearlyBenefitCost], [PercentDiscount], [DiscountMatch]) VALUES (1, 1, CAST(1000.0000 AS Decimal(18, 4)), CAST(500.0000 AS Decimal(18, 4)), 10, N'A')
GO
SET IDENTITY_INSERT [dbo].[Benefits] OFF
GO
