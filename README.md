# Projeto

## Objetivo do Projeto

O objetivo do projeto é implementar uma aplicação em microserviços no ecossistema .NET 8.

### Conceitos a adquirir com este projeto:

**Conceitos Fundamentais:** Explorar os princípios básicos por trás da arquitetura de microserviços, compreender as vantagens sobre as aplicações monolíticas e avaliar a sua adequação para diferentes contextos de projeto.

**Desenvolvimento Passo a Passo:** Construir uma aplicação de microserviços do zero e, gradualmente, adicionar funcionalidades a diferentes componentes, solidificando o entendimento ao longo do processo.

**Tech:** Familiarizar-se com ferramentas e frameworks essenciais para o desenvolvimento de microserviços no ambiente .NET, incluindo .NET API, Ocelot API Gateway, .NET Identity, Entity Framework Core e princípios de arquitetura limpa.

**Autenticação e Autorização:** Implementar mecanismos robustos de autenticação e autorização utilizando .NET Identity, garantindo a segurança de microserviços e dos dados que manipulam.

**Comunicação entre Microserviços:** Explorar diferentes métodos de comunicação entre microserviços, destacando o uso do Azure Service Bus para garantir uma interação eficiente e confiável entre os componentes da aplicação.

**Práticas de Código de Qualidade:** Introduzir práticas de desenvolvimento de software que promovem a escrita de código limpo e manutenível, ajudando a construir soluções de microserviços escaláveis e resilientes.

Em conclusão, o objetivo é ter uma base sólida para o desenvolvimento de aplicações de microserviços no ambiente .NET, preparando-me para enfrentar desafios complexos de desenvolvimento de software com confiança e competência.

## Título e Ideia

Uma aplicação Web para que os clientes de um restaurante possam fazer pedidos e o administrador possa gerir os pedidos dos clientes, bem como gerir o menu do restaurante.

## Autor, Data, Contexto do Trabalho

**Autor:** João Cardoso  
**Data:** 29/04/2024

## Descrição do Problema

A empresa Cardoso LDA necessita de um aplicativo Web para que os clientes do restaurante possam fazer os seus pedidos de forma eficiente. É necessário que os usuários possam criar contas, gerir os pedidos do menu, gerir informações pessoais, finalizar o pedido, tudo dentro da mesma plataforma. Os clientes terão acesso a uma conta pessoal onde poderão gerir os seus pedidos de forma prática. O administrador poderá gerir detalhes dos pedidos, bem como o menu.

## Ferramentas e Versões

- ASP.NET Core
- .NET 8.0
- C#
- SQL Server
- Azure
- Swagger
- Ocelot
- RabbitMQ

## Esquema da Aplicação Web em Microserviços

## Descrição de Processos

**Isto é uma abstração de processos de um aplicativo de pedidos de um restaurante.**

### Utilizador:

**Registro/Login:**

- O usuário acessa o aplicativo e tem a opção de se registrar se for um novo usuário ou fazer login se já tiver uma conta.
- Durante o registo, o usuário fornece informações básicas, como nome, endereço de e-mail e senha.
- Após fazer login, o usuário é direcionado para a página principal, onde pode explorar a ementa do restaurante.
- O utilizador pode ver informações detalhadas sobre cada item do menu, incluindo nome, imagem, preço, descrição e categoria.
- Fazer pedidos da refeição a consumir.
- O utilizador insere as quantidades do item e confirma.
- O utilizador pode gerir o pedido, bem como cancelar.

### Administrador:

- O administrador pode ver todos os pedidos de todos os clientes, bem como o estado de cada pedido.
- Pode gerir os pedidos, como cancelar ou entregar.
- Pode fazer o CRUD das refeições, bem como outros cupões.


## Namespaces

### Serviços

#### CardosoRestaurante.Services.AuthAPI

Este microserviço permite registar e autenticar utilizadores numa aplicação de gestão de restaurantes. Ela também permite atribuir funções aos utilizadores, que podem ser usadas para controlar o acesso a diferentes partes da aplicação. A autenticação é feita através de tokens JWT, que são gerados quando um utilizador faz login e devem ser enviados com cada pedido para autenticar o utilizador.

Principais classes neste microserviço e as suas funcionalidades:

1. AuthAPIController.cs: Este é o controlador da API de autenticação. Ele expõe três endpoints principais: registar, login e atribuirFuncao. O endpoint registar é usado para registar um novo utilizador na aplicação. O endpoint login é usado para autenticar um utilizador existente. O endpoint atribuirFuncao é usado para atribuir uma função a um utilizador.
2. IAuthService.cs: Esta é a interface do serviço de autenticação. Ela define três métodos principais: LoginAsync, RegistarAsync e AtribuirFuncaoAsync. Estes métodos correspondem aos endpoints definidos no AuthAPIController.
3. JwtOptions.cs: Esta classe é usada para configurar as opções do token JWT. Ela contém propriedades para a chave secreta, o emissor e a audiência do token.
4. JwtTokenGenerator.cs: Esta classe é responsável por gerar tokens JWT. Ela usa as opções definidas em JwtOptions para configurar o token. O método GenerateToken cria um novo token JWT que inclui informações sobre o utilizador e as suas funções.


#### CardosoRestaurante.Services.CarrinhoAPI

Este microserviço permite aos utilizadores gerir o seu carrinho de compras numa aplicação de restaurante. Eles podem adicionar produtos ao carrinho, remover produtos, aplicar cupões para obter descontos e enviar o seu carrinho de compras por email. A aplicação comunica-se com outras APIs para obter informações sobre produtos e cupões.

Principais classes neste microserviço:  
**CarrinhoAPIController.cs**: Este é o controlador da API do carrinho de compras. Ele expõe vários endpoints para gerir o carrinho de compras de um utilizador, incluindo a obtenção do carrinho de compras de um utilizador, a atualização do carrinho de compras, a remoção de um item do carrinho, a aplicação de um cupão ao carrinho, a remoção de um cupão e o envio de um pedido de carrinho por email.

**CupaoService.cs**: Este é o serviço que gere os cupões. Ele tem um método GetCupao que faz uma solicitação GET para a API de cupões para obter um cupão específico com base no seu código.

**ProdutoService.cs**: Este é o serviço que gere os produtos. Ele tem um método GetProdutos que faz uma solicitação GET para a API de produtos para obter uma lista de todos os produtos

#### CardosoRestaurante.Services.CupaoAPI

Este microserviço permite aos utilizadores gerir cupões. Eles podem obter informações sobre cupões, adicionar novos cupões, atualizar cupões existentes e eliminar cupões. A aplicação usa um modelo para representar um cupão na base de dados e um DTO para transferir dados de um cupão entre diferentes partes da aplicação ou entre a aplicação e o cliente.

##### Principais classes deste microserviço

1\. CupaoAPIController.cs: Este é o controlador da API de cupões. Ele expõe vários endpoints para gerir os cupões, incluindo a obtenção de todos os cupões, a obtenção de um cupão específico pelo seu ID, a obtenção de um cupão pelo seu código, a adição de um novo cupão, a atualização de um cupão e a eliminação de um cupão. Alguns destes endpoints requerem que o utilizador esteja autenticado e tenha a role de administrador.

2\. CupaoDto.cs: Este é o objeto de transferência de dados (DTO) para um cupão. Ele é usado para transferir dados de um cupão entre diferentes partes da aplicação ou entre a aplicação e o cliente. Ele contém as mesmas propriedades que o modelo de cupão, mas pode ser expandido para incluir outras propriedades se necessário.

3\. Cupao.cs: Este é o modelo de um cupão. Ele define as propriedades de um cupão, incluindo o ID do cupão, o código do cupão, o desconto que o cupão proporciona e o valor mínimo que deve ser gasto para que o cupão seja aplicável.

#### CardosoRestaurante.Services.EmailAPI

Este microserviço recebe mensagens de uma Queue do Azure Service Bus, envia emails com base nessas mensagens e regista os emails enviados. A aplicação usa o Entity Framework Core para gerir os registos de email na base de dados e o Azure Service Bus para receber mensagens.

##### Principais classes deste microserviço

1\. AzureServiceBusConsumer.cs e IAzureServiceBusConsumer.cs: Estes ficheiros definem uma classe e uma interface para consumir mensagens de uma fila do Azure Service Bus. A classe AzureServiceBusConsumer implementa a interface IAzureServiceBusConsumer e contém métodos para iniciar e parar o processamento de mensagens, além de lidar com quaisquer erros que possam ocorrer durante o processo. Quando uma nova mensagem é recebida, a classe deserializa a mensagem e chama o serviço de email para enviar um email e registar a mensagem.

2\. EmailLogger.cs: Este é o modelo para um registo de email. Ele define as propriedades de um registo de email, incluindo o ID do registo, o email, a mensagem e a data e hora em que o email foi enviado.

3\. EmailService.cs e IEmailService.cs: Estes ficheiros definem uma classe e uma interface para o serviço de email. O serviço de email é responsável por enviar emails e registar os emails enviados. Ele contém métodos para enviar um email com o conteúdo de um carrinho de compras e para registar um novo utilizador.

4\. CarrinhoDto.cs e CarrinhoDetalhes.cs: Estes são os objetos de transferência de dados (DTOs) para um carrinho de compras e os detalhes de um carrinho de compras. Eles são usados para transferir dados de um carrinho de compras entre diferentes partes da aplicação ou entre a aplicação e o cliente.

5\. Program.cs: Este é o ponto de entrada da aplicação. Ele configura os serviços necessários, incluindo o serviço de email e o consumidor do Azure Service Bus, e inicia a aplicação.

6\. ApplicationBuilderExtensions.cs: A classe ApplicationBuilderExtensions é uma classe de extensão para a interface IApplicationBuilder. Ela adiciona um método chamado UseAzureServiceBusConsumer que permite registar um consumidor do Azure Service Bus no pipeline da aplicação. Aqui está uma descrição detalhada do que cada parte da classe faz:

#### CardosoRestaurante.Services.PedidoAPI

Este microserviço permite aos utilizadores gerir pedidos na aplicação. Eles podem obter informações sobre pedidos, adicionar novos pedidos, atualizar pedidos existentes e eliminar pedidos. O microserviço usa modelos para representar um pedido na base de dados e DTOs para transferir dados de um pedido entre diferentes partes da aplicação ou entre a aplicação e o cliente. O microserviço também configura a autenticação JWT Bearer para garantir a segurança das solicitações de autenticação.

##### Principais classes deste microserviço

1\. WebApplicationsBuilderExtensions.cs: Este ficheiro contém uma classe de extensão para o WebApplicationBuilder. Ele adiciona um método chamado AddAppAuthentication que configura a autenticação JWT Bearer no serviço de autenticação do aplicativo ASP.NET Core.

2\. PedidoInfo.cs e PedidoDetalhe.cs: Estes são os modelos para as informações do pedido e os detalhes do pedido, respectivamente. Eles definem as propriedades de um pedido e dos detalhes de um pedido, incluindo o ID do pedido, o ID do utilizador, o código do cupão, o desconto, o imposto, o total do pedido, o nome, o último nome, o telemóvel, o email, a data do pedido, o status, o ID da intenção de pagamento, o ID da sessão Stripe e os detalhes do pedido.

3\. PedidoInfoDto.cs e PedidoDetalheDto.cs: Estes são os objetos de transferência de dados (DTOs) para as informações do pedido e os detalhes do pedido, respectivamente. Eles são usados para transferir dados de um pedido entre diferentes partes da aplicação ou entre a aplicação e o cliente.

4\. ResponseDto.cs: Este é o modelo para uma resposta. Ele define as propriedades de uma resposta, incluindo o resultado, o sucesso e a mensagem.

5\. IProdutoService.cs e ProdutoService.cs: Estes ficheiros definem uma interface e uma classe para o serviço de produtos. O serviço de produtos é responsável por obter a lista de produtos da API de produtos.

#### CardosoRestaurante.Services.ProdutoAPI

Este microserviço permite aos utilizadores gerir produtos. Eles podem obter informações sobre produtos, adicionar novos produtos, atualizar produtos existentes e eliminar produtos. A aplicação usa modelos para representar um produto na base de dados e DTOs para transferir dados de um produto entre diferentes partes da aplicação ou entre a aplicação e o cliente. O microserviço também configura a autenticação JWT Bearer para garantir a segurança das solicitações de autenticação.

##### Principais classes deste microserviço

1\. ProdutoAPIController.cs: Este é o controlador da API de produtos. Ele define vários endpoints para gerir produtos, incluindo endpoints para obter todos os produtos, obter um produto por ID ou nome, adicionar um novo produto, atualizar um produto existente e eliminar um produto. O controlador usa o AutoMapper para mapear entre os modelos de produtos e os objetos de transferência de dados (DTOs) de produtos. Além disso, ele usa um objeto ResponseDto para retornar respostas ao cliente.

2\. WebApplicationsBuilderExtensions.cs: Este ficheiro contém uma classe de extensão para o WebApplicationBuilder. Ele adiciona um método chamado AddAppAuthentication que configura a autenticação JWT Bearer no serviço de autenticação do aplicativo ASP.NET Core.

3\. Produto.cs: Este é o modelo para um produto. Ele define as propriedades de um produto, incluindo o ID do produto, o nome, o preço, a descrição, a porção, a categoria e a URL da imagem.

4\. ProdutoDto.cs: Este é o objeto de transferência de dados (DTO) para um produto. Ele é usado para transferir dados de um produto entre diferentes partes da aplicação ou entre a aplicação e o cliente.

5\. ResponseDto.cs: Este é o modelo para uma resposta. Ele define as propriedades de uma resposta, incluindo o resultado, o sucesso e a mensagem.

6\. MappingConfig.cs: Este ficheiro define a configuração de mapeamento para o AutoMapper. Ele configura o mapeamento entre a classe Produto e a classe ProdutoDto.

### FrontEnd

#### CardosoRestaurante.Web

O front-end da aplicação CardosoRestaurante.Web é responsável por interagir com os utilizadores e apresentar os dados de uma forma estruturada.  
Controllers:

1\. ProdutoController: Este controlador lida com todas as operações relacionadas aos produtos. Ele usa o serviço ProdutoService para obter dados de produtos e apresentá-los aos utilizadores.

2\. CupaoController: Este controlador lida com todas as operações relacionadas aos cupões. Ele usa um serviço para obter dados de cupões e apresentá-los aos utilizadores.

3\. AuthController: Este controlador lida com a autenticação e autorização. Ele usa o serviço AuthService para registar utilizadores, iniciar sessão, encerrar sessão e outras operações relacionadas à autenticação.

Services:

1\. AuthService: Este serviço lida com a autenticação. Ele tem métodos para registar utilizadores, iniciar sessão, encerrar sessão e outras operações relacionadas à autenticação.

2\. ProdutoService: Este serviço lida com a obtenção de dados de produtos. Ele tem métodos para obter produtos, obter detalhes de produtos e outras operações relacionadas a produtos.

3\. TokenProvider: Este serviço é usado para gerir tokens JWT para autenticação.

4\. BaseService: Este serviço fornece funcionalidades básicas que podem ser usadas por outros serviços. Ele tem métodos para enviar pedidos HTTP e processar respostas.

Models:

Os modelos são usados para representar os dados na aplicação. Alguns dos modelos usados neste projeto incluem RegistrationRequestDto, UserDto, PedidoInfoDto, CarrinhoInfoDto, etc. Cada um destes modelos tem propriedades que correspondem aos dados que eles representam.

Utility:

A classe SD é usada para armazenar constantes que são usadas em toda a aplicação. Estas podem incluir URLs de API, chaves de configuração, etc.

### Integration

#### CardosoRestaurante.MessageBus

Este microserviço é responsável pela implementação de um barramento de mensagens, especificamente, ele permite a publicação de mensagens num topic ou Queue específica no Service Bus. O Service Bus é um serviço de mensagens na cloud que facilita a comunicação entre componentes de uma aplicação através de mensagens.


