# Projeto RabbitMQ – Série Shameless 🧩

## Integrantes

- **Júlia Costa** – RA: 04201006  
- **Arthur Herculano** – RA: 04201

---

## Descrição

Este projeto demonstra a comunicação entre duas aplicações utilizando **RabbitMQ** como sistema de mensageria.

- **Produtor (Java Spring Boot):** envia mensagens sobre personagens da série *Shameless*.
- **Consumidor (C# .NET 8):** recebe as mensagens da fila e permite visualizá-las através de um endpoint HTTP.

---

## 🐇 Comunicação

- **Fila:** `fila.shameless`
- **Exchange:** pode ser `direct`, `topic` ou `fanout` (não obrigatório para o funcionamento básico)
- **Protocolo:** AMQP (via RabbitMQ)

---

## 🚀 Como enviar uma mensagem (Produtor)

**Endpoint do produtor:**

```
POST http://localhost:8080/shameless/enviar
```

**Método HTTP:** `POST`  
**Headers:**  
```
Content-Type: application/json
```

**Exemplo de JSON a ser enviado:**
```json
{
  "personagem": "Frank Gallagher",
  "descricao": "Pai alcoólatra que sobrevive de golpes e benefícios sociais."
}
```

---

## 🧱 Como subir o ambiente

1. **Subir o RabbitMQ via Docker**
   ```bash
   docker compose up -d
   ```
   *(certifique-se de que o `compose.yaml` está na raiz do projeto e contém o serviço do RabbitMQ)*

2. **Executar o produtor (Spring Boot)**
   ```bash
   ./mvnw spring-boot:run
   ```
   ou, se preferir:
   ```bash
   mvn spring-boot:run
   ```

3. **Executar o consumidor (C#)**
   ```bash
   cd ShamelessConsumer
   dotnet run
   ```

---

## 📥 Enviando uma mensagem de teste

Use o **Postman**, **Insomnia** ou **cURL** para enviar a mensagem:

```bash
curl -X POST http://localhost:8080/shameless/enviar      -H "Content-Type: application/json"      -d '{
           "personagem": "Lip Gallagher",
           "descricao": "Filho inteligente e rebelde da família Gallagher."
         }'
```

---

## 📤 Verificando a mensagem recebida (Consumidor)

**Endpoint do consumidor:**

```
GET http://localhost:5000/mensagens
```

**Exemplo de retorno esperado:**

```json
[
  {
    "personagem": "Lip Gallagher",
    "descricao": "Filho inteligente e rebelde da família Gallagher."
  },
  {
    "personagem": "Frank Gallagher",
    "descricao": "Pai alcoólatra que sobrevive de golpes e benefícios sociais."
  }
]
```

---

## ⚙️ Tecnologias utilizadas

### Produtor (Java Spring Boot)
- Spring Boot 3.x
- Spring AMQP
- Maven
- RabbitMQ

### Consumidor (C#)
- .NET 8 Web API
- RabbitMQ.Client
- ASP.NET Core Minimal API

---

## 📚 Resumo da Arquitetura

```
[Spring Boot API] ---> [RabbitMQ] ---> [C# API]
      POST                  📨               GET
```

O produtor envia mensagens sobre *Shameless* via endpoint HTTP, que são publicadas no RabbitMQ.  
O consumidor lê da fila `fila.shameless` e disponibiliza as mensagens recebidas em um endpoint GET.

---

## 👩‍💻 Créditos

Trabalho desenvolvido para demonstrar comunicação assíncrona entre serviços utilizando RabbitMQ.  
Tema: **Shameless – A família Gallagher e o caos organizado.**
