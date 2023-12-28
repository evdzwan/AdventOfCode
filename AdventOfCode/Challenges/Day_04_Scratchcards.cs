namespace AdventOfCode.Challenges;

internal sealed class Day_04_Scratchcards : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var winningNumbers = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
            var playedNumbers = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
            var matches = playedNumbers.Intersect(winningNumbers).Count();
            if (matches > 0)
            {
                sum += (int)Math.Pow(2, matches - 1);
            }
        }

        return sum;
    }

    protected override object? ExecutePart2(string[] input)
    {
        var matches = new List<int>();
        var counts = new Dictionary<int, int>();

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var winningNumbers = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
            var playedNumbers = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
            var lineMatches = playedNumbers.Intersect(winningNumbers).Count();
            matches.Add(lineMatches);

            counts[i] = counts.TryGetValue(i, out var v) ? v + 1 : 1;
            for (var j = i + 1; j < i + 1 + lineMatches && j < input.Length; j++)
            {
                counts[j] = counts.TryGetValue(j, out var w) ? w + counts[i] : counts[i];
            }
        }

        var sum = counts.Values.Sum();
        return sum;
    }
}
