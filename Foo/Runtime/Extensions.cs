using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


namespace Modules.Foo
{
	[PublicAPI]
	public static class Extensions
	{
		public static bool IsInRange (this Coord2d self, int xMin, int yMin, int xMax, int yMax, bool includeMax = true)
		{
			bool xInRange = self.x >= xMin && self.x < (includeMax ? xMax + 1 : xMax);
			bool yInRange = self.y >= yMin && self.y < (includeMax ? yMax + 1 : yMax);

			return xInRange && yInRange;
		}

		public static bool IsInRange (this Coord2d self, Coord2d min, Coord2d max, bool includeMax = true)
		{
			return self.IsInRange(min.x, min.y, max.x, max.y, includeMax);
		}

		public static bool IsAdjacentTo (this Coord2d self, Coord2d other)
		{
			return self.ManhattanDistanceTo(other) == 1;
		}

		public static Vector2 ToVector2 (this Coord2d self)
		{
			return new Vector2(self.x, self.y);
		}

		public static Vector2Int ToVector2Int (this Coord2d self)
		{
			return new Vector2Int(self.x, self.y);
		}

		public static Vector3 ToVector3 (this Coord2d self, bool zIsUp = false)
		{
			return new Vector3(
				self.x,
				zIsUp ? 0 : self.y,
				zIsUp ? self.y : 0
			);
		}

		public static Vector3Int ToVector3Int (this Coord2d self, bool zIsUp = false)
		{
			return new Vector3Int(
				self.x,
				zIsUp ? 0 : self.y,
				zIsUp ? self.y : 0
			);
		}

		public static Coord2d Offset (this Coord2d self, int x, int y)
		{
			return new Coord2d(self.x + x, self.y + y);
		}

		public static int ManhattanDistanceTo (this Coord2d self, Coord2d other)
		{
			return Math.Abs(self.x - other.x) + Math.Abs(self.y - other.y);
		}

		public static IEnumerable<Coord2d> LineTo (this Coord2d self, Coord2d other)
		{
			int currentX = self.x;
			int currentY = self.y;

			int deltaX = Math.Abs(self.x - other.x);
			int deltaY = Math.Abs(self.y - other.y);

			int stepX = self.x < other.x ? 1 : -1;
			int stepY = self.y < other.y ? 1 : -1;

			int error = deltaX - deltaY;

			while (true)
			{
				yield return new Coord2d(currentX, currentY);

				if (currentX == other.x && currentY == other.y)
				{
					yield break;
				}

				int doubleError = error * 2;

				if (doubleError > -deltaY)
				{
					error    -= deltaY;
					currentX += stepX;
				}

				if (doubleError >= deltaX)
				{
					continue;
				}

				error    += deltaX;
				currentY += stepY;
			}
		}
	}
}
