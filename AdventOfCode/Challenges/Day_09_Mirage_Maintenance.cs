namespace AdventOfCode.Challenges;

internal sealed class Day_09_Mirage_Maintenance : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var deltaCollections = input.Select(line =>
        {
            var deltaCollection = new List<int[]> { line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray() };
            var lastDelta = deltaCollection[0];
            while (true)
            {
                deltaCollection.Insert(0, lastDelta = lastDelta.Zip(lastDelta.Skip(1), (x, y) => y - x).ToArray());
                if (lastDelta.All(value => value == 0))
                {
                    break;
                }
            }

            return deltaCollection;
        }).ToArray();

        var sum = 0;
        foreach (var deltaCollection in deltaCollections)
        {
            var nextValue = 0;
            foreach (var delta in deltaCollection)
            {
                nextValue = delta[^1] + nextValue;
            }

            sum += nextValue;
        }

        return sum;
    }

    protected override object? ExecutePart2(string[] input)
    {
        var deltaCollections = input.Select(line =>
        {
            var deltaCollection = new List<int[]> { line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray() };
            var lastDelta = deltaCollection[0];
            while (true)
            {
                deltaCollection.Insert(0, lastDelta = lastDelta.Zip(lastDelta.Skip(1), (x, y) => y - x).ToArray());
                if (lastDelta.All(value => value == 0))
                {
                    break;
                }
            }

            return deltaCollection;
        }).ToArray();

        var sum = 0;
        foreach (var deltaCollection in deltaCollections)
        {
            var nextValue = 0;
            foreach (var delta in deltaCollection)
            {
                nextValue = delta[0] - nextValue;
            }

            sum += nextValue;
        }

        return sum;
    }
}
