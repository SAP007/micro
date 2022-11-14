﻿using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using MovieService.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Connections;
using MovieService.Controller;

namespace MovieService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MovieController _movieController;
        private const string InQueueName = "movie";

        public Worker(ILogger<Worker> logger, MovieController movieController)
        {
            _logger = logger;
            _movieController = movieController;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Queue [{InQueueName}] is waiting for messages.");
            System.Threading.Thread.Sleep(60000);
            var factory = new ConnectionFactory { HostName = "rabbitmq" };
            factory.UserName = "guest";
            factory.Password = "guest";
            var connection = factory.CreateConnection();
            var inChannel = connection.CreateModel();
            var outChannel = connection.CreateModel();

            inChannel.BasicQos(0, 1, false);

            inChannel.QueueDeclare(queue: InQueueName,
                                   durable: false, // true if sender's durable is true!!!
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);

            var consumer = new EventingBasicConsumer(inChannel);
            consumer.Received += (sender, ea) =>
            {
                var inBody = ea.Body.ToArray();
                var inMessage = Encoding.UTF8.GetString(inBody);

                // publish result on outChannel and keep listening for more messages
                var outMessage = _movieController.MessageRecieved(inMessage);
                var outBody = Encoding.UTF8.GetBytes(outMessage);
                Console.WriteLine("Made it so FAAAAAAR");
                Console.WriteLine(outBody.ToString());
                outChannel.BasicPublish(exchange: "", routingKey: "resmovie", basicProperties: null, body: outBody);

                inChannel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            inChannel.BasicConsume(queue: InQueueName,
                                    autoAck: false,
                                    consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

    }
}




