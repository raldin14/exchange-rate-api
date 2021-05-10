USE [exchange-rate-db]
GO

/****** Object:  Table [dbo].[ForeignCurrencies]    Script Date: 10/5/2021 12:35:23 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ForeignCurrencies]') AND type in (N'U'))
DROP TABLE [dbo].[ForeignCurrencies]
GO

/****** Object:  Table [dbo].[ForeignCurrencies]    Script Date: 10/5/2021 12:35:23 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ForeignCurrencies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Monthly_Amount] [real] NOT NULL,
	[Iso_Code] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ForeignCurrencies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

