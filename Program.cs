using System;
using System.Linq;

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

    public int this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                case 2: return c;
                default:
                    throw new IndexOutOfRangeException($"Invalid index: {index}");
            }
        }
        set
        {
            switch (index)
            {
                case 0: x = value; break;
                case 1: y = value; break;
                case 2: c = value; break;
                default:
                    throw new IndexOutOfRangeException($"Invalid index: {index}");
            }
        }
    }

    public static Point operator ++(Point p)
    {
        p.x++;
        p.y++;
        return p;
    }
    public static Point operator --(Point p)
    {
        p.x--;
        p.y--;
        return p;
    }

    public static bool operator true(Point p) => p.x == p.y;
    public static bool operator false(Point p) => p.x != p.y;

    public static Point operator +(Point p, int scalar)
    {
        return new Point(p.x + scalar, p.y + scalar, p.c);
    }

    public static explicit operator string(Point p)
    {
        return $"({p.x}, {p.y}), color: {p.c}";
    }
    public static explicit operator Point(string s)
    {
        var parts = s.Split();
        if (parts.Length != 3) throw new FormatException("Invalid string format for Point");
        return new Point(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }
}

class VectorInt
{
    protected int[] IntArray;
    protected uint size;
    protected int codeError;
    protected static uint num_vec;

    public VectorInt()
    {
        IntArray = new int[1];
        size = 1;
        IntArray[0] = 0;
        codeError = 0;
        num_vec++;
    }

    public VectorInt(uint size)
    {
        this.size = size;
        IntArray = new int[size];
        for (int i = 0; i < size; i++)
            IntArray[i] = 0;
        codeError = 0;
        num_vec++;
    }

    public VectorInt(uint size, int initValue)
    {
        this.size = size;
        IntArray = new int[size];
        for (int i = 0; i < size; i++)
            IntArray[i] = initValue;
        codeError = 0;
        num_vec++;
    }

    ~VectorInt()
    {
        Console.WriteLine("Vector destroyed");
        num_vec--;
    }

    public void InputVector()
    {
        Console.WriteLine($"Enter {size} elements:");
        for (int i = 0; i < size; i++)
        {
            if (int.TryParse(Console.ReadLine(), out int value))
                IntArray[i] = value;
            else
            {
                codeError = -1;
                Console.WriteLine("Invalid input");
                i--;
            }
        }
    }

    public void PrintVector()
    {
        Console.Write("Vector: [");
        for (int i = 0; i < size; i++)
        {
            Console.Write(IntArray[i]);
            if (i < size - 1)
                Console.Write(", ");
        }
        Console.WriteLine("]");
    }

    public uint Size
    {
        get { return size; }
    }

    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= size)
            {
                codeError = -1;
                return 0;
            }
            return IntArray[index];
        }
        set
        {
            if (index < 0 || index >= size)
                codeError = -1;
            else
                IntArray[index] = value;
        }
    }

    public static VectorInt operator ++(VectorInt v)
    {
        for (int i = 0; i < v.size; i++)
            v.IntArray[i]++;
        return v;
    }

    public static VectorInt operator --(VectorInt v)
    {
        for (int i = 0; i < v.size; i++)
            v.IntArray[i]--;
        return v;
    }

    public static bool operator true(VectorInt v)
    {
        if (v.size == 0)
            return false;
            
        foreach (int element in v.IntArray)
        {
            if (element == 0)
                return false;
        }
        return true;
    }

    public static bool operator false(VectorInt v)
    {
        if (v.size == 0)
            return true;
            
        foreach (int element in v.IntArray)
        {
            if (element == 0)
                return true;
        }
        return false;
    }

    public static VectorInt operator ~(VectorInt v)
    {
        VectorInt result = new VectorInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = ~v.IntArray[i];
        return result;
    }

    public static VectorInt operator +(VectorInt v1, VectorInt v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorInt result = new VectorInt(maxSize);
        
        for (int i = 0; i < maxSize; i++)
        {
            int val1 = i < v1.size ? v1.IntArray[i] : 0;
            int val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 + val2;
        }
        
        return result;
    }
    
    public static VectorInt operator +(VectorInt v, int scalar)
    {
        VectorInt result = new VectorInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] + scalar;
        return result;
    }

    public static VectorInt operator -(VectorInt v1, VectorInt v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorInt result = new VectorInt(maxSize);
        
        for (int i = 0; i < maxSize; i++)
        {
            int val1 = i < v1.size ? v1.IntArray[i] : 0;
            int val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 - val2;
        }
        
        return result;
    }
    
    public static VectorInt operator -(VectorInt v, int scalar)
    {
        VectorInt result = new VectorInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] - scalar;
        return result;
    }

    public static VectorInt operator *(VectorInt v1, VectorInt v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorInt result = new VectorInt(maxSize);
        
        for (int i = 0; i < maxSize; i++)
        {
            int val1 = i < v1.size ? v1.IntArray[i] : 0;
            int val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 * val2;
        }
        
        return result;
    }
    
    public static VectorInt operator *(VectorInt v, int scalar)
    {
        VectorInt result = new VectorInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] * scalar;
        return result;
    }

    public static VectorInt operator /(VectorInt v1, VectorInt v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorInt result = new VectorInt(maxSize);
        
        for (int i = 0; i < maxSize; i++)
        {
            int val1 = i < v1.size ? v1.IntArray[i] : 0;
            int val2 = i < v2.size ? v2.IntArray[i] : 0;
            
            if (val2 == 0)
            {
                result.codeError = -1;
                result.IntArray[i] = 0;
            }
            else
            {
                result.IntArray[i] = val1 / val2;
            }
        }
        
        return result;
    }
    
    public static VectorInt operator /(VectorInt v, int scalar)
    {
        VectorInt result = new VectorInt(v.size);
        
        if (scalar == 0)
        {
            result.codeError = -1;
            return result;
        }
        
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] / scalar;
            
        return result;
    }

    public static VectorInt operator %(VectorInt v1, VectorInt v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorInt result = new VectorInt(maxSize);
        
        for (int i = 0; i < maxSize; i++)
        {
            int val1 = i < v1.size ? v1.IntArray[i] : 0;
            int val2 = i < v2.size ? v2.IntArray[i] : 0;
            
            if (val2 == 0)
            {
                result.codeError = -1;
                result.IntArray[i] = 0;
            }
            else
            {
                result.IntArray[i] = val1 % val2;
            }
        }
        
        return result;
    }

    public static VectorInt operator &(VectorInt v1, VectorInt v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorInt result = new VectorInt(maxSize);
        
        for (int i = 0; i < maxSize; i++)
        {
            int val1 = i < v1.size ? v1.IntArray[i] : 0;
            int val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 & val2;
        }
        
        return result;
    }
    
    public static VectorInt operator &(VectorInt v, int scalar)
    {
        VectorInt result = new VectorInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] & scalar;
        return result;
    }

    public static VectorInt operator |(VectorInt v1, VectorInt v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorInt result = new VectorInt(maxSize);
        
        for (int i = 0; i < maxSize; i++)
        {
            int val1 = i < v1.size ? v1.IntArray[i] : 0;
            int val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 | val2;
        }
        
        return result;
    }
    
    public static VectorInt operator |(VectorInt v, int scalar)
    {
        VectorInt result = new VectorInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] | scalar;
        return result;
    }

    public static VectorInt operator ^(VectorInt v1, VectorInt v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorInt result = new VectorInt(maxSize);
        
        for (int i = 0; i < maxSize; i++)
        {
            int val1 = i < v1.size ? v1.IntArray[i] : 0;
            int val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 ^ val2;
        }
        
        return result;
    }
    
    public static VectorInt operator ^(VectorInt v, int scalar)
    {
        VectorInt result = new VectorInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] ^ scalar;
        return result;
    }

    public static VectorInt operator <<(VectorInt v1, int shift)
    {
        VectorInt result = new VectorInt(v1.size);
        
        for (int i = 0; i < v1.size; i++)
            result.IntArray[i] = v1.IntArray[i] << shift;
        
        return result;
    }

    public static VectorInt operator >>(VectorInt v1, int shift)
    {
        VectorInt result = new VectorInt(v1.size);
        
        for (int i = 0; i < v1.size; i++)
            result.IntArray[i] = v1.IntArray[i] >> shift;
        
        return result;
    }

    public static bool operator >(VectorInt v1, VectorInt v2)
    {
        if (v1.size != v2.size)
            return false;
            
        for (int i = 0; i < v1.size; i++)
        {
            if (v1.IntArray[i] <= v2.IntArray[i])
                return false;
        }
        
        return true;
    }

    public static bool operator <(VectorInt v1, VectorInt v2)
    {
        if (v1.size != v2.size)
            return false;
            
        for (int i = 0; i < v1.size; i++)
        {
            if (v1.IntArray[i] >= v2.IntArray[i])
                return false;
        }
        
        return true;
    }

    public static bool operator >=(VectorInt v1, VectorInt v2)
    {
        if (v1.size != v2.size)
            return false;
            
        for (int i = 0; i < v1.size; i++)
        {
            if (v1.IntArray[i] < v2.IntArray[i])
                return false;
        }
        
        return true;
    }

    public static bool operator <=(VectorInt v1, VectorInt v2)
    {
        if (v1.size != v2.size)
            return false;
            
        for (int i = 0; i < v1.size; i++)
        {
            if (v1.IntArray[i] > v2.IntArray[i])
                return false;
        }
        
        return true;
    }

    public static bool operator ==(VectorInt v1, VectorInt v2)
    {
        if (v1.size != v2.size)
            return false;
            
        for (int i = 0; i < v1.size; i++)
        {
            if (v1.IntArray[i] != v2.IntArray[i])
                return false;
        }
        
        return true;
    }
    
    public static bool operator !=(VectorInt v1, VectorInt v2)
    {
        return !(v1 == v2);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
            
        VectorInt other = (VectorInt)obj;
        return this == other;
    }
    
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + size.GetHashCode();
        foreach (int element in IntArray)
            hash = hash * 23 + element.GetHashCode();
        return hash;
    }
}

class MatrixInt
{
    protected int[][] IntArray;
    protected int n, m;
    protected int codeError;
    protected static int num_vec;

    public MatrixInt()
    {
        n = 1;
        m = 1;
        IntArray = new int[n][];
        IntArray[0] = new int[m];
        IntArray[0][0] = 0;
        codeError = 0;
        num_vec++;
    }

    public MatrixInt(int rows, int cols)
    {
        n = rows;
        m = cols;
        IntArray = new int[n][];
        
        for (int i = 0; i < n; i++)
        {
            IntArray[i] = new int[m];
            for (int j = 0; j < m; j++)
                IntArray[i][j] = 0;
        }
        
        codeError = 0;
        num_vec++;
    }

    public MatrixInt(int rows, int cols, int initValue)
    {
        n = rows;
        m = cols;
        IntArray = new int[n][];
        
        for (int i = 0; i < n; i++)
        {
            IntArray[i] = new int[m];
            for (int j = 0; j < m; j++)
                IntArray[i][j] = initValue;
        }
        
        codeError = 0;
        num_vec++;
    }

    ~MatrixInt()
    {
        Console.WriteLine("Matrix destroyed");
        num_vec--;
    }

    public void InputMatrix()
    {
        Console.WriteLine($"Enter {n}x{m} elements:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write($"[{i},{j}]: ");
                if (int.TryParse(Console.ReadLine(), out int value))
                    IntArray[i][j] = value;
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                    j--;
                    codeError = -1;
                }
            }
        }
    }

    public void PrintMatrix()
    {
        Console.WriteLine("Matrix:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
                Console.Write($"{IntArray[i][j],4}");
            Console.WriteLine();
        }
    }

    public (int Rows, int Columns) Size
    {
        get { return (n, m); }
    }

    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

    public int this[int i, int j]
    {
        get
        {
            if (i < 0 || i >= n || j < 0 || j >= m)
            {
                codeError = -1;
                return 0;
            }
            return IntArray[i][j];
        }
        set
        {
            if (i < 0 || i >= n || j < 0 || j >= m)
                codeError = -1;
            else
                IntArray[i][j] = value;
        }
    }

    public int this[int k]
    {
        get
        {
            if (k < 0 || k >= n * m)
            {
                codeError = -1;
                return 0;
            }
            int i = k / m;
            int j = k % m;
            return IntArray[i][j];
        }
        set
        {
            if (k < 0 || k >= n * m)
                codeError = -1;
            else
            {
                int i = k / m;
                int j = k % m;
                IntArray[i][j] = value;
            }
        }
    }

    public static MatrixInt operator ++(MatrixInt matrix)
    {
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                matrix.IntArray[i][j]++;
        return matrix;
    }

    public static MatrixInt operator --(MatrixInt matrix)
    {
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                matrix.IntArray[i][j]--;
        return matrix;
    }

    public static bool operator true(MatrixInt matrix)
    {
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                if (matrix.IntArray[i][j] == 0)
                    return false;
        return matrix.n > 0 && matrix.m > 0;
    }

    public static bool operator false(MatrixInt matrix)
    {
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                if (matrix.IntArray[i][j] == 0)
                    return true;
        return matrix.n == 0 || matrix.m == 0;
    }

    public static bool operator !(MatrixInt matrix)
    {
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                if (matrix.IntArray[i][j] == 0)
                    return false;
        return true;
    }

    public static MatrixInt operator ~(MatrixInt matrix)
    {
        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = ~matrix.IntArray[i][j];
        return result;
    }

    public static MatrixInt operator +(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return m1;

        MatrixInt result = new MatrixInt(m1.n, m1.m);
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                result.IntArray[i][j] = m1.IntArray[i][j] + m2.IntArray[i][j];
        
        return result;
    }

    public static MatrixInt operator +(MatrixInt matrix, int scalar)
    {
        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = matrix.IntArray[i][j] + scalar;
        
        return result;
    }

    public static MatrixInt operator -(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return m1;

        MatrixInt result = new MatrixInt(m1.n, m1.m);
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                result.IntArray[i][j] = m1.IntArray[i][j] - m2.IntArray[i][j];
        
        return result;
    }

    public static MatrixInt operator -(MatrixInt matrix, int scalar)
    {
        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = matrix.IntArray[i][j] - scalar;
        
        return result;
    }

    public static MatrixInt operator *(MatrixInt m1, MatrixInt m2)
    {
        if (m1.m != m2.n) 
        {
            m1.codeError = -1;
            return m1;
        }

        MatrixInt result = new MatrixInt(m1.n, m2.m);
        
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m2.m; j++)
                for (int k = 0; k < m1.m; k++)
                    result.IntArray[i][j] += m1.IntArray[i][k] * m2.IntArray[k][j];
        
        return result;
    }

    public static VectorInt operator *(MatrixInt matrix, VectorInt vector)
    {
        if (matrix.m != vector.Size)
        {
            matrix.codeError = -1;
            return new VectorInt((uint)matrix.n);
        }

        VectorInt result = new VectorInt((uint)matrix.n);
        
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result[i] += matrix.IntArray[i][j] * vector[j];
        
        return result;
    }

    public static MatrixInt operator *(MatrixInt matrix, int scalar)
    {
        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = matrix.IntArray[i][j] * scalar;
        
        return result;
    }

    public static MatrixInt operator /(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return m1;

        MatrixInt result = new MatrixInt(m1.n, m1.m);
        
        for (int i = 0; i < m1.n; i++)
        {
            for (int j = 0; j < m1.m; j++)
            {
                if (m2.IntArray[i][j] == 0)
                {
                    result.codeError = -1;
                    result.IntArray[i][j] = 0;
                }
                else
                {
                    result.IntArray[i][j] = m1.IntArray[i][j] / m2.IntArray[i][j];
                }
            }
        }
        
        return result;
    }

    public static MatrixInt operator /(MatrixInt matrix, int scalar)
    {
        if (scalar == 0)
        {
            matrix.codeError = -1;
            return matrix;
        }

        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = matrix.IntArray[i][j] / scalar;
        
        return result;
    }

    public static MatrixInt operator %(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return m1;

        MatrixInt result = new MatrixInt(m1.n, m1.m);
        
        for (int i = 0; i < m1.n; i++)
        {
            for (int j = 0; j < m1.m; j++)
            {
                if (m2.IntArray[i][j] == 0)
                {
                    result.codeError = -1;
                    result.IntArray[i][j] = 0;
                }
                else
                {
                    result.IntArray[i][j] = m1.IntArray[i][j] % m2.IntArray[i][j];
                }
            }
        }
        
        return result;
    }

    public static MatrixInt operator &(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return m1;

        MatrixInt result = new MatrixInt(m1.n, m1.m);
        
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                result.IntArray[i][j] = m1.IntArray[i][j] & m2.IntArray[i][j];
        
        return result;
    }

    public static MatrixInt operator &(MatrixInt matrix, int scalar)
    {
        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = matrix.IntArray[i][j] & scalar;
        
        return result;
    }

    public static MatrixInt operator |(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return m1;

        MatrixInt result = new MatrixInt(m1.n, m1.m);
        
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                result.IntArray[i][j] = m1.IntArray[i][j] | m2.IntArray[i][j];
        
        return result;
    }

    public static MatrixInt operator |(MatrixInt matrix, int scalar)
    {
        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = matrix.IntArray[i][j] | scalar;
        
        return result;
    }

    public static MatrixInt operator ^(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return m1;

        MatrixInt result = new MatrixInt(m1.n, m1.m);
        
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                result.IntArray[i][j] = m1.IntArray[i][j] ^ m2.IntArray[i][j];
        
        return result;
    }

    public static MatrixInt operator ^(MatrixInt matrix, int scalar)
    {
        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = matrix.IntArray[i][j] ^ scalar;
        
        return result;
    }

    public static MatrixInt operator <<(MatrixInt matrix, int shift)
    {
        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = matrix.IntArray[i][j] << shift;
        
        return result;
    }

    public static MatrixInt operator >>(MatrixInt matrix, int shift)
    {
        MatrixInt result = new MatrixInt(matrix.n, matrix.m);
        
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.IntArray[i][j] = matrix.IntArray[i][j] >> shift;
        
        return result;
    }

    public static bool operator ==(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return false;

        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                if (m1.IntArray[i][j] != m2.IntArray[i][j])
                    return false;

        return true;
    }

    public static bool operator !=(MatrixInt m1, MatrixInt m2)
    {
        return !(m1 == m2);
    }

    public static bool operator >(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return false;

        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                if (m1.IntArray[i][j] <= m2.IntArray[i][j])
                    return false;

        return true;
    }

    public static bool operator <(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return false;

        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                if (m1.IntArray[i][j] >= m2.IntArray[i][j])
                    return false;

        return true;
    }

    public static bool operator >=(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return false;

        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                if (m1.IntArray[i][j] < m2.IntArray[i][j])
                    return false;

        return true;
    }

    public static bool operator <=(MatrixInt m1, MatrixInt m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            return false;

        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                if (m1.IntArray[i][j] > m2.IntArray[i][j])
                    return false;

        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
            
        MatrixInt other = (MatrixInt)obj;
        return this == other;
    }
    
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + n.GetHashCode();
        hash = hash * 23 + m.GetHashCode();
        
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                hash = hash * 23 + IntArray[i][j].GetHashCode();
                
        return hash;
    }
}

class Program
{
    static void Main()
    {

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

        Console.WriteLine("Testing VectorInt class:");
        
        VectorInt v1 = new VectorInt(3, 5);
        VectorInt v2 = new VectorInt(3, 2);
        
        Console.WriteLine("Vector 1:");
        v1.PrintVector();
        
        Console.WriteLine("Vector 2:");
        v2.PrintVector();
        
        Console.WriteLine("\nArithmetic Operations:");
        
        Console.WriteLine("Addition (v1 + v2):");
        VectorInt sumVec = v1 + v2;
        sumVec.PrintVector();
        
        Console.WriteLine("Subtraction (v1 - v2):");
        VectorInt diff = v1 - v2;
        diff.PrintVector();
        
        Console.WriteLine("Multiplication (v1 * v2):");
        VectorInt prod = v1 * v2;
        prod.PrintVector();
        
        Console.WriteLine("Division (v1 / v2):");
        VectorInt div = v1 / v2;
        div.PrintVector();
        
        Console.WriteLine("\nBitwise Operations:");
        
        Console.WriteLine("AND (v1 & v2):");
        VectorInt and = v1 & v2;
        and.PrintVector();
        
        Console.WriteLine("OR (v1 | v2):");
        VectorInt or = v1 | v2;
        or.PrintVector();
        
        Console.WriteLine("XOR (v1 ^ v2):");
        VectorInt xor = v1 ^ v2;
        xor.PrintVector();
        
        Console.WriteLine("NOT (~v1):");
        VectorInt not = ~v1;
        not.PrintVector();
        
        Console.WriteLine("\nShift Operations:");
        
        Console.WriteLine("Left Shift (v1 << 2):");
        VectorInt leftShift = v1 << 2;
        leftShift.PrintVector();
        
        Console.WriteLine("Right Shift (v1 >> 1):");
        VectorInt rightShift = v1 >> 1;
        rightShift.PrintVector();
        
        Console.WriteLine("\nIncrement/Decrement:");
        
        Console.WriteLine("v1++:");
        v1++;
        v1.PrintVector();
        
        Console.WriteLine("v2--:");
        v2--;
        v2.PrintVector();
        
        Console.WriteLine("\nScalar Operations:");
        
        Console.WriteLine("v1 + 10:");
        VectorInt scalarAdd = v1 + 10;
        scalarAdd.PrintVector();
        
        Console.WriteLine("\nComparison Operations:");
        
        VectorInt v3 = new VectorInt(3, 10);
        VectorInt v4 = new VectorInt(3, 5);
        
        Console.WriteLine("Vector 3:");
        v3.PrintVector();
        
        Console.WriteLine("Vector 4:");
        v4.PrintVector();
        
        Console.WriteLine($"v3 > v4: {v3 > v4}");
        Console.WriteLine($"v3 >= v4: {v3 >= v4}");
        Console.WriteLine($"v3 < v4: {v3 < v4}");
        Console.WriteLine($"v3 <= v4: {v3 <= v4}");
        Console.WriteLine($"v3 == v4: {v3 == v4}");
        Console.WriteLine($"v3 != v4: {v3 != v4}");
        
        Console.WriteLine("\nIndexer:");
        
        Console.WriteLine($"v3[1] = {v3[1]}");
        v3[1] = 20;
        Console.WriteLine("After v3[1] = 20:");
        v3.PrintVector();
        
        Console.WriteLine("\nCreating vector with user input:");
        VectorInt userVector = new VectorInt(2);
        userVector.InputVector();
        Console.WriteLine("User vector:");
        userVector.PrintVector();
    }
}
