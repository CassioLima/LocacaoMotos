# Projeto API em Docker para Locação e Gestão de Motos e Entregadores

Este projeto é uma API para gestão de locações e cadastro de motos e entregadores, utilizando Docker para configuração dos serviços e integração com diversas tecnologias.

## Tecnologias Utilizadas

- MassTransit
- PostgreSQL
- PgAdmin
- RabbitMQ
- Docker
- Serilog
- ElasticSearch + Kibana
- .NET Core 8
- Entity Framework

## Como Subir os Containers

Execute o comando abaixo para subir os containers:
docker-compose up


Foi utilizado o Entity Framework com PostgreSQL. O contexto contém uma classe Seed que popula as tabelas de `entregador` e `moto`, facilitando os testes iniciais.

### Collection de Testes
Na pasta `configurações`, na raiz do projeto, você encontrará uma collection para testes. Basta importá-la em seu ambiente de testes.

---

### PostgreSQL + PgAdmin

Para acessar o PgAdmin, vá para o endereço:

http://localhost:5050/browser/



- Usuário: postgres
- Senha: postgres
- Banco: dockerdb

---

### RabbitMQ

Para acessar o painel de controle do RabbitMQ:

http://localhost:15672/


- Usuário: guest
- Senha: guest

---

### ElasticSearch + Kibana

Para acessar o Kibana:
http://localhost:5601/


Para visualizar os logs, crie o índice: `logs-api*`.

---

### Swagger

Para acessar o Swagger e explorar os endpoints da API:
http://localhost:8082/swagger/index.html


---

## Referências

### MassTransit

MassTransit é uma biblioteca .NET para integração de mensagens e gerenciamento de filas, facilitando a comunicação entre microserviços.





