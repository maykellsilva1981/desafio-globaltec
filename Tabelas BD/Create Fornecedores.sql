USE [GlobalTec]
GO

/****** Object:  Table [dbo].[Fornecedores]    Script Date: 02/08/2021 08:38:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Fornecedores](
	[CodigoFornecedor] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](110) NOT NULL,
	[CpfCnpj] [varchar](14) NOT NULL,
 CONSTRAINT [PK_Fornecedores] PRIMARY KEY CLUSTERED 
(
	[CodigoFornecedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


