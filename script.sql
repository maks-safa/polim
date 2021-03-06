USE [master]
GO
/****** Object:  Database [Diplom]    Script Date: 30.05.2022 9:25:54 ******/
CREATE DATABASE [Diplom]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Diplom', FILENAME = N'C:\asd\MSSQL13.SQLEXPRESS01\MSSQL\DATA\Diplom.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Diplom_log', FILENAME = N'C:\asd\MSSQL13.SQLEXPRESS01\MSSQL\DATA\Diplom_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Diplom] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Diplom].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Diplom] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Diplom] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Diplom] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Diplom] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Diplom] SET ARITHABORT OFF 
GO
ALTER DATABASE [Diplom] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Diplom] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Diplom] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Diplom] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Diplom] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Diplom] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Diplom] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Diplom] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Diplom] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Diplom] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Diplom] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Diplom] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Diplom] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Diplom] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Diplom] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Diplom] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Diplom] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Diplom] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Diplom] SET  MULTI_USER 
GO
ALTER DATABASE [Diplom] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Diplom] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Diplom] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Diplom] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Diplom] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Diplom] SET QUERY_STORE = OFF
GO
USE [Diplom]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Diplom]
GO
/****** Object:  Table [dbo].[Cklad]    Script Date: 30.05.2022 9:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cklad](
	[IdCklad] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Cklad] PRIMARY KEY CLUSTERED 
(
	[IdCklad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cpisanie]    Script Date: 30.05.2022 9:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cpisanie](
	[IdCpisanie] [int] IDENTITY(1,1) NOT NULL,
	[IdCtelag] [int] NOT NULL,
	[НомерДокумента] [int] NOT NULL,
	[Количество] [decimal](10, 3) NOT NULL,
 CONSTRAINT [PK_Cpisanie] PRIMARY KEY CLUSTERED 
(
	[IdCpisanie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ctelag]    Script Date: 30.05.2022 9:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ctelag](
	[IdCtelag] [int] IDENTITY(1,1) NOT NULL,
	[IdCklad] [int] NOT NULL,
	[IdMaterial] [int] NOT NULL,
	[НомерСтелажа] [nvarchar](50) NOT NULL,
	[НомерПолки] [nvarchar](50) NOT NULL,
	[Осталось] [decimal](10, 3) NOT NULL,
 CONSTRAINT [PK_Ctelag] PRIMARY KEY CLUSTERED 
(
	[IdCtelag] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EdIzmer]    Script Date: 30.05.2022 9:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdIzmer](
	[IdEdIzamer] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EdIzmer] PRIMARY KEY CLUSTERED 
(
	[IdEdIzamer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 30.05.2022 9:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[IdMaterial] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](max) NOT NULL,
	[IdEdIzamer] [int] NOT NULL,
	[IdTip] [int] NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[IdMaterial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PoctavYchet]    Script Date: 30.05.2022 9:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PoctavYchet](
	[IdPoctav] [int] NOT NULL,
	[IdPostHaYchet] [int] NOT NULL,
 CONSTRAINT [PK_PoctavYchet] PRIMARY KEY CLUSTERED 
(
	[IdPoctav] ASC,
	[IdPostHaYchet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Poctavzik]    Script Date: 30.05.2022 9:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Poctavzik](
	[IdPoctav] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Poctavzik] PRIMARY KEY CLUSTERED 
(
	[IdPoctav] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostavHaYchet]    Script Date: 30.05.2022 9:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostavHaYchet](
	[IdPostHaYchet] [int] IDENTITY(1,1) NOT NULL,
	[Дата] [date] NOT NULL,
	[НомерДокумента] [int] NOT NULL,
	[IdPoctav] [int] NOT NULL,
	[IdMaterial] [int] NOT NULL,
	[Цена] [decimal](10, 2) NOT NULL,
	[Количество] [decimal](10, 3) NOT NULL,
	[IdCtelag] [int] NOT NULL,
 CONSTRAINT [PK_PostavHaYchet] PRIMARY KEY CLUSTERED 
(
	[IdPostHaYchet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tip]    Script Date: 30.05.2022 9:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tip](
	[IdTip] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Tip] PRIMARY KEY CLUSTERED 
(
	[IdTip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cklad] ON 

INSERT [dbo].[Cklad] ([IdCklad], [Наименование]) VALUES (1, N'Центральный')
SET IDENTITY_INSERT [dbo].[Cklad] OFF
SET IDENTITY_INSERT [dbo].[Ctelag] ON 

INSERT [dbo].[Ctelag] ([IdCtelag], [IdCklad], [IdMaterial], [НомерСтелажа], [НомерПолки], [Осталось]) VALUES (1, 1, 1, N'1', N'1', CAST(120.000 AS Decimal(10, 3)))
INSERT [dbo].[Ctelag] ([IdCtelag], [IdCklad], [IdMaterial], [НомерСтелажа], [НомерПолки], [Осталось]) VALUES (2, 1, 2, N'2', N'12', CAST(120.000 AS Decimal(10, 3)))
SET IDENTITY_INSERT [dbo].[Ctelag] OFF
SET IDENTITY_INSERT [dbo].[EdIzmer] ON 

INSERT [dbo].[EdIzmer] ([IdEdIzamer], [Наименование]) VALUES (1, N'Шт')
INSERT [dbo].[EdIzmer] ([IdEdIzamer], [Наименование]) VALUES (2, N'Кг')
SET IDENTITY_INSERT [dbo].[EdIzmer] OFF
SET IDENTITY_INSERT [dbo].[Material] ON 

INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (1, N'Д16 Пруток 230', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (2, N'Д16 Пруток 50', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (3, N'Д18 Пруток 70', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (4, N'Д18 Пруток 75', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (5, N'Д16 Пруток ткр 100', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (6, N'Д16 Пруток ткр 130', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (7, N'Д16 Пруток ткр 170', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (8, N'Д16 Пруток ткр 200', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (9, N'Д16 Ф10', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (10, N'Д16 Ф150', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (11, N'Д16 Ф85', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (12, N'Д16АM #2', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (13, N'Д16АM #6', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (14, N'Д16AT #1,5', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (15, N'Д16AT #2', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (16, N'Д16AT #3 0', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (17, N'Д16АT #4', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (18, N'Д16T #0,5', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (19, N'Д16Т #1,0', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (20, N'Д16Т #10', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (21, N'Д16Т #10 Деловые отходы', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (22, N'Д16Т #12', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (23, N'Д16Т #14', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (24, N'Д16Т #25', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (25, N'Д16Т #3', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (26, N'Д15Т #3 Деловые отходы', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (27, N'Д16Т #5', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (28, N'Д16T #5 Деловые отходы', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (29, N'Д16T #5', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (30, N'Д16Т Пруток 40', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (31, N'Д16Г Пруток кр 6 ГОСТ 6 ГОСТ 21488-76 ', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (32, N'Д16Т Прутск ткр 10', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (33, N'Д16Т Пруток ткр 110', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (34, N'Д16Т Пруток ткр 120', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (35, N'Д161 Пруток ткр 16', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (36, N'Д16Т Пруток ткр 160', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (37, N'Д16Т Пруток ткр 170', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (38, N'Д16Т Пруток ткр 200', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (39, N'Д16Т Пруток ткр 210 210', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (40, N'Д16Т Пруток ткр 220', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (41, N'Д16Т Пруток ткр 25', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (42, N'Д16Т Пруток ткр 28', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (43, N'Д16Т Пруток ткр 60', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (44, N'Д16Т Пруток ткр 70', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (45, N'Д16Т Прутск ткр 75', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (46, N'Д161 Пруток ткр 80', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (47, N'Д16Т Пруток ткр 85', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (48, N'Д16Т Пруток ткр 90', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (49, N'Д161 Пруток ткр 95', 2, 1)
INSERT [dbo].[Material] ([IdMaterial], [Наименование], [IdEdIzamer], [IdTip]) VALUES (50, N'Д16Т Ф22', 2, 1)
SET IDENTITY_INSERT [dbo].[Material] OFF
SET IDENTITY_INSERT [dbo].[Poctavzik] ON 

INSERT [dbo].[Poctavzik] ([IdPoctav], [Наименование]) VALUES (1, N'первый')
INSERT [dbo].[Poctavzik] ([IdPoctav], [Наименование]) VALUES (2, N'второй')
SET IDENTITY_INSERT [dbo].[Poctavzik] OFF
SET IDENTITY_INSERT [dbo].[PostavHaYchet] ON 

INSERT [dbo].[PostavHaYchet] ([IdPostHaYchet], [Дата], [НомерДокумента], [IdPoctav], [IdMaterial], [Цена], [Количество], [IdCtelag]) VALUES (1, CAST(N'0001-01-01' AS Date), 312414, 1, 1, CAST(0.00 AS Decimal(10, 2)), CAST(0.000 AS Decimal(10, 3)), 1)
SET IDENTITY_INSERT [dbo].[PostavHaYchet] OFF
SET IDENTITY_INSERT [dbo].[Tip] ON 

INSERT [dbo].[Tip] ([IdTip], [Наименование]) VALUES (1, N'Алюминивый прокат')
INSERT [dbo].[Tip] ([IdTip], [Наименование]) VALUES (2, N'Алюминивый профиль')
INSERT [dbo].[Tip] ([IdTip], [Наименование]) VALUES (3, N'Алюминий')
INSERT [dbo].[Tip] ([IdTip], [Наименование]) VALUES (4, N'Сталь')
SET IDENTITY_INSERT [dbo].[Tip] OFF
ALTER TABLE [dbo].[Cpisanie]  WITH CHECK ADD  CONSTRAINT [FK_Cpisanie_Ctelag] FOREIGN KEY([IdCtelag])
REFERENCES [dbo].[Ctelag] ([IdCtelag])
GO
ALTER TABLE [dbo].[Cpisanie] CHECK CONSTRAINT [FK_Cpisanie_Ctelag]
GO
ALTER TABLE [dbo].[Ctelag]  WITH CHECK ADD  CONSTRAINT [FK_Ctelag_Cklad] FOREIGN KEY([IdCklad])
REFERENCES [dbo].[Cklad] ([IdCklad])
GO
ALTER TABLE [dbo].[Ctelag] CHECK CONSTRAINT [FK_Ctelag_Cklad]
GO
ALTER TABLE [dbo].[Ctelag]  WITH CHECK ADD  CONSTRAINT [FK_Ctelag_Material] FOREIGN KEY([IdMaterial])
REFERENCES [dbo].[Material] ([IdMaterial])
GO
ALTER TABLE [dbo].[Ctelag] CHECK CONSTRAINT [FK_Ctelag_Material]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_EdIzmer] FOREIGN KEY([IdEdIzamer])
REFERENCES [dbo].[EdIzmer] ([IdEdIzamer])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_EdIzmer]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_Tip] FOREIGN KEY([IdTip])
REFERENCES [dbo].[Tip] ([IdTip])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_Tip]
GO
ALTER TABLE [dbo].[PoctavYchet]  WITH CHECK ADD  CONSTRAINT [FK_PoctavYchet_Poctavzik] FOREIGN KEY([IdPoctav])
REFERENCES [dbo].[Poctavzik] ([IdPoctav])
GO
ALTER TABLE [dbo].[PoctavYchet] CHECK CONSTRAINT [FK_PoctavYchet_Poctavzik]
GO
ALTER TABLE [dbo].[PoctavYchet]  WITH CHECK ADD  CONSTRAINT [FK_PoctavYchet_PostavHaYchet] FOREIGN KEY([IdPostHaYchet])
REFERENCES [dbo].[PostavHaYchet] ([IdPostHaYchet])
GO
ALTER TABLE [dbo].[PoctavYchet] CHECK CONSTRAINT [FK_PoctavYchet_PostavHaYchet]
GO
ALTER TABLE [dbo].[PostavHaYchet]  WITH CHECK ADD  CONSTRAINT [FK_PostavHaYchet_Ctelag] FOREIGN KEY([IdCtelag])
REFERENCES [dbo].[Ctelag] ([IdCtelag])
GO
ALTER TABLE [dbo].[PostavHaYchet] CHECK CONSTRAINT [FK_PostavHaYchet_Ctelag]
GO
ALTER TABLE [dbo].[PostavHaYchet]  WITH CHECK ADD  CONSTRAINT [FK_PostavHaYchet_Material] FOREIGN KEY([IdMaterial])
REFERENCES [dbo].[Material] ([IdMaterial])
GO
ALTER TABLE [dbo].[PostavHaYchet] CHECK CONSTRAINT [FK_PostavHaYchet_Material]
GO
USE [master]
GO
ALTER DATABASE [Diplom] SET  READ_WRITE 
GO
