namespace LAB1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter circle radius: ");
            double radius = double.Parse(Console.ReadLine());
            Circle circle = new Circle(radius);
            Console.WriteLine($"Circle Perimeter: {circle.GetPerimeter():F2}");
            Console.WriteLine($"Circle Area: {circle.GetArea():F2}");
            Console.WriteLine($"Is Elliptical: {circle.IsElliptical()}");

            Console.Write("Enter rectangle width: ");
            double width = double.Parse(Console.ReadLine());
            Console.Write("Enter rectangle height: ");
            double height = double.Parse(Console.ReadLine());
            Rectangle rectangle = new Rectangle(width, height);
            Console.WriteLine($"Rectangle Perimeter: {rectangle.GetPerimeter():F2}");
            Console.WriteLine($"Rectangle Area: {rectangle.GetArea():F2}");
            Console.WriteLine($"Is Elliptical: {rectangle.IsElliptical()}");

            Console.Write("Enter square side: ");
            double side = double.Parse(Console.ReadLine());
            Square square = new Square(side);
            Console.WriteLine($"Square Perimeter: {square.GetPerimeter():F2}");
            Console.WriteLine($"Square Area (instance method): {square.GetArea():F2}");
            Console.WriteLine($"Square Area (static method): {Square.CalculateArea(side):F2}");
        }
    }
}
