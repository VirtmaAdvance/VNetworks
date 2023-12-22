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
		/// <summary>
		/// Gets the primary <see cref="IPAddress"/> the current machine is assigned on it's active network adapter.
		/// </summary>
		/// <returns>the <see cref="IPAddress"/> representation of the current IP address.</returns>
		public static IPAddress? GetPrimaryIPAddress() => NetworkInformation.GetActiveIPAddress(NetworkInformation.GetActiveNetworkInterface());
		/// <summary>
		/// Gets all network interfaces.
		/// </summary>
		/// <returns>an array of <see cref="NetworkInterface"/>s available to the current machine.</returns>
		public static NetworkInterface[] GetAllInterfaces() => NetworkInterface.GetAllNetworkInterfaces();

	}
}