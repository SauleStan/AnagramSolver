using System;
using AnagramSolver.Generics;
using NUnit.Framework;

namespace AnagramSolver.Tests;

public class ValueMapperTests
{
    [Test]
    public void MapIntToGender_ReturnsGenderEnum_WhenProvidedAnInt()
    {
        // Arrange
        var value = 1;

        // Act
        var result = ValueMapper.MapValueToEnum<ValueMapper.Gender, int>(value);

        // Assert
        Assert.AreEqual(ValueMapper.Gender.Male, result);
    }
    
    [Test]
    public void MapIntToGender_ReturnsGenderEnum_WhenProvidedAString()
    {
        // Arrange
        var value = "Male";

        // Act
        var result = ValueMapper.MapValueToEnum<ValueMapper.Gender, string>(value);

        // Assert
        Assert.AreEqual(ValueMapper.Gender.Male, result);
    }
    [Test]
    public void MapStringToWeekday_ReturnsWeekdayEnum_WhenProvidedAString()
    {
        // Arrange
        var value = "Monday";

        // Act
        var result = ValueMapper.MapValueToEnum<ValueMapper.Weekday, string>(value);

        // Assert
        Assert.AreEqual(ValueMapper.Weekday.Monday, result);
    }
    
}