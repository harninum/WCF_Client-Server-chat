using System.Windows;
using System.Windows.Input;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IService_ChatCallback
    {
        bool isConnected = false;
        Service_ChatClient client;
        int ID;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }


        void ConnectUser() 
        { 
            if(!isConnected)

            {
                client = new Service_ChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);           
                tbUserName.IsEnabled = false;
                bConnectdisconnect.Content = "Disconnect";
                isConnected = true;
            }
        }
        void DisconnectUser() 
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true;
                bConnectdisconnect.Content = "Connect";
                isConnected = false;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();

            }
            else
            {
                ConnectUser();

            }
        }

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(client!=null)
                {
                    client.Send_Msg(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
                }
                
            }
        }
    }
}
