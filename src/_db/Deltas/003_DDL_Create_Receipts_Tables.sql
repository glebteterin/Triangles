SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Receipts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[Payer] [nvarchar](400) NOT NULL,
	[Description] [nvarchar](400) NULL,
 CONSTRAINT [PK_Receipts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReceiptRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptId] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Participant] [nvarchar](400) NOT NULL,
	[Description] [nvarchar](400) NOT NULL,
 CONSTRAINT [PK_ReceiptRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  ForeignKey [FK_Receipts_Sessions]    Script Date: 01/16/2012 10:04:33 ******/
ALTER TABLE [dbo].[Receipts]  WITH CHECK ADD  CONSTRAINT [FK_Receipts_Sessions] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receipts] CHECK CONSTRAINT [FK_Receipts_Sessions]
GO
/****** Object:  ForeignKey [FK_ReceiptRecords_Receipts]    Script Date: 01/16/2012 10:04:33 ******/
ALTER TABLE [dbo].[ReceiptRecords]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptRecords_Receipts] FOREIGN KEY([ReceiptId])
REFERENCES [dbo].[Receipts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReceiptRecords] CHECK CONSTRAINT [FK_ReceiptRecords_Receipts]
GO