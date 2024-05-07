namespace CardosoRestaurante.MessageBus
{
    public interface IMessageBus
    {
        /// <summary>
        /// A classe <see cref="MessagePublisher"/> é responsável por implementar a funcionalidade de um barramento de mensagens.
        /// Ela contém um método chamado <see cref="PublishMessage"/> que permite publicar uma mensagem em um tópico ou fila específica no Service Bus.
        /// </summary>
        /// <remarks>
        /// <para>Segue abaixo uma explicação passo a passo do que o método <see cref="PublishMessage"/> faz:</para>
        /// <list type="number">
        /// <item>Cria uma instância do cliente <see cref="ServiceBusClient"/> usando a string de conexão fornecida no campo <paramref name="connectionString"/>.</item>
        /// <item>Cria um remetente (<see cref="ServiceBusSender"/>) usando o nome do tópico ou fila fornecido no parâmetro <paramref name="topicQueueName"/>.</item>
        /// <item>Serializa o objeto <paramref name="message"/> em formato JSON usando o método <see cref="JsonConvert.SerializeObject"/>.</item>
        /// <item>Cria uma instância de <see cref="ServiceBusMessage"/> com o conteúdo da mensagem serializada em UTF-8.</item>
        /// <item>Define o CorrelationId da mensagem como um novo <see cref="Guid"/> gerado aleatoriamente.</item>
        /// <item>Envia a mensagem para o Service Bus usando o método <see cref="SendMessageAsync"/> do remetente.</item>
        /// <item>Libera os recursos do cliente e do remetente chamando os métodos <see cref="DisposeAsync"/>.</item>
        /// </list>
        /// <para>Em resumo, essa classe permite que você publique mensagens em um tópico ou fila específica no Service Bus,</para>
        /// <para>facilitando a comunicação assíncrona entre diferentes partes de um sistema distribuído.</para>
        /// </remarks>
        Task PublishMessage(object message, string topic_queue_Name);
    }
}