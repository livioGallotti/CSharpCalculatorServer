using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using NUnit.Framework;

namespace CSharpServerCalculator.test
{
    public class ServerTest
    {
        private GameServer server;
        private FakeTransport transport;
        private FakeEndPoint client;

        [SetUp]
        public void SetUpTest()
        {
            transport = new FakeTransport();
            server = new GameServer(transport);
            client = new FakeEndPoint();
        }
        [Test]
        public void TestAddition()
        {
            FakePacket packet = new FakePacket('0',1.0f,1.0f);
            transport.ClientEnqueue(packet,"tester",0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result,Is.EqualTo(2.0f));
        }
        [Test]
        public void TestAdditionNegative()
        {
            FakePacket packet = new FakePacket('0', -1.0f, -1.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(-2.0f));
        }
        [Test]
        public void TestAdditionWithZero()
        {
            FakePacket packet = new FakePacket('0', 0.0f, 0.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(0.0f));
        }

        [Test]
        public void TestSubtraction()
        {
            FakePacket packet = new FakePacket('1', 1.0f, 1.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(0.0f));
        }
        [Test]
        public void TestSubtractionNegative()
        {
            FakePacket packet = new FakePacket('1', 0.0f, -1.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(1.0f));
        }
        [Test]
        public void TestSubtractionWithZero()
        {
            FakePacket packet = new FakePacket('1', 0.0f, 0.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(0.0f));
        }
        [Test]
        public void TestDivision()
        {
            FakePacket packet = new FakePacket('3', 10.0f, 5.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(2.0f));
        }
        [Test]
        public void TestDivisionNegative()
        {
            FakePacket packet = new FakePacket('3', 10.0f, -5.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(-2.0f));
        }
        [Test]
        public void TestDivisionWithZero()
        {
            FakePacket packet = new FakePacket('3',0.0f, 0.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            
            Assert.That(() => server.SingleStep(), Throws.InstanceOf<ServerException>());
        }
        [Test]
        public void TestDivisionWithZeroB()
        {
            FakePacket packet = new FakePacket('3',10.0f, 0.0f);
            transport.ClientEnqueue(packet, "tester", 0);

            Assert.That(() => server.SingleStep(), Throws.InstanceOf<ServerException>());
        }
        [Test]
        public void TestDivisionWithZeroA()
        {
            FakePacket packet = new FakePacket('3', 0.0f, 10.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(0.0f));
        }
        [Test]
        public void TestMoltiplication()
        {
            FakePacket packet = new FakePacket('2',2.0f, 2.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(4.0f));
        }
        [Test]
        public void TestMoltiplicationNegative()
        {
            FakePacket packet = new FakePacket('2', 2.0f, -2.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(-4.0f));
        }
        [Test]
        public void TestMoltiplicationWithZero()
        {
            FakePacket packet = new FakePacket('2', 2.0f, 0.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();
            float result = BitConverter.ToSingle(transport.ClientDequeue().data, 0);
            Assert.That(result, Is.EqualTo(0.0f));
        }

        [Test]
        public void TestCommandError()
        {
            FakePacket packet = new FakePacket('4', 1.0f, 1.0f);
            transport.ClientEnqueue(packet, "tester", 0);
            
            Assert.That(() => server.SingleStep(), Throws.InstanceOf<ServerException>());
        }
    }
}
