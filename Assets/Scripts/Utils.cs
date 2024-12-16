using System;

public class Utils
{
    private static Random s_random = new();

    public static int GetRandomNumber(int minValue, int maxValue) => s_random.Next(minValue, maxValue + 1);

    public static int GetRandomNumber(int maxValue) => s_random.Next(0, maxValue + 1);
}
