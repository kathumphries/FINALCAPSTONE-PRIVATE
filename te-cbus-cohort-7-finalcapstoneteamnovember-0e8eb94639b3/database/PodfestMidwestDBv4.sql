USE [master]
GO
/****** Object:  Database [PodfestMidwestDB]    Script Date: 4/10/2019 8:18:59 PM ******/
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
USE [PodfestMidwestDB]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 4/10/2019 8:18:59 PM ******/
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
	[logo] [nvarchar](500) NULL,
	[copy] [nvarchar](max) NULL,
	[podcastURL] [nvarchar](500) NULL,
	[ticketLevel] [nvarchar](50) NULL,
	[upsaleCopy] [nvarchar](max) NULL,
	[isFinalized] [bit] NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event_Genre]    Script Date: 4/10/2019 8:18:59 PM ******/
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
/****** Object:  Table [dbo].[Event_Tag]    Script Date: 4/10/2019 8:18:59 PM ******/
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
/****** Object:  Table [dbo].[Event_Ticket]    Script Date: 4/10/2019 8:18:59 PM ******/
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
/****** Object:  Table [dbo].[Genre]    Script Date: 4/10/2019 8:18:59 PM ******/
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
/****** Object:  Table [dbo].[Podcast]    Script Date: 4/10/2019 8:18:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Podcast](
	[podcastID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[hosting] [nvarchar](50) NULL,
	[url] [nchar](100) NULL,
	[title] [nvarchar](100) NULL,
	[description] [nvarchar](max) NULL,
	[genreID] [int] NULL,
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
	[inOhio] [bit] NULL,
	[isSponsored] [bit] NULL,
 CONSTRAINT [PK_Podcast] PRIMARY KEY CLUSTERED 
(
	[podcastID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PodcasterAvailabilty]    Script Date: 4/10/2019 8:18:59 PM ******/
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
/****** Object:  Table [dbo].[Tag]    Script Date: 4/10/2019 8:18:59 PM ******/
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
/****** Object:  Table [dbo].[TIcketLevel]    Script Date: 4/10/2019 8:18:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIcketLevel](
	[ticketID] [int] IDENTITY(1,1) NOT NULL,
	[ticketType] [nvarchar](50) NOT NULL,
	[isVisible] [bit] NOT NULL,
 CONSTRAINT [PK_TIcketLevel_1] PRIMARY KEY CLUSTERED 
(
	[ticketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/10/2019 8:18:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[isAdmin] [bit] NULL,
	[isProducer] [bit] NULL,
	[phoneNumber] [nvarchar](15) NULL,
	[ticketLevel] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Event]    Script Date: 4/10/2019 8:18:59 PM ******/
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
/****** Object:  Table [dbo].[Venue]    Script Date: 4/10/2019 8:18:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venue](
	[venueID] [int] IDENTITY(1,1) NOT NULL,
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
	[venueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [logo], [copy], [podcastURL], [ticketLevel], [upsaleCopy], [isFinalized], [name]) VALUES (1, CAST(N'2019-04-05T00:00:00.000' AS DateTime), CAST(N'2019-05-09T00:00:00.000' AS DateTime), 1, 1, N'www.logo1.com', N'copy1', N'www.google.com', N'VIP', N'buy more1', 1, N'Fancy Ballroom')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [logo], [copy], [podcastURL], [ticketLevel], [upsaleCopy], [isFinalized], [name]) VALUES (2, CAST(N'2019-03-04T00:00:00.000' AS DateTime), CAST(N'2019-03-05T00:00:00.000' AS DateTime), 1, 2, N'https://rachelcorbett.com.au/wp-content/uploads/2018/07/PodSchool-Feature-Image-2-1-1.jpg', N'copy2', N'www.amazon.com', N'All', N'more', 1, N'comic room')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [logo], [copy], [podcastURL], [ticketLevel], [upsaleCopy], [isFinalized], [name]) VALUES (3, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2002-01-01T00:00:00.000' AS DateTime), NULL, NULL, N'dad', N'copy goes here', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [logo], [copy], [podcastURL], [ticketLevel], [upsaleCopy], [isFinalized], [name]) VALUES (4, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2002-01-01T00:00:00.000' AS DateTime), NULL, NULL, N'asdasdd', N'adad', N'adad', N'aadsad', N'adasdad', 1, N'test')
SET IDENTITY_INSERT [dbo].[Event] OFF
SET IDENTITY_INSERT [dbo].[Genre] ON 

INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (1, N'Arts ', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (2, N'Business ', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (3, N'Comedy ', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (4, N'Education', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (5, N'Games&Hobbies ', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (6, N'Government&Organizations ', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (7, N'Health', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (8, N'Muisc ', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (9, N'News&Politics', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (10, N'Religion&Spirituality ', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (11, N'Science&Medicine', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (12, N'Society&Culture', 1)
SET IDENTITY_INSERT [dbo].[Genre] OFF
SET IDENTITY_INSERT [dbo].[Podcast] ON 

INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [numOfEpisodes], [numOfDownloads], [measurementPlatform], [demographics], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored]) VALUES (1, 1, N'whatever', NULL, N'Cat Podcast', N'Cat', 1, 1, CAST(N'2001-04-05T00:00:00.000' AS DateTime), 1, N'1', 1, 1, 1, N'1', N'1', N'1', N'1', N'1', 1, 1)
SET IDENTITY_INSERT [dbo].[Podcast] OFF
SET IDENTITY_INSERT [dbo].[TIcketLevel] ON 

INSERT [dbo].[TIcketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (2, N'VIP', 1)
INSERT [dbo].[TIcketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (4, N'All', 1)
INSERT [dbo].[TIcketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (6, N'General Admission', 1)
INSERT [dbo].[TIcketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (7, N'Free', 1)
SET IDENTITY_INSERT [dbo].[TIcketLevel] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel]) VALUES (1, N'Bella', N'email@email.com', 0, 0, NULL, N'VIP')
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Venue] ON 

INSERT [dbo].[Venue] ([venueID], [displayName], [roomName], [buildingName], [address1], [address2], [city], [state], [zipcode], [phoneNumber], [additionalInfo], [parkingInfo], [isVisible]) VALUES (1, N'Fancy BallRoom', N'ballroom', N'B building', N'123 east easy st', NULL, N'columbus', N'Ohio', 43215, N'614 555-5555', N'parkthere', N'park here', 1)
INSERT [dbo].[Venue] ([venueID], [displayName], [roomName], [buildingName], [address1], [address2], [city], [state], [zipcode], [phoneNumber], [additionalInfo], [parkingInfo], [isVisible]) VALUES (2, N'comic store', N'comic room', N'comic building', N'111 easy st', NULL, N'dublin', N'Ohio', 45672, N'614 777-8998', N'comics', N'comics here', 1)
SET IDENTITY_INSERT [dbo].[Venue] OFF
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Venue] FOREIGN KEY([venueID])
REFERENCES [dbo].[Venue] ([venueID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Venue]
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
USE [master]
GO
ALTER DATABASE [PodfestMidwestDB] SET  READ_WRITE 
GO
