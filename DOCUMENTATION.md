## Documentação do Projeto

### Estrutura do Projeto

O projeto está organizado em múltiplas camadas para promover a separação de responsabilidades e facilitar a manutenção. A estrutura é a seguinte:

- **Application:**  
  - **Services:** Contém a implementação dos serviços, como o *ProdutoService*, que encapsula a lógica de negócio.  
  - **Validators:** Possui as classes de validação (como *ProdutoValidator*) que garantem a integridade dos dados.

- **Domain:**  
  Define o núcleo do negócio.  
  - **Entities:** Inclui as classes que representam as entidades do domínio (ex.: *Produto*).  
  - **DTOs:** Objetos para transferência de dados, como *ProdutoUpdatePriceDTO*.  
  - **Interfaces:** Contratos que definem os serviços que serão implementados.

- **Infrastructure:**  
  Implementa os detalhes de acesso a dados e migrações.  
  - **Data:** Contém o contexto do Entity Framework Core.  
  - **Migrations:** Arquivos de migração usando o FluentMigrator para criação e atualização do banco de dados.  
  - **Repositories:** Implementa o padrão Repository para encapsular o acesso aos dados.

- **Presentation:**  
  Gerencia a interação com o usuário.  
  - **Controllers:** Responsáveis por receber as requisições, processar a lógica e retornar as views.  
  - **Views:** Arquivos Razor que definem a interface do usuário.  
  - **Shared:** Contém o layout comum e os scripts de validação.

- **Tests:**  
  Projeto de testes unitários utilizando xUnit para validar o comportamento dos controllers e serviços.

### Descrição das Camadas e Responsabilidades

- **Domain:**  
  Define as regras de negócio, representadas pelas entidades (Produto) e pelos contratos dos serviços (IProdutoService).
  
- **Application:**  
  Contém a lógica de negócio e validações, assegurando que os dados estejam corretos antes de serem processados.

- **Infrastructure:**  
  Implementa a persistência de dados, gerencia migrações e encapsula o acesso ao banco utilizando o padrão Repository.

- **Presentation:**  
  Responsável pela interação com o usuário, utilizando uma interface web (Views) e controladores para gerenciar as requisições.

- **Tests:**  
  Garante a integridade da aplicação através de testes unitários escritos com xUnit.

### Escolha de Tecnologias e Padrões de Projeto

- **ASP.NET Core MVC & Web API:** Escolhidos por sua robustez, desempenho e flexibilidade para criar aplicações escaláveis e modulares.
- **Entity Framework Core:** Permite o mapeamento objeto-relacional de forma eficiente e simplificada.
- **FluentMigrator:** Facilita o controle de versões e migrações do banco de dados, garantindo que as mudanças sejam aplicadas de forma consistente.
- **FluentValidation:** Proporciona uma forma fluente e centralizada de validar dados, mantendo a lógica de validação separada da lógica de negócio.
- **xUnit:** Utilizado para escrever testes unitários, permitindo uma cobertura de testes eficiente e moderna.
- **Docker:** Facilita a containerização e o deploy da aplicação, garantindo que o ambiente de execução seja idêntico em desenvolvimento e produção.
- **Padrões SOLID e Repository/Service Pattern:** Ajudam a manter a separação de responsabilidades, facilitando a manutenção, testabilidade e escalabilidade da aplicação.

### Desafios Encontrados e Soluções

- **Integração entre API e Interface MVC:**  
  A separação das camadas API e MVC exigiu um design cuidadoso para que ambas pudessem compartilhar a mesma lógica de negócio. A solução foi implementar a camada de serviços e usar o padrão Repository para abstrair o acesso aos dados.

- **Gerenciamento de Migrações:**  
  Garantir que as migrações fossem aplicadas corretamente em ambientes diferentes foi um desafio. A utilização do FluentMigrator permitiu versionar e automatizar as migrações, garantindo que o banco de dados estivesse sempre sincronizado com o modelo de domínio.

- **Validação de Dados:**  
  A implementação de regras de validação complexas exigiu a utilização do FluentValidation, que separa a lógica de validação do modelo, melhorando a manutenibilidade e a clareza do código.

- **Testes Unitários:**  
  Isolar as camadas e escrever testes unitários que cobririam os cenários principais foi um desafio. A criação de mocks para os serviços e a utilização de xUnit permitiram uma cobertura de testes robusta, ajudando a identificar e corrigir problemas precocemente.

- **Containerização:**  
  Configurar o Docker para que a aplicação rodasse de forma consistente em diferentes ambientes foi resolvido através de um Dockerfile bem estruturado, permitindo que o mesmo ambiente de execução seja replicado tanto em desenvolvimento quanto em produção.

### Plano de Testes

O plano de testes abrange os seguintes cenários:

- **Testes dos Controllers:**  
  - Verificar se as ações retornam a view correta com os dados esperados (por exemplo, listagem de produtos, tela de criação, edição e exclusão).  
  - Validar a resposta para ModelState inválido, garantindo que os erros sejam exibidos.
  - Testar o redirecionamento após operações bem-sucedidas (criação, edição, exclusão).

- **Testes dos Serviços e Repositórios:**  
  - Verificar se os métodos de adição, atualização e exclusão persistem corretamente os dados no banco.
  - Validar a execução de migrações e o funcionamento do EF Core.

- **Testes de Integração com a API:**  
  - Utilizar o Swagger para testar os endpoints da API.
  - Simular chamadas HTTP para verificar a comunicação entre as camadas.

- **Testes Unitários:**  
  - Usar xUnit para escrever testes que garantam a integridade e o comportamento correto da aplicação, utilizando mocks para isolar as camadas.

## Conclusão

Este README.md oferece um guia completo para configurar, executar, testar e implantar a aplicação. Ao seguir as instruções, qualquer desenvolvedor ou usuário poderá rapidamente colocar o projeto em funcionamento, seja localmente ou via Docker, e contribuir com melhorias ou correções no código.

Se surgirem dúvidas ou problemas, consulte os logs gerados pela aplicação ou entre em contato com a equipe de desenvolvimento.
