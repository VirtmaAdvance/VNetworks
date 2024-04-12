namespace VNetworks
{
	/// <summary>
	/// Specifies the port type.
	/// </summary>
	[Flags]
	public enum PortTransportType
	{
		/// <summary>
		/// The port type is unknown.
		/// </summary>
		Unknown=0x0,
		/// <summary>
		/// The port uses TCP.
		/// </summary>
		Tcp=0x1,
		/// <summary>
		/// The port uses UDP.
		/// </summary>
		Udp=0x2,

	}
}