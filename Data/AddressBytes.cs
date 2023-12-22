namespace VNetworks.Data
{
	/// <summary>
	/// Stores an array of bytes (Designed to be used in an array of byte arrays).
	/// </summary>
	public readonly struct AddressBytes
	{
		/// <summary>
		/// The byte array of the value.
		/// </summary>
		public readonly byte[] Bytes;
		/// <summary>
		/// Gets the number of bytes that are in the array of bytes.
		/// </summary>
		public int Length => Bytes.Length;
		/// <summary>
		/// Gets the <see cref="byte"/> value at the given index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public byte this[int index] => Bytes[index];


		/// <summary>
		/// Creates a new instance of the <see cref="AddressBytes"/> struct object.
		/// </summary>
		/// <param name="bytes"></param>
		public AddressBytes(byte[] bytes) => Bytes=bytes;
		/// <inheritdoc cref="AddressBytes(byte[])"/>
		public static explicit operator AddressBytes(byte[] bytes) => new(bytes);
		/// <inheritdoc cref="AddressBytes(byte[])"/>
		public static explicit operator AddressBytes(System.Net.IPAddress ipAddress) => new(ipAddress.GetAddressBytes());

		public static bool operator ==(AddressBytes a, AddressBytes b) => a.Bytes.Equals(b.Bytes);

		public static bool operator !=(AddressBytes a, AddressBytes b) => !a.Bytes.Equals(b.Bytes);

		public override readonly bool Equals(object? obj)
		{
			if(obj is not null)
				return Bytes.Equals(obj) || base.Equals(obj);
			return false;
		}
		/// <summary>
		/// Gets the hash code for this object.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode() => Bytes.GetHashCode();
	}
}