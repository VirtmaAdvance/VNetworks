using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using VNetworks.Data;

namespace VNetworks
{
	/// <summary>
	/// Provides easily accessible network information.
	/// </summary>
	public class NetworkInformation : NetworkInterface
	{
		/// <summary>
		/// Gets the physical/MAC (Media Access Control) address.
		/// </summary>
		public PhysicalAddress PhysicalAddress => GetPhysicalAddress();
		/// <summary>
		/// Gets the properties of the IP.
		/// </summary>
		public IPInterfaceProperties IPProperties => GetIPProperties();
		/// <summary>
		/// Gets the statistical data for the IP.
		/// </summary>
		public IPInterfaceStatistics IPStatistics => GetIPStatistics();
		/// <summary>
		/// Gets the DHCP server addresses.
		/// </summary>
		public IPAddressCollection DhcpServerAddresses => IPProperties.DhcpServerAddresses;
		/// <summary>
		/// Gets the collection of unicast addresses.
		/// </summary>
		public UnicastIPAddressInformationCollection UnicastAddresses => IPProperties.UnicastAddresses;
		/// <summary>
		/// Gets the collection of unicast internetwork addresses.
		/// </summary>
		public IEnumerable<UnicastIPAddressInformation> InterNetworkUnicastAddresses => UnicastAddresses.Where(q=>q.Address.AddressFamily.HasAny(AddressFamily.InterNetwork, AddressFamily.InterNetworkV6));
		/// <summary>
		/// Gets the collection of IP addresses on the internetwork.
		/// </summary>
		public IEnumerable<IPAddress> InterNetworkIPAddresses => InterNetworkUnicastAddresses.Select(q=>q?.Address).Where(q=>q is not null)!;
		/// <summary>
		/// Gets the <see cref="IPAddress"/> object associated with this object.
		/// </summary>
		public IPAddress? IPv4Address => InterNetworkIPAddresses.Where(q=>q.AddressFamily.HasFlag(AddressFamily.InterNetwork)).FirstOrDefault();
		/// <summary>
		/// Gets the <see cref="IPAddress"/> object associated with this object.
		/// </summary>
		public IPAddress? IPv6Address => InterNetworkIPAddresses.Where(q=>q.AddressFamily.HasFlag(AddressFamily.InterNetworkV6)).FirstOrDefault();
		/// <summary>
		/// Gets the primary/active IPAddress.
		/// </summary>
		public IPAddress? PrimaryIPAddress
		{
			get => UnicastAddresses.Where(q=>(q.Address is not null) && q.Address.AddressFamily.HasAny(AddressFamily.InterNetwork, AddressFamily.InterNetworkV6)).Select(q=>q.Address).FirstOrDefault();
		}
		/// <summary>
		/// Gets the IPv4 subnet mask.
		/// </summary>
		public IPAddress? IPv4Mask => InterNetworkUnicastAddresses.FirstOrDefault(q=>q.Address.AddressFamily.HasFlag(AddressFamily.InterNetwork))?.IPv4Mask;

		public int PrefixLength => CalculatePrefixLength(IPv4Address, IPv4Mask);



		/// <summary>
		/// Gets the network interface that the <paramref name="ipAddress"/> exists within.
		/// </summary>
		/// <param name="ipAddress">The <see cref="IPAddress"/> object to look for.</param>
		/// <returns>the <see cref="NetworkInterface"/> object that holds the given <paramref name="ipAddress"/> upon success, or <see langword="null"/> otherwise.</returns>
		public static NetworkInterface? GetNetworkInterfaceFromIPAddress(IPAddress ipAddress)
		{
			foreach(var sel in GetAllNetworkInterfaces())
				if(sel.GetIPProperties().UnicastAddresses.FirstOrDefault(q=>q.Address.Equals(ipAddress)) is not null)
					return sel;
			return null;
		}
		/// <summary>
		/// Gets the IPAddress object.
		/// </summary>
		/// <param name="net_int"></param>
		/// <returns></returns>
		private static IPAddress? GetIPAddress(NetworkInterface? net_int) => net_int?.GetIPProperties().UnicastAddresses.Where(n => n.Address.AddressFamily==AddressFamily.InterNetwork).Select(g => g?.Address).Where(a => a!=null).FirstOrDefault();
		/// <summary>
		/// Calculates the prefix length when given both the <paramref name="ipAddress"/> and <paramref name="subnetMask"/>.
		/// </summary>
		/// <param name="ipAddress"></param>
		/// <param name="subnetMask"></param>
		/// <returns></returns>
		private static int CalculatePrefixLength(IPAddress? ipAddress, IPAddress? subnetMask)
		{
			var addresses=Internal_GetAddressBytes(ipAddress, subnetMask);
			if(addresses is null)
				return -1;
			int prefixLength = 0;
			for (int i = 0; i < addresses[0].Length; i++)
			{
				var res=Internal_GetPrefixLengthValue(addresses[0][i], addresses[1][i], prefixLength);
				prefixLength=res.Result;
				if(!res.Status)
					break;
			}
			return prefixLength;
		}

		private static IntResult Internal_GetPrefixLengthValue(byte ipByte, byte maskByte, int prefixLength=0) => Internal_CompareAddresses(ipByte, maskByte) ? new (prefixLength+8, true) : new (prefixLength+GetBitsToCount(ipByte, maskByte), true);

		private static bool Internal_CompareAddresses(byte a, byte b) => a == b;

		private static bool CheckIP(params IPAddress?[] addresses) => addresses.All(q=>q is not null);

		private static AddressBytesCollection? Internal_GetAddressBytes(IPAddress? address1, IPAddress? address2)
		{
			if(CheckIP(address1, address2))
			{
				var a=(AddressBytes)address1!.GetAddressBytes();
				var b=(AddressBytes)address2!.GetAddressBytes();
				return a.Length==b.Length ? new AddressBytesCollection(a, b) : null;
			}
			return null;
		}

		private static byte GetXorResult(byte ipByte, byte maskByte) => (byte)(ipByte ^ maskByte);

		private static int GetBitsToCount(byte ipByte, byte maskByte) => GetBitsToCount(GetXorResult(ipByte, maskByte));

		private static int GetBitsToCount(byte xorResult)
		{
			int bitsToCount=0;
			while(xorResult>0)
			{
				bitsToCount++;
				xorResult>>=1;
			}
			return bitsToCount;
		}


		/// <summary>
		/// Gets the currently active network interface.
		/// </summary>
		/// <returns></returns>
		public static NetworkInterface? GetActiveNetworkInterface() => GetAllNetworkInterfaces().FirstOrDefault(q=>q.OperationalStatus.HasFlag(OperationalStatus.Up));

		public static IPAddress? GetActiveIPAddress(NetworkInterface? networkInterface) => (networkInterface is not null) ? GetAllActiveIPAddresses(networkInterface).FirstOrDefault() : null;

		public static IPAddress? GetActiveIPAddress(NetworkInterface? networkInterface, AddressFamily addressFamily) => (networkInterface is not null) ? GetAllActiveIPAddresses(networkInterface).FirstOrDefault(q=>q.AddressFamily.HasAny(addressFamily.GetInstanceValues())) : null;

		public static IEnumerable<IPAddress> GetAllActiveIPAddresses(NetworkInterface sel) => GetAllActiveUnicastAddresses(sel).Select(q=>q.Address);

		public static IEnumerable<UnicastIPAddressInformation> GetAllActiveUnicastAddresses(NetworkInterface sel) => GetAllUnicastAddresses(sel).Where(q=>(q.Address is not null) && q.Address.AddressFamily.HasAny(AddressFamily.InterNetwork, AddressFamily.InterNetworkV6));

		public static UnicastIPAddressInformationCollection GetAllUnicastAddresses(NetworkInterface sel) => sel.GetIPProperties().UnicastAddresses;

	}
}
