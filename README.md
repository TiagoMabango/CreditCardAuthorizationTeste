Credit Card Authorization Service
Este projeto é um serviço de autorização de transações com cartão de crédito, construído com .NET e Entity Framework. A solução processa transações diretamente e inclui funcionalidades de fallback e dependência do comerciante.

Funcionalidades
Autorizador Simples (L1): Processa transações usando MCC para mapear categorias de benefícios e ajustar saldos.
Autorizador com Fallback (L2): Verifica a categoria CASH se o saldo da categoria mapeada não for suficiente.
Dependência do Comerciante (L3): Substitui MCCs com base no nome do comerciante.
Requisitos
.NET 7
SQL Server
Instalação
1. Clonar o Repositório
Clone o repositório do projeto:

bash
Copy code
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio
2. Instalar Dependências
Instale as dependências do projeto:

bash
Copy code
dotnet restore
3. Configurar o Projeto
Configure a string de conexão do banco de dados no appsettings.json:

json
Copy code
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CreditCardAuthorization;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
4. Migrar o Banco de Dados
Execute as migrações do Entity Framework para configurar o banco de dados:

bash
Copy code
dotnet ef migrations add InitialCreate
dotnet ef database update
5. Executar o Projeto
Inicie o serviço:

bash
Copy code
dotnet run
API
Enviar Transação
Envia uma transação para ser processada.

Método: POST
URL: /api/transactions
Corpo da Requisição:
json
Copy code
{
  "Account": "123",
  "TotalAmount": 100.00,
  "MCC": "5811",
  "Merchant": "PADARIA DO ZE SAO PAULO BR"
}
Resposta:
json
Copy code
{
  "Code": "00"
}
Arquitetura
Serviço de Processamento de Transações (TransactionService): Processa transações, aplica lógica de fallback e atualiza saldos.
Banco de Dados: Utiliza Entity Framework para persistência de dados em um banco SQL Server.
Testes
Os testes unitários podem ser executados com o comando:

bash
Copy code
dotnet test
Os testes cobrem:

Processamento de transações com saldo suficiente.
Processamento de transações utilizando o saldo de CASH quando necessário.
Rejeição de transações sem saldo suficiente.
Contribuição
Contribuições são bem-vindas! Por favor, envie um pull request ou abra uma issue para discutir mudanças.

Licença
Este projeto está licenciado sob a MIT License.
