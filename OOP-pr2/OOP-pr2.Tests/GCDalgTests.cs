namespace OOP_pr2.Tests;

public class GCDalgTests
{
    [Fact]
    public void GCD_TwoValues_ReturnCorrectValue()
    {
        // Arrange
        var (a, b) = (48, 18);

        // Act
        var (result, err) = GCDalg.GCD(a, b);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCDStein_TwoValues_ReturnCorrectValue()
    {
        // Arrange
        var (a, b) = (48, 18);

        // Act
        var (result, err) = GCDalg.GCDStein(a, b);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCD_MultipleValues_ReturnsCorrectValue()
    {
        // Arrange
        int[] numbers = [48, 18, 12, 6];

        // Act
        var (result, err) = GCDalg.GCD(numbers);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCDStein_MultipleValues_ReturnsCorrectValue()
    {
        // Arrange
        int[] numbers = [48, 18, 12, 6];

        // Act
        var (result, err) = GCDalg.GCDStein(numbers);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCD_SingleValue_ReturnSameValue()
    {
        // Arrange
        int[] numbers = [17];

        // Act
        var (result, err) = GCDalg.GCD(numbers);

        // Assert
        Assert.Equal(17, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCDStein_SingleValue_ReturnSameValue()
    {
        // Arrange
        int[] numbers = [17];

        // Act
        var (result, err) = GCDalg.GCDStein(numbers);

        // Assert
        Assert.Equal(17, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCD_EmptyArray_ReturnsError()
    {
        // Arrange
        int[] numbers = [];

        // Act
        var (result, err) = GCDalg.GCD(numbers);

        // Assert
        Assert.Equal(0, result);
        Assert.Equal("Введите хотя бы одно число!", err);
    }

    [Fact]
    public void GCDStein_EmptyArray_ReturnsError()
    {
        // Arrange
        int[] numbers = [];

        // Act
        var (result, err) = GCDalg.GCDStein(numbers);

        // Assert
        Assert.Equal(0, result);
        Assert.Equal("Введите хотя бы одно число!", err);
    }

    [Fact]
    public void GCD_WithZero_ReturnsNonZero()
    {
        // Arrange
        int[] numbers = [0, 15, 0];

        // Act
        var (result, err) = GCDalg.GCD(numbers);

        // Assert
        Assert.Equal(15, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCDStein_WithZero_ReturnsNonZero()
    {
        // Arrange
        int[] numbers = [0, 15, 0];

        // Act
        var (result, err) = GCDalg.GCDStein(numbers);

        // Assert
        Assert.Equal(15, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCD_StringInput_ValidValues_ReturnsCorrectValue()
    {
        // Arrange
        string[] inputs = ["48 18    ", "  12 "];

        // Act
        var (result, err) = GCDalg.GCD(inputs);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCDStein_StringInput_ValidValues_ReturnsCorrectValue()
    {
        // Arrange
        string[] inputs = ["48 18    ", "  12 "];

        // Act
        var (result, err) = GCDalg.GCDStein(inputs);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal("", err);
    }

    [Fact]
    public void GCD_StringInput_InvalidValues_ReturnsError()
    {
        // Arrange
        string[] inputs = ["48 18    ", "abc", "  12 "];

        // Act
        var (result, err) = GCDalg.GCD(inputs);

        // Assert
        Assert.Equal(0, result);
        Assert.Equal("Неверный ввод!", err);
    }

    [Fact]
    public void GCDStein_StringInput_InvalidValues_ReturnsError()
    {
        // Arrange
        string[] inputs = ["48 18    ", "abc", "  12 "];

        // Act
        var (result, err) = GCDalg.GCDStein(inputs);

        // Assert
        Assert.Equal(0, result);
        Assert.Equal("Неверный ввод!", err);
    }

    [Fact]
    public void TimerThis_MeasuresExecutionTime()
    {
        // Arrange
        static (int, string) testFunc(int[] nums) => (nums.Sum(), "");

        // Act
        var (result, err, time) = GCDalg.TimerThis(testFunc, 1, 2, 3, 4, 5);

        // Assert
        Assert.Equal(15, result);
        Assert.Equal("", err);
        Assert.True(time > 0);
    }

    [Theory]
    [InlineData(48, 18, 6)]
    [InlineData(17, 13, 1)]
    [InlineData(100, 25, 25)]
    [InlineData(0, 15, 15)]
    [InlineData(15, 0, 15)]
    public void GCD_VariousInputs_ReturnsExpected(int a, int b, int expected)
    {
        // Act
        var (result, err) = GCDalg.GCD(a, b);

        // Assert
        Assert.Equal(expected, result);
        Assert.Equal("", err);
    }
    
    [Theory]
    [InlineData(48, 18, 6)]
    [InlineData(17, 13, 1)]
    [InlineData(100, 25, 25)]
    [InlineData(0, 15, 15)]
    [InlineData(15, 0, 15)]
    public void GCDStein_VariousInputs_ReturnsExpected(int a, int b, int expected)
    {
        // Act
        var (result, err) = GCDalg.GCDStein(a, b);
        
        // Assert
        Assert.Equal(expected, result);
        Assert.Equal("", err);
    }
}
