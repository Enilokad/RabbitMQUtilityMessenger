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
        public static void SendMessage(string chosenExchange, string chosenRoutingKey, string message)
        {
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: chosenExchange, type: "direct");

                    channel.BasicPublish(exchange: chosenExchange,
                                         routingKey: chosenRoutingKey,
                                         basicProperties: null,
                                         body: Encoding.UTF8.GetBytes(message));
                }
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
            //validation
            if (string.IsNullOrWhiteSpace(exchangeBox.Text) | string.IsNullOrWhiteSpace(routingKeyBox.Text) | string.IsNullOrWhiteSpace(messageBox.Text))
            {
                validationBox.Visibility = Visibility.Visible;
            }
            else
            {
                validationBox.Visibility = Visibility.Hidden;
                Rabbit.SendMessage(exchangeBox.Text, routingKeyBox.Text, messageBox.Text);
            }
            
        }
    }
}
