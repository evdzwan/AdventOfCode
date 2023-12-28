namespace AdventOfCode.Challenges;

internal sealed class Day_03_Gear_Ratios : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var matrix = input.Select(l => l.ToCharArray()).ToArray();
        var sum = 0;

        for (var y = 0; y < input.Length; y++)
        {
            var row = input[y];
            for (var x = 0; x < row.Length; x++)
            {
                var ch = row[x];
                if (char.IsDigit(ch) && (x == 0 || !char.IsDigit(row[x - 1])))
                {
                    var startX = x;

                    var number = string.Empty;
                    while (char.IsDigit(ch))
                    {
                        number += ch;
                        if (x == row.Length - 1)
                        {
                            break;
                        }

                        ch = row[++x];
                    }

                    var coordinatesToCheck = new List<(int X, int Y)> { (startX - 1, y - 1), (startX - 1, y), (startX - 1, y + 1), (startX + number.Length, y - 1), (startX + number.Length, y), (startX + number.Length, y + 1) };
                    for (var nx = startX; nx < startX + number.Length; nx++)
                    {
                        coordinatesToCheck.AddRange([(nx, y - 1), (nx, y + 1)]);
                    }

                    var isValid = coordinatesToCheck.Where(c => c.X >= 0 && c.Y >= 0 && c.Y < input.Length && c.X < input[c.Y].Length)
                                                    .Any(c => matrix[c.Y][c.X] != '.' && !char.IsDigit(matrix[c.Y][c.X]));

                    if (isValid)
                    {
                        sum += int.Parse(number);
                    }
                }
            }
        }

        return sum;
    }

    protected override object? ExecutePart2(string[] input)
    {
        var matrix = input.Select(l => l.ToCharArray()).ToArray();
        var matches = new Dictionary<(int GearX, int GearY), List<int>>();

        for (var y = 0; y < input.Length; y++)
        {
            var row = input[y];
            for (var x = 0; x < row.Length; x++)
            {
                var ch = row[x];
                if (char.IsDigit(ch) && (x == 0 || !char.IsDigit(row[x - 1])))
                {
                    var startX = x;

                    var number = string.Empty;
                    while (char.IsDigit(ch))
                    {
                        number += ch;
                        if (x == row.Length - 1)
                        {
                            break;
                        }

                        ch = row[++x];
                    }

                    var coordinatesToCheck = new List<(int X, int Y)> { (startX - 1, y - 1), (startX - 1, y), (startX - 1, y + 1), (startX + number.Length, y - 1), (startX + number.Length, y), (startX + number.Length, y + 1) };
                    for (var nx = startX; nx < startX + number.Length; nx++)
                    {
                        coordinatesToCheck.AddRange([(nx, y - 1), (nx, y + 1)]);
                    }

                    var gears = coordinatesToCheck.Where(c => c.X >= 0 && c.Y >= 0 && c.Y < input.Length && c.X < input[c.Y].Length)
                                                  .Where(c => matrix[c.Y][c.X] != '.' && !char.IsDigit(matrix[c.Y][c.X]));

                    foreach (var gear in gears)
                    {
                        if (!matches.TryGetValue(gear, out var match))
                        {
                            matches[gear] = match = [];
                        }

                        match.Add(int.Parse(number));
                    }
                }
            }
        }

        var sum = matches.Where(m => m.Value.Count == 2).Sum(m => m.Value[0] * m.Value[1]);
        return sum;
    }
}
