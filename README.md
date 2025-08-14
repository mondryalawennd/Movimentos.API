# 📦 Movimentos API

API para gerenciamento de **Movimentos Manuais Digitados**, permitindo **inserção** e **consulta** filtrada por Mês, Ano e Produto.  
O sistema mantém controle de **número sequencial de lançamentos** e integra dados das tabelas **Produto** e **Produto_Cosif**.

---

## 🚀 Tecnologias Utilizadas
- **.NET 8** – API REST com ASP.NET Core ([link](https://dotnet.microsoft.com/))
- **Entity Framework Core** – Acesso e persistência de dados ([link](https://learn.microsoft.com/ef/core))
- **SQL Server** – Banco de dados relacional ([link](https://www.microsoft.com/sql-server))
- **LINQ** – Consultas tipadas em C# ([link](https://learn.microsoft.com/dotnet/csharp/programming-guide/concepts/linq/))
- **FluentValidation** – Validação de dados de entrada ([link](https://fluentvalidation.net/))
- **Serilog** – Logging estruturado ([link](https://serilog.net/))
- **Injeção de Dependência** – Nativa do ASP.NET Core
- **Swagger** – Documentação da API

---

## 📋 Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/sql-server)
- Visual Studio 2022 ou VS Code

---

## ✅ Boas práticas aplicadas
- **SOLID** e **Clean Code**
- Separação de responsabilidades
- **DRY** (Don't Repeat Yourself)
- Validação centralizada
- Logs estruturados
- Tratamento de exceções no Service e Controller

---

## 🏛 Arquitetura

A aplicação segue **arquitetura em camadas (Layered Architecture)**, separando responsabilidades de forma clara:  
**Controller → Service → Repository**

### 📂 Estrutura do Projeto
  Movimentos.API/            # Controllers e Startup
  Movimentos.Business/       # Regras de negócio (Services)
  Movimentos.Data/           # Acesso a dados (Repositories, Context)
  Movimentos.CrossCutting/   # Utilitários e serviços transversais
  Movimentos.Entities/       # Entidades e DTOs


**Descrição das camadas:**
- **Movimentos.API**: Exposição dos endpoints HTTP, validações iniciais e orquestração das chamadas para os services.  
- **Movimentos.Business**: Contém a lógica de negócio e coordena as operações.  
- **Movimentos.Data**: Responsável pela persistência e consultas no banco de dados.  
- **Movimentos.CrossCutting**: Funcionalidades reutilizáveis (ex: logging, autenticação).  
- **Movimentos.Entities**: Estruturas de dados (entidades e DTOs) utilizadas pelo sistema.  

> Estrutura preparada para evoluir para **DDD (Domain-Driven Design)** caso necessário.

---

## 📋 Funcionalidades
- Inserção de movimentos manuais digitados para um determinado mês e ano.
- Consulta de movimentos utilizando **Stored Procedure**.
- Seleção de produtos e códigos COSIF via combobox.
- Geração automática do número de lançamento por mês/ano.
- Validação com **FluentValidation**.
- Autenticação via **HttpContext** (usuário logado ou `SISTEMA` como padrão).
- Logs estruturados com **Serilog**.

---

## ⚙️ Configuração e Execução

## 1️⃣ Clonar o repositório
git clone https://github.com/mondryalawennd/Movimentos.API.git
cd Movimentos.API

## 🚀 Como Executar
## 2️⃣ Criar Banco de dados e Tabelas
Como o projeto **não utiliza migrations**, você precisa criar o banco manualmente no SQL Server.

## 🗄️Estrutura do Banco de dados 
O sistema utiliza **SQL Server** e as tabelas devem ser criadas manualmente com o script abaixo (sem migrations).
⚠️ Certifique-se que o usuário configurado no appsettings.json tenha permissão de leitura e escrita no banco.

```sql
CREATE DATABASE MovimentosManuais;
GO

-- ==============================
-- TABELA PRODUTO
-- ==============================
CREATE TABLE PRODUTO (
    COD_PRODUTO   CHAR(4)      NOT NULL, -- Código do Produto
    DES_PRODUTO   VARCHAR(30)  NULL,     -- Descrição do Produto
    STA_STATUS    CHAR(1)      NULL,     -- Status: (A) Ativo, (I) Inativo
    CONSTRAINT PK_PRODUTO PRIMARY KEY (COD_PRODUTO)
);
GO

-- ==============================
-- TABELA PRODUTO_COSIF
-- ==============================
CREATE TABLE PRODUTO_COSIF (
    COD_PRODUTO       CHAR(4)      NOT NULL, -- Código do Produto
    COD_COSIF         VARCHAR(11)  NOT NULL, -- Código COSIF Analítico
    COD_CLASSIFICACAO CHAR(6)      NULL,     -- Classificação da Conta COSIF: Normal / MTM
    STA_STATUS        CHAR(1)      NULL,     -- Status: (A) Ativo, (I) Inativo
    CONSTRAINT PK_PRODUTO_COSIF PRIMARY KEY (COD_PRODUTO, COD_COSIF),
    CONSTRAINT FK_PRODUTO_COSIF_PRODUTO FOREIGN KEY (COD_PRODUTO)
        REFERENCES PRODUTO(COD_PRODUTO)
);
GO

-- ==============================
-- TABELA MOVIMENTO_MANUAL
-- ==============================
CREATE TABLE MOVIMENTO_MANUAL (
    DAT_MES         INT           NOT NULL, -- Mês do Movimento
    DAT_ANO         INT           NOT NULL, -- Ano do Movimento
    NUM_LANCAMENTO  BIGINT        NOT NULL, -- Número do Lançamento no mês/ano
    COD_PRODUTO     CHAR(4)       NOT NULL, -- Código do Produto
    COD_COSIF       VARCHAR(11)   NOT NULL, -- Código COSIF Analítico
    VAL_VALOR       DECIMAL(18,2) NOT NULL, -- Valor do Movimento
    DES_DESCRICAO   VARCHAR(50)   NOT NULL, -- Descrição do Lançamento
    DAT_MOVIMENTO   SMALLDATETIME NOT NULL, -- Data atualização do registro
    COD_USUARIO     VARCHAR(15)   NOT NULL, -- Usuário que fez o movimento
    CONSTRAINT PK_MOVIMENTO_MANUAL PRIMARY KEY (DAT_MES, DAT_ANO, NUM_LANCAMENTO),
    CONSTRAINT FK_MOVIMENTO_PRODUTO FOREIGN KEY (COD_PRODUTO)
        REFERENCES PRODUTO(COD_PRODUTO),
    CONSTRAINT FK_MOVIMENTO_PRODUTO_COSIF FOREIGN KEY (COD_PRODUTO, COD_COSIF)
        REFERENCES PRODUTO_COSIF(COD_PRODUTO, COD_COSIF)
);
GO
```

## 3️⃣ Configurar String de Conexão
No arquivo appsettings.json do projeto Movimentos.API, ajuste conforme seu ambiente:
"ConnectionStrings": {
  "DBConnection": "Server=.\\SQLEXPRESS;Database=MovimentosManuais;User Id=candidato;Password=candidato123;TrustServerCertificate=True;"
}

## 5️⃣ Restaurar Dependências
dotnet restore

## 5️⃣ Executar o projeto
dotnet run --project src/Movimentos.API

A API estará disponível em: https://localhost:5001/swagger

---

## 📊 Diagrama de Arquitetura

flowchart LR
    A[Controller] --> B[Service]
    B --> C[Repository]
    C --> D[(Banco de Dados)]

---

## 📜 Licença
Este projeto está sob a licença MIT. Sinta-se livre para usar e modificar.
