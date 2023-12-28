namespace AdventOfCode.Challenges;

internal sealed class Day_11_Cosmic_Expansion : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var universe = input.Select(row => row.Select(ch => ch == '#').ToList()).ToList();
        var universeX = universe[0].Count;
        var universeY = universe.Count;

        for (var x = 0; x < universeX; x++)
        {
            if (universe.All(row => !row[x]))
            {
                for (var y = 0; y < universeY; y++)
                {
                    universe[y].Insert(x, false);
                }

                universeX++;
                x++;
            }
        }

        for (var y = 0; y < universeY; y++)
        {
            if (universe[y].All(hasGalaxy => !hasGalaxy))
            {
                universe.Insert(y, Enumerable.Repeat(false, universeX).ToList());
                universeY++;
                y++;
            }
        }

        var galaxies = universe.Select((row, y) =>
        {
            return row.Select((hasGalaxy, x) => (HasGalaxy: hasGalaxy, X: x))
                      .Where(i => i.HasGalaxy)
                      .Select(i => (i.X, Y: y));
        }).SelectMany(p => p).ToList();

        var distances = galaxies.SelectMany((source, sourceIndex) => galaxies.Select((target, targetIndex) => (Source: sourceIndex, Target: targetIndex, Distance: Math.Abs(target.X - source.X) + Math.Abs(target.Y - source.Y))).Where(d => d.Source != d.Target).ToArray()).ToArray();
        var distancesLookup = new Dictionary<(int SourceIndex, int TargetIndex), int>();
        foreach (var (source, target, distance) in distances)
        {
            var key = (Math.Min(source, target), Math.Max(source, target));
            distancesLookup[key] = distance;
        }

        var sum = distancesLookup.Values.Sum();
        return sum;
    }

    protected override object? ExecutePart2(string[] input)
    {
        var universe = input.Select(row => row.Select(ch => ch == '#').ToList()).ToList();
        var universeX = universe[0].Count;
        var universeY = universe.Count;

        var horizontallyEmpty = new List<int>();
        for (var x = 0; x < universeX; x++)
        {
            if (universe.All(row => !row[x]))
            {
                horizontallyEmpty.Add(x);
            }
        }

        var verticallyEmpty = new List<int>();
        for (var y = 0; y < universeY; y++)
        {
            if (universe[y].All(hasGalaxy => !hasGalaxy))
            {
                verticallyEmpty.Add(y);
            }
        }

        var galaxies = universe.Select((row, y) =>
        {
            return row.Select((hasGalaxy, x) => (HasGalaxy: hasGalaxy, X: x))
                      .Where(i => i.HasGalaxy)
                      .Select(i => (i.X, Y: y));
        }).SelectMany(p => p).ToList();

        var distances = galaxies.SelectMany((source, sourceIndex) => galaxies.Select((target, targetIndex) =>
        {
            var distance = (ulong)(Math.Abs(target.X - source.X) + Math.Abs(target.Y - source.Y));

            var minX = Math.Min(target.X, source.X) + 1;
            var maxX = Math.Max(target.X, source.X);
            for (var x = minX; x < maxX; x++)
            {
                if (horizontallyEmpty.Contains(x))
                {
                    distance += 999_999;
                }
            }

            var minY = Math.Min(target.Y, source.Y) + 1;
            var maxY = Math.Max(target.Y, source.Y);
            for (var y = minY; y < maxY; y++)
            {
                if (verticallyEmpty.Contains(y))
                {
                    distance += 999_999;
                }
            }

            return (Source: sourceIndex, Target: targetIndex, Distance: distance);
        }).Where(d => d.Source != d.Target).ToArray()).ToArray();

        var distancesLookup = new Dictionary<(int SourceIndex, int TargetIndex), ulong>();
        foreach (var (source, target, distance) in distances)
        {
            var key = (Math.Min(source, target), Math.Max(source, target));
            distancesLookup[key] = distance;
        }

        var sum = 0ul;
        foreach (var distance in distancesLookup.Values)
        {
            sum += distance;
        }

        return sum;
    }
}
