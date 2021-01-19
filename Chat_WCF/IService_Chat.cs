using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chat_WCF
{

    [ServiceContract(CallbackContract =typeof(IserverChatCallback))]
    public interface IService_Chat
    {
        [OperationContract]
        int Connect(string name); //   Called when the client connects to the chat/ We need the answer, because it retern client's id 

        [OperationContract]
        void  Disconnect(int id); //   Called when the client disconnects to the chat. Accepts as a id client parameter/

        [OperationContract(IsOneWay = true)] // We don't need to wait for a response from the server

        void Send_Msg(string msg, int ID );
    }
    public interface IserverChatCallback //The server sends the message to all clients. On the server side - the interface, on the client-the implementation
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg); 
    }
        


}
