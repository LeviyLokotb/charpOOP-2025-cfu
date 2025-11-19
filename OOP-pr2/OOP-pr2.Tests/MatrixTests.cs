namespace OOP_pr2.Tests;

public class MatrixTests
{
    [Fact]
    public void Constructor_CreatesMatrixWithValidDimensions()
    {
        // Arrange & Act
        Matrix matrix = new(3, 4);

        // Assert
        Assert.Equal(3, matrix.Rows);
        Assert.Equal(4, matrix.Cols);
    }

    [Fact]
    public void Constructor_CreatesMatrixWithInvalidDimensions()
    {
        // Arrange & Act
        Matrix matrix = new(0, -1);

        // Assert
        Assert.Equal(1, matrix.Rows);
        Assert.Equal(1, matrix.Cols);
    }

    [Fact]
    public void GetElement_ValidCoordinates_ReturnsValue()
    {
        // Arrange
        Matrix matrix = new(2, 2);
        matrix.SetElement(1, 1, 5.5);

        // Act
        var result = matrix.GetElement(1, 1);

        // Assert
        Assert.Equal(5.5, result);
    }

    [Fact]
    public void GetElement_InvalidCoordinates_ReturnsNull()
    {
        // Arrange
        Matrix matrix = new(2, 2);

        // Act
        var result = matrix.GetElement(5, 5);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void SetElement_ValidCoordinates_ReturnsTrue()
    {
        // Arrange
        Matrix matrix = new(2, 2);

        // Act
        var result = matrix.SetElement(1, 1, 10.5);

        // Assert
        Assert.True(result);
        Assert.Equal(10.5, matrix.GetElement(1, 1));
    }

    [Fact]
    public void SetElement_InvalidCoordinates_ReturnsFalse()
    {
        // Arrange
        Matrix matrix = new(2, 2);

        // Act
        var result = matrix.SetElement(5, 5, 10.5);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddRow_IncreasesRowCount()
    {
        // Arrange
        Matrix matrix = new(2, 2);

        // Act
        matrix.AddRow();

        // Assert
        Assert.Equal(3, matrix.Rows);
        Assert.Equal(2, matrix.Cols);
    }

    [Fact]
    public void AddRow_WithArray_ValidLength_ReturnsTrue()
    {
        // Arrange
        Matrix matrix = new(2, 2);
        double[] newRow = [1.0, 2.0];

        // Act
        var result = matrix.AddRow(newRow);

        // Assert
        Assert.True(result);
        Assert.Equal(3, matrix.Rows);
    }

    [Fact]
    public void AddRow_WithArray_InvalidLength_ReturnsFalse()
    {
        // Arrange
        Matrix matrix = new(2, 2);
        double[] newRow = [ 1.0, 2.0, 3.0 ]; // Неверная длина

        // Act
        var result = matrix.AddRow(newRow);

        // Assert
        Assert.False(result);
        Assert.Equal(2, matrix.Rows);
    }

    [Fact]
    public void RemoveRow_ValidIndex_ReturnsTrue()
    {
        // Arrange
        Matrix matrix = new(3, 2);

        // Act
        var result = matrix.RemoveRow(1);

        // Assert
        Assert.True(result);
        Assert.Equal(2, matrix.Rows);
    }

    [Fact]
    public void RemoveRow_InvalidIndex_ReturnsFalse()
    {
        // Arrange
        Matrix matrix = new(2, 2);

        // Act
        var result = matrix.RemoveRow(5);

        // Assert
        Assert.False(result);
        Assert.Equal(2, matrix.Rows);
    }

    [Fact]
    public void RemoveRow_SingleRow_ReturnsFalse()
    {
        // Arrange
        Matrix matrix = new(1, 2);

        // Act
        var result = matrix.RemoveRow();

        // Assert
        Assert.False(result);
        Assert.Equal(1, matrix.Rows);
    }

    [Fact]
    public void MatrixMultiplication_ValidDimensions_ReturnsResult()
    {
        // Arrange
        Matrix matrixA = new(2, 3, randomValues: true);
        Matrix matrixB = new(3, 2, randomValues: true);

        // Act
        Matrix? result = matrixA * matrixB;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Rows);
        Assert.Equal(2, result.Cols);
    }

    [Fact]
    public void MatrixMultiplication_InvalidDimensions_ReturnsNull()
    {
        // Arrange
        Matrix matrixA = new(2, 3);
        Matrix matrixB = new(2, 3);

        // Act
        var result = matrixA * matrixB;

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Enumerator_IteratesThroughAllElements()
    {
        // Arrange
        var matrix = new Matrix(2, 2);
        matrix.SetElement(0, 0, 1); matrix.SetElement(0, 1, 2);
        matrix.SetElement(1, 0, 3); matrix.SetElement(1, 1, 4);

        (int, int, double)[] elements = [];

        // Act
        foreach (var element in matrix)
        {
            elements = [.. elements, element];
        }
        
         // Assert
        Assert.Equal(4, elements.Length);
        Assert.Contains((0, 0, 1.0), elements);
        Assert.Contains((0, 1, 2.0), elements);
        Assert.Contains((1, 0, 3.0), elements);
        Assert.Contains((1, 1, 4.0), elements);
    }

    ///////////////// Матрицы с исключениями /////////////////////
    [Fact]
    public void MatrixMultiply_ValidArguments_ReturnCorrectMatrix()
    {
        // Arrange
        Matrix matrix1 = new([
            [1, 2],
            [3, 4],
        ]);
        Matrix matrix2 = new([
            [5, 6],
            [7, 8],
        ]);
        Matrix expected = new([
            [19, 22],
            [43, 50],
        ]);
        Matrix actual = new(1, 1);
        
        // Act
        try { actual = Matrix.MatrixMultipy(matrix1, matrix2); }
        
        // Assert
        catch { Assert.True(false); }

        Assert.True(actual == expected); // оператор переопределён
    }

    [Fact]
    public void MatrixMultiply_InvalidArguments_TrowArgumentException()
    {
        // Arrange
        Type ExpectedException = Type.GetType("System.ArgumentException")!;
        Assert.NotNull(ExpectedException);

        Matrix matrix1 = new([
            [1, 2],
            [3, 4],
        ]);
        Matrix matrix2 = new([
            [5, -1],
            [7, 8],
        ]);

        // Act
        try 
        { 
            Matrix.MatrixMultipy(matrix1, matrix2); 
            Assert.True(false); // Если исключение не выброшено
        } 
        catch( Exception e )
        {
            Type ActualException = e.GetType();
            // Assert
            Assert.Equal(ExpectedException, ActualException);
        }
    }

    [Fact]
    public void MatrixMultiply_InvalidDimentions_TrowArgumentException()
    {
        // Arrange
        Type ExpectedException = Type.GetType("System.ArgumentException")!;
        Assert.NotNull(ExpectedException);

        Matrix matrix1 = new(2, 2);
        Matrix matrix2 = new(3, 3);

        // Act
        try 
        { 
            Matrix.MatrixMultipy(matrix1, matrix2); 
            Assert.True(false); // Если исключение не выброшено
        } 
        catch( Exception e )
        {
            Type ActualException = e.GetType();
            // Assert
            Assert.Equal(ExpectedException, ActualException);
        }
    }
}