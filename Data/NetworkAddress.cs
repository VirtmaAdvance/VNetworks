using System.Net;

namespace VNetworks.Data
{
	/// <summary>
	/// Provides a structred object that stores an IPAddress value.
	/// </summary>
	public readonly struct NetworkAddress:IEquatable<NetworkAddress>, IComparable<NetworkAddress>
	{

		private readonly IPAddress? _ipAddress;
		private readonly string? _addressValue;
		private readonly NetworkIP? _networkIP;
		/// <summary>
		/// The Address to send to.
		/// </summary>
		public readonly string? Address => _addressValue??_networkIP?.ToString()??_ipAddress?.ToString();


		/// <inheritdoc cref="NetworkAddress(NetworkIP, IPAddress, string)"/>
		public NetworkAddress(IPAddress ipAddress)
		{
			_ipAddress=ipAddress;
			_networkIP=new NetworkIP(ipAddress);
			_addressValue=ipAddress.ToString();
		}
		/// <inheritdoc cref="NetworkAddress(NetworkIP, IPAddress, string)"/>
		public NetworkAddress(NetworkIP networkIP)
		{
			_ipAddress=networkIP.Value;
			_networkIP=networkIP;
			_addressValue=networkIP.ToString();
		}
		/// <inheritdoc cref="NetworkAddress(NetworkIP, IPAddress, string)"/>
		public NetworkAddress(string value)
		{
			_addressValue=value;
			_ipAddress=IPAddress.Parse(value);
			_networkIP=new NetworkIP(_ipAddress);
		}
		/// <summary>
		/// Creates a new instance of the <see cref="NetworkAddress"/> struct object.
		/// </summary>
		/// <param name="networkIP">The <see cref="NetworkIP"/> object to use.</param>
		/// <param name="ipAddress">The <see cref="IPAddress"/> object to use.</param>
		/// <param name="value">The <see cref="string"/> value to use.</param>
		public NetworkAddress(NetworkIP? networkIP, IPAddress? ipAddress, string? value)
		{
			_ipAddress=ipAddress;
			_networkIP=networkIP;
			_addressValue=value;
		}
		/// <inheritdoc cref="NetworkAddress(NetworkIP, IPAddress, string)"/>
		public static explicit operator NetworkAddress(string value) => new(value);
		/// <inheritdoc cref="NetworkAddress(NetworkIP, IPAddress, string)"/>
		public static explicit operator NetworkAddress(IPAddress value) => new(value);
		/// <inheritdoc cref="NetworkAddress(NetworkIP, IPAddress, string)"/>
		public static explicit operator NetworkAddress(NetworkIP value) => new(value);
		/// <summary>
		/// Gets the string representation of this object.
		/// </summary>
		/// <returns></returns>
		public new string ToString() => _addressValue??"";

		public bool Equals(NetworkAddress other) => other._ipAddress == _ipAddress || other._addressValue == _addressValue || other._networkIP == _networkIP;

		public int CompareTo(NetworkAddress other) => (other._addressValue is not null) ? other._addressValue.CompareTo(_addressValue) : throw new InvalidOperationException("Comparing data with invalid or null values.");

		public override bool Equals(object? obj)
		{
			if(obj is not null)
			{
				string? val=obj.ToString();
				return val is not null && val.Equals(_addressValue);
			}
			return false;
		}

		public override int GetHashCode() => _addressValue?.GetHashCode() ?? _ipAddress?.GetHashCode() ?? _networkIP?.GetHashCode() ?? 0;

		public static bool operator ==(NetworkAddress left, NetworkAddress right) => left.Equals(right);

		public static bool operator !=(NetworkAddress left, NetworkAddress right) => !(left==right);

		public static bool operator <(NetworkAddress left, NetworkAddress right) => left.CompareTo(right)<0;

		public static bool operator <=(NetworkAddress left, NetworkAddress right) => left.CompareTo(right)<=0;

		public static bool operator >(NetworkAddress left, NetworkAddress right) => left.CompareTo(right)>0;

		public static bool operator >=(NetworkAddress left, NetworkAddress right) => left.CompareTo(right)>=0;
	}
}