﻿using System;
using System.Net;
using System.Net.Sockets;

namespace CSharpServerCalculator.test
{
    class TransportIPv4 : ITransport
    {
        private Socket socket;

        public TransportIPv4()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
        }

        public void Bind(string address, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any/*IPAddress.Parse(address)*/, port);
            socket.Bind(endPoint);
        }

        public bool Send(byte[] data, EndPoint endPoint)
        {
            bool success = false;

            try
            {
                int rlen = socket.SendTo(data, endPoint);
                if (rlen == data.Length)
                    success = true;
            }
            catch
            {
                success = false;
            }

            return success;
            //socket.SendTo(data,endPoint);
        }

        public byte[] Recv(int bufferSize, ref EndPoint sender)
        {
            int rlen = -1;
            byte[] data = new byte[bufferSize];

            try
            {
                rlen = socket.ReceiveFrom(data, ref sender);
                if (rlen <= 0)
                    return null;
            }
            catch
            {
                return null;
            }

            byte[] trueData = new byte[rlen];
            Buffer.BlockCopy(data, 0, trueData, 0, rlen);

            return trueData;
        }

        public EndPoint CreateEndPoint()
        {
            return new IPEndPoint(0, 0);
        }

       
    }
}
