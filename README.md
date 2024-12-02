# 🧁 Cup Sweet  

**Cup Sweet** é um projeto fictício desenvolvido como trabalho integrador transdisciplinar para a faculdade. Trata-se de uma loja de cupcakes que simula o processo de venda, desde a escolha dos produtos até a finalização do pedido. Feito por Matheus Hora

## 📚 Tecnologias Utilizadas  

O projeto é composto por duas partes principais:  

### 🌐 Frontend  
- **Framework:** Angular  
- **Linguagem:** TypeScript  
- **Bibliotecas adicionais:**  
  - RxJS (para gerenciamento de streams de dados)  
  - Outros módulos relevantes para a implementação de funcionalidades  

### 🔧 Backend  
- **Framework:** .NET  
- **Linguagem:** C#  
- **Banco de Dados:** Postgress
- **Bibliotecas adicionais:**  
  - Entity Framework Core (para ORM)  
  - ASP.NET Core (para APIs RESTful)  

## 🚀 Funcionalidades  

### 🛒 Loja de Cupcakes  
- Catálogo de cupcakes com descrição, preço e imagens  
- Filtro por sabores, tamanhos ou preços  
- Adicionar/remover cupcakes no carrinho  

### 🛍️ Carrinho de Compras  
- Resumo dos itens adicionados  
- Cálculo do subtotal, descontos e valor total  
- Finalização de compra com simulação de pagamento  

### 🔐 Autenticação  
- Login e registro de usuários  
- Controle de acesso para administradores e clientes  

## 🖥️ Estrutura do Projeto  

### 📁 Frontend  
- `src/app`  
  - `components/` - Componentes da interface de usuário  
  - `services/` - Serviços para integração com a API  
  - `models/` - Modelos de dados  
  - `pages/` - Páginas principais do sistema  

### 📁 Backend  
- `Controllers/` - Endpoints da API  
- `Models/` - Estruturas de dados e entidades  
- `Services/` - Regras de negócio e lógica  
- `Data/` - Configuração do banco de dados e repositórios  

## 🔧 Como Executar  

### 🖥️ Pré-requisitos  
- Node.js (versão 14 ou superior)  
- .NET SDK (versão 8 ou superior)  
- SQL Server  

### 🌐 Configurando o Frontend  
```bash  
cd frontend  
npm install  
ng serve  

## 🔧 Como Executar  

### 🖥️ Pré-requisitos  
- **Node.js** (versão 14 ou superior)  
- **.NET SDK** (versão 6 ou superior)  
- **SQL Server**  

### 🌐 Configurando o Frontend  
1. Navegue até a pasta do frontend:  
   ```bash
   cd frontend
   ```  
2. Instale as dependências:  
   ```bash
   npm install
   ```  
3. Inicie o servidor de desenvolvimento:  
   ```bash
   ng serve
   ```  
   O frontend estará disponível em `http://localhost:4200`.  

### 🔧 Configurando o Backend  
1. Configure o arquivo `appsettings.json` com as credenciais do banco de dados.  
2. Aplique as migrações para criar o banco de dados:  
   ```bash
   dotnet ef database update
   ```  
3. Inicie o servidor:  
   ```bash
   dotnet run
   ```  
   A API estará disponível em `http://localhost:5000/api`.  

