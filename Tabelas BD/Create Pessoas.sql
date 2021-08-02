USE [GlobalTec]
GO

/****** Object:  Table [dbo].[Pessoas]    Script Date: 02/08/2021 08:39:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pessoas](
	[pessoaId] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](500) NOT NULL,
	[CPF] [varchar](11) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Senha] [varchar](50) NOT NULL,
	[UF] [char](2) NOT NULL,
	[DataNascimento] [date] NOT NULL,
 CONSTRAINT [PK_Pessoas] PRIMARY KEY CLUSTERED 
(
	[pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


