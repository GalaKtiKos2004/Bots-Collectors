using System;

public class Utils
{
    private static Random _random = new();

    public static int GetRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue + 1);

    public static int GetRandomNumber(int maxValue) => _random.Next(0, maxValue + 1);
}
