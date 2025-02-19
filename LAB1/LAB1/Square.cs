using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1
{
    sealed class Square : Rectangle
    {
        public Square(double side) : base(side, side) { }

        // Override на метода IsElliptical (sealed - не може да бъде наследяван)
        public sealed override bool IsElliptical()
        {
            return false;
        }

        // Статичен метод за изчисляване на лицето на квадрат
        public static double CalculateArea(double side)
        {
            return side * side;
        }
    }
}
