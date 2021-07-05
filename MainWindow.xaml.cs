using System;
using System.Diagnostics;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RabbitMQUtilityMessenger
{
    public class Rabbit
    {
        public static void SendMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost"};

            //SocketException: Подключение не установлено, т.к. конечный компьютер отверг запрос на подключение.
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "first",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "TestTestTest";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "first",
                                     basicProperties: null,
                                     body: body);

                //Проверить отправку сообщения в консоли вывода
                Trace.WriteLine(" [x] Sent {0}", message);
            }
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            Rabbit.SendMessage();
        }
    }
}
