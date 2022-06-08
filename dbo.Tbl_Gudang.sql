USE [GudangDB]
GO

/****** Object:  Table [dbo].[Tbl_Gudang]    Script Date: 08/06/2022 22:12:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tbl_Gudang](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[NamaBarang] [nvarchar](50) NULL,
	[Berat(Gram)] [int] NULL,
	[Isi] [int] NULL,
	[Tempat] [nvarchar](50) NULL,
	[Kadaluarsa] [datetime] NULL,
 CONSTRAINT [PK_Tbl_Gudang] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

