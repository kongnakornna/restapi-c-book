using Jtech.Common.Brokers.Base;
using Microsoft.EntityFrameworkCore.Metadata;
using MongoDB.Bson;
using MongoDB.Driver.Core.Connections;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IConnection = RabbitMQ.Client.IConnection;
using IModel = RabbitMQ.Client.IModel;
using Helpers = Jtech.Common.Helpers;
using Jtech.Common.Helpers;

namespace Jtech.Common.Brokers.Providers.Rabbit
{
    public class RabbitWorkerPublisherProvider: PublisherProviderBase
    {
        private IConnection _connection;
     
        public RabbitWorkerPublisherProvider( ConnectionFactory factory,List<string> hostList)
        {
            _connection = factory.CreateConnection(hostList);
        }

        protected override async Task PublishToBrokerAsync<TContact>(TContact Message, string QName)
        {
            using (IModel channel = _connection.CreateModel())
            {
                channel.QueueDeclare(QName, false, false, false, null);
                channel.ConfirmSelect();
                IBasicProperties basicProperties = channel.CreateBasicProperties();
                basicProperties.Persistent = true;

                var body = Encoding.UTF8.GetBytes(Helpers.Json.Serialize(Message));

                DateTime timeOut = DateTime.Now.GetGMTNow().AddMilliseconds(1000);
                bool isExit = false;
                string exceptionMsg = "";
                //For Retry
                int countRetry = 0;
                while (!isExit && timeOut >= DateTime.Now.GetGMTNow())
                {
                    try
                    {
                        channel.BasicPublish(exchange: "",
                                                       routingKey: QName,
                                                       basicProperties: basicProperties,
                                                       body: body);
                        channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));
                        isExit = true;
                    }
                    catch (Exception ex)
                    {
                        exceptionMsg = ex.Message;
                        countRetry++;
                    }
                }
            }
            await Task.CompletedTask;
        }
    }
}
