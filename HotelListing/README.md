Ol� :)

Aqui vamos ter alguns passosos quais foram utilizados para desenvolver esta API!

Disclaimer: os commits v�o ter o n�mero da se��o seguidos da aula, ficando com esta conven��o:
S1A1 -> Sec��o 1 Aula 1

1- Quando criamos esta API, no �ltimo passo, habilitamos a checkbox "Enable Open API Support" de forma
a incluir, logo na cria��o da API, que esta viesse logo com o Swahsbuckle, aka Swagger j� instalado

2- instalamos o package "Serilog.AspNetCore" para os logs da API
O Serilog � definido na classe Pragram.cs, onde definimos aqui qual a classe de logs que a API vai
utilizar 

3- Definimos o Swagger (este j� veio imbutido na API) ->Startup.cs

4- Definimos o Cors ->Startup.cs

5- Criamos a classe DatabaseContext (vai criar a liga��o � base de dados)

6- instalamos os seguintes packages:
	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools

7- depois vamos ao appsettings.json e inclu�mos a connection string para criar a liga��o � nossa base de dados
	-> colocamos "server=(localdb)\\mssqllocaldb" para apontar para a base de dados local do VS, caso queira ligar a outra
		BD s� temos de colocar a inst�ncia dessa base de dados

8- depois temos de criar as classes para as tabelas, temos o exemplo de Hotel e Country

9- Para criarmos as bases de dados temos de criar uma migration, para isso temos de aceder ao 
Package Manager Console e depois correr o seguinte programa para criar uma migration:

Add-Migration NomeMigrationTudoJunto -> aqui adicionamos a migration
Update-Database -> aqui fazemos o update da base de dados (apenas fazemos o update depois de adicionarmos as migrations)

A base de dados ir� ficar dispon�vel depois de correr o update-database, que est� na esquerda, na tab
"SQL Server Object Explorer"

Se quiser remover a migration, tenho de fazer: Remove-Migration
Mas para remover a migration, n�o posso ter feito o update-database

10- De seguida criamos o IRepository e o Repository, onde vamos utiliar DI

11- de Seguida criamos o IUnitOfWork e o UnitOfWork

12- o pr�ximo passo � criar as DTOs para os pa�ses e para os hot�is

13- Instalamos o package AutoMapper.Extensions.Microsoft.DependencyInjection 
depois temos de colocar no startup para que seja logo inicializado quando a API iniciar

