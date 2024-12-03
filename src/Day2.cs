using System.Diagnostics;

namespace aoc24;

internal class Day2
{
    public static void PT2()
    {
        var dataLines = File.ReadAllLines("./Day2.data");

        var reports = dataLines.Select(static dl => dl.Split().Select(int.Parse).ToArray());

        int safes = 0;
        foreach (var report in reports)
        {
            var x = IsReportSafe(report);
            if (x) safes++;
        }

        Debugger.Break();

        static bool IsReportSafe(IEnumerable<int> report, bool dampened = false)
        {
            int[] source = report as int[] ?? report.ToArray();

            var levelPairs = source.Pairwise().Select(AnalyzeLevels);

            Direction lastDir = Direction.None;

            foreach (var (i, di, de) in levelPairs)
            {
                if (i is 0) lastDir = di;

                if (di is Direction.None || di != lastDir)
                {
                    if (dampened) return false;

                    for (int a = i, j = source.Length; a < j; a++)
                    {
                        if (IsReportSafe(report.WithoutIndex(a).ToArray(), true))
                            return true;
                    }
                }

                if (de >= 4)
                {
                    if (dampened) return false;

                    for (int a = i, j = source.Length; a < j; a++)
                    {
                        if (IsReportSafe(report.WithoutIndex(a).ToArray(), true))
                            return true;
                    }
                }

                lastDir = di;
            }

            return true;

            static ValueTuple<int, Direction, int> AnalyzeLevels(ValueTuple<int, int> pair, int index)
            {
                var (l, r) = pair;
                int delta = l - r;
                Direction dir = delta is 0 ? Direction.None : delta < 0 ? Direction.Increasing : Direction.Decreasing;
                return (index, dir, Math.Abs(delta));
            }
        }
    }
    enum Direction
    {
        None = 0, Increasing, Decreasing
    }
}
