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
        public static void SendMessage(string queue, string data)
        {
            using (IConnection connection = new ConnectionFactory() { HostName = "localhost" }.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, false, false, false, null);
                    channel.BasicPublish(string.Empty, queue, null, Encoding.UTF8.GetBytes(data));
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
            if (string.IsNullOrEmpty(messageBox.Text))
            {
                validationBox.Visibility = Visibility.Visible;
            }
            else 
            {
                validationBox.Visibility = Visibility.Hidden;
                Rabbit.SendMessage("queue1", messageBox.Text);
            }
            
        }
    }
}
