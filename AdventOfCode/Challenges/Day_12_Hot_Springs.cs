namespace AdventOfCode.Challenges;

internal sealed class Day_12_Hot_Springs : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var rows = input.Select(line =>
        {
            var parts = line.Split(' ');

            var pattern = parts[0];
            var counts = parts[1].Split(',').Select(int.Parse).ToArray();

            return new
            {
                Counts = counts,
                Pattern = pattern,
                Possibilities = 0 //TODO count
            };
        });

        var sum = rows.Sum(row => row.Possibilities);
        return sum;
    }
}
