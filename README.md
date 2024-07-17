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
