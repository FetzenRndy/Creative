using System.Numerics;

namespace CpuEmulator.Emulator.Tokens
{
	public class Label : Token
	{
		public readonly BigInteger FirstInstructionIndex;

		public readonly string Name;

		public Label(string name, BigInteger firstInstructionIndex)
		{
			Name = name;
			FirstInstructionIndex = firstInstructionIndex;
		}
	}
}