using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Classes
{
    public struct Note : IComparable<Note>, IEquatable<Note>, IComparable
    {
        private int semitonesFromA;
        public int SemitonesFromA { get { return semitonesFromA; } }

        public Note(int semitonesFromA)
        {
            this.semitonesFromA = semitonesFromA;
        }

        public int CompareTo(object other)
        {
            if (!(other is Note))
            {
                throw new InvalidOperationException("Compare to: not a note");
            }

            return CompareTo((Note)other);
        }

        public int CompareTo(Note other)
        {
            if (Equals(other))
            {
                return 0;
            }

            return semitonesFromA.CompareTo(other.semitonesFromA);
        }

        public static bool operator < (Note n1, Note n2)
        {
            return n1.CompareTo(n2) < 0;
        }

        public static bool operator > (Note n1, Note n2)
        {
            return n1.CompareTo(n2) > 0;
        }

        public bool Equals(Note other) => semitonesFromA == other.semitonesFromA;

        public override bool Equals(object obj)
        {
            if (!(obj is Note))
            {
                return false;
            }

            return Equals((Note)obj);
        }

        public override int GetHashCode()
        {
            return semitonesFromA.GetHashCode();
        }

        public static bool operator == (Note n1, Note n2) => n1.Equals(n2);

        public static bool operator != (Note n1, Note n2) => !(n1 == n2);
    }
}
