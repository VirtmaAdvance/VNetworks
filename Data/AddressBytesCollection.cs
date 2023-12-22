namespace VNetworks.Data
{
	/// <summary>
	/// Provides a means to store a collection of address bytes.
	/// </summary>
	public class AddressBytesCollection
	{
		/// <summary>
		/// The address bytes.
		/// </summary>
		public AddressBytes[] Bytes=Array.Empty<AddressBytes>();
		/// <summary>
		/// Gets the <see cref="AddressBytes"/> object at the given index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public AddressBytes this[int index]
		{
			get => Bytes[index];
		}

		/// <summary>
		/// Creates a new instance of the <see cref="AddressBytesCollection"/> class.
		/// </summary>
		/// <param name="addressBytes">an array of <see cref="AddressBytes"/>.</param>
		public AddressBytesCollection(params AddressBytes[] addressBytes)
		{
			Bytes=addressBytes;
		}

	}
}