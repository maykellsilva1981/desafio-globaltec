USE [GlobalTec]
GO

/****** Object:  Table [dbo].[ContasPagas]    Script Date: 02/08/2021 08:37:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContasPagas](
	[Numero] [float] NOT NULL,
	[CodigoFornecedor] [int] NOT NULL,
	[DataVencimento] [date] NOT NULL,
	[DataPagamento] [date] NOT NULL,
	[Valor] [numeric](18, 6) NOT NULL,
	[Acrescimo] [numeric](18, 6) NULL,
	[Desconto] [numeric](18, 6) NULL,
 CONSTRAINT [PK_ContasPagas] PRIMARY KEY CLUSTERED 
(
	[Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ContasPagas]  WITH CHECK ADD  CONSTRAINT [FK_ContasPagas_Fornecedores] FOREIGN KEY([CodigoFornecedor])
REFERENCES [dbo].[Fornecedores] ([CodigoFornecedor])
GO

ALTER TABLE [dbo].[ContasPagas] CHECK CONSTRAINT [FK_ContasPagas_Fornecedores]
GO


