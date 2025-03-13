# Produtos API & MVC

Este projeto implementa uma aplicação completa para operações de CRUD da entidade **Produto** utilizando **ASP.NET Core**. A arquitetura do projeto segue os princípios **SOLID** e adota as seguintes tecnologias:

- **ASP.NET Core MVC e Web API:** Interface web e API para operações CRUD.
- **Entity Framework Core:** ORM para manipulação de dados.
- **FluentMigrator:** Gerenciamento de migrações do banco de dados.
- **FluentValidation:** Validação de dados da aplicação.
- **xUnit:** Testes unitários para qualidade do código.
- **Docker:** Containerização para facilitar o deploy.
- **Git:** Controle de versão.

## 📌 Descrição do Projeto

Esta solução permite o gerenciamento de produtos por meio de uma API e interface MVC, possibilitando **criação, leitura, atualização e exclusão** de produtos. O projeto integra:

- **ASP.NET Core MVC & API** para estruturação da aplicação.
- **Entity Framework Core** para persistência de dados.
- **FluentMigrator** para gerenciamento das migrações do banco.
- **FluentValidation** para validação dos dados inseridos.
- **xUnit** para testes automatizados.
- **Docker** para containerização.
- **Git** para versionamento e colaboração.

---

## 🚀 Configurando e Executando o Projeto Localmente

### ✅ Pré-requisitos

- [.NET 8 ou superior](https://dotnet.microsoft.com/download)
- [MySQL Server](https://www.mysql.com/)
- [Docker](https://www.docker.com/get-started)
- [Git](https://git-scm.com/)

### 🛠️ Passos para Configuração

1. **Clonar o Repositório:**

   ```bash
   git clone https://github.com/theonicoleli/Produtos-API
   cd Produtos-API/ProductsAPI
   ```

2. **Configurar o Banco de Dados:**

   Atualize a connection string no arquivo `appsettings.json` para se conectar ao seu servidor MySQL:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "server=host.docker.internal;user=(SEULOGIN);password=(SUASENHA);database=ProdutoAPI"
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }
   ```

3. **Rodar as Migrações:**

   O **FluentMigrator** executará as migrações automaticamente durante a inicialização da aplicação. Basta executar:

   ```bash
   dotnet run
   ```

4. **Executar a Aplicação:**

   Na raiz do projeto, rode:

   ```bash
   dotnet run
   ```

   A aplicação abrirá uma interface onde você poderá **adicionar, remover e alterar produtos**.

   Para acessar a documentação da API via **Swagger**, basta pegar a porta em que a aplicação está rodando e adicionar `/swagger/index.html`, por exemplo:

   ```
   http://localhost:5000/swagger/index.html
   ```

---

## ✅ Executando os Testes Unitários

1. **Navegue até a pasta do projeto de testes:**

   ```bash
   cd Tests
   ```

2. **Execute os testes:**

   ```bash
   dotnet test
   ```

   Isso executará os testes automatizados escritos com **xUnit** e exibirá os resultados no console.

---

## 🐳 Docker

### Construir a Imagem Docker

O projeto inclui um Dockerfile configurado para construir e publicar a aplicação. Segue um exemplo:

```dockerfile
#See https://aka.ms/customizecontainer to learn how to customize your debug container.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProductsAPI/ProductsAPI.csproj", "ProductsAPI/"]
RUN dotnet restore "./ProductsAPI/ProductsAPI.csproj"
COPY . .
WORKDIR "/src/ProductsAPI"
RUN dotnet build "ProductsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProductsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductsAPI.dll"]
```

### Construir a Imagem

No terminal, execute:

```bash
docker build -t seuusuario/produtosapi .
```

### Executar o Container

Após construir a imagem, inicie o container com:

```bash
docker run -d -p 8080:8080 -p 8081:8081 seuusuario/produtosapi
```

A aplicação estará disponível em:

- **Interface MVC:** [https://localhost:32773/ProdutosMvc/Index](https://localhost:32773/ProdutosMvc/Index)

---

💻 Desenvolvido por **Theo Nicoleli**

