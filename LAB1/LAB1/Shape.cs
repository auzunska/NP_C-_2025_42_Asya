using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1
{
    abstract class Shape
    {
        public abstract double GetPerimeter();
        public abstract double GetArea();

        private int color;
        public int Color
        {
            get
            {
                int r = (color >> 16) & 0xFF;
                int g = (color >> 8) & 0xFF;
                int b = color & 0xFF;

                if (r == 255 && g == 0 && b == 0) return 1; // Червено
                if (r == 0 && g == 255 && b == 0) return 2; // Зелено
                if (r == 0 && g == 0 && b == 255) return 3; // Синьо
                return 0; // Неопределен цвят
            }
            set
            {
                switch (value)
                {
                    case 1: color = (255 << 24) | (255 << 16) | (0 << 8) | 0; break; // Червено (ARGB: 255,255,0,0)
                    case 2: color = (255 << 24) | (0 << 16) | (255 << 8) | 0; break; // Зелено (ARGB: 255,0,255,0)
                    case 3: color = (255 << 24) | (0 << 16) | (0 << 8) | 255; break; // Синьо (ARGB: 255,0,0,255)
                    default: color = (255 << 24) | (0 << 16) | (0 << 8) | 0; break; // Черно по подразбиране
                }
            }
        }

        public class Colors
        {
            public static int Red = 1;
            public static int Green = 2;
            public static int Blue = 3;
        }

        public void PrintColorInfo()
        {
            int a = (color >> 24) & 0xFF;
            int r = (color >> 16) & 0xFF;
            int g = (color >> 8) & 0xFF;
            int b = color & 0xFF;

            Console.WriteLine($"ARGB: ({a}, {r}, {g}, {b})");
            Console.WriteLine($"RGB: ({r}, {g}, {b})");
        }

    }
}
