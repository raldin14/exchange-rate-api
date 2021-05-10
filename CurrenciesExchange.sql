USE [exchange-rate-db]
GO

/****** Object:  Table [dbo].[CurrenciesExchange]    Script Date: 10/5/2021 12:34:59 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrenciesExchange]') AND type in (N'U'))
DROP TABLE [dbo].[CurrenciesExchange]
GO

/****** Object:  Table [dbo].[CurrenciesExchange]    Script Date: 10/5/2021 12:34:59 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CurrenciesExchange](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ISO_Code] [nvarchar](max) NULL,
	[Purchase] [real] NOT NULL,
	[Sale] [real] NOT NULL,
	[Today_Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CurrenciesExchange] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

