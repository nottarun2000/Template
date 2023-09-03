using DotNet.gRPC.Wrapper;
using DotNet.WPF.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNet.WPF.DataContext
{
    public class SecondWindowDataContext : BaseViewDataContext 
    {
        gRPCClient client { get; set; }

        public ICommand ConnectgRPC { get; set; }
        public ICommand ChangeColor { get; set; }

        public SecondWindowDataContext()
        {
            ConnectgRPC = new RelayCommand(this.ConnectParent , CanExecute);
            ChangeColor = new RelayCommand(this.CommunicateParent, CanExecute);
        }

        public void ConnectParent(object obj)
        {
            try
            {
                client = new gRPCClient("localhost", 5000);

                client.ClientConnected += (e, args) => {
                
                };

                client.ClientDisconnected += (e, args) => {

                };

                client.MessageReceived += (e, args) => {

                };

                client.ExceptionOccurred += (e, args) => {

                };

                client.StartClient();

            }
            catch (Exception ex)
            {

            }

        }


        public void CommunicateParent(object obj)
        {
            try
            {
                client.SendMessageToServer("Hello" , gRPC.Common.Enum.ServerMessageType.Internal);

            }
            catch(Exception ex)
            {

            }

        }


        public bool CanExecute(object parameters)
        {
            return true;
        }

        
    }
}
