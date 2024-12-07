//Day3.Part1();
//Day3.Part2();
//Day4.Part2();
new Day5().Part1();

internal sealed class Day5
{
    readonly Dictionary<int, HashSet<int>> forwards = [];

    readonly List<int[]> reprints = [];

    public Day5()
    {
        bool readingForwards = true;
        foreach(var line in File.ReadAllLines("./Day5.data"))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                readingForwards = false;
                continue;
            }

            if (readingForwards)
            {
                var pairs = line
                    .Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();

                if (!forwards.TryGetValue(pairs[0], out var l))
                {
                    l = [new()];
                    forwards[pairs[0]] = l;
                    continue;
                }
                l.Add(pairs[1]);
                continue;
            }

            var parts = line
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();
            reprints.Add(parts);
        }
    }

    public void Part1()
    {
        foreach(int[] reprint in reprints)
        {
            for (int i = 0, j = reprint.Length; i < j; i++)
            {
                //Do we have a rule?
                if (!forwards.TryGetValue(reprint[i], out var followers))
                    continue;

                //We do, verify that each subsequent page adheres to the rule
                for (int y = 0; y < j; y++)
                {
                    //Is this page anything we care about?
                    if (!followers.Contains(y))
                    {

                    }
                }
            }
        }
    }
}