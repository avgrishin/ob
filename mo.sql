USE [master]
GO
/****** Object:  Database [MiddleOffice]    Script Date: 24.11.2017 22:40:29 ******/
CREATE DATABASE [MiddleOffice]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MiddleOffice_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MiddleOffice.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MiddleOffice_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MiddleOffice.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MiddleOffice] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MiddleOffice].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MiddleOffice] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MiddleOffice] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MiddleOffice] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MiddleOffice] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MiddleOffice] SET ARITHABORT OFF 
GO
ALTER DATABASE [MiddleOffice] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MiddleOffice] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MiddleOffice] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MiddleOffice] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MiddleOffice] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MiddleOffice] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MiddleOffice] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MiddleOffice] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MiddleOffice] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MiddleOffice] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MiddleOffice] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MiddleOffice] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MiddleOffice] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MiddleOffice] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MiddleOffice] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MiddleOffice] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MiddleOffice] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MiddleOffice] SET RECOVERY FULL 
GO
ALTER DATABASE [MiddleOffice] SET  MULTI_USER 
GO
ALTER DATABASE [MiddleOffice] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MiddleOffice] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MiddleOffice] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MiddleOffice] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MiddleOffice', N'ON'
GO
USE [MiddleOffice]
GO
/****** Object:  Table [dbo].[taLib]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[taLib](
	[LID] [int] IDENTITY(1,1) NOT NULL,
	[LParent] [int] NULL,
	[LConcept] [int] NULL,
	[LCode] [varchar](10) NULL,
	[LName] [varchar](80) NULL,
	[LName1] [varchar](80) NULL,
	[LName2] [varchar](80) NULL,
	[LID1] [int] NULL,
	[LID2] [int] NULL,
	[LID3] [int] NULL,
	[LID4] [int] NULL,
	[LID5] [int] NULL,
	[LInt1] [int] NULL,
	[LInt2] [int] NULL,
	[LDate1] [smalldatetime] NULL,
	[LDate2] [smalldatetime] NULL,
	[LNum1] [float] NULL,
	[LNum2] [float] NULL,
	[LBInt1] [bigint] NULL,
	[InDateTime] [datetime] NULL,
	[LDate3] [smalldatetime] NULL,
 CONSTRAINT [PK_taLib] PRIMARY KEY CLUSTERED 
(
	[LID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tConseil]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tConseil](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Violation] [varchar](max) NULL,
	[Conseil] [varchar](max) NULL,
	[Terme] [smalldatetime] NULL,
	[Prolongation] [smalldatetime] NULL,
	[Possesseur] [varchar](255) NULL,
	[Commentaire] [varchar](max) NULL,
	[EmailTo] [varchar](max) NULL,
	[EmailCc] [varchar](max) NULL,
	[InDateTime] [datetime] NULL,
	[IsEnabled] [bit] NULL,
	[PrononceDate] [smalldatetime] NULL,
	[Priorite] [int] NULL,
	[ExecDate] [smalldatetime] NULL,
	[MinNomRiskPrice] [money] NULL,
	[MaxNomRiskPrice] [money] NULL,
 CONSTRAINT [PK_tConseil] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tConseilHoraire]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tConseilHoraire](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ConseilID] [int] NOT NULL,
	[EnvoiHoraireTypeID] [int] NOT NULL,
	[Day] [int] NULL,
	[Month] [int] NULL,
 CONSTRAINT [PK_tConseilHoraire] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tEnvoi]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tEnvoi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Num] [int] NULL,
	[TypeInf] [varchar](1000) NULL,
	[SrokRask] [varchar](1000) NULL,
	[Mesto] [varchar](255) NULL,
	[Osnovan] [varchar](1000) NULL,
	[EmailTo] [varchar](max) NULL,
	[EmailCc] [varchar](max) NULL,
	[PoryadPredst] [varchar](150) NULL,
	[Periodich] [varchar](150) NULL,
	[PeriodichID] [int] NULL,
	[VidAktiv] [varchar](150) NULL,
	[SrokRass] [varchar](255) NULL,
	[IsAuto] [bit] NULL,
	[IsEnabled] [bit] NULL,
	[TypeID] [int] NULL,
	[Responsible] [varchar](50) NULL,
 CONSTRAINT [PK_tEnvoi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tEnvoiExec]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tEnvoiExec](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EnvoiID] [int] NOT NULL,
	[Date1] [smalldatetime] NULL,
	[Date2] [smalldatetime] NULL,
	[Comment] [varchar](250) NULL,
	[Date3] [smalldatetime] NULL,
	[InDateTime] [datetime] NULL,
 CONSTRAINT [PK_tEnvoiExec] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tEnvoiHoraire]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tEnvoiHoraire](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EnvoiID] [int] NOT NULL,
	[EnvoiHoraireTypeID] [int] NOT NULL,
	[ModeID] [int] NOT NULL,
	[Day] [int] NULL,
	[Month] [int] NULL,
	[Comment] [varchar](50) NULL,
 CONSTRAINT [PK_tEnvoiHoraire] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tEnvoiHoraireType]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tEnvoiHoraireType](
	[ID] [int] NOT NULL,
	[Name] [varchar](150) NULL,
 CONSTRAINT [PK_tEnvoiHoraireType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tObjClassifier]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tObjClassifier](
	[ObjClassifierID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[ObjType] [int] NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[NameBrief] [varchar](250) NOT NULL,
	[UniqueFlag] [int] NOT NULL,
	[RequiredFlag] [int] NOT NULL,
	[OnDateFlag] [int] NOT NULL,
	[Comment] [varchar](250) NOT NULL,
	[InDateTime] [datetime] NOT NULL,
	[UserName] [varchar](25) NOT NULL,
 CONSTRAINT [XPKtObjClassifier] PRIMARY KEY CLUSTERED 
(
	[ObjClassifierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRiskMap]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRiskMap](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JurPersonne] [varchar](25) NULL,
	[Dep] [varchar](35) NULL,
	[BisProc] [varchar](20) NULL,
	[NumRisk] [int] NULL,
	[RiskName] [varchar](1000) NULL,
	[Influence] [int] NULL,
	[Probabilite] [int] NULL,
	[ControlForce] [int] NULL,
	[EssentielRisk] [bit] NULL,
	[But] [varchar](1000) NULL,
	[PossesseurBut] [varchar](255) NULL,
	[Control] [varchar](max) NULL,
	[PossesseurControl] [varchar](255) NULL,
	[EmailTo] [varchar](max) NULL,
	[EmailCc] [varchar](max) NULL,
	[InDateTime] [datetime] NULL,
	[IsEnabled] [bit] NULL,
 CONSTRAINT [PK_tRiskMap] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRiskMapHoraire]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRiskMapHoraire](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RiskMapID] [int] NOT NULL,
	[EnvoiHoraireTypeID] [int] NOT NULL,
	[Day] [int] NULL,
	[Month] [int] NULL,
 CONSTRAINT [PK_tRiskMapHoraire] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tWorkDate]    Script Date: 24.11.2017 22:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tWorkDate](
	[WorkDateID] [int] IDENTITY(1,1) NOT NULL,
	[WorkDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[taLib] ON 

INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (1, 458622, 458622, NULL, N'Гришин А.В.', N'GrishinAV@uralsib.ru', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-11-01T09:43:49.970' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (7, 458622, 458622, NULL, N'Ковригин В.Н.', N'KovriginVN@uralsib.ru', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-11-08T17:19:12.433' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482639, 0, 0, NULL, N'Справочник для Карты рисков', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-07T14:21:59.643' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482640, 482639, 482639, NULL, N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2013-11-07T14:26:56.250' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482641, 482639, 482639, NULL, N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, CAST(N'2013-11-07T14:27:27.610' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482642, 482639, 482639, NULL, N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2013-11-07T14:27:45.493' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482643, 482639, 482639, NULL, N'High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, CAST(N'2013-11-07T14:28:25.087' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482644, 0, 0, NULL, N'Справочник для Карты рисков 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:38:49.087' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482645, 482644, 482644, NULL, N'HighHighHigh', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.403' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482646, 482644, 482644, NULL, N'HighMedium HighHigh', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.403' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482647, 482644, 482644, NULL, N'HighMedium LowHigh', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.403' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482648, 482644, 482644, NULL, N'HighLowHigh', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.403' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482649, 482644, 482644, NULL, N'Medium HighHighHigh', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.403' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482650, 482644, 482644, NULL, N'Medium HighMedium HighHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482651, 482644, 482644, NULL, N'Medium HighMedium LowHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482652, 482644, 482644, NULL, N'Medium HighLowHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482653, 482644, 482644, NULL, N'Medium LowHighHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482654, 482644, 482644, NULL, N'Medium LowMedium HighHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482655, 482644, 482644, NULL, N'Medium LowMedium LowHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482656, 482644, 482644, NULL, N'Medium LowLowHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482657, 482644, 482644, NULL, N'LowHighHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482658, 482644, 482644, NULL, N'LowMedium HighHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482659, 482644, 482644, NULL, N'LowMedium LowHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482660, 482644, 482644, NULL, N'LowLowHigh', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482661, 482644, 482644, NULL, N'HighHighMedium High', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482662, 482644, 482644, NULL, N'HighMedium HighMedium High', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482663, 482644, 482644, NULL, N'HighMedium LowMedium High', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.407' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482664, 482644, 482644, NULL, N'HighLowMedium High', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482665, 482644, 482644, NULL, N'Medium HighHighMedium High', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482666, 482644, 482644, NULL, N'Medium HighMedium HighMedium High', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482667, 482644, 482644, NULL, N'Medium HighMedium LowMedium High', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482668, 482644, 482644, NULL, N'Medium HighLowMedium High', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482669, 482644, 482644, NULL, N'Medium LowHighMedium High', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482670, 482644, 482644, NULL, N'Medium LowMedium HighMedium High', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482671, 482644, 482644, NULL, N'Medium LowMedium LowMedium High', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482672, 482644, 482644, NULL, N'Medium LowLowMedium High', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482673, 482644, 482644, NULL, N'LowHighMedium High', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482674, 482644, 482644, NULL, N'LowMedium HighMedium High', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482675, 482644, 482644, NULL, N'LowMedium LowMedium High', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482676, 482644, 482644, NULL, N'LowLowMedium High', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482677, 482644, 482644, NULL, N'HighHighMedium Low', N'High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.410' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482678, 482644, 482644, NULL, N'HighMedium HighMedium Low', N'High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.420' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482679, 482644, 482644, NULL, N'HighMedium LowMedium Low', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.420' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482680, 482644, 482644, NULL, N'HighLowMedium Low', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.420' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482681, 482644, 482644, NULL, N'Medium HighHighMedium Low', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.420' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482682, 482644, 482644, NULL, N'Medium HighMedium HighMedium Low', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.420' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482683, 482644, 482644, NULL, N'Medium HighMedium LowMedium Low', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.420' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482684, 482644, 482644, NULL, N'Medium HighLowMedium Low', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.420' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482685, 482644, 482644, NULL, N'Medium LowHighMedium Low', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.420' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482686, 482644, 482644, NULL, N'Medium LowMedium HighMedium Low', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.420' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482687, 482644, 482644, NULL, N'Medium LowMedium LowMedium Low', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482688, 482644, 482644, NULL, N'Medium LowLowMedium Low', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482689, 482644, 482644, NULL, N'LowHighMedium Low', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482690, 482644, 482644, NULL, N'LowMedium HighMedium Low', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482691, 482644, 482644, NULL, N'LowMedium LowMedium Low', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482692, 482644, 482644, NULL, N'LowLowMedium Low', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482693, 482644, 482644, NULL, N'HighHighLow', N'High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482694, 482644, 482644, NULL, N'HighMedium HighLow', N'High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482695, 482644, 482644, NULL, N'HighMedium LowLow', N'High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482696, 482644, 482644, NULL, N'HighLowLow', N'High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482697, 482644, 482644, NULL, N'Medium HighHighLow', N'High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482698, 482644, 482644, NULL, N'Medium HighMedium HighLow', N'High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482699, 482644, 482644, NULL, N'Medium HighMedium LowLow', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.423' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482700, 482644, 482644, NULL, N'Medium HighLowLow', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.427' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482701, 482644, 482644, NULL, N'Medium LowHighLow', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.427' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482702, 482644, 482644, NULL, N'Medium LowMedium HighLow', N'Medium High', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.427' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482703, 482644, 482644, NULL, N'Medium LowMedium LowLow', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.427' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482704, 482644, 482644, NULL, N'Medium LowLowLow', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.427' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482705, 482644, 482644, NULL, N'LowHighLow', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.427' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482706, 482644, 482644, NULL, N'LowMedium HighLow', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.427' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482707, 482644, 482644, NULL, N'LowMedium LowLow', N'Medium Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.427' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (482708, 482644, 482644, NULL, N'LowLowLow', N'Low', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2013-11-08T13:41:22.427' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (483545, 0, 0, NULL, N'Приоритет журнала рекомендаций', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2014-07-17T09:59:30.180' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (483546, 483545, 483545, NULL, N'Критичная', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2014-07-17T10:00:14.050' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (483547, 483545, 483545, NULL, N'Важная', NULL, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, CAST(N'2014-07-17T10:00:31.617' AS DateTime), NULL)
INSERT [dbo].[taLib] ([LID], [LParent], [LConcept], [LCode], [LName], [LName1], [LName2], [LID1], [LID2], [LID3], [LID4], [LID5], [LInt1], [LInt2], [LDate1], [LDate2], [LNum1], [LNum2], [LBInt1], [InDateTime], [LDate3]) VALUES (483548, 483545, 483545, NULL, N'Умеренная', NULL, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2014-07-17T10:00:43.897' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[taLib] OFF
SET IDENTITY_INSERT [dbo].[tConseil] ON 

INSERT [dbo].[tConseil] ([ID], [Violation], [Conseil], [Terme], [Prolongation], [Possesseur], [Commentaire], [EmailTo], [EmailCc], [InDateTime], [IsEnabled], [PrononceDate], [Priorite], [ExecDate], [MinNomRiskPrice], [MaxNomRiskPrice]) VALUES (1, N'1', N'2', CAST(N'2017-11-15T00:00:00' AS SmallDateTime), NULL, N'1', NULL, N'GrishinAV@uralsib.ru', NULL, CAST(N'2017-11-09T12:05:46.033' AS DateTime), 1, CAST(N'2017-11-08T00:00:00' AS SmallDateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[tConseil] ([ID], [Violation], [Conseil], [Terme], [Prolongation], [Possesseur], [Commentaire], [EmailTo], [EmailCc], [InDateTime], [IsEnabled], [PrononceDate], [Priorite], [ExecDate], [MinNomRiskPrice], [MaxNomRiskPrice]) VALUES (2, NULL, NULL, CAST(N'2017-11-19T00:00:00' AS SmallDateTime), NULL, NULL, NULL, NULL, NULL, CAST(N'2017-11-09T11:50:54.330' AS DateTime), 1, CAST(N'2017-11-09T00:00:00' AS SmallDateTime), 2, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tConseil] OFF
SET IDENTITY_INSERT [dbo].[tEnvoi] ON 

INSERT [dbo].[tEnvoi] ([ID], [Num], [TypeInf], [SrokRask], [Mesto], [Osnovan], [EmailTo], [EmailCc], [PoryadPredst], [Periodich], [PeriodichID], [VidAktiv], [SrokRass], [IsAuto], [IsEnabled], [TypeID], [Responsible]) VALUES (1, 1, N'2', N'1', N'1', N'1', N'GrishinAV@uralsib.ru', N'GrishinAV@uralsib.ru', N'1', NULL, 26179, N'1', N'1', 1, 1, 1, N'1')
INSERT [dbo].[tEnvoi] ([ID], [Num], [TypeInf], [SrokRask], [Mesto], [Osnovan], [EmailTo], [EmailCc], [PoryadPredst], [Periodich], [PeriodichID], [VidAktiv], [SrokRass], [IsAuto], [IsEnabled], [TypeID], [Responsible]) VALUES (2, NULL, N'3', N'3', N'3', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, NULL)
INSERT [dbo].[tEnvoi] ([ID], [Num], [TypeInf], [SrokRask], [Mesto], [Osnovan], [EmailTo], [EmailCc], [PoryadPredst], [Periodich], [PeriodichID], [VidAktiv], [SrokRass], [IsAuto], [IsEnabled], [TypeID], [Responsible]) VALUES (3, NULL, N'asdasd', NULL, NULL, N'sвавава', NULL, NULL, NULL, NULL, 26181, NULL, NULL, 0, 1, 2, NULL)
SET IDENTITY_INSERT [dbo].[tEnvoi] OFF
SET IDENTITY_INSERT [dbo].[tEnvoiExec] ON 

INSERT [dbo].[tEnvoiExec] ([ID], [EnvoiID], [Date1], [Date2], [Comment], [Date3], [InDateTime]) VALUES (1, 1, CAST(N'2017-11-09T00:00:00' AS SmallDateTime), NULL, NULL, NULL, CAST(N'2017-11-09T11:52:10.077' AS DateTime))
INSERT [dbo].[tEnvoiExec] ([ID], [EnvoiID], [Date1], [Date2], [Comment], [Date3], [InDateTime]) VALUES (2, 3, CAST(N'2017-11-09T00:00:00' AS SmallDateTime), NULL, NULL, NULL, CAST(N'2017-11-09T11:52:46.990' AS DateTime))
SET IDENTITY_INSERT [dbo].[tEnvoiExec] OFF
SET IDENTITY_INSERT [dbo].[tEnvoiHoraire] ON 

INSERT [dbo].[tEnvoiHoraire] ([ID], [EnvoiID], [EnvoiHoraireTypeID], [ModeID], [Day], [Month], [Comment]) VALUES (1, 1, 1, 0, 14, 11, NULL)
INSERT [dbo].[tEnvoiHoraire] ([ID], [EnvoiID], [EnvoiHoraireTypeID], [ModeID], [Day], [Month], [Comment]) VALUES (2, 2, 12, 0, 46, NULL, NULL)
INSERT [dbo].[tEnvoiHoraire] ([ID], [EnvoiID], [EnvoiHoraireTypeID], [ModeID], [Day], [Month], [Comment]) VALUES (3, 2, 12, 1, 50, NULL, N'Примечание')
SET IDENTITY_INSERT [dbo].[tEnvoiHoraire] OFF
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (1, N'День, месяц')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (2, N'N рабочий день со дня окончания года')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (3, N'N рабочий день со дня окончания квартала')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (4, N'N рабочий день со дня окончания месяца')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (5, N'Последнее число месяца, следующего за днем окончания квартала')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (6, N'Каждый понедельник')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (7, N'Каждый вторник')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (8, N'Каждую среду')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (9, N'Каждый четверг')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (10, N'Каждую пятницу')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (11, N'N календарный день со дня окончания года')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (12, N'N календарный день со дня окончания квартала')
INSERT [dbo].[tEnvoiHoraireType] ([ID], [Name]) VALUES (13, N'N календарный день со дня окончания месяца')
SET IDENTITY_INSERT [dbo].[tObjClassifier] ON 

INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6249, 0, 0, N'Справочники', N'Справочники', 0, 0, 0, N'', CAST(N'2017-11-19T21:02:58.770' AS DateTime), N'')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6250, 6249, 0, N'Срок на устранение', N'', 0, 0, 0, N'', CAST(N'2014-04-01T11:37:41.747' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6251, 6250, 0, N'1 месяц', N'1 мес.', 0, 0, 0, N'', CAST(N'2014-04-01T11:38:14.357' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6252, 6250, 0, N'3 раб. дня', N'3 рабочих дня.', 0, 0, 0, N'', CAST(N'2014-04-01T11:38:36.600' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6253, 6250, 0, N'6 месяцев', N'6 мес.', 0, 0, 0, N'', CAST(N'2014-04-01T11:38:58.777' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6254, 6250, 0, N'1 год', N'1 год.', 0, 0, 0, N'', CAST(N'2014-04-01T11:39:09.867' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6255, 6249, 0, N'Способ устранения', N'', 0, 0, 0, N'', CAST(N'2014-04-01T11:39:34.797' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6257, 6255, 0, N'Продажа ЦБ', N'', 0, 0, 0, N'', CAST(N'2014-04-01T11:40:15.837' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6258, 6255, 0, N'Изменение оценочной стоимости ЦБ', N'', 0, 0, 0, N'', CAST(N'2014-04-01T11:40:45.863' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6259, 6255, 0, N'Изменение доли оц/ст неликвидных ц/б в общей стоимости имущества фонда', N'', 0, 0, 0, N'', CAST(N'2014-04-01T11:42:04.863' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6260, 6255, 0, N'Изменение доли обязательств по контракту', N'', 0, 0, 0, N'', CAST(N'2014-04-01T11:42:22.040' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6261, 6249, 0, N'Вид нарушений', N'', 0, 0, 0, N'', CAST(N'2014-04-01T11:42:58.580' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6262, 6261, 0, N'Активное нарушение', N'', 0, 0, 0, N'', CAST(N'2014-04-01T11:43:23.683' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (6263, 6261, 0, N'Пассивное нарушение', N'', 0, 0, 0, N'', CAST(N'2014-04-01T11:43:35.333' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (26178, 26177, 0, N'Периодичность', N'', 0, 0, 0, N'', CAST(N'2015-10-26T10:48:46.110' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (26179, 26178, 0, N'Ежегодно', N'', 0, 0, 0, N'', CAST(N'2015-10-26T11:13:34.040' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (26180, 26178, 0, N'Ежеквартально', N'', 0, 0, 0, N'', CAST(N'2015-10-26T11:13:46.857' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (26181, 26178, 0, N'Ежемесячно', N'', 0, 0, 0, N'', CAST(N'2015-10-26T11:14:12.617' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (26182, 26178, 0, N'Еженедельно', N'', 0, 0, 0, N'', CAST(N'2015-10-26T11:14:29.287' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (26183, 26178, 0, N'Каждый интервал', N'', 0, 0, 0, N'', CAST(N'2015-10-26T11:14:47.910' AS DateTime), N'GrishinAV')
INSERT [dbo].[tObjClassifier] ([ObjClassifierID], [ParentID], [ObjType], [Name], [NameBrief], [UniqueFlag], [RequiredFlag], [OnDateFlag], [Comment], [InDateTime], [UserName]) VALUES (26184, 26178, 0, N'По факту', N'', 0, 0, 0, N'', CAST(N'2015-10-26T11:15:25.853' AS DateTime), N'GrishinAV')
SET IDENTITY_INSERT [dbo].[tObjClassifier] OFF
SET IDENTITY_INSERT [dbo].[tWorkDate] ON 

INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (1, CAST(N'2017-01-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (2, CAST(N'2017-01-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (3, CAST(N'2017-01-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (4, CAST(N'2017-01-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (5, CAST(N'2017-01-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (6, CAST(N'2017-01-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (7, CAST(N'2017-01-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (8, CAST(N'2017-01-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (9, CAST(N'2017-01-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (10, CAST(N'2017-01-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (11, CAST(N'2017-01-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (12, CAST(N'2017-01-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (13, CAST(N'2017-01-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (14, CAST(N'2017-01-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (15, CAST(N'2017-01-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (16, CAST(N'2017-01-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (17, CAST(N'2017-01-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (18, CAST(N'2017-02-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (19, CAST(N'2017-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (20, CAST(N'2017-02-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (21, CAST(N'2017-02-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (22, CAST(N'2017-02-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (23, CAST(N'2017-02-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (24, CAST(N'2017-02-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (25, CAST(N'2017-02-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (26, CAST(N'2017-02-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (27, CAST(N'2017-02-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (28, CAST(N'2017-02-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (29, CAST(N'2017-02-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (30, CAST(N'2017-02-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (31, CAST(N'2017-02-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (32, CAST(N'2017-02-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (33, CAST(N'2017-02-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (34, CAST(N'2017-02-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (35, CAST(N'2017-02-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (36, CAST(N'2017-03-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (37, CAST(N'2017-03-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (38, CAST(N'2017-03-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (39, CAST(N'2017-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (40, CAST(N'2017-03-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (41, CAST(N'2017-03-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (42, CAST(N'2017-03-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (43, CAST(N'2017-03-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (44, CAST(N'2017-03-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (45, CAST(N'2017-03-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (46, CAST(N'2017-03-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (47, CAST(N'2017-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (48, CAST(N'2017-03-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (49, CAST(N'2017-03-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (50, CAST(N'2017-03-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (51, CAST(N'2017-03-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (52, CAST(N'2017-03-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (53, CAST(N'2017-03-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (54, CAST(N'2017-03-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (55, CAST(N'2017-03-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (56, CAST(N'2017-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (57, CAST(N'2017-03-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (58, CAST(N'2017-04-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (59, CAST(N'2017-04-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (60, CAST(N'2017-04-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (61, CAST(N'2017-04-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (62, CAST(N'2017-04-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (63, CAST(N'2017-04-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (64, CAST(N'2017-04-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (65, CAST(N'2017-04-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (66, CAST(N'2017-04-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (67, CAST(N'2017-04-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (68, CAST(N'2017-04-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (69, CAST(N'2017-04-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (70, CAST(N'2017-04-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (71, CAST(N'2017-04-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (72, CAST(N'2017-04-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (73, CAST(N'2017-04-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (74, CAST(N'2017-04-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (75, CAST(N'2017-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (76, CAST(N'2017-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (77, CAST(N'2017-04-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (78, CAST(N'2017-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (79, CAST(N'2017-05-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (80, CAST(N'2017-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (81, CAST(N'2017-05-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (82, CAST(N'2017-05-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (83, CAST(N'2017-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (84, CAST(N'2017-05-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (85, CAST(N'2017-05-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (86, CAST(N'2017-05-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (87, CAST(N'2017-05-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (88, CAST(N'2017-05-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (89, CAST(N'2017-05-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (90, CAST(N'2017-05-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (91, CAST(N'2017-05-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (92, CAST(N'2017-05-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (93, CAST(N'2017-05-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (94, CAST(N'2017-05-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (95, CAST(N'2017-05-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (96, CAST(N'2017-05-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (97, CAST(N'2017-05-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (98, CAST(N'2017-06-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (99, CAST(N'2017-06-02T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (100, CAST(N'2017-06-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (101, CAST(N'2017-06-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (102, CAST(N'2017-06-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (103, CAST(N'2017-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (104, CAST(N'2017-06-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (105, CAST(N'2017-06-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (106, CAST(N'2017-06-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (107, CAST(N'2017-06-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (108, CAST(N'2017-06-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (109, CAST(N'2017-06-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (110, CAST(N'2017-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (111, CAST(N'2017-06-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (112, CAST(N'2017-06-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (113, CAST(N'2017-06-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (114, CAST(N'2017-06-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (115, CAST(N'2017-06-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (116, CAST(N'2017-06-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (117, CAST(N'2017-06-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (118, CAST(N'2017-06-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (119, CAST(N'2017-07-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (120, CAST(N'2017-07-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (121, CAST(N'2017-07-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (122, CAST(N'2017-07-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (123, CAST(N'2017-07-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (124, CAST(N'2017-07-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (125, CAST(N'2017-07-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (126, CAST(N'2017-07-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (127, CAST(N'2017-07-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (128, CAST(N'2017-07-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (129, CAST(N'2017-07-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (130, CAST(N'2017-07-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (131, CAST(N'2017-07-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (132, CAST(N'2017-07-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (133, CAST(N'2017-07-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (134, CAST(N'2017-07-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (135, CAST(N'2017-07-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (136, CAST(N'2017-07-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (137, CAST(N'2017-07-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (138, CAST(N'2017-07-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (139, CAST(N'2017-07-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (140, CAST(N'2017-08-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (141, CAST(N'2017-08-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (142, CAST(N'2017-08-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (143, CAST(N'2017-08-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (144, CAST(N'2017-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (145, CAST(N'2017-08-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (146, CAST(N'2017-08-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (147, CAST(N'2017-08-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (148, CAST(N'2017-08-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (149, CAST(N'2017-08-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (150, CAST(N'2017-08-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (151, CAST(N'2017-08-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (152, CAST(N'2017-08-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (153, CAST(N'2017-08-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (154, CAST(N'2017-08-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (155, CAST(N'2017-08-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (156, CAST(N'2017-08-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (157, CAST(N'2017-08-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (158, CAST(N'2017-08-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (159, CAST(N'2017-08-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (160, CAST(N'2017-08-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (161, CAST(N'2017-08-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (162, CAST(N'2017-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (163, CAST(N'2017-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (164, CAST(N'2017-09-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (165, CAST(N'2017-09-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (166, CAST(N'2017-09-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (167, CAST(N'2017-09-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (168, CAST(N'2017-09-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (169, CAST(N'2017-09-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (170, CAST(N'2017-09-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (171, CAST(N'2017-09-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (172, CAST(N'2017-09-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (173, CAST(N'2017-09-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (174, CAST(N'2017-09-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (175, CAST(N'2017-09-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (176, CAST(N'2017-09-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (177, CAST(N'2017-09-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (178, CAST(N'2017-09-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (179, CAST(N'2017-09-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (180, CAST(N'2017-09-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (181, CAST(N'2017-09-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (182, CAST(N'2017-09-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (183, CAST(N'2017-09-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (184, CAST(N'2017-10-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (185, CAST(N'2017-10-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (186, CAST(N'2017-10-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (187, CAST(N'2017-10-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (188, CAST(N'2017-10-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (189, CAST(N'2017-10-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (190, CAST(N'2017-10-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (191, CAST(N'2017-10-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (192, CAST(N'2017-10-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (193, CAST(N'2017-10-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (194, CAST(N'2017-10-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (195, CAST(N'2017-10-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (196, CAST(N'2017-10-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (197, CAST(N'2017-10-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (198, CAST(N'2017-10-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (199, CAST(N'2017-10-23T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (200, CAST(N'2017-10-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (201, CAST(N'2017-10-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (202, CAST(N'2017-10-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (203, CAST(N'2017-10-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (204, CAST(N'2017-10-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (205, CAST(N'2017-10-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (206, CAST(N'2017-11-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (207, CAST(N'2017-11-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (208, CAST(N'2017-11-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (209, CAST(N'2017-11-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (210, CAST(N'2017-11-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (211, CAST(N'2017-11-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (212, CAST(N'2017-11-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (213, CAST(N'2017-11-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (214, CAST(N'2017-11-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (215, CAST(N'2017-11-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (216, CAST(N'2017-11-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (217, CAST(N'2017-11-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (218, CAST(N'2017-11-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (219, CAST(N'2017-11-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (220, CAST(N'2017-11-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (221, CAST(N'2017-11-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (222, CAST(N'2017-11-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (223, CAST(N'2017-11-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (224, CAST(N'2017-11-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (225, CAST(N'2017-11-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (226, CAST(N'2017-11-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (227, CAST(N'2017-12-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (228, CAST(N'2017-12-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (229, CAST(N'2017-12-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (230, CAST(N'2017-12-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (231, CAST(N'2017-12-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (232, CAST(N'2017-12-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (233, CAST(N'2017-12-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (234, CAST(N'2017-12-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (235, CAST(N'2017-12-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (236, CAST(N'2017-12-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (237, CAST(N'2017-12-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (238, CAST(N'2017-12-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (239, CAST(N'2017-12-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (240, CAST(N'2017-12-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (241, CAST(N'2017-12-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (242, CAST(N'2017-12-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (243, CAST(N'2017-12-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (244, CAST(N'2017-12-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (245, CAST(N'2017-12-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (246, CAST(N'2017-12-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (247, CAST(N'2017-12-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (248, CAST(N'2018-01-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (249, CAST(N'2018-01-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (250, CAST(N'2018-01-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (251, CAST(N'2018-01-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (252, CAST(N'2018-01-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (253, CAST(N'2018-01-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (254, CAST(N'2018-01-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (255, CAST(N'2018-01-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (256, CAST(N'2018-01-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (257, CAST(N'2018-01-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (258, CAST(N'2018-01-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (259, CAST(N'2018-01-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (260, CAST(N'2018-01-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (261, CAST(N'2018-01-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (262, CAST(N'2018-01-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (263, CAST(N'2018-01-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (264, CAST(N'2018-01-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (265, CAST(N'2018-02-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (266, CAST(N'2018-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (267, CAST(N'2018-02-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (268, CAST(N'2018-02-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (269, CAST(N'2018-02-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (270, CAST(N'2018-02-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (271, CAST(N'2018-02-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (272, CAST(N'2018-02-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (273, CAST(N'2018-02-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (274, CAST(N'2018-02-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (275, CAST(N'2018-02-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (276, CAST(N'2018-02-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (277, CAST(N'2018-02-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (278, CAST(N'2018-02-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (279, CAST(N'2018-02-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (280, CAST(N'2018-02-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (281, CAST(N'2018-02-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (282, CAST(N'2018-02-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (283, CAST(N'2018-02-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (284, CAST(N'2018-03-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (285, CAST(N'2018-03-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (286, CAST(N'2018-03-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (287, CAST(N'2018-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (288, CAST(N'2018-03-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (289, CAST(N'2018-03-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (290, CAST(N'2018-03-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (291, CAST(N'2018-03-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (292, CAST(N'2018-03-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (293, CAST(N'2018-03-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (294, CAST(N'2018-03-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (295, CAST(N'2018-03-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (296, CAST(N'2018-03-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (297, CAST(N'2018-03-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (298, CAST(N'2018-03-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (299, CAST(N'2018-03-23T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (300, CAST(N'2018-03-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (301, CAST(N'2018-03-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (302, CAST(N'2018-03-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (303, CAST(N'2018-03-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (304, CAST(N'2018-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (305, CAST(N'2018-04-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (306, CAST(N'2018-04-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (307, CAST(N'2018-04-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (308, CAST(N'2018-04-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (309, CAST(N'2018-04-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (310, CAST(N'2018-04-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (311, CAST(N'2018-04-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (312, CAST(N'2018-04-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (313, CAST(N'2018-04-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (314, CAST(N'2018-04-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (315, CAST(N'2018-04-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (316, CAST(N'2018-04-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (317, CAST(N'2018-04-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (318, CAST(N'2018-04-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (319, CAST(N'2018-04-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (320, CAST(N'2018-04-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (321, CAST(N'2018-04-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (322, CAST(N'2018-04-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (323, CAST(N'2018-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (324, CAST(N'2018-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (325, CAST(N'2018-04-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (326, CAST(N'2018-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (327, CAST(N'2018-05-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (328, CAST(N'2018-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (329, CAST(N'2018-05-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (330, CAST(N'2018-05-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (331, CAST(N'2018-05-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (332, CAST(N'2018-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (333, CAST(N'2018-05-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (334, CAST(N'2018-05-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (335, CAST(N'2018-05-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (336, CAST(N'2018-05-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (337, CAST(N'2018-05-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (338, CAST(N'2018-05-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (339, CAST(N'2018-05-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (340, CAST(N'2018-05-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (341, CAST(N'2018-05-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (342, CAST(N'2018-05-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (343, CAST(N'2018-05-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (344, CAST(N'2018-05-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (345, CAST(N'2018-05-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (346, CAST(N'2018-05-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (347, CAST(N'2018-06-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (348, CAST(N'2018-06-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (349, CAST(N'2018-06-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (350, CAST(N'2018-06-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (351, CAST(N'2018-06-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (352, CAST(N'2018-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (353, CAST(N'2018-06-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (354, CAST(N'2018-06-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (355, CAST(N'2018-06-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (356, CAST(N'2018-06-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (357, CAST(N'2018-06-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (358, CAST(N'2018-06-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (359, CAST(N'2018-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (360, CAST(N'2018-06-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (361, CAST(N'2018-06-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (362, CAST(N'2018-06-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (363, CAST(N'2018-06-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (364, CAST(N'2018-06-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (365, CAST(N'2018-06-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (366, CAST(N'2018-06-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (367, CAST(N'2018-07-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (368, CAST(N'2018-07-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (369, CAST(N'2018-07-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (370, CAST(N'2018-07-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (371, CAST(N'2018-07-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (372, CAST(N'2018-07-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (373, CAST(N'2018-07-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (374, CAST(N'2018-07-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (375, CAST(N'2018-07-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (376, CAST(N'2018-07-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (377, CAST(N'2018-07-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (378, CAST(N'2018-07-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (379, CAST(N'2018-07-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (380, CAST(N'2018-07-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (381, CAST(N'2018-07-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (382, CAST(N'2018-07-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (383, CAST(N'2018-07-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (384, CAST(N'2018-07-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (385, CAST(N'2018-07-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (386, CAST(N'2018-07-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (387, CAST(N'2018-07-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (388, CAST(N'2018-07-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (389, CAST(N'2018-08-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (390, CAST(N'2018-08-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (391, CAST(N'2018-08-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (392, CAST(N'2018-08-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (393, CAST(N'2018-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (394, CAST(N'2018-08-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (395, CAST(N'2018-08-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (396, CAST(N'2018-08-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (397, CAST(N'2018-08-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (398, CAST(N'2018-08-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (399, CAST(N'2018-08-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (400, CAST(N'2018-08-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (401, CAST(N'2018-08-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (402, CAST(N'2018-08-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (403, CAST(N'2018-08-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (404, CAST(N'2018-08-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (405, CAST(N'2018-08-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (406, CAST(N'2018-08-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (407, CAST(N'2018-08-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (408, CAST(N'2018-08-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (409, CAST(N'2018-08-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (410, CAST(N'2018-08-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (411, CAST(N'2018-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (412, CAST(N'2018-09-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (413, CAST(N'2018-09-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (414, CAST(N'2018-09-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (415, CAST(N'2018-09-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (416, CAST(N'2018-09-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (417, CAST(N'2018-09-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (418, CAST(N'2018-09-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (419, CAST(N'2018-09-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (420, CAST(N'2018-09-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (421, CAST(N'2018-09-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (422, CAST(N'2018-09-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (423, CAST(N'2018-09-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (424, CAST(N'2018-09-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (425, CAST(N'2018-09-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (426, CAST(N'2018-09-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (427, CAST(N'2018-09-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (428, CAST(N'2018-09-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (429, CAST(N'2018-09-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (430, CAST(N'2018-09-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (431, CAST(N'2018-09-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (432, CAST(N'2018-10-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (433, CAST(N'2018-10-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (434, CAST(N'2018-10-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (435, CAST(N'2018-10-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (436, CAST(N'2018-10-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (437, CAST(N'2018-10-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (438, CAST(N'2018-10-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (439, CAST(N'2018-10-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (440, CAST(N'2018-10-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (441, CAST(N'2018-10-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (442, CAST(N'2018-10-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (443, CAST(N'2018-10-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (444, CAST(N'2018-10-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (445, CAST(N'2018-10-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (446, CAST(N'2018-10-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (447, CAST(N'2018-10-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (448, CAST(N'2018-10-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (449, CAST(N'2018-10-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (450, CAST(N'2018-10-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (451, CAST(N'2018-10-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (452, CAST(N'2018-10-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (453, CAST(N'2018-10-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (454, CAST(N'2018-10-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (455, CAST(N'2018-11-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (456, CAST(N'2018-11-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (457, CAST(N'2018-11-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (458, CAST(N'2018-11-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (459, CAST(N'2018-11-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (460, CAST(N'2018-11-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (461, CAST(N'2018-11-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (462, CAST(N'2018-11-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (463, CAST(N'2018-11-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (464, CAST(N'2018-11-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (465, CAST(N'2018-11-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (466, CAST(N'2018-11-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (467, CAST(N'2018-11-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (468, CAST(N'2018-11-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (469, CAST(N'2018-11-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (470, CAST(N'2018-11-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (471, CAST(N'2018-11-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (472, CAST(N'2018-11-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (473, CAST(N'2018-11-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (474, CAST(N'2018-11-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (475, CAST(N'2018-11-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (476, CAST(N'2018-12-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (477, CAST(N'2018-12-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (478, CAST(N'2018-12-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (479, CAST(N'2018-12-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (480, CAST(N'2018-12-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (481, CAST(N'2018-12-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (482, CAST(N'2018-12-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (483, CAST(N'2018-12-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (484, CAST(N'2018-12-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (485, CAST(N'2018-12-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (486, CAST(N'2018-12-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (487, CAST(N'2018-12-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (488, CAST(N'2018-12-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (489, CAST(N'2018-12-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (490, CAST(N'2018-12-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (491, CAST(N'2018-12-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (492, CAST(N'2018-12-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (493, CAST(N'2018-12-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (494, CAST(N'2018-12-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (495, CAST(N'2018-12-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (496, CAST(N'2018-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (504, CAST(N'2016-01-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (505, CAST(N'2016-01-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (506, CAST(N'2016-01-13T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (507, CAST(N'2016-01-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (508, CAST(N'2016-01-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (509, CAST(N'2016-01-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (510, CAST(N'2016-01-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (511, CAST(N'2016-01-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (512, CAST(N'2016-01-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (513, CAST(N'2016-01-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (514, CAST(N'2016-01-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (515, CAST(N'2016-01-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (516, CAST(N'2016-01-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (517, CAST(N'2016-01-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (518, CAST(N'2016-01-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (519, CAST(N'2016-02-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (520, CAST(N'2016-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (521, CAST(N'2016-02-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (522, CAST(N'2016-02-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (523, CAST(N'2016-02-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (524, CAST(N'2016-02-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (525, CAST(N'2016-02-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (526, CAST(N'2016-02-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (527, CAST(N'2016-02-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (528, CAST(N'2016-02-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (529, CAST(N'2016-02-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (530, CAST(N'2016-02-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (531, CAST(N'2016-02-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (532, CAST(N'2016-02-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (533, CAST(N'2016-02-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (534, CAST(N'2016-02-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (535, CAST(N'2016-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (536, CAST(N'2016-02-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (537, CAST(N'2016-02-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (538, CAST(N'2016-02-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (539, CAST(N'2016-02-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (540, CAST(N'2016-03-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (541, CAST(N'2016-03-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (542, CAST(N'2016-03-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (543, CAST(N'2016-03-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (544, CAST(N'2016-03-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (545, CAST(N'2016-03-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (546, CAST(N'2016-03-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (547, CAST(N'2016-03-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (548, CAST(N'2016-03-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (549, CAST(N'2016-03-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (550, CAST(N'2016-03-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (551, CAST(N'2016-03-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (552, CAST(N'2016-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (553, CAST(N'2016-03-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (554, CAST(N'2016-03-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (555, CAST(N'2016-03-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (556, CAST(N'2016-03-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (557, CAST(N'2016-03-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (558, CAST(N'2016-03-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (559, CAST(N'2016-03-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (560, CAST(N'2016-03-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (561, CAST(N'2016-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (562, CAST(N'2016-03-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (563, CAST(N'2016-04-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (564, CAST(N'2016-04-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (565, CAST(N'2016-04-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (566, CAST(N'2016-04-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (567, CAST(N'2016-04-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (568, CAST(N'2016-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (569, CAST(N'2016-04-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (570, CAST(N'2016-04-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (571, CAST(N'2016-04-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (572, CAST(N'2016-04-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (573, CAST(N'2016-04-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (574, CAST(N'2016-04-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (575, CAST(N'2016-04-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (576, CAST(N'2016-04-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (577, CAST(N'2016-04-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (578, CAST(N'2016-04-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (579, CAST(N'2016-04-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (580, CAST(N'2016-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (581, CAST(N'2016-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (582, CAST(N'2016-04-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (583, CAST(N'2016-04-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (584, CAST(N'2016-05-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (585, CAST(N'2016-05-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (586, CAST(N'2016-05-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (587, CAST(N'2016-05-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (588, CAST(N'2016-05-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (589, CAST(N'2016-05-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (590, CAST(N'2016-05-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (591, CAST(N'2016-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (592, CAST(N'2016-05-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (593, CAST(N'2016-05-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (594, CAST(N'2016-05-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (595, CAST(N'2016-05-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (596, CAST(N'2016-05-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (597, CAST(N'2016-05-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (598, CAST(N'2016-05-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (599, CAST(N'2016-05-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (600, CAST(N'2016-05-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (601, CAST(N'2016-05-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (602, CAST(N'2016-05-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (603, CAST(N'2016-05-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (604, CAST(N'2016-05-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (605, CAST(N'2016-05-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (606, CAST(N'2016-06-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (607, CAST(N'2016-06-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (608, CAST(N'2016-06-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (609, CAST(N'2016-06-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (610, CAST(N'2016-06-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (611, CAST(N'2016-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (612, CAST(N'2016-06-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (613, CAST(N'2016-06-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (614, CAST(N'2016-06-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (615, CAST(N'2016-06-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (616, CAST(N'2016-06-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (617, CAST(N'2016-06-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (618, CAST(N'2016-06-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (619, CAST(N'2016-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (620, CAST(N'2016-06-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (621, CAST(N'2016-06-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (622, CAST(N'2016-06-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (623, CAST(N'2016-06-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (624, CAST(N'2016-06-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (625, CAST(N'2016-06-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (626, CAST(N'2016-06-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (627, CAST(N'2016-06-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (628, CAST(N'2016-07-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (629, CAST(N'2016-07-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (630, CAST(N'2016-07-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (631, CAST(N'2016-07-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (632, CAST(N'2016-07-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (633, CAST(N'2016-07-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (634, CAST(N'2016-07-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (635, CAST(N'2016-07-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (636, CAST(N'2016-07-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (637, CAST(N'2016-07-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (638, CAST(N'2016-07-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (639, CAST(N'2016-07-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (640, CAST(N'2016-07-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (641, CAST(N'2016-07-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (642, CAST(N'2016-07-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (643, CAST(N'2016-07-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (644, CAST(N'2016-07-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (645, CAST(N'2016-07-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (646, CAST(N'2016-07-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (647, CAST(N'2016-07-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (648, CAST(N'2016-07-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (649, CAST(N'2016-08-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (650, CAST(N'2016-08-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (651, CAST(N'2016-08-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (652, CAST(N'2016-08-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (653, CAST(N'2016-08-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (654, CAST(N'2016-08-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (655, CAST(N'2016-08-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (656, CAST(N'2016-08-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (657, CAST(N'2016-08-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (658, CAST(N'2016-08-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (659, CAST(N'2016-08-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (660, CAST(N'2016-08-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (661, CAST(N'2016-08-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (662, CAST(N'2016-08-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (663, CAST(N'2016-08-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (664, CAST(N'2016-08-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (665, CAST(N'2016-08-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (666, CAST(N'2016-08-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (667, CAST(N'2016-08-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (668, CAST(N'2016-08-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (669, CAST(N'2016-08-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (670, CAST(N'2016-08-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (671, CAST(N'2016-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (672, CAST(N'2016-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (673, CAST(N'2016-09-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (674, CAST(N'2016-09-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (675, CAST(N'2016-09-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (676, CAST(N'2016-09-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (677, CAST(N'2016-09-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (678, CAST(N'2016-09-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (679, CAST(N'2016-09-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (680, CAST(N'2016-09-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (681, CAST(N'2016-09-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (682, CAST(N'2016-09-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (683, CAST(N'2016-09-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (684, CAST(N'2016-09-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (685, CAST(N'2016-09-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (686, CAST(N'2016-09-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (687, CAST(N'2016-09-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (688, CAST(N'2016-09-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (689, CAST(N'2016-09-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (690, CAST(N'2016-09-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (691, CAST(N'2016-09-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (692, CAST(N'2016-09-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (693, CAST(N'2016-09-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (694, CAST(N'2016-10-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (695, CAST(N'2016-10-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (696, CAST(N'2016-10-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (697, CAST(N'2016-10-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (698, CAST(N'2016-10-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (699, CAST(N'2016-10-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (700, CAST(N'2016-10-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (701, CAST(N'2016-10-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (702, CAST(N'2016-10-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (703, CAST(N'2016-10-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (704, CAST(N'2016-10-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (705, CAST(N'2016-10-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (706, CAST(N'2016-10-19T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (707, CAST(N'2016-10-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (708, CAST(N'2016-10-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (709, CAST(N'2016-10-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (710, CAST(N'2016-10-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (711, CAST(N'2016-10-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (712, CAST(N'2016-10-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (713, CAST(N'2016-10-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (714, CAST(N'2016-10-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (715, CAST(N'2016-11-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (716, CAST(N'2016-11-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (717, CAST(N'2016-11-03T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (718, CAST(N'2016-11-04T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (719, CAST(N'2016-11-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (720, CAST(N'2016-11-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (721, CAST(N'2016-11-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (722, CAST(N'2016-11-10T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (723, CAST(N'2016-11-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (724, CAST(N'2016-11-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (725, CAST(N'2016-11-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (726, CAST(N'2016-11-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (727, CAST(N'2016-11-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (728, CAST(N'2016-11-18T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (729, CAST(N'2016-11-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (730, CAST(N'2016-11-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (731, CAST(N'2016-11-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (732, CAST(N'2016-11-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (733, CAST(N'2016-11-25T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (734, CAST(N'2016-11-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (735, CAST(N'2016-11-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (736, CAST(N'2016-11-30T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (737, CAST(N'2016-12-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (738, CAST(N'2016-12-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (739, CAST(N'2016-12-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (740, CAST(N'2016-12-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (741, CAST(N'2016-12-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (742, CAST(N'2016-12-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (743, CAST(N'2016-12-09T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (744, CAST(N'2016-12-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (745, CAST(N'2016-12-13T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (746, CAST(N'2016-12-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (747, CAST(N'2016-12-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (748, CAST(N'2016-12-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (749, CAST(N'2016-12-19T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (750, CAST(N'2016-12-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (751, CAST(N'2016-12-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (752, CAST(N'2016-12-22T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (753, CAST(N'2016-12-23T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (754, CAST(N'2016-12-26T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (755, CAST(N'2016-12-27T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (756, CAST(N'2016-12-28T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (757, CAST(N'2016-12-29T00:00:00.000' AS DateTime))
INSERT [dbo].[tWorkDate] ([WorkDateID], [WorkDate]) VALUES (758, CAST(N'2016-12-30T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[tWorkDate] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_taLib]    Script Date: 24.11.2017 22:40:29 ******/
CREATE NONCLUSTERED INDEX [IX_taLib] ON [dbo].[taLib]
(
	[LConcept] ASC,
	[LName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PKWorkDateID]    Script Date: 24.11.2017 22:40:29 ******/
ALTER TABLE [dbo].[tWorkDate] ADD  CONSTRAINT [PKWorkDateID] PRIMARY KEY NONCLUSTERED 
(
	[WorkDateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
ALTER TABLE [dbo].[taLib] ADD  CONSTRAINT [DF_taLib_InDateTime]  DEFAULT (getdate()) FOR [InDateTime]
GO
ALTER TABLE [dbo].[tEnvoi] ADD  CONSTRAINT [DF_tEnvoi_IsAuto]  DEFAULT ((1)) FOR [IsAuto]
GO
ALTER TABLE [dbo].[tEnvoiHoraire] ADD  CONSTRAINT [DF_tEnvoiHoraire_ModeID]  DEFAULT ((0)) FOR [ModeID]
GO
ALTER TABLE [dbo].[tObjClassifier] ADD  CONSTRAINT [tObjClassifierUniqueFlag]  DEFAULT ((0)) FOR [UniqueFlag]
GO
ALTER TABLE [dbo].[tObjClassifier] ADD  CONSTRAINT [tObjClassifierRequiredFlag]  DEFAULT ((0)) FOR [RequiredFlag]
GO
ALTER TABLE [dbo].[tObjClassifier] ADD  CONSTRAINT [tObjClassifierOnDateFlag]  DEFAULT ((0)) FOR [OnDateFlag]
GO
ALTER TABLE [dbo].[tObjClassifier] ADD  CONSTRAINT [tObjClassifierComment]  DEFAULT ('') FOR [Comment]
GO
ALTER TABLE [dbo].[tObjClassifier] ADD  CONSTRAINT [tObjClassifierInDateTime]  DEFAULT (getdate()) FOR [InDateTime]
GO
ALTER TABLE [dbo].[tObjClassifier] ADD  DEFAULT (suser_sname()) FOR [UserName]
GO
ALTER TABLE [dbo].[tConseilHoraire]  WITH CHECK ADD  CONSTRAINT [FK_tConseilHoraire_tConseil] FOREIGN KEY([ConseilID])
REFERENCES [dbo].[tConseil] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tConseilHoraire] CHECK CONSTRAINT [FK_tConseilHoraire_tConseil]
GO
ALTER TABLE [dbo].[tConseilHoraire]  WITH CHECK ADD  CONSTRAINT [FK_tConseilHoraire_tEnvoiHoraireType] FOREIGN KEY([EnvoiHoraireTypeID])
REFERENCES [dbo].[tEnvoiHoraireType] ([ID])
GO
ALTER TABLE [dbo].[tConseilHoraire] CHECK CONSTRAINT [FK_tConseilHoraire_tEnvoiHoraireType]
GO
ALTER TABLE [dbo].[tEnvoiExec]  WITH CHECK ADD  CONSTRAINT [FK_tEnvoiExec_tEnvoi] FOREIGN KEY([EnvoiID])
REFERENCES [dbo].[tEnvoi] ([ID])
GO
ALTER TABLE [dbo].[tEnvoiExec] CHECK CONSTRAINT [FK_tEnvoiExec_tEnvoi]
GO
ALTER TABLE [dbo].[tEnvoiHoraire]  WITH CHECK ADD  CONSTRAINT [FK_tEnvoiHoraire_tEnvoi] FOREIGN KEY([EnvoiID])
REFERENCES [dbo].[tEnvoi] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tEnvoiHoraire] CHECK CONSTRAINT [FK_tEnvoiHoraire_tEnvoi]
GO
ALTER TABLE [dbo].[tEnvoiHoraire]  WITH CHECK ADD  CONSTRAINT [FK_tEnvoiHoraire_tEnvoiHoraireType] FOREIGN KEY([EnvoiHoraireTypeID])
REFERENCES [dbo].[tEnvoiHoraireType] ([ID])
GO
ALTER TABLE [dbo].[tEnvoiHoraire] CHECK CONSTRAINT [FK_tEnvoiHoraire_tEnvoiHoraireType]
GO
ALTER TABLE [dbo].[tRiskMapHoraire]  WITH CHECK ADD  CONSTRAINT [FK_tRiskMapHoraire_tEnvoiHoraireType] FOREIGN KEY([EnvoiHoraireTypeID])
REFERENCES [dbo].[tEnvoiHoraireType] ([ID])
GO
ALTER TABLE [dbo].[tRiskMapHoraire] CHECK CONSTRAINT [FK_tRiskMapHoraire_tEnvoiHoraireType]
GO
ALTER TABLE [dbo].[tRiskMapHoraire]  WITH CHECK ADD  CONSTRAINT [FK_tRiskMapHoraire_tRiskMap] FOREIGN KEY([RiskMapID])
REFERENCES [dbo].[tRiskMap] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tRiskMapHoraire] CHECK CONSTRAINT [FK_tRiskMapHoraire_tRiskMap]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 - Рейтинговые агентства
2 - Эмитенты
3 - Рейтинги CBONDS
4 - Эмиссии
6 - Справочник рейтингов
10 - Сроки фондирования
11 - Листинги
12 - К/О/П
13 - Отрасль облигации
14 - Тип дефолта облигации
15 - Валюта сумм дефолта облигации
16 - Дефолт облигаций
17 - Типы счетов
18 - Эмиссия Эмитент ISIN
19 - Периоды для отчет
20 - Периоды (данные)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'taLib', @level2type=N'COLUMN',@level2name=N'LConcept'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата вынесения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tConseil', @level2type=N'COLUMN',@level2name=N'PrononceDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата выполнения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tConseil', @level2type=N'COLUMN',@level2name=N'ExecDate'
GO
USE [master]
GO
ALTER DATABASE [MiddleOffice] SET  READ_WRITE 
GO
