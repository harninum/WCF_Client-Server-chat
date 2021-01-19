using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Chat_WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service_Chat : IService_Chat        
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextID = 1;


        public int Connect(string name)
        {
            ServerUser user = new ServerUser() //create a new user
            //user fields
            {
                ID = nextID, //filed ID
                Name = name, // filed name
                operationContext = OperationContext.Current

            };

            nextID++; // increase ID by 1
            Send_Msg(user.Name+" is connected. Welcome a new member! ", 0); //message when connecting a new client 
            users.Add(user); // add a new user
            return user.ID;  // return user ID
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if(user!= null) // if there is no user, it will be NULL. Need to check for NULL
            {
                users.Remove(user); // Remove user
                Send_Msg(user.Name+" was disconnected. Bye bye!", 0); //farewell message
            }
        }


        public void DoWork()
        {
        }

        public void Send_Msg(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString(); //date + message (11:41:54 - Hello)
                var user = users.FirstOrDefault(i => i.ID == id); 
                if (user != null) // if there is no user, it will be NULL. Need to check for NULL
                {
                    answer += ": " + user.Name+" ";
                }
                answer += msg;

                item.operationContext.GetCallbackChannel<IserverChatCallback>().MsgCallback(answer); //refer to the operationcontext filed, the user of wich we iterate in te loop, 
                                                                                                     //call the method CallBackCannel, pass the interface, 
                                                                                                     //call the method MSGCALLBACK in interface IserverChatCallback
                                                                                                     //client will resived the message (answer)
                
            }
        }
    }
}
