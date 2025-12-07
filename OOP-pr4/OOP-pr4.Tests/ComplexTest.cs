namespace OOP_pr4.Tests;
using MyMath;


public class ComplexTest
{
    const double EPS = 1e-9;

    [Fact]
    public void CreateComplex_Default_Correct()
    {
        // Arrange
        Complex n = new();

        // Assert
        Assert.Equal(0, n.Re);
        Assert.Equal(0, n.Im);
    }
    [Fact]
    public void CreateComplex_FromSumWith_i_Correct()
    {
        // Arrange
        Complex n = 2 + 3*Complex.i; // 2+3i

        // Assert
        Assert.Equal(2, n.Re);
        Assert.Equal(3, n.Im);
    }
    [Fact]
    public void CreateComplex_From2Double_Correct()
    {
        // Arrange
        Complex n = new(3, 4);

        // Assert
        Assert.Equal(3, n.Re);
        Assert.Equal(4, n.Im);
        Assert.Equal(5, n.Abs);
    }
    [Fact]
    public void CreateComplex_ExpFrom2Double_Correct()
    {
        // Arrange
        Complex n = Complex.CreateExpComplex(3.605551275463989, 0.982793723247329);

        // Assert
        Assert.True( Math.Abs(n.Re - 2) <= EPS );
        Assert.True( Math.Abs(n.Im - 3) <= EPS );
    }
    [Fact]
    public void Convertions_Implicit_Correct()
    {
        // Arrange
        Complex fromInt = 1;
        Complex fromDouble = 2.718281828;
        //(double re, double im) = new MyMath.Complex(3, 4);

        // Assert
        Assert.Equal(1, fromInt.Re);
        Assert.Equal(0, fromInt.Im);
        Assert.Equal(2.718281828, fromDouble.Re);
        Assert.Equal(0, fromDouble.Im);
    }
    [Fact]
    public void Convertions_Deconstructor_Correct()
    {
        // Arrange
        Complex n = new(2, 3);
        (double re, double im) = n;

        // Assert
        Assert.Equal(n.Re, re);
        Assert.Equal(n.Im, im);
    }
    [Fact]
    public void Convertions_ExplicitFromString_Correct()
    {
        // Arrange
        Dictionary<string, Complex> d = new()
        {
            {"1+2i", new Complex(1, 2)},
            {"-3+4j", new Complex(-3, 4)},
            {"++++5-6iiiii", new Complex(5, -6)},
            {"3.14-1i", new Complex(3.14, -1)},
            {"-42-13.63j", new Complex(-42, -13.63)},
        };

        // Act
        foreach( var (s, c) in d ) 
        {
            Complex? res = (Complex?)s;
            // Assert
            Assert.NotNull(res);
            Assert.Equal(c, res);
        }
    }
    [Fact]
    public void Convertions_ExplicitFromString_InvalidReturnNull()
    {
        // Arrange
        string[] d = [
            "--1i+2i",
            "-3+4u",
            "3.14-+1i",
            "-42-13.6.3j",
        ];

        // Act
        foreach( var s in d ) 
        {
            Complex? res = (Complex?)s;
            // Assert
            Assert.Null(res);
        }
    }
    [Fact]
    public void Convertions_ExplicitToString_Correct()
    {
        // Arrange
        Dictionary<string, Complex> d = new()
        {
            {"1+2i", new Complex(1, 2)},
            {"-3+4i", new Complex(-3, 4)},
            {"5-6i", new Complex(5, -6)},
            {"-7-8i", new Complex(-7, -8)},
            {"9+i", new Complex(9, 1)},
            {"3.14-i", new Complex(3.14, -1)},
            {"-42-13.63i", new Complex(-42, -13.63)},
            {"i", new Complex(0, 1)},
            {"0", new Complex(0, 0)},
        };

        // Act
        foreach( var (s, c) in d ) 
        {
            string resString = (string)c;
            string resToString = c.ToString();
            // Assert
            Assert.Equal(s, resString);
            Assert.Equal($"({s})", resToString);
        }
    }
    [Fact]
    public void Convertions_ExpToString_Correct()
    {
        // Arrange
        Dictionary<string, Complex> d = new()
        {
            {"e^(2i)", Complex.CreateExpComplex(1, 2)},
            {"2e^(i)", Complex.CreateExpComplex(2, 1)},
            {"2", Complex.CreateExpComplex(2, 0)},
            {"0", Complex.CreateExpComplex(0, 99)},
            {"3.14e^(-i)", Complex.CreateExpComplex(3.14, -1)},
            {"-42e^(-13.63i)", Complex.CreateExpComplex(-42, -13.63)},
        };

        // Act
        foreach( var (s, c) in d ) 
        {
            string resToString = c.ToString(expMode: true);
            // Assert
            Assert.Equal(s, resToString);
        }
    }
    [Fact]
    public void ComplexMath_SimpleOperations_Correct()
    {
        // Arrange
        Complex a = new(1, 2);
        Complex b = new(4, 8);

        // Act
        var sum = a + b;
        var sub = b - a;
        var mult = a * b;
        var div = b / a;

        // Assert
        Assert.Equal(new Complex(5, 10), sum);
        Assert.Equal(new Complex(3, 6), sub);
        Assert.Equal(new Complex(-12, 16), mult);
        Assert.Equal(new Complex(4, 0), div);
    }
    [Fact]
    public void ComplexMath_ExpOperations_Correct()
    {
        // Arrange
        Complex a = new Complex(1, 2);
        Complex b = new Complex(4, 8);

        // Act
        var mult = Complex.ExpMultipy(a, b);
        var right_mult = new Complex(-12, 16);
        
        var div = Complex.ExpDivision(b, a);
        var right_div = new Complex(4, 0);

        var exp = Complex.Exp(a);
        var right_exp = Complex.CreateExpComplex(2.718281828, 2);
        
        // Assert
        Assert.True(Math.Abs(right_mult.Re - mult.Re) < EPS );
        Assert.True(Math.Abs(right_mult.Im - mult.Im) < EPS );
        Assert.True(Math.Abs(right_div.Re - div.Re) < EPS);
        Assert.True(Math.Abs(right_div.Im - div.Im) < EPS);
        Assert.True(Math.Abs(right_exp.Re - exp.Re) < EPS);
        Assert.True(Math.Abs(right_exp.Im - exp.Im) < EPS);
    }
    // Это нестандартный тест, его цель -- проверить время а не правильность
    // Что-то типо бенчмарка
    [Fact]
    public void ComplexMath_Multipy_TimeTest()
    {
        Complex a = new(999.999, 999.999);
        Complex b = new(999.999, 999.999);
        Random gen = new();

        System.Diagnostics.Stopwatch AlgTimer = new();
        System.Diagnostics.Stopwatch ExpTimer = new();

        // long TotalAlg = 0;
        // long TotalExp = 0;

        for(int i=0; i<500; i++)
        {
            // Побоялся что компилятор соптимизирует, поэтому рандом
            double ra1 = gen.NextDouble()-0.5;
            double ra2 = gen.NextDouble()-0.5;
            double rb1 = gen.NextDouble()-0.5;
            double rb2 = gen.NextDouble()-0.5;
            var aa = a+new Complex(ra1, ra2);
            var bb = b+new Complex(rb1, rb2);
            AlgTimer.Start();
            _ = aa*bb;
            _ = aa/bb;
            AlgTimer.Stop();
            // TotalAlg += AlgTimer.ElapsedTicks;
            ExpTimer.Start();
            _ = Complex.ExpMultipy(aa, bb);
            _ = Complex.ExpDivision(aa, bb);
            ExpTimer.Stop();
            // TotalExp += ExpTimer.ElapsedTicks;
        }

        Console.WriteLine($"Average alg operations time: {AlgTimer.ElapsedTicks / 100} (ticks)");
        Console.WriteLine($"Average exp operations time: {ExpTimer.ElapsedTicks / 100} (ticks)");

        Assert.True(true);
    }
}
