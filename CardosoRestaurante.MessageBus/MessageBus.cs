using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace CardosoRestaurante.MessageBus
{/*Esta classe é responsável por implementar a funcionalidade de um barramento de mensagens. Mais especificamente, ela contém um método chamado PublishMessage que permite publicar uma mensagem em um tópico ou fila específica no Service Bus.
Aqui está uma explicação passo a passo do que o método PublishMessage faz:
1.	Cria uma instância do cliente ServiceBusClient usando a string de conexão fornecida no campo connectionString.
2.	Cria um remetente (ServiceBusSender) usando o nome do tópico ou fila fornecido no parâmetro topic_queue_Name.
3.	Serializa o objeto message em formato JSON usando o método JsonConvert.SerializeObject.
4.	Cria uma instância de ServiceBusMessage com o conteúdo da mensagem serializada em UTF-8.
5.	Define o CorrelationId da mensagem como um novo Guid gerado aleatoriamente.
6.	Envia a mensagem para o Service Bus usando o método SendMessageAsync do remetente.
7.	Libera os recursos do cliente e do remetente chamando os métodos DisposeAsync.
Em resumo, essa classe permite que você publique mensagens em um tópico ou fila específica no Service Bus, facilitando a comunicação assíncrona entre diferentes partes de um sistema distribuído.
*/

    public class MessageBus : IMessageBus
    {
        //1.	Cria uma instância do cliente ServiceBusClient usando a string de conexão fornecida no campo connectionString.
        private string connectionString = "Endpoint=sb://cardosorestauranteweb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=zYsIzPdW2uHwP+N5ERNnGOxPdOUut7shK+ASbNQuBuE=;EntityPath=emailcarrinho";

        public async Task PublishMessage(object message, string topic_queue_Name)
        {
            //Chave Primaria do Shared access Policies do Service Bus
            await using var client = new ServiceBusClient(connectionString); //1.	Cria uma instância do cliente ServiceBusClient usando a string de conexão fornecida no campo connectionString.

            ServiceBusSender sender = client.CreateSender(topic_queue_Name); //2.	Cria um remetente (ServiceBusSender) usando o nome do tópico ou fila fornecido no parâmetro topic_queue_Name.

            var jsonMessage = JsonConvert.SerializeObject(message); //3.	Serializa o objeto message em formato JSON usando o método JsonConvert.SerializeObject.

            //UTF-8 é uma codificação de caracteres Unicode que usa um ou mais bytes para cada caractere. É uma codificação comum para a web e também para arquivos de texto, pois suporta todos os caracteres Unicode, incluindo caracteres de idiomas diferentes do inglês.
            //4.	Cria uma instância de ServiceBusMessage com o conteúdo da mensagem serializada em UTF-8.
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString() //5.	Define o CorrelationId da mensagem como um novo Guid gerado aleatoriamente.
            };

            await sender.SendMessageAsync(finalMessage); //6.	Envia a mensagem para o Service Bus usando o método SendMessageAsync do remetente
            await client.DisposeAsync(); //7.	Libera os recursos do cliente e do remetente chamando os métodos DisposeAsync.
        }
    }
}