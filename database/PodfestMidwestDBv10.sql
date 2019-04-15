USE [master]
GO
/****** Object:  Database [PodfestMidwestDB]    Script Date: 4/14/2019 10:17:58 PM ******/
CREATE DATABASE [PodfestMidwestDB]
 GO
ALTER DATABASE [PodfestMidwestDB] SET COMPATIBILITY_LEVEL = 130
GO
USE [PodfestMidwestDB]
GO
CREATE TABLE [dbo].[Event](
	[eventID] [int] IDENTITY(1,1) NOT NULL,
	[beginning] [datetime] NULL,
	[ending] [datetime] NULL,
	[podcastID] [int] NULL,
	[venueID] [int] NULL,
	[coverPhoto] [nvarchar](500) NULL,
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
/****** Object:  Table [dbo].[Event_Tag]    Script Date: 4/14/2019 10:17:58 PM ******/
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
/****** Object:  Table [dbo].[Event_Ticket]    Script Date: 4/14/2019 10:17:58 PM ******/

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
/****** Object:  Table [dbo].[Genre]    Script Date: 4/14/2019 10:17:58 PM ******/
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
/****** Object:  Table [dbo].[Podcast]    Script Date: 4/14/2019 10:17:58 PM ******/
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
	[originalRelease] [datetime] NOT NULL,
	[runTime] [nvarchar](50) NULL,
	[releaseFrequency] [nvarchar](50) NULL,
	[averageLength] [nvarchar](50) NULL,
	[episodeCount] [int] NULL,
	[downloadCount] [int] NULL,
	[measurementPlatform] [nvarchar](50) NULL,
	[demographic] [nvarchar](50) NULL,
	[affiliations] [nvarchar](500) NULL,
	[broadcastCity] [nvarchar](50) NULL,
	[broadcastState] [nvarchar](50) NULL,
	[inOhio] [bit] NULL,
	[isSponsored] [bit] NULL,
	[sponsor] [nvarchar](500) NULL,
 CONSTRAINT [PK_Podcast] PRIMARY KEY CLUSTERED 
(
	[podcastID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PodcasterAvailabilty]    Script Date: 4/14/2019 10:17:58 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 4/14/2019 10:17:58 PM ******/
CREATE TABLE [dbo].[Role](
	[roleID] [int] IDENTITY(1,1) NOT NULL,
	[roleDescription] [nvarchar](150) NULL,
	[roleName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 4/14/2019 10:17:58 PM ******/
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
/****** Object:  Table [dbo].[TicketLevel]    Script Date: 4/14/2019 10:17:58 PM ******/
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
/****** Object:  Table [dbo].[User_Ticket]    Script Date: 4/14/2019 10:17:58 PM ******/
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
/****** Object:  Table [dbo].[Venue]    Script Date: 4/14/2019 10:17:58 PM ******/
GO
CREATE TABLE [dbo].[Venue](
	[venueID] [int] IDENTITY(1,1) NOT NULL,
	[venueName] [nvarchar](100) NOT NULL,
	[address1] [nvarchar](200) NOT NULL,
	[address2] [nvarchar](200) NULL,
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
/****** Object:  Table [dbo].[Venue_Tag]    Script Date: 4/14/2019 10:17:58 PM ******/
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
SET IDENTITY_INSERT [dbo].[Podcast] OFF
SET IDENTITY_INSERT [dbo].[Genre] OFF
SET IDENTITY_INSERT [dbo].[TicketLevel] OFF
SET IDENTITY_INSERT [dbo].[Venue] OFF
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

INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (1, 1, N'iTunes', N'SoundCloud.com/thefinalstretchwithclarissaf', N'The Final Stretch With Clarissa F', N'The Final Stretch With Clarissa F follows me and those who inspire me as I grow up and move out of my home town', 12, NULL, CAST(N'2015-01-24T00:00:00.000' AS DateTime), NULL, N'Monthly', N'40-75 minutes', 29, 1000000, N'SoundCloud', N'Columbus locals 18-35', NULL, N'unknown', N'unknown', 1, 0, NULL)
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (2, 2, NULL, NULL, N'An Hour of our Time', N'Three hosts spend one hour discussing their individual hours of research of a chosen topic.', 4, NULL, CAST(N'2015-07-24T00:00:00.000' AS DateTime), NULL, N'Bi-Weekly', N'Just over one hour', 6, 100, N'SoundCloud', N'20-25 year old educated liberals, largely educators.', NULL, N'unknown', N'unknown', 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[Podcast] OFF
 
 SET IDENTITY_INSERT [dbo].[Role] ON

INSERT [dbo].[Role] ([roleID], [roleDescription], [roleName]) VALUES (1, N'SuperUser Control', N'Admin')
INSERT [dbo].[Role] ([roleID], [roleDescription], [roleName]) VALUES (2, N'Registered User, able to see schedule and favorite events', N'RegisteredUser')
INSERT [dbo].[Role] ([roleID], [roleDescription], [roleName]) VALUES (3, N'Podcaster', N'Podcaster')
INSERT [dbo].[Role] ([roleID], [roleDescription], [roleName]) VALUES (4, N'Anonymous User', N'Anonymous')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[TicketLevel]ON

INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (1, N'VIP', 1)
INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (2, N'All', 1)
INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (3, N'General Admission', 1)
INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (4, N'Free', 1)
SET IDENTITY_INSERT [dbo].[TicketLevel] OFF
SET IDENTITY_INSERT [dbo].[User] ON 


INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (1, N'Clarissa', N'admin@admin.com', 0, 1, N'(614) 787-7044', N'VIP',NULL, N'srrpvIPoRCl9xFrlkwiqrIE4ocw=', N'1', N'uAS340i2t/w=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (2, N'Joe Spurlock', N'reguser@email.com', 0, 1, N'(985) 774-6787', N'FREE',NULL, N'6a3S+40VZKNT1o7pdRg8BhmvBSo=', N'2', N'7i9trssjBlU=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (3, N'Tim Fulton', N'test@test.com', 1, 1, N'(614) 209-1021', N'VIP', NULL, N'3Zd0dNkD1NExEJm3nh390DKRZ28=', N'2', N'dwRhFiyqt90=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (4, NULL, N'kat@kat.com', NULL, NULL, NULL, NULL, NULL, N'3P0//wtV+M1sRGWOpoD/pNXOL4w=', N'2', N'jXIaP2dB/Wk=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (5, NULL, N'bobo@bobo.com', NULL, NULL, NULL, NULL, NULL, N'p0HN/1rHKjlViEyxuzySfJKyV68=', N'2', N'XxZfpWONd/Q=')
SET IDENTITY_INSERT [dbo].[User] OFF


SET IDENTITY_INSERT [dbo].[Event] ON

INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (8, 1)
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (8, 4)
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (8, 6)
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (16, 1)
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (16, 2)
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (16, 4)
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (16, 6)

SET IDENTITY_INSERT [dbo].[Event] ON





INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (1, CAST(N'2018-05-13T14:15:00.000' AS DateTime), CAST(N'2018-05-13T15:30:00.000' AS DateTime), 2, 2, NULL, N'Topics range from sociopolitical, to scientific, to historical, we try to maintain variety.For mature audiences.', 1, NULL, 0, N'An Hour of our Time')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (2, CAST(N'2018-05-12T14:30:00.000' AS DateTime), CAST(N'2018-05-12T15:45:00.000' AS DateTime), 1, 1, N'ep-30-anti-inauguration-ft-dj-valentine-and-marquis-graves.html', N'Clarissa F, DJ Valentine and Marquis Graves discuss music, traveling, and Caucasian Americans', 2, N'All the cool kids are doing it!', 1, N'The Final Stretch With Clarissa F')
SET IDENTITY_INSERT [dbo].[Event] OFF


SET IDENTITY_INSERT [dbo].[Venue] ON
INSERT [dbo].[Venue] ([venueID], [venueName], [address1], [address2], [city], [state], [zipCode], [phoneNumber], [additionalInfo], [parkingInfo], [isVisible]) VALUES (1, N'Gateway Film Center', N'1550 N. High Street', NULL, N'Columbus', N'Ohio', 43201, N'(614) 247-4433', N'Lively, modern space for a variety of movies & film-related events, plus an undersea-themed bar.', N'Easy, cheap parking is available
in the garage located right next door to the cinema on both 9th and 11th avenues.', 1)
INSERT [dbo].[Venue] ([venueID], [venueName], [address1], [address2], [city], [state], [zipCode], [phoneNumber], [additionalInfo], [parkingInfo], [isVisible]) VALUES (2, N'Southern Theatre', N'21 E. Main Street', NULL, N'Columbus', N'Ohio', 43215, N'NULL', N'The oldest surviving theatre in central Ohio and one of the oldest in the state, the Southern Theatre opened in 1896 as part of a performance space and hotel complex on the corner of High and Main Streets.', N'Columbus Commons Parking Garage', 1)
SET IDENTITY_INSERT [dbo].[Venue] OFF
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Venue] FOREIGN KEY([venueID])
REFERENCES [dbo].[Venue] ([venueID])

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
ALTER TABLE [dbo].[Event_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Event_Ticket_TicketLevel] FOREIGN KEY([ticketID])
REFERENCES [dbo].[TicketLevel] ([ticketID])
GO
ALTER TABLE [dbo].[Event_Ticket] CHECK CONSTRAINT [FK_Event_Ticket_TicketLevel]
GO
ALTER TABLE [dbo].[Podcast]  WITH CHECK ADD  CONSTRAINT [FK_Podcast_Genre] FOREIGN KEY([genreID])
REFERENCES [dbo].[Genre] ([genreID])
GO
ALTER TABLE [dbo].[Podcast] CHECK CONSTRAINT [FK_Podcast_Genre]
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
ALTER TABLE [dbo].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tag_Genre] FOREIGN KEY([genreID])
REFERENCES [dbo].[Genre] ([genreID])
GO
ALTER TABLE [dbo].[Tag] CHECK CONSTRAINT [FK_Tag_Genre]
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
USE [master]
GO
ALTER DATABASE [PodfestMidwestDB] SET  READ_WRITE 
GO
