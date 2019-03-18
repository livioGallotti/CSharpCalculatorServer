using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace CSharpServerCalculator.test
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        {
        }
    }
    public class GameServer
    {
        private delegate void GameCommand(byte[] data, EndPoint sender);
        private Dictionary<char, GameCommand> commandsTable;
        private ITransport transport;
        public GameServer(ITransport Transport)
        {
            this.transport = Transport;
            commandsTable = new Dictionary<char, GameCommand>();
            commandsTable['0'] = addition;
            commandsTable['1'] = subtraction;
            commandsTable['2'] = multiplication;
            commandsTable['3'] = division;
        }
        private void addition(byte[] data, EndPoint sender)
        {
            float a = BitConverter.ToSingle(data, 1);
            float b = BitConverter.ToSingle(data, 5);

            float result = a + b;
            byte[] finalResult = BitConverter.GetBytes(result);

            transport.Send(finalResult, sender);


        }
        private void subtraction(byte[] data, EndPoint sender)
        {
            float a = BitConverter.ToSingle(data, 1);
            float b = BitConverter.ToSingle(data, 5);
            float result = a - b;
            byte[] finalResult = BitConverter.GetBytes(result);
            
            transport.Send(finalResult, sender);
        }
        private void multiplication(byte[] data, EndPoint sender)
        {
            float a = BitConverter.ToSingle(data, 1);
            float b = BitConverter.ToSingle(data, 5);
            float result = a * b;
            byte[] finalResult = BitConverter.GetBytes(result);

            transport.Send(finalResult, sender);
            
        }
        public void division(byte[] data, EndPoint sender)
        {
            float a = BitConverter.ToSingle(data, 1);
            float b = BitConverter.ToSingle(data, 5);
            float result;
            if (b==0||a==0 && b==0)
            {
                throw new ServerException("Impossible to divide by Zero");
            }
            else
            {
                result = a / b;
            }
            
            byte[] finalResult = BitConverter.GetBytes(result);

            transport.Send(finalResult, sender);
            
        }
        public void Update()
        {
            
            while (true)
            {
                SingleStep();
            }
        }

        public void SingleStep()
        {
            EndPoint sender = transport.CreateEndPoint();
            byte[] data = transport.Recv(256, ref sender);

            if (data != null)
            {
                char gameCommand =BitConverter.ToChar(data,0);
                if (commandsTable.ContainsKey(gameCommand))
                    commandsTable[gameCommand](data, sender);
                else
                {
                    throw new ServerException("Invalid GameCommand");
                }
                
                
                
            }
            
            
        }
    }
}
