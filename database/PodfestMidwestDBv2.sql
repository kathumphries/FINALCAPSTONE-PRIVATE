USE [master]
GO

/****** Object:  Database [PodfestMidwestDB]    Script Date: 4/10/2019 12:24:29 PM ******/
DROP DATABASE [PodfestMidwestDB]
GO

/****** Object:  Database [PodfestMidwestDB]    Script Date: 4/10/2019 12:24:29 PM ******/
CREATE DATABASE [PodfestMidwestDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PodfestMidwestDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\PodfestMidwestDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PodfestMidwestDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\PodfestMidwestDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [PodfestMidwestDB] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PodfestMidwestDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PodfestMidwestDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PodfestMidwestDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PodfestMidwestDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [PodfestMidwestDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PodfestMidwestDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [PodfestMidwestDB] SET  MULTI_USER 
GO

ALTER DATABASE [PodfestMidwestDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PodfestMidwestDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [PodfestMidwestDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [PodfestMidwestDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [PodfestMidwestDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [PodfestMidwestDB] SET QUERY_STORE = OFF
GO

USE [PodfestMidwestDB]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [PodfestMidwestDB] SET  READ_WRITE 
GO


USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Venue]
GO

/****** Object:  Table [dbo].[Event]    Script Date: 4/10/2019 12:25:02 PM ******/
DROP TABLE [dbo].[Event]
GO

/****** Object:  Table [dbo].[Event]    Script Date: 4/10/2019 12:25:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Event](
	[eventID] [int] NOT NULL,
	[beginning] [datetime] NOT NULL,
	[ending] [datetime] NOT NULL,
	[podcastID] [int] NOT NULL,
	[venueID] [int] NOT NULL,
	[logo] [nvarchar](50) NULL,
	[copy] [nvarchar](max) NULL,
	[podcastURL] [nvarchar](100) NULL,
	[ticketLevel] [nvarchar](50) NOT NULL,
	[upsaleCopy] [nvarchar](max) NULL,
	[isFinalized] [bit] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Venue] FOREIGN KEY([venueID])
REFERENCES [dbo].[Venue] ([VenueID])
GO

ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Venue]
GO

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[Event_Genre] DROP CONSTRAINT [FK_Event_Genre_Tag]
GO

ALTER TABLE [dbo].[Event_Genre] DROP CONSTRAINT [FK_Event_Genre_Genre]
GO

/****** Object:  Table [dbo].[Event_Genre]    Script Date: 4/10/2019 12:26:02 PM ******/
DROP TABLE [dbo].[Event_Genre]
GO

/****** Object:  Table [dbo].[Event_Genre]    Script Date: 4/10/2019 12:26:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Event_Genre](
	[eventID] [int] NOT NULL,
	[genreID] [int] NOT NULL,
 CONSTRAINT [PK_Event_Genre] PRIMARY KEY CLUSTERED 
(
	[eventID] ASC,
	[genreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Event_Genre]  WITH CHECK ADD  CONSTRAINT [FK_Event_Genre_Genre] FOREIGN KEY([genreID])
REFERENCES [dbo].[Genre] ([genreID])
GO

ALTER TABLE [dbo].[Event_Genre] CHECK CONSTRAINT [FK_Event_Genre_Genre]
GO

ALTER TABLE [dbo].[Event_Genre]  WITH CHECK ADD  CONSTRAINT [FK_Event_Genre_Tag] FOREIGN KEY([genreID])
REFERENCES [dbo].[Tag] ([tagID])
GO

ALTER TABLE [dbo].[Event_Genre] CHECK CONSTRAINT [FK_Event_Genre_Tag]
GO

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[Event_Tag] DROP CONSTRAINT [FK_Event_Tag_Tag]
GO

ALTER TABLE [dbo].[Event_Tag] DROP CONSTRAINT [FK_Event_Tag_Event]
GO

/****** Object:  Table [dbo].[Event_Tag]    Script Date: 4/10/2019 12:27:09 PM ******/
DROP TABLE [dbo].[Event_Tag]
GO

/****** Object:  Table [dbo].[Event_Tag]    Script Date: 4/10/2019 12:27:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Event_Tag](
	[eventID] [int] NOT NULL,
	[tagID] [int] NOT NULL,
 CONSTRAINT [PK_Event_Tag] PRIMARY KEY CLUSTERED 
(
	[eventID] ASC,
	[tagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Event_Tag]  WITH CHECK ADD  CONSTRAINT [FK_Event_Tag_Event] FOREIGN KEY([eventID])
REFERENCES [dbo].[Event] ([eventID])
GO

ALTER TABLE [dbo].[Event_Tag] CHECK CONSTRAINT [FK_Event_Tag_Event]
GO

ALTER TABLE [dbo].[Event_Tag]  WITH CHECK ADD  CONSTRAINT [FK_Event_Tag_Tag] FOREIGN KEY([tagID])
REFERENCES [dbo].[Tag] ([tagID])
GO

ALTER TABLE [dbo].[Event_Tag] CHECK CONSTRAINT [FK_Event_Tag_Tag]
GO

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[Event_Ticket] DROP CONSTRAINT [FK_Event_Ticket_TIcketLevel]
GO

ALTER TABLE [dbo].[Event_Ticket] DROP CONSTRAINT [FK_Event_Ticket_Event]
GO

/****** Object:  Table [dbo].[Event_Ticket]    Script Date: 4/10/2019 12:27:20 PM ******/
DROP TABLE [dbo].[Event_Ticket]
GO

/****** Object:  Table [dbo].[Event_Ticket]    Script Date: 4/10/2019 12:27:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Event_Ticket](
	[ticketID] [int] NOT NULL,
	[eventID] [int] NOT NULL,
 CONSTRAINT [PK_Event_Ticket] PRIMARY KEY CLUSTERED 
(
	[ticketID] ASC,
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Event_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Event_Ticket_Event] FOREIGN KEY([eventID])
REFERENCES [dbo].[Event] ([eventID])
GO

ALTER TABLE [dbo].[Event_Ticket] CHECK CONSTRAINT [FK_Event_Ticket_Event]
GO

ALTER TABLE [dbo].[Event_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Event_Ticket_TIcketLevel] FOREIGN KEY([ticketID])
REFERENCES [dbo].[TIcketLevel] ([ticketID])
GO

ALTER TABLE [dbo].[Event_Ticket] CHECK CONSTRAINT [FK_Event_Ticket_TIcketLevel]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[Genre]    Script Date: 4/10/2019 12:27:30 PM ******/
DROP TABLE [dbo].[Genre]
GO

/****** Object:  Table [dbo].[Genre]    Script Date: 4/10/2019 12:27:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Genre](
	[genreID] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[isVisible] [bit] NOT NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[genreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[Podcast] DROP CONSTRAINT [FK_Podcast_User]
GO

ALTER TABLE [dbo].[Podcast] DROP CONSTRAINT [FK_Podcast_Genre]
GO

/****** Object:  Table [dbo].[Podcast]    Script Date: 4/10/2019 12:27:40 PM ******/
DROP TABLE [dbo].[Podcast]
GO

/****** Object:  Table [dbo].[Podcast]    Script Date: 4/10/2019 12:27:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Podcast](
	[podcastID] [int] NOT NULL,
	[userID] [int] NOT NULL,
	[hosting] [nvarchar](50) NULL,
	[url] [nchar](100) NULL,
	[title] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
	[genreID] [int] NOT NULL,
	[soundByte] [bit] NULL,
	[originalRelease] [datetime] NULL,
	[runTime] [float] NULL,
	[releaseFrequency] [nvarchar](50) NULL,
	[averageLength] [float] NULL,
	[numOfEpisodes] [int] NULL,
	[numOfDownloads] [int] NULL,
	[measurementPlatform] [nvarchar](50) NULL,
	[demographics] [nvarchar](50) NULL,
	[affiliations] [nvarchar](500) NULL,
	[broadcastCity] [nvarchar](50) NULL,
	[broadcastState] [nvarchar](50) NULL,
	[inOhio] [bit] NOT NULL,
	[isSponsored] [bit] NULL,
 CONSTRAINT [PK_Podcast] PRIMARY KEY CLUSTERED 
(
	[podcastID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Podcast]  WITH CHECK ADD  CONSTRAINT [FK_Podcast_Genre] FOREIGN KEY([genreID])
REFERENCES [dbo].[Genre] ([genreID])
GO

ALTER TABLE [dbo].[Podcast] CHECK CONSTRAINT [FK_Podcast_Genre]
GO

ALTER TABLE [dbo].[Podcast]  WITH CHECK ADD  CONSTRAINT [FK_Podcast_User] FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO

ALTER TABLE [dbo].[Podcast] CHECK CONSTRAINT [FK_Podcast_User]
GO

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[PodcasterAvailabilty] DROP CONSTRAINT [FK_PodcasterAvailabilty_User]
GO

ALTER TABLE [dbo].[PodcasterAvailabilty] DROP CONSTRAINT [FK_PodcasterAvailabilty_Event]
GO

/****** Object:  Table [dbo].[PodcasterAvailabilty]    Script Date: 4/10/2019 12:27:50 PM ******/
DROP TABLE [dbo].[PodcasterAvailabilty]
GO

/****** Object:  Table [dbo].[PodcasterAvailabilty]    Script Date: 4/10/2019 12:27:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PodcasterAvailabilty](
	[userID] [int] NOT NULL,
	[eventID] [int] NOT NULL,
	[beginning] [datetime] NOT NULL,
	[ending] [datetime] NOT NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_PodcasterAvailabilty] PRIMARY KEY CLUSTERED 
(
	[userID] ASC,
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PodcasterAvailabilty]  WITH CHECK ADD  CONSTRAINT [FK_PodcasterAvailabilty_Event] FOREIGN KEY([eventID])
REFERENCES [dbo].[Event] ([eventID])
GO

ALTER TABLE [dbo].[PodcasterAvailabilty] CHECK CONSTRAINT [FK_PodcasterAvailabilty_Event]
GO

ALTER TABLE [dbo].[PodcasterAvailabilty]  WITH CHECK ADD  CONSTRAINT [FK_PodcasterAvailabilty_User] FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO

ALTER TABLE [dbo].[PodcasterAvailabilty] CHECK CONSTRAINT [FK_PodcasterAvailabilty_User]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[Tag]    Script Date: 4/10/2019 12:28:04 PM ******/
DROP TABLE [dbo].[Tag]
GO

/****** Object:  Table [dbo].[Tag]    Script Date: 4/10/2019 12:28:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tag](
	[tagID] [int] NOT NULL,
	[genreID] [int] NOT NULL,
	[ticketLevel] [nvarchar](50) NOT NULL,
	[venueID] [int] NOT NULL,
	[isVisible] [bit] NOT NULL,
	[tagDescription] [nvarchar](500) NULL,
 CONSTRAINT [PK_Tag_1] PRIMARY KEY CLUSTERED 
(
	[tagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[TIcketLevel]    Script Date: 4/10/2019 12:28:16 PM ******/
DROP TABLE [dbo].[TIcketLevel]
GO

/****** Object:  Table [dbo].[TIcketLevel]    Script Date: 4/10/2019 12:28:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TIcketLevel](
	[ticketID] [int] NOT NULL,
	[ticketLevel] [nvarchar](50) NOT NULL,
	[isVisible] [bit] NOT NULL,
 CONSTRAINT [PK_TIcketLevel_1] PRIMARY KEY CLUSTERED 
(
	[ticketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[User]    Script Date: 4/10/2019 12:28:28 PM ******/
DROP TABLE [dbo].[User]
GO

/****** Object:  Table [dbo].[User]    Script Date: 4/10/2019 12:28:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[userID] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[isAdmin] [bit] NOT NULL,
	[isProducer] [bit] NOT NULL,
	[phoneNumber] [nvarchar](15) NULL,
	[ticketLevel] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[User_Event] DROP CONSTRAINT [FK_User_Event_User]
GO

ALTER TABLE [dbo].[User_Event] DROP CONSTRAINT [FK_User_Event_Event]
GO

/****** Object:  Table [dbo].[User_Event]    Script Date: 4/10/2019 12:28:37 PM ******/
DROP TABLE [dbo].[User_Event]
GO

/****** Object:  Table [dbo].[User_Event]    Script Date: 4/10/2019 12:28:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User_Event](
	[userID] [int] NOT NULL,
	[eventID] [int] NOT NULL,
 CONSTRAINT [PK_User_Event] PRIMARY KEY CLUSTERED 
(
	[userID] ASC,
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[User_Event]  WITH CHECK ADD  CONSTRAINT [FK_User_Event_Event] FOREIGN KEY([eventID])
REFERENCES [dbo].[Event] ([eventID])
GO

ALTER TABLE [dbo].[User_Event] CHECK CONSTRAINT [FK_User_Event_Event]
GO

ALTER TABLE [dbo].[User_Event]  WITH CHECK ADD  CONSTRAINT [FK_User_Event_User] FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO

ALTER TABLE [dbo].[User_Event] CHECK CONSTRAINT [FK_User_Event_User]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[Venue]    Script Date: 4/10/2019 12:28:48 PM ******/
DROP TABLE [dbo].[Venue]
GO

/****** Object:  Table [dbo].[Venue]    Script Date: 4/10/2019 12:28:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Venue](
	[VenueID] [int] NOT NULL,
	[displayName] [nvarchar](100) NOT NULL,
	[roomName] [nvarchar](100) NULL,
	[buildingName] [nvarchar](100) NULL,
	[address1] [nvarchar](200) NOT NULL,
	[address2] [nvarchar](200) NULL,
	[city] [nvarchar](100) NOT NULL,
	[state] [nvarchar](50) NOT NULL,
	[zipcode] [int] NULL,
	[phoneNumber] [nvarchar](15) NULL,
	[additionalInfo] [nvarchar](500) NULL,
	[parkingInfo] [nvarchar](500) NULL,
	[isVisible] [bit] NOT NULL,
 CONSTRAINT [PK_Venue] PRIMARY KEY CLUSTERED 
(
	[VenueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

