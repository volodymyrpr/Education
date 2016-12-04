using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Numerics;

namespace Education
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.DoEverything();

            Console.ReadLine();
        }

        private void DoEverything()
        {

        }
    }

    class Test<T>
    {
        private T value;

        public void SetValue(T value)
        {
            if (!object.Equals(this.value, value))
            {
                this.value = value;
            }
        }

        private void OnValueChanged(T oldValue, T newValue)
        {
            Console.WriteLine(oldValue.ToString() + "->" + newValue.ToString());
        }
    }

    class Area : IEquatable<Area>
    {
        private int width;

        private int height;

        public Area(int dim1, int dim2)
        {
            this.height = Math.Max(dim1, dim2);
            this.width = Math.Min(dim1, dim2);
        }

        public bool Equals(Area otherArea)
        {
            return width == otherArea.width && height == otherArea.height;
        }

        public override bool Equals(object other)
        {
            if (!(other is Area))
            {
                return false;
            }
            var otherArea = (Area)other;

            return this.Equals(otherArea);
        }

        public override int GetHashCode()
        {
            return height * 31 + width;
        }

        public static bool operator == (Area a1, Area a2) => a1.Equals(a2);

        public static bool operator != (Area a1, Area a2) => !a1.Equals(a2);
    }
}