using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Runtime.InteropServices;
using RabbitMQ.Client;
using MessageService.Models;
using System.Net;

namespace MessageService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IConfiguration _config;

        public MessageController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("movies")]
        public IActionResult GetAllMovies()
        {
            Console.WriteLine("HEEEEEEEEEERREEEEEEE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq"

            };

            factory.UserName = "guest";
            factory.Password = "guest";

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "movie",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // SearchMovies()
                var movieMessage = new MovieMessage
                {
                    //MessageQueueName = "movie",
                    FunctionToExecute = "GetAllMovies",
                    //FunctionToExecute = "SearchMovies",
                    Columns = Tuple.Create("Title", "ter")
                };


                string message = JsonSerializer.Serialize(movieMessage);
                Console.WriteLine(message);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "movie",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
            return StatusCode(200);
        }

    }

}
