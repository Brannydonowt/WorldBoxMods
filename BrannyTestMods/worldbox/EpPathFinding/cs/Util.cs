using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000317 RID: 791
	public class Util
	{
		// Token: 0x06001282 RID: 4738 RVA: 0x0009E4F7 File Offset: 0x0009C6F7
		public static DiagonalMovement GetDiagonalMovement(bool iCrossCorners, bool iCrossAdjacentPoint)
		{
			if (iCrossCorners && iCrossAdjacentPoint)
			{
				return DiagonalMovement.Always;
			}
			if (iCrossCorners)
			{
				return DiagonalMovement.IfAtLeastOneWalkable;
			}
			return DiagonalMovement.OnlyWhenNoObstacles;
		}
	}
}
