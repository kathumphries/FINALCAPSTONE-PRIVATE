USE [PodfestMidwestDB]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[Event_Tag]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[Event_Ticket]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[Genre]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[Podcast]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[PodcasterAvailabilty]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[Tag]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[TicketLevel]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[User_Event]    Script Date: 4/18/2019 1:21:35 PM ******/
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
/****** Object:  Table [dbo].[User_Ticket]    Script Date: 4/18/2019 1:21:36 PM ******/
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
/****** Object:  Table [dbo].[Venue]    Script Date: 4/18/2019 1:21:36 PM ******/
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
/****** Object:  Table [dbo].[Venue_Tag]    Script Date: 4/18/2019 1:21:36 PM ******/
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

INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (1, CAST(N'2018-05-13T14:15:00.000' AS DateTime), CAST(N'2018-05-13T15:30:00.000' AS DateTime), 2, 2, N'https://d3wo5wojvuv7l.cloudfront.net/t_square_limited_320/images.spreaker.com/original/b74dcafd3289a2bf67fbf25cfd8082c5.jpg', N'Topics range from sociopolitical, to scientific, to historical, we try to maintain variety.For mature audiences.', 1, N'All the cool kids are doing it!', 0, N'An Hour of our Time')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (2, CAST(N'2018-05-12T14:30:00.000' AS DateTime), CAST(N'2018-05-12T15:45:00.000' AS DateTime), 1, 1, N'ep-30-anti-inauguration-ft-dj-valentine-and-marquis-graves.html', N'Clarissa F, DJ Valentine and Marquis Graves discuss music, traveling, and Caucasian Americans', 2, N'', 1, N'The Final Stretch With Clarissa F')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (3, CAST(N'2019-08-22T12:30:00.000' AS DateTime), CAST(N'2019-08-22T13:30:00.000' AS DateTime), 10, 1, N'Columbus Cares.jpg', N'Columbus Cares with Travis Kendall and Katie Thomas, a podcast sharing the stories of Columbus, Ohio non-profits.

', 3, N'', 1, N'Columbus Cares')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (4, CAST(N'2019-08-23T18:00:00.000' AS DateTime), CAST(N'2019-08-23T19:00:00.000' AS DateTime), 3, 2, N'http://theconfluencecast.com/wp-content/uploads/2016/10/confluence_logo.pnghttps://d3wo5wojvuv7l.cloudfront.net/t_square_limited_320/images.spreaker.com/original/b74dcafd3289a2bf67fbf25cfd8082c5.jpg', N'The Confluence Cast, presented by Columbus Underground is a weekly, Columbus-centric podcast focusing on the civics, lifestyle, entertainment, and people of our city', 1, N'All the cool kids are doing it!', 1, N'Confulence Cast')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (5, CAST(N'2019-08-24T14:45:00.000' AS DateTime), CAST(N'2019-08-24T15:30:00.000' AS DateTime), 11, 1, N'https://images.theabcdn.com/i/26601041/2000x500/c', N'Two Dudes perspective on legal frivolity
', 1, N'All the cool kids are doing it!', 1, N'Court Appointed')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (6, CAST(N'2019-08-25T11:15:00.000' AS DateTime), CAST(N'2019-08-25T12:00:00.000' AS DateTime), 17, 2, N'https://ohiovtheworldpodcast.files.wordpress.com/2018/07/logo-text.png?w=300', N'It’s the brainchild of Columbus attorney Alex Hastie to share his love of history and his home state. ', 4, N'', 0, N'Ohio V. The World')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (7, CAST(N'2019-08-22T16:00:00.000' AS DateTime), CAST(N'2019-08-22T17:30:00.000' AS DateTime), 18, 2, N'https://www.maximumfun.org/shows/sawbones', N'Dr. Sydnee finally took a much belated trip to the dentists office and she and Justin are celebrating in the only way they know how: Digging through all the retched ways we''ve tried to fix teeth since the dawn of time.', 1, N'All the cool kids are doing it!', 1, N'Sawbones: Cavities')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (8, CAST(N'2019-08-23T13:00:00.000' AS DateTime), CAST(N'2019-08-23T14:00:00.000' AS DateTime), 19, 1, N'https://upside.fm/wp-content/uploads/2019/01/upside-iphone_crop2.png', N'upside // a podcast about startups outside of silicon valley', 2, NULL, 0, N'upside')
INSERT [dbo].[Event] ([eventID], [beginning], [ending], [podcastID], [venueID], [coverPhoto], [descriptionCopy], [ticketID], [upsaleCopy], [isFinalized], [eventName]) VALUES (9, CAST(N'2019-08-24T17:15:00.000' AS DateTime), CAST(N'2019-08-24T18:30:00.000' AS DateTime), 20, 2, NULL, N'In his revolutionary new episodic podcast, comedian and philosopher Nick Glaser explores the very limits of what spoken word entertainment can be. A rare and important existential journey into the minds of his guests, Fastcast proves that despite our differences, we are all of us similar where it counts.', 4, NULL, 1, N'Fart Cast')
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

INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (1, 1, N'iTunes', N'SoundCloud.com/thefinalstretchwithclarissaf                                                                                                                                                                                                               ', N'The Final Stretch With Clarissa F', N'The Final Stretch With Clarissa F follows me and those who inspire me as I grow up and move out of my home town', 12, NULL, CAST(N'2015-01-24T00:00:00.000' AS DateTime), NULL, N'Monthly', N'40-75 minutes', 29, 265, N'SoundCloud', N'Columbus locals 18-35', NULL, N'unknown', N'unknown', 1, 0, N'None')
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (2, 2, NULL, N'thisistheworst.com                                                                                                                                                                                                                                        ', N'An Hour of our Time', N'Three hosts spend one hour discussing their individual hours of research of a chosen topic.', 4, NULL, CAST(N'2015-07-24T00:00:00.000' AS DateTime), NULL, N'Bi-Weekly', N'Just over one hour', 6, 100, N'SoundCloud', N'20-25 year old educated liberals, largely educators.', NULL, N'unknown', N'unknown', 1, 0, N'None')
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (3, 3, N'Stitcher', N'http://theconfluencecast.com/                                                                                                                                                                                                                             ', N'Confulence Cast', N'The Confluence Cast, presented by Columbus Underground is a weekly, Columbus-centric podcast focusing on the civics, lifestyle, entertainment, and people of our city', 12, NULL, CAST(N'2016-07-24T00:00:00.000' AS DateTime), N'3 years', N'Monthly', N'60 minutes', 73, 10, NULL, N'Central Ohio Philanthropist', NULL, N'Columbus', N'Ohio', 1, 1, N'CIty of Columbus, ')
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (10, 4, N'iTunes', N'https://podcasts.apple.com/us/podcast/columbus-cares/id1299166171?mt=2                                                                                                                                                                                    ', N'Columbus Cares', N'Columbus Cares with Travis Kendall and Katie Thomas, a podcast sharing the stories of Columbus, Ohio non-profits.', 12, NULL, CAST(N'2017-09-17T00:00:00.000' AS DateTime), NULL, NULL, N'60 minutes', 6, 68, N'SoundCloud', N'Central Ohio Philanthropist', NULL, N'Columbus', N'Ohio', 1, 1, N'The Columbus Foundation')
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (11, 10, N'Spotify, iHeart, iTunes, Stitcher, Deezer
', N'https://audioboom.com/channel/court-appointed                                                                                                                                                                                                             ', N'Court Appointed', N'How can you avoid trespassing while playing Pokemon GO? Is "To Catch a Predator" entrapment? Can you use a spring gun in the fall? Lawyer Mike Meadows and his dude like brother-in-law Tommy Smirl tackle the pressing issues of the day in a legal podcast geared to educate and entertain lawyers and non-lawyers alike.', 6, NULL, CAST(N'2016-07-25T00:00:00.000' AS DateTime), N'3 years', N'Monthly', N'60 minutes', 141, 2500, NULL, N'Lawyers and Non-Lawyers', N'Audioboom', NULL, NULL, 0, 1, N'Pets')
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (17, 11, N'Stitcher', N'https://ohiovtheworldpodcast.com/                                                                                                                                                                                                                         ', N'Ohio vs. The World', N'Ohio V. The World is the only podcast in the world dedicated entirely to the history of the Buckeye State. It’s the brainchild of Columbus attorney Alex Hastie to share his love of history and his home state. ', 9, NULL, CAST(N'2016-07-25T00:00:00.000' AS DateTime), N'2.5 years', N'Monthly', N'60 minutes', 50, 625, N'SoundCloud', N'Informed Ohioans!', NULL, N'Cleveland', N'Ohio', 1, 0, N'None')
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (18, 12, N'iTunes', N'https://www.maximumfun.org/shows/sawbones                                                                                                                                                                                                                 ', N'Sawbones', N'Dr. Sydnee McElroy and her husband Justin welcome you to Sawbones: A Marital Tour of Misguided Medicine. Every Friday, they dig through the annals of medical history to uncover all the odd, weird, wrong, dumb and just gross ways we''ve tried to fix people over the years. Educational? You bet! Fun? We hope!', 7, NULL, CAST(N'2013-06-21T00:00:00.000' AS DateTime), N'6 years', N'Bi-Weekly', N'30 minutes', 271, 8675, NULL, NULL, N'MaximumFun', NULL, NULL, 0, 1, N'Cats')
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (19, 13, N'Google, ITunes', N'https://upside.fm/                                                                                                                                                                                                                                        ', N'upside', N'a podcast about startup 
investing outside of silicon valley
the startup investment landscape is changing, and world-class companies are being built outside of silicon valley.

we find them, talk with them, and discuss the upside of investing in them.', 2, NULL, CAST(N'2018-05-03T00:00:00.000' AS DateTime), N'1.5 years', N'Weekly', N'1 hour', 40, 750, NULL, N'Technology Investors', NULL, NULL, NULL, 0, 1, N'clever, PRETECKT, testcard')
INSERT [dbo].[Podcast] ([podcastID], [userID], [hosting], [url], [title], [description], [genreID], [soundByte], [originalRelease], [runTime], [releaseFrequency], [averageLength], [episodeCount], [downloadCount], [measurementPlatform], [demographic], [affiliations], [broadcastCity], [broadcastState], [inOhio], [isSponsored], [sponsor]) VALUES (20, 14, N'iTunes', N'http://squeezecast.com/category/podcasts/fartcast/                                                                                                                                                                                                        ', N'The Fart Cast', N'A rare and important existential journey into the minds of his guests, Fastcast proves that despite our differences, we are all of us similar where it counts.', 3, NULL, CAST(N'2016-12-31T00:00:00.000' AS DateTime), N'2.75 years', N'Daily', N'10 minutes', 1005, 100000, N'SoundCloud', N'Everyone!', NULL, NULL, NULL, 1, 1, N'Dogs')
SET IDENTITY_INSERT [dbo].[Podcast] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([roleID], [roleDescription], [roleName]) VALUES (1, N'SuperUser Control', N'Admin')
INSERT [dbo].[Role] ([roleID], [roleDescription], [roleName]) VALUES (2, N'Registered User, able to see schedule and favorite events', N'RegisteredUser')
INSERT [dbo].[Role] ([roleID], [roleDescription], [roleName]) VALUES (3, N'Podcaster', N'Podcaster')
INSERT [dbo].[Role] ([roleID], [roleDescription], [roleName]) VALUES (4, N'Anonymous User', N'Anonymous')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[TicketLevel] ON 

INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (1, N'VIP', 1)
INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (2, N'All', 1)
INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (3, N'General Admission', 1)
INSERT [dbo].[TicketLevel] ([ticketID], [ticketType], [isVisible]) VALUES (4, N'Free', 1)
SET IDENTITY_INSERT [dbo].[TicketLevel] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (1, N'Clarissa', N'admin@admin.com', 0, 1, N'(614) 787-7044', N'1', NULL, N'srrpvIPoRCl9xFrlkwiqrIE4ocw=', N'1', N'uAS340i2t/w=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (2, N'Joe Spurlock', N'reguser@email.com', 0, 1, N'(985) 774-6787', N'4', NULL, N'6a3S+40VZKNT1o7pdRg8BhmvBSo=', N'2', N'7i9trssjBlU=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (3, N'Tim Fulton', N'test@test.com', 1, 1, N'(614) 209-1021', N'1', NULL, N'3Zd0dNkD1NExEJm3nh390DKRZ28=', N'2', N'dwRhFiyqt90=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (4, N'Katie Thomas', N'kat@kat.com', 1, 1, NULL, N'1', NULL, N'3P0//wtV+M1sRGWOpoD/pNXOL4w=', N'1', N'jXIaP2dB/Wk=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (5, N'Kat Humphries', N'bobo@bobo.com', 1, 0, N'(614) 329-8831', N'1', N'kathumphries', N'p0HN/1rHKjlViEyxuzySfJKyV68=', N'1', N'XxZfpWONd/Q=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (6, N'Abby Bougher', N'abbybougher@gmail.com', 1, 0, N'(614) 554-2135', N'1', N'abbybougher', N'spDpdrsjlPfkG2jq43jMh5WDTpc=', N'1', N'lVNKXega3YI=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (10, N'Mike Meadows', N'mikemeadows@courtappointed.com', 0, 1, N'(212) 542-3144', N'1', N'mikemeadows', N'2JSl0fMwpt4O7+jPLT5hgerqDxU=', N'2', N'tEgGMbH2kH8=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (11, N'Alex Hastie', N'ohiovtheworld@gmail.com', 0, 1, NULL, N'3', N'alexhastie', N'7i2oHthYqe4UVNbCH9NECt8HyEM=', N'2', N'cbFbNfTYsZY=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (12, N'Dr. Sydney McElroy', N'sydneemcelroy@sawbones.com', 0, 1, N'(312) 361-0342', N'1', N'drsydney', N'xW7TInMIFgI2s+BzNiPmMaTkRww=', N'2', N'xgjBALBnr4U=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (13, N'Eric Horning', N'erichornung@upside.com', 0, 1, N'(614) 253-1174', N'2', N'erichornung', N'Ixa3GFZePm4DQohw8ZrPq6+BnrA=', N'2', N'kXdlDbV2ToU=')
INSERT [dbo].[User] ([userID], [name], [email], [isAdmin], [isProducer], [phoneNumber], [ticketLevel], [username], [password], [role], [salt]) VALUES (14, N'Richard Hess', N'richardhess@fartcast.com', 0, 1, N'(937) 456-2317', N'4', N'richardhess', N'97j/e1VHHLU/7cnjCNaS2hOaFn8=', N'2', N'P0apVHfvDaA=')
SET IDENTITY_INSERT [dbo].[User] OFF
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (1, 2)
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (2, 1)
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (4, 3)
INSERT [dbo].[User_Event] ([userID], [eventID]) VALUES (5, 3)
SET IDENTITY_INSERT [dbo].[Venue] ON 

INSERT [dbo].[Venue] ([venueID], [venueName], [address1], [city], [state], [zipCode], [phoneNumber], [additionalInfo], [parkingInfo], [isVisible]) VALUES (1, N'Gateway Film Center', N'1550 N. High Street', N'Columbus', N'Ohio', 43201, N'(614) 247-4433', N'Lively, modern space for a variety of movies & film-related events, plus an undersea-themed bar.', N'Easy, cheap parking is available
in the garage located right next door to the cinema on both 9th and 11th avenues.', 1)
INSERT [dbo].[Venue] ([venueID], [venueName], [address1], [city], [state], [zipCode], [phoneNumber], [additionalInfo], [parkingInfo], [isVisible]) VALUES (2, N'Southern Theatre', N'21 E. Main Street', N'Columbus', N'Ohio', 43215, NULL, N'The oldest surviving theatre in central Ohio and one of the oldest in the state, the Southern Theatre opened in 1896 as part of a performance space and hotel complex on the corner of High and Main Streets.', N'Columbus Commons Parking Garage', 1)
SET IDENTITY_INSERT [dbo].[Venue] OFF
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Venue] FOREIGN KEY([venueID])
REFERENCES [dbo].[Venue] ([venueID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Venue]
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
