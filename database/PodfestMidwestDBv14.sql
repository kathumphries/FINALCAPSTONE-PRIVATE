USE [master]
GO

/****** Object:  Database [PodfestMidwestDB]    Script Date: 4/18/2019 12:36:06 PM ******/
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

ALTER DATABASE [PodfestMidwestDB] SET AUTO_CLOSE ON 
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

ALTER DATABASE [PodfestMidwestDB] SET  ENABLE_BROKER 
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

/****** Object:  Table [dbo].[Event]    Script Date: 4/18/2019 12:36:27 PM ******/
DROP TABLE [dbo].[Event]
GO

/****** Object:  Table [dbo].[Event]    Script Date: 4/18/2019 12:36:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Event](
	[eventID] [int] IDENTITY(1,1) NOT NULL,
	[beginning] [datetime] NULL,
	[ending] [datetime] NULL,
	[podcastID] [int] NULL,
	[venueID] [int] NULL,
	[coverPhoto] [nvarchar](max) NULL,
	[descriptionCopy] [nvarchar](max) NULL,
	[ticketID] [int] NULL,
	[upsaleCopy] [nvarchar](max) NULL,
	[isFinalized] [bit] NULL,
	[eventName] [nvarchar](200) NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Venue] FOREIGN KEY([venueID])
REFERENCES [dbo].[Venue] ([venueID])
GO

ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Venue]
GO

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[Event_Tag] DROP CONSTRAINT [FK_Event_Tag_Tag]
GO

ALTER TABLE [dbo].[Event_Tag] DROP CONSTRAINT [FK_Event_Tag_Event]
GO

/****** Object:  Table [dbo].[Event_Tag]    Script Date: 4/18/2019 12:36:38 PM ******/
DROP TABLE [dbo].[Event_Tag]
GO

/****** Object:  Table [dbo].[Event_Tag]    Script Date: 4/18/2019 12:36:38 PM ******/
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

ALTER TABLE [dbo].[Event_Ticket] DROP CONSTRAINT [FK_Event_Ticket_TicketLevel]
GO

ALTER TABLE [dbo].[Event_Ticket] DROP CONSTRAINT [FK_Event_Ticket_Event]
GO

/****** Object:  Table [dbo].[Event_Ticket]    Script Date: 4/18/2019 12:36:48 PM ******/
DROP TABLE [dbo].[Event_Ticket]
GO

/****** Object:  Table [dbo].[Event_Ticket]    Script Date: 4/18/2019 12:36:48 PM ******/
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

ALTER TABLE [dbo].[Event_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Event_Ticket_TicketLevel] FOREIGN KEY([ticketID])
REFERENCES [dbo].[TicketLevel] ([ticketID])
GO

ALTER TABLE [dbo].[Event_Ticket] CHECK CONSTRAINT [FK_Event_Ticket_TicketLevel]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[Genre]    Script Date: 4/18/2019 12:37:00 PM ******/
DROP TABLE [dbo].[Genre]
GO

/****** Object:  Table [dbo].[Genre]    Script Date: 4/18/2019 12:37:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Genre](
	[genreID] [int] IDENTITY(1,1) NOT NULL,
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

ALTER TABLE [dbo].[Podcast] DROP CONSTRAINT [FK_Podcast_Genre]
GO

/****** Object:  Table [dbo].[Podcast]    Script Date: 4/18/2019 12:37:30 PM ******/
DROP TABLE [dbo].[Podcast]
GO

/****** Object:  Table [dbo].[Podcast]    Script Date: 4/18/2019 12:37:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Podcast](
	[podcastID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[hosting] [nvarchar](300) NULL,
	[url] [nchar](250) NULL,
	[title] [nvarchar](300) NULL,
	[description] [nvarchar](max) NULL,
	[genreID] [int] NULL,
	[soundByte] [bit] NULL,
	[originalRelease] [datetime] NOT NULL,
	[runTime] [nvarchar](100) NULL,
	[releaseFrequency] [nvarchar](150) NULL,
	[averageLength] [nvarchar](150) NULL,
	[episodeCount] [int] NULL,
	[downloadCount] [int] NULL,
	[measurementPlatform] [nvarchar](50) NULL,
	[demographic] [nvarchar](max) NULL,
	[affiliations] [nvarchar](250) NULL,
	[broadcastCity] [nvarchar](50) NULL,
	[broadcastState] [nvarchar](50) NULL,
	[inOhio] [bit] NULL,
	[isSponsored] [bit] NULL,
	[sponsor] [nvarchar](300) NULL,
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

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[PodcasterAvailabilty] DROP CONSTRAINT [FK_PodcasterAvailabilty_User]
GO

ALTER TABLE [dbo].[PodcasterAvailabilty] DROP CONSTRAINT [FK_PodcasterAvailabilty_Event]
GO

/****** Object:  Table [dbo].[PodcasterAvailabilty]    Script Date: 4/18/2019 12:38:02 PM ******/
DROP TABLE [dbo].[PodcasterAvailabilty]
GO

/****** Object:  Table [dbo].[PodcasterAvailabilty]    Script Date: 4/18/2019 12:38:02 PM ******/
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

/****** Object:  Table [dbo].[Role]    Script Date: 4/18/2019 12:38:16 PM ******/
DROP TABLE [dbo].[Role]
GO

/****** Object:  Table [dbo].[Role]    Script Date: 4/18/2019 12:38:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Role](
	[roleID] [int] IDENTITY(1,1) NOT NULL,
	[roleDescription] [nvarchar](150) NULL,
	[roleName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[Tag] DROP CONSTRAINT [FK_Tag_Genre]
GO

/****** Object:  Table [dbo].[Tag]    Script Date: 4/18/2019 12:38:31 PM ******/
DROP TABLE [dbo].[Tag]
GO

/****** Object:  Table [dbo].[Tag]    Script Date: 4/18/2019 12:38:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tag](
	[tagID] [int] IDENTITY(1,1) NOT NULL,
	[genreID] [int] NULL,
	[ticketLevel] [nvarchar](50) NULL,
	[venueID] [int] NULL,
	[isVisible] [bit] NOT NULL,
	[tagDescription] [nvarchar](500) NULL,
 CONSTRAINT [PK_Tag_1] PRIMARY KEY CLUSTERED 
(
	[tagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tag_Genre] FOREIGN KEY([genreID])
REFERENCES [dbo].[Genre] ([genreID])
GO

ALTER TABLE [dbo].[Tag] CHECK CONSTRAINT [FK_Tag_Genre]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[TicketLevel]    Script Date: 4/18/2019 12:38:41 PM ******/
DROP TABLE [dbo].[TicketLevel]
GO

/****** Object:  Table [dbo].[TicketLevel]    Script Date: 4/18/2019 12:38:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TicketLevel](
	[ticketID] [int] IDENTITY(1,1) NOT NULL,
	[ticketType] [nvarchar](50) NOT NULL,
	[isVisible] [bit] NOT NULL,
 CONSTRAINT [PK_TIcketLevel_1] PRIMARY KEY CLUSTERED 
(
	[ticketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[User]    Script Date: 4/18/2019 12:38:52 PM ******/
DROP TABLE [dbo].[User]
GO

/****** Object:  Table [dbo].[User]    Script Date: 4/18/2019 12:38:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[email] [nvarchar](100) NOT NULL,
	[isAdmin] [bit] NULL,
	[isProducer] [bit] NULL,
	[phoneNumber] [nvarchar](15) NULL,
	[ticketLevel] [nvarchar](50) NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NOT NULL,
	[role] [nvarchar](50) NOT NULL,
	[salt] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[User_Event]    Script Date: 4/18/2019 12:39:05 PM ******/
DROP TABLE [dbo].[User_Event]
GO

/****** Object:  Table [dbo].[User_Event]    Script Date: 4/18/2019 12:39:05 PM ******/
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

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[User_Ticket] DROP CONSTRAINT [FK_User_Ticket_User]
GO

ALTER TABLE [dbo].[User_Ticket] DROP CONSTRAINT [FK_User_Ticket_TicketLevel]
GO

/****** Object:  Table [dbo].[User_Ticket]    Script Date: 4/18/2019 12:39:17 PM ******/
DROP TABLE [dbo].[User_Ticket]
GO

/****** Object:  Table [dbo].[User_Ticket]    Script Date: 4/18/2019 12:39:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User_Ticket](
	[ticketID] [int] NOT NULL,
	[userID] [int] NOT NULL,
 CONSTRAINT [PK_User_Ticket] PRIMARY KEY CLUSTERED 
(
	[ticketID] ASC,
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[User_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_User_Ticket_TicketLevel] FOREIGN KEY([ticketID])
REFERENCES [dbo].[TicketLevel] ([ticketID])
GO

ALTER TABLE [dbo].[User_Ticket] CHECK CONSTRAINT [FK_User_Ticket_TicketLevel]
GO

ALTER TABLE [dbo].[User_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_User_Ticket_User] FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO

ALTER TABLE [dbo].[User_Ticket] CHECK CONSTRAINT [FK_User_Ticket_User]
GO

USE [PodfestMidwestDB]
GO

/****** Object:  Table [dbo].[Venue]    Script Date: 4/18/2019 12:39:26 PM ******/
DROP TABLE [dbo].[Venue]
GO

/****** Object:  Table [dbo].[Venue]    Script Date: 4/18/2019 12:39:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Venue](
	[venueID] [int] IDENTITY(1,1) NOT NULL,
	[venueName] [nvarchar](100) NOT NULL,
	[address1] [nvarchar](200) NOT NULL,
	[city] [nvarchar](100) NOT NULL,
	[state] [nvarchar](50) NOT NULL,
	[zipCode] [int] NULL,
	[phoneNumber] [nvarchar](15) NULL,
	[additionalInfo] [nvarchar](500) NULL,
	[parkingInfo] [nvarchar](500) NULL,
	[isVisible] [bit] NOT NULL,
 CONSTRAINT [PK_Venue] PRIMARY KEY CLUSTERED 
(
	[venueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [PodfestMidwestDB]
GO

ALTER TABLE [dbo].[Venue_Tag] DROP CONSTRAINT [FK_Venue_Tag_Venue]
GO

ALTER TABLE [dbo].[Venue_Tag] DROP CONSTRAINT [FK_Venue_Tag_Tag]
GO

/****** Object:  Table [dbo].[Venue_Tag]    Script Date: 4/18/2019 12:39:47 PM ******/
DROP TABLE [dbo].[Venue_Tag]
GO

/****** Object:  Table [dbo].[Venue_Tag]    Script Date: 4/18/2019 12:39:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Venue_Tag](
	[venueID] [int] NOT NULL,
	[tagID] [int] NOT NULL,
 CONSTRAINT [PK_Venue_Tag] PRIMARY KEY CLUSTERED 
(
	[venueID] ASC,
	[tagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Venue_Tag]  WITH CHECK ADD  CONSTRAINT [FK_Venue_Tag_Tag] FOREIGN KEY([tagID])
REFERENCES [dbo].[Tag] ([tagID])
GO

ALTER TABLE [dbo].[Venue_Tag] CHECK CONSTRAINT [FK_Venue_Tag_Tag]
GO

ALTER TABLE [dbo].[Venue_Tag]  WITH CHECK ADD  CONSTRAINT [FK_Venue_Tag_Venue] FOREIGN KEY([venueID])
REFERENCES [dbo].[Venue] ([venueID])
GO

ALTER TABLE [dbo].[Venue_Tag] CHECK CONSTRAINT [FK_Venue_Tag_Venue]
GO


