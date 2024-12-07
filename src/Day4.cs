//Day3.Part1();
//Day3.Part2();
internal static class Day4
{
    public static void Part2()
    {
        //Read in
        List<List<char>> plane = [];
        foreach (ReadOnlySpan<char> line in File.ReadAllLines("./Day4.data"))
        {
            plane.Add([.. line]);
        }

        int answer = 0;
        //Seek the A
        for (int y = 0, m = plane.Count; y < m; y++)
        {
            List<char> row = plane[y];
            for (int x = 0, n = row.Count; x < n; x++)
            {
                if (row[x] is 'A')
                {
                    if (!TryLocateCorners(x,y, out var corners))
                    {
                        continue;
                    }

                    var (a, b) = corners;
                    var (na, sa) = a;
                    var (nb, sb) = b;

                    if (!((na is 'M' && sa is 'S') || (na is 'S' && sa is 'M')))
                        continue;

                    if (!((nb is 'M' && sb is 'S') || (nb is 'S' && sb is 'M')))
                        continue;

                    answer++;
                }
            }
        }

        Console.WriteLine($"The Answer is {answer}");

        bool TryLocateCorners(int ox, int oy, out ((char, char), (char, char)) corners)
        {
            corners = (((char)0, (char)0), ((char)0, (char)0));
            //If we're at an edge, there is no way to make an cross so we're done
            if (ox is 0 || oy is 0) return false;
            if (plane.Count <= oy+1) return false;

            List<char> north = plane[oy - 1];
            if (north.Count <= ox+1) return false;

            List<char> south = plane[oy + 1];

            corners = ((north[ox - 1], south[ox + 1]), (north[ox + 1], south[ox -1]));
            return true;
        }
    }

    public static void Part1()
    {
        //Read in
        List<List<char>> plane = [];
        foreach (ReadOnlySpan<char> line in File.ReadAllLines("./Day4.data"))
        {
            plane.Add([.. line]);
        }

        int answer = 0;
        //Seek the X
        for (int y = 0, m = plane.Count; y < m; y++)
        {
            List<char> row = plane[y];
            for (int x = 0, n = row.Count; x < n; x++)
            {
                if (row[x] is 'X')
                {
                    var directionalOffsets = EnumerateSeekDirections(x, y);
                    foreach (var (offsetx, offsety) in directionalOffsets)
                    {
                        if (TryFinishXmas(x, y, in offsetx, in offsety))
                        {
                            answer++;
                        }
                    }
                }
            }
        }

        Console.WriteLine($"The Answer is {answer}");

        bool TryFinishXmas(int x, int y, in int ox, in int oy)
        {
            //Laziness
            try
            {
                if (plane[y][x] is not 'X') return false;

                if (plane[y += oy][x += ox] is not 'M') return false;

                if (plane[y += oy][x += ox] is not 'A') return false;

                if (plane[y += oy][x += ox] is not 'S') return false;
            }
            catch (Exception)
            { return false; }

            return true;
        }

        IEnumerable<(int nx, int ny)> EnumerateSeekDirections(int ox, int oy)
        {
            for (int y = -1, a = oy + y; y < 2; a = oy + ++y)
            {
                if (a < 0 || a >= plane.Count) continue;
                List<char> row = plane[a];
                for (int x = -1, b = x + ox; x < 2; b = ox + ++x)
                {
                    if (b < 0 || b >= row.Count) continue;
                    if (row[b] is 'M')
                    {
                        yield return (x, y);
                    }
                }
            }
        }
    }
}