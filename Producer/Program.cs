using Confluent.Kafka;
using System;
using System.Text.Json;

class Producer
{
    static void Main(string[] args)
    {
        const string topic = "sensor_data2";
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            Acks = Acks.All
        };

        TopicPartition topicPartition = new TopicPartition(topic, new Partition(0));
        using (var producer = new ProducerBuilder<string, string>(config).Build())
        {
            while (true)
            {
                string message = Console.ReadLine() ?? "Message is null";
                var messageForTopic = new MessageForTopic(message, topicPartition.Partition.ToString());
                string jsonValue = JsonSerializer.Serialize(messageForTopic);

                producer.Produce(topicPartition, new Message<string, string> { Key = "0", Value = jsonValue },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: key = '0' value = {messageForTopic.message}");
                        }
                    });
                producer.Flush(TimeSpan.FromSeconds(10));
                Console.WriteLine($" Message was produced to topic {topic}");
            }
        }
    }
}

public record MessageForTopic(string message, string partitionId);