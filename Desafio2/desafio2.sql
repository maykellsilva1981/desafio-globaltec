select
	ca.Numero as NumeroProcesso,
	fo.Nome as Fornecedor,	
	convert(varchar,ca.DataVencimento,103) as DataVencimento,
	convert(varchar,'',103) as DataPagamento,
	(ca.Valor + ca.Acrescimo) - ca.Desconto as ValorLiquido,
	'A PAGAR' AS situcao
from
	dbo.ContasApagar ca 
	inner join dbo.Fornecedores fo on ca.CodigoFornecedor = fo.CodigoFornecedor
	left join dbo.ContasPagas cp on ca.Numero = cp.Numero
where
	cp.numero is null
union
select
	cp.Numero as NumeroProcesso,
	fo.Nome as Fornecedor,	
	convert(varchar,cp.DataVencimento,103) as DataVencimento,
	convert(varchar,cp.DataPagamento,103),
	(cp.Valor + cp.Acrescimo) - cp.Desconto as ValorLiquido,
	'PAGA' AS situcao
from
	dbo.ContasPagas cp 
	inner join dbo.Fornecedores fo on cp.CodigoFornecedor = fo.CodigoFornecedor
	