using System.Text.RegularExpressions;

internal static partial class Day3
{
    [GeneratedRegex("mul\\((\\d{1,3}),(\\d{1,3})\\)", RegexOptions.IgnoreCase)]
    private static partial Regex GetPart1Regex();

    [GeneratedRegex("mul\\((\\d{1,3}),(\\d{1,3})\\)|do\\(\\)|don't\\(\\)", RegexOptions.IgnoreCase)]
    private static partial Regex GetPart2Regex();

    public static void Part2()
    {
        string buff = File.ReadAllText("./Day3.data");

        Regex r = GetPart2Regex();

        long answer = 0;
        bool capturing = true;
        foreach (Match match in r.Matches(buff))
        {
            switch (match.Value)
            {
                case "don't()":
                    capturing = false;
                    continue;
                case "do()":
                    capturing = true;
                    continue;
                default:
                    if (capturing)
                    {
                        int a = int.Parse(match.Groups[1].Value)
                            , b = int.Parse(match.Groups[2].Value);

                        answer += a * b;
                    }
                    continue;
            }
        }

        Console.WriteLine($"Answer: {answer}");
    }

    public static void Part1()
    {
        string buff = File.ReadAllText("./Day3.data");

        Regex r = GetPart1Regex();

        long answer = 0;
        foreach (Match match in r.Matches(buff))
        {
            int a = int.Parse(match.Groups[1].Value)
                , b = int.Parse(match.Groups[2].Value);

            answer += a * b;
        }

        Console.WriteLine($"Answer: {answer}");
    }
}