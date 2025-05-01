using System;
using System.Linq;

abstract class Transport
{
    public string Name { get; set; }
    public int MaxSpeed { get; set; }
    public Transport(string name, int maxSpeed)
    {
        Name = name;
        MaxSpeed = maxSpeed;
    }
    public abstract void Show();
}

class Car : Transport
{
    public string NumberPlate { get; set; }
    public Car(string name, int maxSpeed, string numberPlate) : base(name, maxSpeed)
    {
        NumberPlate = numberPlate;
    }
    public override void Show()
    {
        Console.WriteLine($"Автомобіль: {Name}, Швидкість: {MaxSpeed}, Номер: {NumberPlate}");
    }
}

class Train : Transport
{
    public int Carriages { get; set; }
    public Train(string name, int maxSpeed, int carriages) : base(name, maxSpeed)
    {
        Carriages = carriages;
    }
    public override void Show()
    {
        Console.WriteLine($"Поїзд: {Name}, Швидкість: {MaxSpeed}, Вагонів: {Carriages}");
    }
}

class Express : Train
{
    public string Route { get; set; }
    public Express(string name, int maxSpeed, int carriages, string route) : base(name, maxSpeed, carriages)
    {
        Route = route;
    }
    public override void Show()
    {
        Console.WriteLine($"Експрес: {Name}, Швидкість: {MaxSpeed}, Вагонів: {Carriages}, Маршрут: {Route}");
    }
}

class Point
{
    protected int x, y;
    protected int c;

    public Point()
    {
        x = 0;
        y = 0;
        c = 0;
    }

    public Point(int x, int y, int c)
    {
        this.x = x;
        this.y = y;
        this.c = c;
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }
    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public int Color
    {
        get { return c; }
    }

    public void Print()
    {
        Console.WriteLine($"Точка ({x}, {y}), колір: {c}");
    }

    public double DistanceToOrigin()
    {
        return Math.Sqrt(x * x + y * y);
    }

    public void Move(int dx, int dy)
    {
        x += dx;
        y += dy;
    }
}

class Program
{
    static void Main()
    {
        Transport[] transports = new Transport[]
        {
            new Car("Toyota", 180, "AA1234BB"),
            new Train("Intercity", 160, 10),
            new Express("Hyundai Express", 200, 8, "Київ-Львів"),
            new Car("BMW", 220, "BC5678CD"),
            new Train("Regional", 120, 12)
        };

        var sorted = transports.OrderByDescending(t => t.MaxSpeed).ToArray();
        Console.WriteLine("Відсортовано за максимальною швидкістю:");
        foreach (var t in sorted)
            t.Show();

        Point[] points = new Point[]
        {
            new Point(1, 2, 1),
            new Point(3, 4, 2),
            new Point(-2, 5, 3),
            new Point(0, 0, 4)
        };

        double sum = 0;
        foreach (var p in points)
            sum += p.DistanceToOrigin();
        double avg = sum / points.Length;

        Console.WriteLine("Інформація про точки:");
        foreach (var p in points)
        {
            p.Print();
            Console.WriteLine($"Відстань до початку координат: {p.DistanceToOrigin():F2}");
        }

        int dx = 1, dy = 1;
        foreach (var p in points)
        {
            if (p.DistanceToOrigin() > avg)
                p.Move(dx, dy);
        }

        Console.WriteLine("\nПісля переміщення:");
        foreach (var p in points)
        {
            p.Print();
            Console.WriteLine($"Відстань до початку координат: {p.DistanceToOrigin():F2}");
        }
    }
}
