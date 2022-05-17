namespace AnagramSolver.Generics;

public class ValueMapper
{
    public enum Gender: int
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
    public enum Weekday
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public static Gender MapIntToGender(int value)
    {
        Gender result;
        if (!Gender.TryParse(value.ToString(), out result))
        {
            throw new Exception($"Value '{value}' is not part of Gender enum");
        }

        return result;
    }
    public static Gender MapStringToGender(string value)
    {
        Gender result;
        if (!Gender.TryParse(value, out result))
        {
            throw new Exception($"Value '{value}' is not part of Gender enum");
        }

        return result;
    }
    public static Weekday MapStringToWeekday(string value)
    {
        Weekday result;
        if (!Weekday.TryParse(value, out result))
        {
            throw new Exception($"Value '{value}' is not part of Weekday enum");
        }
        return result;
    }

    public static T MapValueToEnum<T, T2>(T2 value) where T : struct
    {
        
        T result;
        if (!Enum.TryParse(value.ToString(), out result))
        {
            throw new Exception($"Value '{value}' is not part of {typeof(T)} enum");
        }

        return result;
    }
}