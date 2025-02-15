# Delteaching

Este repositório contém os arquivos necessários para configurar e executar a aplicação **Delteaching** em um ambiente Dockerizado. A arquitetura do projeto segue o padrão **Clean Architecture**, garantindo separação de responsabilidades e facilidade de manutenção.

## Estrutura do Projeto

A aplicação está organizada da seguinte forma:

```
Delteaching.API/                                      # Camada responsável por expor os endpoints da API
│   ├── Controllers/                      # Controladores que lidam com as requisições HTTP
│   ├── Middlewares/                      # Componentes para interceptação e manipulação de requisições/respostas
│
├── Delteaching.Application/                          # Camada de aplicação que coordena a lógica do negócio
│   ├── DTOs/                             # Data Transfer Objects usados para transportar dados entre camadas
│   ├── Interfaces/                       # Interfaces dos serviços de aplicação
│   └── Services/                         # Implementações dos serviços de aplicação
│
├── Delteaching.Domain/                               # Camada de domínio que contém as entidades do negócio
│   ├── Entities/                         # Entidades do domínio que representam os objetos principais do negócio
│   ├── Repositories/                     # Interfaces para repositórios
│   └── Exceptions/                       # Exceção personalizada a ser usada como erro por regra de negócio
│
├── Delteaching.Infra.Data/                           # Infraestrutura de dados, responsável pela comunicação com o banco de dados
│   ├── Context/                          # Contexto do Entity Framework
│   ├── Migrations/                       # Migrações para criar ou atualizar o banco de dados
│   └── Repositories/                     # Repositórios que lidam com operações CRUD e consultas complexas
│
└── Delteaching.Infra.IoC/                            # Configurações de injeção de dependência
    ├── DependencyInjection/              # Configuração de injeções de dependência para serviços e repositórios
```

## Pré-requisitos

Para executar a aplicação, é necessário ter instalado:

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## Instruções de Uso

### 1. Subir os serviços de infraestrutura

```bash
docker-compose up -d --build rabbitmq postgres
```

Esse comando iniciará os seguintes serviços:
- **RabbitMQ** (mensageria)
- **Postgres** (banco de dados relacional)

### 2. Subir a aplicação backend

Após garantir que os serviços estão rodando corretamente, inicie a aplicação backend:

```bash
docker-compose up -d --build api
```

A aplicação backend estará acessível em:

```
http://localhost:80
```

## Observações

- Certifique-se de que as portas necessárias (como a **porta 80** para a aplicação backend) não estejam sendo usadas por outros serviços na sua máquina.
- Caso haja necessidade de ajustes no banco de dados, utilize o Entity Framework para rodar as **migrations** automaticamente.