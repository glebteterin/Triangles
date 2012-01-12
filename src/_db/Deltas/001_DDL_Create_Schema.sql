SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[changelog](
	[change_number] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[applied_by] [varchar](100) NOT NULL,
	[description] [varchar](500) NOT NULL,
 CONSTRAINT [PK_changelog] PRIMARY KEY CLUSTERED 
(
	[change_number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 01/12/2012 19:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniqueUrl] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expenditures]    Script Date: 01/12/2012 19:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenditures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[Who] [nvarchar](400) NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Description] [nvarchar](400) NULL,
 CONSTRAINT [PK_Expenditures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Expenditures_Sessions]    Script Date: 01/12/2012 19:08:01 ******/
ALTER TABLE [dbo].[Expenditures]  WITH CHECK ADD  CONSTRAINT [FK_Expenditures_Sessions] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([Id])
GO
ALTER TABLE [dbo].[Expenditures] CHECK CONSTRAINT [FK_Expenditures_Sessions]
GO
