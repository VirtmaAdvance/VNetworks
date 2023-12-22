using System.Net;
using System.Net.NetworkInformation;

namespace VNetworks
{
	/// <summary>
	/// Provides a class object that manages networking operations.
	/// </summary>
	public class VNetwork
	{
		/// <summary>
		/// Gets the current IPv4 address.
		/// </summary>
		public static IPAddress? CurrentIPv4Address => NetworkInformation.GetActiveIPAddress(NetworkInformation.GetActiveNetworkInterface(), System.Net.Sockets.AddressFamily.InterNetwork);
		/// <summary>
		/// Gets the current IPv6 address.
		/// </summary>
		public static IPAddress? CurrentIPv6Address => NetworkInformation.GetActiveIPAddress(NetworkInformation.GetActiveNetworkInterface(), System.Net.Sockets.AddressFamily.InterNetworkV6);

		public static IPAddress? GetPrimaryIPAddress() => NetworkInformation.GetActiveIPAddress(NetworkInformation.GetActiveNetworkInterface());
		public static NetworkInterface[] GetAllInterfaces() => NetworkInterface.GetAllNetworkInterfaces();

	}
}