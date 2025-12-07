namespace OOP_pr4.Tests;
using MyMath;


public class VectorTest
{
    [Fact]
    public void CreateVector_Default_Correct()
    {
        // Act
        Vector3 v = new();

        // Assert
        Assert.Equal(0, v.X);
        Assert.Equal(0, v.Y);
        Assert.Equal(0, v.Z);
    }

    [Fact]
    public void CreateVector_From3Double_Correct()
    {
        // Act
        Vector3 v = new(1, 2, 4);

        // Assert
        Assert.Equal(1, v.X);
        Assert.Equal(2, v.Y);
        Assert.Equal(4, v.Z);
    }

    [Fact]
    public void CreateVector_FromGoodArray_Correct()
    {
        // Arrange
        double[] arr = [1, 2, 4];

        // Act
        Vector3 v = new(arr);

        // Assert
        Assert.Equal(1, v.X);
        Assert.Equal(2, v.Y);
        Assert.Equal(4, v.Z);
    }

    [Fact]
    public void CreateVector_FromBadArray_ThrowIndexOutOfRangeException()
    {
        // Arrange
        double[] arr1 = [1, 2, 4, 8];
        double[] arr2 = [1, 2];

        // Act
        try
        { 
            Vector3 v1 = new(arr1);
            Vector3 v2 = new(arr2);
            // exception не возник
            Assert.True(false);
        }
        catch (IndexOutOfRangeException) { Assert.True(true); }
        // не тот exception
        catch { Assert.True(false); }
    }

    [Fact]
    public void Convertions_ToString_Correct()
    {
        // Arrange
        Dictionary<string, Vector3> d = new()
        {
            {"<1, 2, 4>", new Vector3(1, 2, 4)},
            {"<0, 0, 0>", new Vector3()},
            {"<2.71828, 0, 0>", new Vector3(2.71828, 0, 0)},
            {"<0, 3.14159, 0>", new Vector3(0, 3.14159, 0)},
            {"<0, 0, 1.618>", new Vector3(0, 0, 1.618)},
        };

        // Act
        foreach( var (s, c) in d ) 
        {
            string resString = (string)c;
            string resToString = c.ToString();
            // Assert
            Assert.Equal(s, resString);
            Assert.Equal(s, resToString);
        }
    }

    [Fact]
    public void VectorMath_SimpleOperations_Correct()
    {
        // Arrange
        Vector3 a = new(1, 2, 4);
        Vector3 b = new(8, 16, 32);

        // Act
        var sum = a+b;
        var sub = b-a;
        var rev_a = -a;
        var abs = a.Abs;

        // Assert
        Assert.Equal(new Vector3(9, 18, 36), sum);
        Assert.Equal(new Vector3(7, 14, 28), sub);
        Assert.Equal(new Vector3(-1, -2, -4), rev_a);
        Assert.Equal(21, abs*abs);
        Assert.True(a == new Vector3(1, 2, 4));
        Assert.False(a == new Vector3(-1, 2, 4));
        Assert.True(a != new Vector3(-1, 2, 4));
        Assert.False(a != new Vector3(1, 2, 4));
    }

    [Fact]
    public void VectorMath_VectorOperations_Correct()
    {
        // Arrange
        Vector3 a = new(0, 1, 2);
        Vector3 b = new(4, 8, 16);

        // Act
        var mult1 = a * b;
        var mult2 = b * a;
        var div1  = a / b;
        var div2  = b / a;
        var scal  = a & b;

        // Assert
        Assert.Equal(new Vector3(0, 8, -4), mult1);
        Assert.Equal(new Vector3(0, -8, 4), mult2);
        Assert.Equal(new Vector3(0, -8, 4), div1);
        Assert.Equal(new Vector3(0, 8, -4), div2);
        Assert.Equal(40, scal);
    }

    [Fact]
    public void VectorAsArray_ToArray_Correct()
    {
        // Arrange
        Vector3 v1 = new();
        double[] correct_arr1 = [0, 0, 0];
        Vector3 v2 = new(1, 2, 4);
        double[] correct_arr2 = [1, 2, 4];

        // Assert
        Assert.Equal(correct_arr1, v1.ToArray());
        Assert.Equal(correct_arr2, v2.ToArray());
    }

    [Fact]
    public void VectorAsArray_ValidIndex_Correct()
    {
        // Arrange
        Vector3 v = new(1, 2, 4);

        // Assert
        Assert.Equal(1, v[0]);
        Assert.Equal(2, v[1]);
        Assert.Equal(4, v[2]);
    }

    [Fact]
    public void VectorAsArray_InvalidIndex_ThrowIndexOutOfRangeException()
    {
        // Arrange
        Vector3 v = new(1, 2, 4);

        // Act / Assert
        try
        {
            _ = v[3];

            Assert.True(false);
        }catch(IndexOutOfRangeException) { Assert.True(true); }
        catch { Assert.True(false); }

        try
        {
            _ = v[-1];

            Assert.True(false);
        }catch(IndexOutOfRangeException) { Assert.True(true); }
        catch { Assert.True(false); }

        try
        {
            v[3] = 0;
            Assert.True(false);
        }catch(IndexOutOfRangeException) { Assert.True(true); }
        catch { Assert.True(false); }

        try
        {
            v[-1] = 0;
            Assert.True(false);
        }catch(IndexOutOfRangeException) { Assert.True(true); }
        catch { Assert.True(false); }
    }
}