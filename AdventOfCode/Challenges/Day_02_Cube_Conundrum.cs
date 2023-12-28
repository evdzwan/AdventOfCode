namespace AdventOfCode.Challenges;

internal sealed class Day_02_Cube_Conundrum : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var game = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var gameId = int.Parse(game[0].Replace("Game ", string.Empty));
            var rounds = game[1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var isValid = true;
            foreach (var round in rounds)
            {
                var sets = round.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var reds = int.Parse(sets.SingleOrDefault(s => s.Contains("red"))?.Replace(" red", string.Empty) ?? "0");
                if (reds > 12)
                {
                    isValid = false;
                    break;
                }

                var greens = int.Parse(sets.SingleOrDefault(s => s.Contains("green"))?.Replace(" green", string.Empty) ?? "0");
                if (greens > 13)
                {
                    isValid = false;
                    break;
                }

                var blues = int.Parse(sets.SingleOrDefault(s => s.Contains("blue"))?.Replace(" blue", string.Empty) ?? "0");
                if (blues > 14)
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                sum += gameId;
            }
        }

        return sum;
    }

    protected override object? ExecutePart2(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var game = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var gameId = int.Parse(game[0].Replace("Game ", string.Empty));
            var rounds = game[1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var sets = rounds.Select(round =>
            {
                var sets = round.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var reds = int.Parse(sets.SingleOrDefault(s => s.Contains("red"))?.Replace(" red", string.Empty) ?? "0");
                var greens = int.Parse(sets.SingleOrDefault(s => s.Contains("green"))?.Replace(" green", string.Empty) ?? "0");
                var blues = int.Parse(sets.SingleOrDefault(s => s.Contains("blue"))?.Replace(" blue", string.Empty) ?? "0");

                return new { Reds = reds, Greens = greens, Blues = blues };
            }).ToArray();

            var requiredReds = sets.Max(s => s.Reds);
            var requiredGreens = sets.Max(s => s.Greens);
            var requiredBlues = sets.Max(s => s.Blues);

            var power = requiredReds * requiredGreens * requiredBlues;
            sum += power;
        }

        return sum;
    }
}
