using System.Net;
using System.Net.NetworkInformation;

namespace VNetworks
{
	/// <summary>
	/// Provides additional IPAddress information while also maintaining a conversion with the <see cref="IPAddress"/> class.
	/// </summary>
	public class NetworkIP:IPAddress
	{
		/// <summary>
		/// The <see cref="IPAddress"/> object.
		/// </summary>
		public IPAddress Value => new(base.GetAddressBytes());
		/// <summary>
		/// Gets the active network interface.
		/// </summary>
		public NetworkInterface? ActiveNetworkInterface => NetworkInformation.GetNetworkInterfaceFromIPAddress(Value);
		/// <summary>
		/// Gets the active network information object.
		/// </summary>
		public NetworkInformation? ActiveNetworkInformation => ActiveNetworkInterface is null ? null : ((NetworkInformation)ActiveNetworkInterface);
		/// <summary>
		/// Gets the subnetmask of the current IPAddress.
		/// </summary>
		public IPAddress? SubnetMask => ActiveNetworkInformation?.IPv4Mask;


		/// <inheritdoc cref="NetworkIP(ReadOnlySpan{byte}, long)"/>
		public NetworkIP(byte[] address) : base(address)
		{
		}
		/// <inheritdoc cref="NetworkIP(ReadOnlySpan{byte}, long)"/>
		public NetworkIP(long newAddress) : base(newAddress)
		{
		}
		/// <inheritdoc cref="NetworkIP(ReadOnlySpan{byte}, long)"/>
		public NetworkIP(ReadOnlySpan<byte> address) : base(address)
		{
		}
		/// <inheritdoc cref="NetworkIP(ReadOnlySpan{byte}, long)"/>
		public NetworkIP(byte[] address, long scopeid) : base(address, scopeid)
		{
		}
		/// <summary>
		/// Creates a new instance of the <see cref="NetworkIP"/> class object.
		/// </summary>
		/// <param name="address">The address value.</param>
		/// <param name="scopeid">The scope id.</param>
		public NetworkIP(ReadOnlySpan<byte> address, long scopeid) : base(address, scopeid)
		{
		}
		/// <inheritdoc cref="NetworkIP(ReadOnlySpan{byte}, long)"/>
		public NetworkIP(IPAddress address) : base(address.GetAddressBytes())
		{
		}
		/// <inheritdoc cref="NetworkIP(ReadOnlySpan{byte}, long)"/>
		public NetworkIP(string address) : this(Parse(address))
		{
		}

	}
}