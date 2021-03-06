USE [InfoManage]
GO
/****** Object:  Table [dbo].[Awards]    Script Date: 2017/3/25 18:26:32 ******/
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
/****** Object:  Table [dbo].[Course]    Script Date: 2017/3/25 18:26:32 ******/
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
	[TeacherUserName] [nvarchar](50) NULL,
	[Subject] [nvarchar](50) NULL,
	[Time] [datetime] NULL,
	[TeacherName] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CourseChoose]    Script Date: 2017/3/25 18:26:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseChoose](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[StudentNO] [nvarchar](50) NOT NULL,
	[ExamTime] [datetime] NULL,
	[ExamPosition] [nvarchar](50) NULL,
	[Score] [decimal](18, 0) NULL,
	[ModifyDate] [datetime] NULL,
	[TeacherUserName] [nvarchar](50) NULL,
	[TeacherName] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 2017/3/25 18:26:32 ******/
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
/****** Object:  Table [dbo].[DepGroup]    Script Date: 2017/3/25 18:26:32 ******/
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
/****** Object:  Table [dbo].[Files]    Script Date: 2017/3/25 18:26:32 ******/
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
/****** Object:  Table [dbo].[News]    Script Date: 2017/3/25 18:26:32 ******/
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
/****** Object:  Table [dbo].[Reviews]    Script Date: 2017/3/25 18:26:32 ******/
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
/****** Object:  Table [dbo].[UserProfile]    Script Date: 2017/3/25 18:26:32 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 2017/3/25 18:26:32 ******/
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
/****** Object:  Table [dbo].[VotesList]    Script Date: 2017/3/25 18:26:32 ******/
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
/****** Object:  Table [dbo].[VotesStatic]    Script Date: 2017/3/25 18:26:32 ******/
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
