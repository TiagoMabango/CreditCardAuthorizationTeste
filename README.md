# Credit Card Authorization Service

Este projeto é um serviço de autorização de transações com cartão de crédito, construído com .NET e Entity Framework. A solução processa transações diretamente e inclui funcionalidades de fallback e dependência do comerciante.

## Funcionalidades

- **Autorizador Simples (L1):** Processa transações usando MCC para mapear categorias de benefícios e ajustar saldos.
- **Autorizador com Fallback (L2):** Verifica a categoria CASH se o saldo da categoria mapeada não for suficiente.
- **Dependência do Comerciante (L3):** Substitui MCCs com base no nome do comerciante.

## Requisitos

- .NET 7
- SQL Server

## Instalação

1. **Clonar o Repositório**

   Clone o repositório do projeto:

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
