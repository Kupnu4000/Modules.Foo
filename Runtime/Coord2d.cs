using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


// TODO Refactor: rename Coord
// TODO Coords3d
// TODO проверить как сериализуется
// TODO Refactor: add assembly
// TODO написать сериализаторы под разные джсоны
namespace Modules.Foo
{
	[PublicAPI]
	[Serializable]
	public readonly struct Coord2d : IEquatable<Coord2d>, IComparable<Coord2d>
	{
		public static readonly Coord2d Zero  = new Coord2d(0,  0);
		public static readonly Coord2d One   = new Coord2d(1,  1);
		public static readonly Coord2d Up    = new Coord2d(0,  1);
		public static readonly Coord2d Down  = new Coord2d(0,  -1);
		public static readonly Coord2d Left  = new Coord2d(-1, 0);
		public static readonly Coord2d Right = new Coord2d(1,  0);

		public readonly int x;
		public readonly int y;

		public Coord2d (int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Coord2d (Vector2Int vector) : this(vector.x, vector.y) {}

		public Coord2d (Vector3Int vector, bool zIsUp = false)
		{
			x = vector.x;
			y = zIsUp ? vector.z : vector.y;
		}

		public static Coord2d operator + (Coord2d a, Coord2d b)
		{
			return new Coord2d(a.x + b.x, a.y + b.y);
		}

		public static Coord2d operator - (Coord2d a, Coord2d b)
		{
			return new Coord2d(a.x - b.x, a.y - b.y);
		}

		public static bool operator == (Coord2d a, Coord2d b)
		{
			return a.Equals(b);
		}

		public static bool operator != (Coord2d a, Coord2d b)
		{
			return !a.Equals(b);
		}

		public bool Equals (Coord2d other)
		{
			return x == other.x && y == other.y;
		}

		public override bool Equals (object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			return obj is Coord2d other && Equals(other);
		}

		public int CompareTo (Coord2d other)
		{
			return y == other.y
				? x.CompareTo(other.x)
				: y.CompareTo(other.y);
		}

		public static Coord2d Parse (string input)
		{
			ReadOnlySpan<char> span = input.AsSpan();

			int delimiterIndex = span.IndexOf(' ');

			if (delimiterIndex >= 0)
			{
				bool resultX = int.TryParse(span[..delimiterIndex],       out int x);
				bool resultY = int.TryParse(span[(delimiterIndex + 1)..], out int y);

				if (resultX && resultY)
				{
					return new Coord2d(x, y);
				}
			}

			throw new FormatException($"Input string '{input}' is not in the correct format.");
		}

		public static bool TryParse (string input, out Coord2d result)
		{
			ReadOnlySpan<char> span = input.AsSpan();

			int delimiterIndex = span.IndexOf(' ');

			if (delimiterIndex >= 0)
			{
				bool resultX = int.TryParse(span[..delimiterIndex],       out int x);
				bool resultY = int.TryParse(span[(delimiterIndex + 1)..], out int y);

				if (resultX && resultY)
				{
					result = new Coord2d(x, y);
					return true;
				}
			}

			result = default;
			return false;
		}

		public IEnumerable<Coord2d> GetNeighbors (bool includeOrthogonal = false)
		{
			yield return this + Left;
			yield return this + Right;
			yield return this + Up;
			yield return this + Down;

			if (includeOrthogonal == false)
				yield break;

			yield return this + Left + Down;
			yield return this + Right + Down;
			yield return this + Left + Up;
			yield return this + Right + Up;
		}

		public override string ToString ()
		{
			return $"{x.ToString()} {y.ToString()}";
		}

		public override int GetHashCode ()
		{
			unchecked
			{
				return (x * 397) ^ y;
			}
		}

		public void Deconstruct (out int x, out int y)
		{
			x = this.x;
			y = this.y;
		}
	}
}
