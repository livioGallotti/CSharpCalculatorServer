using System;
using System.IO;

namespace CSharpServerCalculator.test
{
    public class FakePacket
    {
        private MemoryStream stream;
        private BinaryWriter writer;

        public FakePacket()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public FakePacket(char command, params object[] elements) : this()
        {
            writer.Write(command);
            foreach (object element in elements)
            {
                
                if (element is float)
                    writer.Write((float)element);
                else if (element is byte)
                    writer.Write((byte)element);
                else if (element is char)
                    writer.Write((char)element);
                
                else
                    throw new Exception("unknown type");
            }
        }

        public byte[] GetData()
        {
            return stream.ToArray();
        }
    }
}
