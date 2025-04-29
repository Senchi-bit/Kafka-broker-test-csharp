using Confluent.Kafka;

class Consumer
{
    public static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            
            GroupId         = "mygroup",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        const string topic = "sensor_data2";
        
        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => {
            e.Cancel = true;
            cts.Cancel();
        };
        
        using (var consumer = new ConsumerBuilder<string, string>(config).Build())
        {
            TopicPartition topicPartition = new TopicPartition(topic, 0);
            consumer.Assign(topicPartition);
            try {
                while (true) {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed event from topic {topic}: key = '0' value = {cr.Message.Value}");
                }
            }
            catch (OperationCanceledException) {
                // Ctrl-C was pressed.
            }
            finally{
                consumer.Close();
            }
        }
    }
}