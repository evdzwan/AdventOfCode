namespace AdventOfCode.Challenges;

internal sealed class Day_01_Trebuchet : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var numericalValues = new string(line.Where(char.IsDigit).ToArray());
            var value = int.Parse($"{numericalValues.FirstOrDefault()}{numericalValues.LastOrDefault()}");
            sum += value;
        }

        return sum;
    }

    protected override object? ExecutePart2(string[] input)
    {
        string[] textualNumbers = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        var sum = 0;

        foreach (var line in input)
        {
            var correctedLine = line;

            while (true)
            {
                var match = textualNumbers.Select((t, i) => new { TextualNumber = t, Number = i + 1, Index = correctedLine.IndexOf(t, StringComparison.OrdinalIgnoreCase) })
                                          .Where(m => m.Index >= 0)
                                          .MinBy(m => m.Index);

                if (match is null)
                {
                    break;
                }

                correctedLine = correctedLine.Remove(match.Index) + match.Number + correctedLine[(match.Index + 1)..];
            }

            var numericalValues = new string(correctedLine.Where(char.IsDigit).ToArray());
            var value = int.Parse($"{numericalValues.FirstOrDefault()}{numericalValues.LastOrDefault()}");
            sum += value;
        }

        return sum;
    }
}
