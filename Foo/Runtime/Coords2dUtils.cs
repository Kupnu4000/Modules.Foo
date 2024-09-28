using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


namespace Modules.Foo
{
	[PublicAPI]
	public static class Coords2dUtils
	{
		public static IEnumerable<Coord2d> GetAllPositionsWithin (Vector2Int from, Vector2Int to)
		{
			return GetAllPositionsWithin(from.x, from.y, to.x, to.y);
		}

		public static IEnumerable<Coord2d> GetAllPositionsWithin (Coord2d from, Coord2d to)
		{
			return GetAllPositionsWithin(from.x, from.y, to.x, to.y);
		}

		public static IEnumerable<Coord2d> GetAllPositionsWithin (int minX, int minY, int maxX, int maxY)
		{
			for (int y = minY; y < maxY; y++)
			{
				for (int x = minX; x < maxX; x++)
				{
					yield return new Coord2d(y, x);
				}
			}
		}
	}
}
