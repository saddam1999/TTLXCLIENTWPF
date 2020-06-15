using System;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x02000016 RID: 22
	internal class ArrayTraverse
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003C08 File Offset: 0x00001E08
		public ArrayTraverse(Array array)
		{
			this.maxLengths = new int[array.Rank];
			for (int i = 0; i < array.Rank; i++)
			{
				this.maxLengths[i] = array.GetLength(i) - 1;
			}
			this.Position = new int[array.Rank];
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C60 File Offset: 0x00001E60
		public bool Step()
		{
			for (int i = 0; i < this.Position.Length; i++)
			{
				if (this.Position[i] < this.maxLengths[i])
				{
					this.Position[i]++;
					for (int j = 0; j < i; j++)
					{
						this.Position[j] = 0;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400001F RID: 31
		public int[] Position;

		// Token: 0x04000020 RID: 32
		private int[] maxLengths;
	}
}
