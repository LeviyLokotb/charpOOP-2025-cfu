using System.Collections;

public class Matrix : IEnumerable<(int i, int j, double value)>
{
    private double[][] data = [];
    private int rows = 1;
    private int cols = 1;
    /// <summary>Количество строк </summary>
    public int Rows => rows;
    /// <summary>Количество столбцов </summary>
    public int Cols => cols;
    /// <summary>Новая матрица [rows x cols], заполненная случайными числами (по-умолчанию) или 0.0</summary>
    /// <param name="rows">Количество столбцов</param>
    /// <param name="cols">Количество строк</param>
    /// <param name="randomValues">Заполнять ли случайными числами</param>
    public Matrix(int rows, int cols, bool randomValues = false)
    {
        this.rows = (rows < 1) ? 1 : rows;
        this.cols = (cols < 1) ? 1 : cols;
        //data = new double[0][];

        for (int i = 0; i < this.rows; i++)
        {
            double[] temp = new double[0];
            for (int j = 0; j < this.cols; j++)
            {
                if (randomValues)
                {
                    // [0-99] + [0.0-1.0]
                    temp = [.. temp, rand()];
                }
                else
                {
                    temp = [.. temp, 0.0];
                }
            }
            data = [.. data, temp];
        }
    }
    public Matrix(double[][] matrix)
    {
        this.rows = matrix.Length;
        this.cols = matrix[0].Length;
        for(int i=0; i < matrix.Length; i++)
        {
            data = [.. data, matrix[i]];
        }
    }
    private double rand()
    {
        Random generator = new();
        return double.Round(generator.NextDouble(), 2) + generator.NextInt64() % 15;
    }
    /// <summary>Получает значение элемента M[row][col]</summary>
    /// <returns>Значение или null, если такого элемента нет</returns>
    public double? GetElement(int row, int col)
    {
        if (!((row >= 0 && row < rows) && (col >= 0 && col < cols))) return null;
        return double.Round(data[row][col], 2);
    }
    /// <summary>Задаёт значение элемента M[row][col]</summary>
    /// <returns>Получилось ли задать элемент</returns>
    public bool SetElement(int row, int col, double value)
    {
        if (!((row >= 0 && row < rows) && (col >= 0 && col < cols))) return false;
        data[row][col] = value;
        return true;
    }

    public void AddRow(bool randomValues = false)
    {
        rows++;
        Random generator = new();
        var temp = from t in new double[cols] select (randomValues ? rand() : 0.0);
        // Используем  collection expressions
        data = [.. data, [.. temp]];
    }
    public bool AddRow(double[] row)
    {
        if (row.Length != cols) return false;
        rows++;
        // Добавление массива в конец (используются collection expressions)
        data = [.. data, row];
        return true;
    }

    public bool RemoveRow(int row = -1)
    {
        if (row == -1) row = rows - 1;
        if (!(row >= 0 && row < rows)) return false;
        if (rows <= 1) return false;

        data.ToList().RemoveAt(row);
        rows--;
        return true;
    }

    public bool RemoveCol(int col = -1)
    {
        if (col == -1) col = cols - 1;
        if (!(col >= 0 && col < cols)) return false;
        if (cols <= 1) return false;

        //Console.WriteLine($"Cols: {cols} | Col: {col}");
        for (int i = 0; i < rows; i++) data[i].ToList().RemoveAt(col);
        cols--;
        return true;
    }

    public void AddCol(bool randomValues = false)
    {
        cols++;
        Random generator = new();
        data = [.. data.Select(row => row.Append(randomValues ? rand() : 0.0).ToArray())];
    }
    public bool AddCol(double[] col)
    {
        if (col.Length != cols) return false;
        cols++;
        data = [.. data.Select((row, i) => row.Append(col[i]).ToArray())];
        return true;
    }

    /// <summary>Итерация по элементам</summary>
    public IEnumerator<(int i, int j, double value)> GetEnumerator()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                yield return (i, j, GetElement(i, j) ?? 0.0);
            }
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Умножение матриц, таких что
    /// Cols матрицы a равен Rows матрицы b
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>null если матрицы неподходящего размера</returns>
    public static Matrix? operator *(Matrix a, Matrix b)
    {
        if (a.Cols != b.Rows) return null;

        Matrix c = new(a.Rows, b.Cols, randomValues: false);
        int iter_amount = a.Cols;

        foreach (var (i, j, _) in c)
        {
            double res = 0.0;
            for (int k = 0; k < iter_amount; k++)
            {
                // ?? просто чтобы компилятор успокоился, null здесь не будет
                res += (a.GetElement(i, k) ?? 0) * (b.GetElement(k, j) ?? 0);
            }
            c.SetElement(i, j, res);
        }
        return c;
    }

    public static bool operator ==(Matrix a, Matrix b)
    {
        if ( (a.Rows != b.Rows) || (a.Cols != b.Cols) ) return false;
        foreach (var (i, j, _) in a) if (a.GetElement(i, j) != b.GetElement(i, j)) return false;
        return true;
    }

    public static bool operator !=(Matrix a, Matrix b) => !(a == b);

    /// <summary>
    /// Умножение с исключенями.
    /// Обе матрицы не должны содержать отрицательных элементов,
    /// а Cols матрицы a должен быть равен Rows матрицы b
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns></returns>
    public static Matrix MatrixMultipy(Matrix a, Matrix b)
    {
        if (a.Cols != b.Rows) throw new ArgumentException("The number of Columns of matrix A must be equal to the number of Rows of matrix B");
        foreach(var (i, j, num) in a) if (num < 0) throw new ArgumentException($"Matrix A contains an invalid entry in cell [{i}][{j}]");
        foreach(var (i, j, num) in b) if (num < 0) throw new ArgumentException($"Matrix B contains an invalid entry in cell [{i}][{j}]");
        return (a * b)!; // null не будет, потому что мы уже проверили
    }
}