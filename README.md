## This is my test Apache Kafka on c# language with library Confluent.Kafka

For run this projects, you should already dot net 9!

[Dot Net 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

And you should already have Apache Kafka. 
You can download it from the official website:

[Apache Kafka](https://kafka.apache.org/)

Alternative way:
You should download docker image bitnami/kafka:latest.

```bash
  cd Dotnet-test
  docker-compose up -d
```

For run Producer: 
```bash
  cd Dotnet-test
  dotnet run --project ./Producer/Producer.csproj
```
For run Consumer:
```bash
  cd Dotnet-test
  dotnet run --project ./Consumer/Consumer.csproj
```