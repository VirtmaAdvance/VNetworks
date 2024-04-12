namespace VNetworks
{
	/// <summary>
	/// A structured object for storing port information.
	/// </summary>
	/// <remarks>
	/// Stores port information.
	/// </remarks>
	/// <param name="value"></param>
	/// <param name="name"></param>
	/// <param name="isOpen"></param>
	/// <param name="portType"></param>
	public readonly struct PortInfo(int value, string name, bool isOpen, PortTransportType portType)
	{
		/// <summary>
		/// The port number.
		/// </summary>
		public readonly int Value=value;
		/// <summary>
		/// The name of the service running on the port.
		/// </summary>
		public readonly string Name=name;
		/// <summary>
		/// Indicates if the port is open or closed.
		/// </summary>
		public readonly bool IsOpen=isOpen;
		/// <summary>
		/// Indicates the transport type being used for the port.
		/// </summary>
		public readonly PortTransportType PortType=portType;
	}
}