namespace VNetworks.Data
{
	public readonly struct IntResult
	{

		public readonly int Result;
		public readonly bool Status;


		/// <summary>
		/// Creates a new instance of the <see cref="IntResult"/> struct object.
		/// </summary>
		/// <param name="result"></param>
		/// <param name="status"></param>
		public IntResult(int result, bool status)
		{
			Result=result;
			Status=status;
		}

	}
}
