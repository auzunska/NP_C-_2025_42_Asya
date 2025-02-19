using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1
{
    class Triangle<T> where T : struct
    {
        public T SideA { get; }
        public T SideB { get; }
        public T SideC { get; }

        private Triangle(T a, T b, T c)
        {
            SideA = a;
            SideB = b;
            SideC = c;
        }

        public static bool GetInstance(T a, T b, T c, out Triangle<T> triangle)
        {
            triangle = null;

            if (!(typeof(T) == typeof(int) || typeof(T) == typeof(float)))
            {
                Console.WriteLine("Invalid type! Only int and float are allowed.");
                return false;
            }

            dynamic sideA = a, sideB = b, sideC = c;

            if (sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA)
            {
                triangle = new Triangle<T>(a, b, c);
                return true;
            }

            Console.WriteLine("Invalid triangle sides!");
            return false;
        }

        public override string ToString()
        {
            return $"Triangle sides: A = {SideA}, B = {SideB}, C = {SideC}";
        }
    }

}
