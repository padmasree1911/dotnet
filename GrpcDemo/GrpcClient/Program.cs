using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please provide username.");
                var username = Console.ReadLine();

                Console.WriteLine("Please provide password.");
                var password = Console.ReadLine();

                var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

                var headers = new Metadata();
                headers.Add("Authorization", $"Basic {token}");

                using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new Greeter.GreeterClient(channel);

                Console.WriteLine("Sending unary call...");

                var reply = await client.SayHelloAsync(
                    new HelloRequest { Name = "GreeterClient" }, headers);

                Console.WriteLine("Unary Response: " + reply.Message);

                Console.WriteLine("Sending request for server stream...");
                using (var call = client.SayManyHellos(new HelloRequest { Name = "GreeterClient" }, headers))
                {
                    await foreach (var response in call.ResponseStream.ReadAllAsync())
                    {
                        Console.WriteLine("New element from response stream: " + response.Message);
                    }
                }

                Console.WriteLine("Sending client stream...");
                var listOfNames = new List<String> { "John", "James", "Freddy", "David" };

                Console.WriteLine("Names about to be sent: " + string.Join(", ", listOfNames));
                using (var call = client.SayHelloToLastRequest(headers))
                {
                    foreach (var name in listOfNames)
                    {
                        await call.RequestStream.WriteAsync(new HelloRequest { Name = name });
                    }

                    await call.RequestStream.CompleteAsync();

                    Console.WriteLine("Response from client stream: " + (await call.ResponseAsync).Message);
                }

                Console.WriteLine("Sending bi-directional stream...");
                using (var call = client.SayHelloToEveryRequest(headers))
                {
                    foreach (var name in listOfNames)
                    {
                        await call.RequestStream.WriteAsync(new HelloRequest { Name = name });
                    }

                    await call.RequestStream.CompleteAsync();

                    await foreach (var response in call.ResponseStream.ReadAllAsync())
                    {
                        Console.WriteLine("Individual item from bi-directional call: " + response.Message);
                    }
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch
            {
               
            }

        }
    }
}
