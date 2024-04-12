using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VNetworks
{
	public class PortCollection : List<PortInfo>
	{
		/// <summary>
		/// The number of items within the collection.
		/// </summary>
		public int Length => Count;

		protected readonly IPAddress Address;

		protected readonly TcpClient TCPClient;

		protected readonly UdpClient UDPClient;

		public event EventHandler<object?> OnUdpReceive;

		public event EventHandler<object?> OnUdpSend;


		public PortCollection(IPAddress address)
		{
			Address=address;
			TCPClient=new TcpClient();
			UDPClient = new UdpClient();
			OnUdpReceive+=PortCollection_OnUdpReceive;
		}

		protected PortInfo TestPort(int port)
		{

		}

		protected async Task<PortInfo> TcpTest(int port)
		{
			using(TcpClient client = new TcpClient())
			{
				try
				{
					var res=client.BeginConnect(Address, port, null, null);
					var reportHandle=res.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(100));
					string serviceName=reportHandle ? await GetService(client) : "";
					return new PortInfo(port, serviceName, reportHandle, PortTransportType.Tcp);
				}
				catch
				{
					return await UdpTest(port);
				}
			}
		}

		protected async Task<PortInfo> UdpTest(int port)
		{
			using(UdpClient client = new UdpClient())
			{
				try
				{
					client.Connect(Address, port);
					client.Send(
					client.BeginReceive(UdpRecieve, client);
				}
				catch
				{
					return new PortInfo(port, "", false, PortTransportType.Udp);
				}
			}
		}

		private async void UdpRecieve(object? value)
		{
			if(value is IAsyncResult iar)
			{
				UdpClient client=(UdpClient)iar.AsyncState!;
				IPEndPoint endPoint = (IPEndPoint)iar.AsyncState!;
				byte[] receiveBytes = client.EndReceive(iar, ref endPoint!);
				string receiveString = Encoding.ASCII.GetString(receiveBytes);
				Console.WriteLine(receiveString);
			}
		}

		private void PortCollection_OnUdpReceive(object? sender, object? value)
		{
			if(sender is IAsyncResult iar)
			{
				UdpClient client=(UdpClient)iar.AsyncState!;
				IPEndPoint endPoint = (IPEndPoint)iar.AsyncState!;
				byte[] receiveBytes = client.EndReceive(iar, ref endPoint!);
				string receiveString = Encoding.ASCII.GetString(receiveBytes);
				Console.WriteLine(receiveString);
			}
		}

		private async Task<string> GetService(TcpClient client)
		{
			NetworkStream stream=client.GetStream();
			byte[] data=Encoding.ASCII.GetBytes("Hello, server!");
			byte[] buffer=new byte[1024];
			int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
			return Encoding.ASCII.GetString(buffer, 0, bytesRead);
		}


	}
}
