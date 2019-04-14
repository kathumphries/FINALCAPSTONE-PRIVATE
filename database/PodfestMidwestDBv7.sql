USE [master]
GO
/****** Object:  Database [PodfestMidwestDB]    Script Date: 4/12/2019 10:48:27 AM ******/
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
/****** Object:  Table [dbo].[Event]    Script Date: 4/12/2019 10:48:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[eventID] [int] IDENTITY(1,1) NOT NULL,
	[beginning] [datetime] NOT NULL,
	[ending] [datetime] NOT NULL,
	[podcastID] [int] NOT NULL,
	[venueID] [int] NOT NULL,
	[coverPhoto] [nvarchar](500) NULL,
	[descriptionCopy] [nvarchar](max) NULL,
	[ticketID] [int] NOT NULL,
	[upsaleCopy] [nvarchar](max) NULL,
	[isFinalized] [bit] NOT NULL,
	[eventName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event_Ticket]    Script Date: 4/12/2019 10:48:27 AM ******/
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
/****** Object:  Table [dbo].[Genre]    Script Date: 4/12/2019 10:48:27 AM ******/
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
/****** Object:  Table [dbo].[Podcast]    Script Date: 4/12/2019 10:48:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Podcast](
	[podcastID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[hosting] [nvarchar](50) NULL,
	[url] [nchar](250) NULL,
	[title] [nvarchar](250) NOT NULL,
	[description] [nvarchar](max) NULL,
	[genreID] [int] NOT NULL,
	[soundByte] [nvarchar](max) NULL,
	[originalRelease] [datetime] NULL,
	[runTime] [nvarchar](50) NULL,
	[releaseFrequency] [nvarchar](50) NULL,
	[averageLength] [nvarchar](50) NULL,
	[episodeCount] [int] NULL,
	[downloadCount] [int] NULL,
	[measurementPlatform] [nvarchar](50) NULL,
	[demographic] [nvarchar](50) NULL,
	[affiliations] [nvarchar](250) NULL,
	[broadcastCity] [nvarchar](100) NOT NULL,
	[broadcastState] [nvarchar](50) NOT NULL,
	[inOhio] [bit] NULL,
	[isSponsored] [bit] NULL,
	[sponsor] [nvarchar](250) NULL,
 CONSTRAINT [PK_Podcast] PRIMARY KEY CLUSTERED 
(
	[podcastID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PodcasterAvailabilty]    Script Date: 4/12/2019 10:48:27 AM ******/
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
/****** Object:  Table [dbo].[Tag]    Script Date: 4/12/2019 10:48:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[tagID] [int] IDENTITY(1,1) NOT NULL,
	[genreID] [int] NOT NULL,
	[ticketLevel] [nvarchar](50) NOT NULL,
	[venueID] [int] NOT NULL,
	[isVisible] [bit] NOT NULL,
	[tagDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tag_1] PRIMARY KEY CLUSTERED 
(
	[tagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketLevel]    Script Date: 4/12/2019 10:48:27 AM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 4/12/2019 10:48:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](250) NOT NULL,
	[email] [nvarchar](250) NOT NULL,
	[isAdmin] [bit] NOT NULL,
	[isProducer] [bit] NOT NULL,
	[phoneNumber] [nvarchar](15) NULL,
	[ticketLevel] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Event]    Script Date: 4/12/2019 10:48:27 AM ******/
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
/****** Object:  Table [dbo].[User_Ticket]    Script Date: 4/12/2019 10:48:27 AM ******/
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
/****** Object:  Table [dbo].[Venue]    Script Date: 4/12/2019 10:48:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venue](
	[venueID] [int] IDENTITY(1,1) NOT NULL,
	[venueName] [nvarchar](100) NOT NULL,
	[address1] [nvarchar](200) NOT NULL,
	[address2] [nvarchar](200) NULL,
	[city] [nvarchar](100) NOT NULL,
	[state] [nvarchar](50) NOT NULL,
	[zipCode] [int] NOT NULL,
	[phoneNumber] [nvarchar](25) NULL,
	[additionalInfo] [nvarchar](max) NULL,
	[parkingInfo] [nvarchar](max) NULL,
	[isVisible] [bit] NOT NULL,
 CONSTRAINT [PK_Venue] PRIMARY KEY CLUSTERED 
(
	[venueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venue_Tag]    Script Date: 4/12/2019 10:48:27 AM ******/
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
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (1, CAST(N'2018-05-13T14:15:00.000' AS DateTime), CAST(N'2018-05-13T015:30:00.000' AS DateTime), 2, 2, NULL, N'In An Hour of our Time Podcast, we choose a new topic each episode. Topics range from sociopolitical, to scientific, to historical, we try to maintain variety. We then each perform an hour of research followed by a marginally focused one hour discussion. Featuring: Joe Wallace, Dave Buker, and Mark Cracraft. 
Fun for anyone who enjoys funny but sometimes frustrating tangents of referential conversation. For mature audiences.', 4, NULL, 0, N'An Hour of our Time')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (2, CAST(N'2018-05-12T14:30:00.000' AS DateTime), CAST(N'2018-05-12T15:45:00.000' AS DateTime), 1, 1, N'ep-30-anti-inauguration-ft-dj-valentine-and-marquis-graves.html', N'Join me, I Am Clarissa F, DJ Valentine and Marquis Graves as we discuss music, traveling, Caucasian Americans and most importantly: Donald Trump. It is one of my favorite and arguable one of our most candid. Tune in and enjoy the show!', 2, N'All the cool kids are doing it!', 1, N'The Final Stretch With Clarissa F')
SET IDENTITY_INSERT [dbo].[Event] OFF
SET IDENTITY_INSERT [dbo].[Genre] ON 

INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (1, N'Arts', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (2, N'Business', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (3, N'Comedy', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (4, N'Education', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (5, N'Games & Hobbies', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (6, N'Government & Organizations', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (7, N'Health', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (8, N'Muisc', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (9, N'News & Politics', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (10, N'Religion & Spirituality ', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (11, N'Science & Medicine', 1)
INSERT [dbo].[Genre] ([genreID], [name], [isVisible]) VALUES (12, N'Society & Culture', 1)
SET IDENTITY_INSERT [dbo].[Genre] OFF
SET IDENTITY_INSERT [dbo].[Podcast] ON 

INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (1, 1, N'iTunes', 'SoundCloud.com/thefinalstretchwithclarissaf', N'The Final Stretch With Clarissa F', N'The Final Stretch With Clarissa F follows me and those who inspire me as I grow up and move out of my home town', 12, NULL, CAST(N'2015-1-24T00:00:00.000' AS DateTime), N NULL, N'Monthly', N'40-75 minutes', 29, NULL, N'Sound Cloud', N'Columbus locals 18-35', N'0', N'unknown', N'unknown', 1, 0, NULL)
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (2, 2, NULL, NULL, N'An Hour of our Time', N'Three hosts spend one hour discussing their individual hours of research of a chosen topic.', 4, NULL, NULL, NULL, N'Bi-Weekly', N'Just over one hour', 6, 100, N'SoundCloud', N'20-25 year old educated liberals, largely educators.', N'0', N'unknown', N'unknown', 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[Podcast] OFF
SET IDENTITY_INSERT [dbo].[TicketLevel] ON 

INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (1, N'VIP', 1)
INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (2, N'All', 1)
INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (3, N'General Admission', 1)
INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (4, N'Free', 1)
SET IDENTITY_INSERT [dbo].[TicketLevel] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel]) VALUES (1, N'Clarissa', N'iamclarissaf@gmail.com', 0, 1, (614) 787-7044, N'VIP')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel]) VALUES (2, N'Joe Spurlock', N'joseph.spurlock@gmail.com', 0, 1, (985) 774-6787, N'Free')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel]) VALUES (3, N'Tim Fulton', N'tim@timfulton.com', 1, 1, (614) 209-1021, N'VIP')
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Venue] ON 

INSERT [dbo].[Venue] ([venueID], [venueName], [address1], [address2], [city], [state], [zipCode], [phoneNumber], [additionalInfo], [parkingInfo], [isVisible]) VALUES (1, N'Gateway Film Center', N'1550 N. High Street', NULL, N'Columbus', N'Ohio', 43201, N'(614) 247-4433', N'Lively, modern space for a variety of movies & film-related events, plus an undersea-themed bar.', N'Easy, cheap parking is available
in the garage located right next door to the cinema on both 9th and 11th avenues.', 1)
INSERT [dbo].[Venue] ([venueID], [venueName], [address1], [address2], [city], [state], [zipCode], [phoneNumber], [additionalInfo], [parkingInfo], [isVisible]) VALUES (2, N'Southern Theatre', N'21 E. Main Street', NULL, N'Columbus', N'Ohio', 43215, N'NULL', N'The oldest surviving theatre in central Ohio and one of the oldest in the state, the Southern Theatre opened in 1896 as part of a performance space and hotel complex on the corner of High and Main Streets.', N'Columbus Commons Parking Garage', 1)
SET IDENTITY_INSERT [dbo].[Venue] OFF
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Venue] FOREIGN KEY([venueID])
REFERENCES [dbo].[Venue] ([venueID])