# Desafio Globaltec - Desenvolvedor

# Autor: Maykell Ribeiro da Silva

O projeto foi desenvolvido no Visual Studio Community 2019.

# Pasta ProjetoAPi - Contém a API

# Pasta Desafio2 - Contém a API: Consulta sql solicitada na parte desafio 2 do teste

Não tinha sido solicitado, mais criei a pasta Tabelas BD com os scripts das tabelas que utilizei no desafio

# OBSERVAÇÕES

A base de dados está armazenada em um servidor compartilhado que possuo e se necessário segue os dados para conexão.

ServerName: mssql.webgyn.com.br
Login: globaltec

usuário: #M81a53y87

Base: GlobalTec

# Para autenticação inicial na API pode utilizar os dados abaixo:

e-mail: maykell.ribeiro@gmail.com
senha: 123

Por padrão quando cria uma nova pessoa a senha gerada está sendo sempre 123, porém no banco de dados armazeno ela criptografada

# String de conexão da API encontra-se no appsettings.json

# Login : 
	Para realizar o login e ter o token gerado para utilização nos demais serviços, deve selecionar o serviço post(login).
	Será retornado o número do token que deverá ser utilizado para autenticar nos demais serviços.
	Realizei os testes pelo próprio navegador utilizando a interface do Swagger.
	Para informar o token de autorização basta clicar no botão Autorize, será aberta uma modal e dentro dela digitar o seguinte comando Bearer token . 
  Ex: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik1heWtlbGwiLCJjZXJ0c2VyaWFsbnVtYmVyIjoiMiIsImVtYWlsIjoibWF5a2VsbC5yaWJlaXJvQGdtYWlsLmNvbSIsIm5iZiI6MTYyNzkzNDEyMiwiZXhwIjoxNjI4MDIwNTIyLCJpYXQiOjE2Mjc5MzQxMjJ9.m6ayn-5D2GZcJbFP1oEsNiskX4w8DoBQ8p4MM7wA4j8
			
   "token" = token gerado no serviço de login, o valor a ser copiado tem que ser somente os que estão dentro das aspas, ignorar as aspas.

  Após a autorização informando o Bearen os demais serviços estarão com autorização para serem consumidos.
  
  
