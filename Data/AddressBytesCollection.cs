namespace VNetworks.Data
{
	public class AddressBytesCollection
	{

		public AddressBytes[] Bytes=Array.Empty<AddressBytes>();

		public AddressBytesCollection(params AddressBytes[] addressBytes)
		{
			Bytes=addressBytes;
		}

		public AddressBytes this[int index]
		{
			get => Bytes[index];
		}

	}
}
