using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

public class ShamelessConsumerService
{

    private readonly MessagesStorage _storage;
    private readonly Random _random = new();

    private readonly string[] _characters =
    {
        "Frank Gallagher",
        "Fiona Gallagher",
        "Lip Gallagher",
        "Ian Gallagher",
        "Debbie Gallagher",
        "Carl Gallagher",
        "Kev",
        "Veronica"
    };

    public ShamelessConsumerService(MessagesStorage storage)
    {
        _storage = storage;
    }

    public void StartListening()
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "arthurejulia",
        Password = "segredo" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        // Essa fila é a mesma que o produtor usa no codigo em java
        var queueName = "fila.shameless";
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var mensagemRecebida = System.Text.Encoding.UTF8.GetString(body);

            var character = _characters[_random.Next(_characters.Length)];

             string mensagemPersonalizada = character switch
    {
        "Frank Gallagher" => $"Frank continua bebendo.. Mas ainda assim lembrou de te falar:: {mensagemRecebida}",
        "Fiona Gallagher" => $"Fiona sempre esta cuidando da família! Mas parou para te enviar a mensagem: {mensagemRecebida}",
        "Lip Gallagher" => $"Lip, espero que esteja estudando! Se não, entregue a mensagem:: {mensagemRecebida}",
        "Ian Gallagher" => $"Ian, peça pro Micky passar a mensagem:: {mensagemRecebida}",
        "Debbie Gallagher" => $"Debbie sempre cumpre suas tarefas! E uma delas era enviar isso para vocÊ: {mensagemRecebida}",
        "Carl Gallagher" => $"Carl está causando um caos e não se deu ao trabalho de tentar enviar nada, então aqui está a mensagem: {mensagemRecebida}",
        "Kev" => $"Kev cuidando do bar! Deixou isso pra você: {mensagemRecebida}",
        "Veronica" => $"Veronica encantou todo mundo no caminho para te dar isso: {mensagemRecebida}",
        _ => $"Personagem misterioso apareceu! Mensagem: {mensagemRecebida}"
    };

            var messageObj = new
            {
                personagem = character,
                recebidoEm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                conteudo = mensagemRecebida
            };

            var json = JsonSerializer.Serialize(messageObj, new JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine($"{mensagemPersonalizada}");
            Console.WriteLine($"{character} disse: chegou a mensagem ai?");
            _storage.AddMessage(json);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        Console.WriteLine("Shameless Consumer pronto pra receber mensagens!");
    }
}