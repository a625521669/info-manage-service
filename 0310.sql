USE [InfoManage]
GO
/****** Object:  Table [dbo].[Awards]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Awards](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[NO] [nvarchar](50) NOT NULL,
	[PubDate] [datetime] NULL,
	[HasAttacher] [bit] NULL,
	[Detail] [nvarchar](500) NULL,
 CONSTRAINT [PK_Awards] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Course]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[ID] [int] IDENTITY(10000,1) NOT NULL,
	[CourseName] [nvarchar](50) NULL,
	[Contents] [nvarchar](500) NULL,
	[Credit] [decimal](18, 0) NULL,
	[QuantityLimit] [int] NULL,
	[ExamTime] [datetime] NULL,
	[ExamPosition] [nvarchar](50) NULL,
	[TeacherUserName] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[ID] [int] IDENTITY(10000,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Secretery] [nvarchar](50) NULL,
	[Levels] [int] NULL,
	[Leader] [nvarchar](50) NULL,
	[PubDate] [datetime] NULL,
	[Contacts] [nvarchar](50) NULL,
	[ContatctsPhone] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DepGroup]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepGroup](
	[ID] [int] IDENTITY(10000,1) NOT NULL,
	[OwnDep] [int] NOT NULL,
	[Leader] [nvarchar](50) NULL,
	[Member] [nvarchar](500) NULL,
	[Detail] [nvarchar](500) NULL,
	[Name] [nvarchar](50) NULL,
	[PubDate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Files]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[ID] [int] IDENTITY(10000,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Signature] [nvarchar](50) NULL,
	[PageCount] [int] NULL,
	[HasAttacher] [bit] NULL CONSTRAINT [DF_Files_HasAttacher]  DEFAULT ((0)),
	[Url] [nvarchar](500) NULL,
	[PubDate] [datetime] NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](500) NULL,
	[PubDate] [datetime] NULL,
	[Author] [nvarchar](50) NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TeacherUserName] [nvarchar](50) NULL,
	[TeacherName] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
	[Contents] [nvarchar](500) NULL,
	[Reviews] [nvarchar](500) NULL,
	[UserName] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserID] [nvarchar](50) NOT NULL,
	[NO] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Sex] [nvarchar](50) NULL,
	[Nation] [nvarchar](50) NULL,
	[BorthDate] [datetime] NULL,
	[Display] [nvarchar](50) NULL,
	[Origin] [nvarchar](50) NULL,
	[Education] [nvarchar](50) NULL,
	[Marry] [bit] NULL,
	[OwnPart] [nvarchar](50) NULL,
	[OwnGroup] [nvarchar](50) NULL,
	[Job] [nvarchar](50) NULL,
	[PoliceNO] [nvarchar](50) NULL,
	[SoldierNO] [nvarchar](50) NULL,
	[StudentNo] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
	[JoinGroupDate] [datetime] NULL,
	[ApplyDate] [datetime] NULL,
	[JoinPartDate] [datetime] NULL,
	[BeRegularDate] [datetime] NULL,
	[Address] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL CONSTRAINT [DF_Users_Password]  DEFAULT ((123456)),
	[Pemission] [int] NULL CONSTRAINT [DF_Users_Pemission]  DEFAULT ((3)),
	[Disabled] [bit] NULL CONSTRAINT [DF_Users_Disabled]  DEFAULT ((0)),
	[CreateOn] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VotesList]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VotesList](
	[ID] [int] IDENTITY(5000,1) NOT NULL,
	[NO] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Detail] [nvarchar](500) NULL,
	[PubDate] [datetime] NULL,
	[BeginDate] [datetime] NULL,
	[EndDate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VotesStatic]    Script Date: 2017/3/10 18:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VotesStatic](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](50) NOT NULL,
	[VoteID] [int] NOT NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Awards] ON 

INSERT [dbo].[Awards] ([ID], [Type], [NO], [PubDate], [HasAttacher], [Detail]) VALUES (6, N'处分', N'XX004', CAST(N'2017-01-28 18:06:18.693' AS DateTime), 0, N'不是一个好人')
INSERT [dbo].[Awards] ([ID], [Type], [NO], [PubDate], [HasAttacher], [Detail]) VALUES (7, N'优秀工作者', N'XX003', CAST(N'2017-01-28 18:06:25.783' AS DateTime), 0, N'是一个好人')
INSERT [dbo].[Awards] ([ID], [Type], [NO], [PubDate], [HasAttacher], [Detail]) VALUES (8, N'优秀党员', N'XX003', CAST(N'2017-01-28 21:31:33.697' AS DateTime), 0, N'BBBKKKKKK')
INSERT [dbo].[Awards] ([ID], [Type], [NO], [PubDate], [HasAttacher], [Detail]) VALUES (1000, N'优秀党员', N'XX003', CAST(N'2017-01-28 21:31:55.967' AS DateTime), 0, N'ssss')
SET IDENTITY_INSERT [dbo].[Awards] OFF
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([ID], [Name], [Secretery], [Levels], [Leader], [PubDate], [Contacts], [ContatctsPhone]) VALUES (10000, N'77777777777777', N'33', 3, N'777', CAST(N'2017-02-07 20:46:03.790' AS DateTime), N'333', N'333')
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[DepGroup] ON 

INSERT [dbo].[DepGroup] ([ID], [OwnDep], [Leader], [Member], [Detail], [Name], [PubDate]) VALUES (10000, 0, N'222', N'222', N'22', N'222', CAST(N'2017-02-08 00:17:42.213' AS DateTime))
INSERT [dbo].[DepGroup] ([ID], [OwnDep], [Leader], [Member], [Detail], [Name], [PubDate]) VALUES (10001, 0, N'AAA', N'SSS', N'DDDD', N'QQQ', CAST(N'2017-02-08 00:20:27.100' AS DateTime))
INSERT [dbo].[DepGroup] ([ID], [OwnDep], [Leader], [Member], [Detail], [Name], [PubDate]) VALUES (10002, 0, N'DDD', N'AAA', N'EEE', N'VVV', CAST(N'2017-02-08 00:20:50.313' AS DateTime))
INSERT [dbo].[DepGroup] ([ID], [OwnDep], [Leader], [Member], [Detail], [Name], [PubDate]) VALUES (10003, 10000, N'666', N'666', N'66', N'666', CAST(N'2017-02-08 00:30:02.463' AS DateTime))
SET IDENTITY_INSERT [dbo].[DepGroup] OFF
SET IDENTITY_INSERT [dbo].[Files] ON 

INSERT [dbo].[Files] ([ID], [Type], [Name], [Signature], [PageCount], [HasAttacher], [Url], [PubDate]) VALUES (10000, N'档案', N'毕业设计文档', N'优雅的‘', NULL, 0, N'/uploads/files/31231640997.基于ASP.NET的军校党务信息管理系统.doc', CAST(N'2017-01-31 23:16:41.003' AS DateTime))
SET IDENTITY_INSERT [dbo].[Files] OFF
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([ID], [Title], [Type], [Detail], [PubDate], [Author]) VALUES (1, N'放假啦', N'新闻', N'今天开始放假11111', CAST(N'2017-01-28 21:19:43.453' AS DateTime), N'0')
INSERT [dbo].[News] ([ID], [Title], [Type], [Detail], [PubDate], [Author]) VALUES (2, N'放假啦2', N'通知', N'今天开始放假11111', CAST(N'2017-01-28 21:19:53.833' AS DateTime), N'0')
INSERT [dbo].[News] ([ID], [Title], [Type], [Detail], [PubDate], [Author]) VALUES (3, N'放假啦3', N'公告', N'今天开始放假11111日日日日日日日', CAST(N'2017-01-28 21:19:58.673' AS DateTime), N'0')
INSERT [dbo].[News] ([ID], [Title], [Type], [Detail], [PubDate], [Author]) VALUES (5, N'放假啦334', N'公告', N'今天开始放假11111', CAST(N'2017-01-28 21:20:05.333' AS DateTime), N'0')
INSERT [dbo].[News] ([ID], [Title], [Type], [Detail], [PubDate], [Author]) VALUES (8, N'444444444', N'公开课', N'4444444', CAST(N'2017-03-08 17:58:03.463' AS DateTime), N'A201740')
SET IDENTITY_INSERT [dbo].[News] OFF
SET IDENTITY_INSERT [dbo].[Reviews] ON 

INSERT [dbo].[Reviews] ([ID], [TeacherUserName], [TeacherName], [CreateOn], [Contents], [Reviews], [UserName], [Name]) VALUES (2, N'A201740', NULL, CAST(N'2017-03-10 12:14:25.377' AS DateTime), N'666666666', N'444444444444444', N'A201740', NULL)
SET IDENTITY_INSERT [dbo].[Reviews] OFF
INSERT [dbo].[UserProfile] ([UserID], [NO], [Name], [Sex], [Nation], [BorthDate], [Display], [Origin], [Education], [Marry], [OwnPart], [OwnGroup], [Job], [PoliceNO], [SoldierNO], [StudentNo], [CreateOn], [JoinGroupDate], [ApplyDate], [JoinPartDate], [BeRegularDate], [Address], [Phone], [Type], [UserName]) VALUES (N'c9c36f1c-7157-4b26-a52d-fd044dc737b1', NULL, N'里斯', N'男', NULL, CAST(N'2017-03-19 00:00:00.000' AS DateTime), N'群众', NULL, NULL, NULL, N'XX3334', NULL, NULL, NULL, NULL, NULL, CAST(N'2017-03-06 18:06:24.000' AS DateTime), NULL, NULL, NULL, NULL, N'南京2', N'18888888888', N'教师', N'T201741')
INSERT [dbo].[UserProfile] ([UserID], [NO], [Name], [Sex], [Nation], [BorthDate], [Display], [Origin], [Education], [Marry], [OwnPart], [OwnGroup], [Job], [PoliceNO], [SoldierNO], [StudentNo], [CreateOn], [JoinGroupDate], [ApplyDate], [JoinPartDate], [BeRegularDate], [Address], [Phone], [Type], [UserName]) VALUES (N'd5ea53c8-bb00-4a36-bb14-886e727a67fa', NULL, N'黄傻', N'女', NULL, CAST(N'2017-03-17 00:00:00.000' AS DateTime), N'团员', NULL, NULL, NULL, N'XX007test', NULL, NULL, NULL, NULL, NULL, CAST(N'2017-03-06 18:02:57.000' AS DateTime), NULL, NULL, NULL, NULL, N'上海', N'1943345345345', N'学生', N'A201740')
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [UserID], [UserName], [Password], [Pemission], [Disabled], [CreateOn]) VALUES (41, N'd5ea53c8-bb00-4a36-bb14-886e727a67fa', N'A201740', N'123456', 3, 0, CAST(N'2017-03-06 18:02:57.447' AS DateTime))
INSERT [dbo].[Users] ([ID], [UserID], [UserName], [Password], [Pemission], [Disabled], [CreateOn]) VALUES (42, N'c9c36f1c-7157-4b26-a52d-fd044dc737b1', N'T201741', N'123456', 2, 0, CAST(N'2017-03-06 18:06:24.313' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[VotesList] ON 

INSERT [dbo].[VotesList] ([ID], [NO], [Name], [Title], [Type], [Detail], [PubDate], [BeginDate], [EndDate]) VALUES (5000, N'XX222', N'222', N'222', N'222', N'222', CAST(N'2011-02-02 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[VotesList] ([ID], [NO], [Name], [Title], [Type], [Detail], [PubDate], [BeginDate], [EndDate]) VALUES (5002, N'XX003', N'liyibb', N'竞选领导', N'换届选举', N'竞选竞选竞选竞选竞选竞选竞选竞选', CAST(N'2017-01-30 13:24:52.703' AS DateTime), CAST(N'2017-01-14 00:00:00.000' AS DateTime), CAST(N'2017-02-25 00:00:00.000' AS DateTime))
INSERT [dbo].[VotesList] ([ID], [NO], [Name], [Title], [Type], [Detail], [PubDate], [BeginDate], [EndDate]) VALUES (5003, N'XX003', N'liyibb', N'竞选司令', N'换届选举', N'竞选竞选竞选竞选竞选竞选竞选竞选', CAST(N'2017-01-30 13:25:03.670' AS DateTime), CAST(N'2013-01-13 00:00:00.000' AS DateTime), CAST(N'2017-02-27 00:00:00.000' AS DateTime))
INSERT [dbo].[VotesList] ([ID], [NO], [Name], [Title], [Type], [Detail], [PubDate], [BeginDate], [EndDate]) VALUES (5004, N'XX003', N'liyibb', N'评选三号学生', N'年终考核', N'3333333333333', CAST(N'2017-01-30 13:28:26.287' AS DateTime), CAST(N'2017-01-14 00:00:00.000' AS DateTime), CAST(N'2017-01-15 00:00:00.000' AS DateTime))
INSERT [dbo].[VotesList] ([ID], [NO], [Name], [Title], [Type], [Detail], [PubDate], [BeginDate], [EndDate]) VALUES (5006, N'XX001', N'zhangyiqun', N'评选三号学生', N'年终考核', N'3333333333333', CAST(N'2017-01-30 13:29:06.163' AS DateTime), CAST(N'2019-01-29 00:00:00.000' AS DateTime), CAST(N'2020-01-15 00:00:00.000' AS DateTime))
INSERT [dbo].[VotesList] ([ID], [NO], [Name], [Title], [Type], [Detail], [PubDate], [BeginDate], [EndDate]) VALUES (5007, N'XX002', N'liyi', N'评选三号学生', N'年终考核', N'3333333333333', CAST(N'2017-01-30 13:29:09.473' AS DateTime), CAST(N'2019-01-29 00:00:00.000' AS DateTime), CAST(N'2020-01-15 00:00:00.000' AS DateTime))
INSERT [dbo].[VotesList] ([ID], [NO], [Name], [Title], [Type], [Detail], [PubDate], [BeginDate], [EndDate]) VALUES (5005, N'XX004', N'bbv', N'评选三号学生', N'评优评先', N'3333333333333', CAST(N'2017-01-30 13:28:52.140' AS DateTime), CAST(N'2019-01-29 00:00:00.000' AS DateTime), CAST(N'2020-01-15 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[VotesList] OFF
SET IDENTITY_INSERT [dbo].[VotesStatic] ON 

INSERT [dbo].[VotesStatic] ([ID], [UserID], [VoteID]) VALUES (2, N'b0113ef5-bef3-4aa7-8536-c60c59cba399', 5001)
INSERT [dbo].[VotesStatic] ([ID], [UserID], [VoteID]) VALUES (3, N'b0113ef5-bef3-4aa7-8536-c60c59cba399', 5002)
INSERT [dbo].[VotesStatic] ([ID], [UserID], [VoteID]) VALUES (4, N'b0113ef5-bef3-4aa7-8536-c60c59cba399', 5003)
INSERT [dbo].[VotesStatic] ([ID], [UserID], [VoteID]) VALUES (5, N'b0113ef5-bef3-4aa7-8536-c60c59cba399', 5007)
INSERT [dbo].[VotesStatic] ([ID], [UserID], [VoteID]) VALUES (6, N'D', 5003)
INSERT [dbo].[VotesStatic] ([ID], [UserID], [VoteID]) VALUES (8, N'D', 5003)
INSERT [dbo].[VotesStatic] ([ID], [UserID], [VoteID]) VALUES (9, N'D', 5003)
SET IDENTITY_INSERT [dbo].[VotesStatic] OFF
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'民族' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserProfile', @level2type=N'COLUMN',@level2name=N'Nation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型：0预备党员，1党员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserProfile', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限：1学生 2教师 3 管理员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Pemission'
GO
