namespace AdventOfCode.Challenges;

internal sealed class Day_06_Wait_For_It : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var times = input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
        var distances = input[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
        var races = times.Select((t, i) => new { Time = t, Distance = distances[i] }).ToArray();

        var beatenRecords = new List<int>();
        foreach (var race in races)
        {
            var raceDistances = new List<int>();
            for (var speed = 0; speed < race.Time; speed++)
            {
                var raceDistance = speed * (race.Time - speed);
                raceDistances.Add(raceDistance);
            }

            beatenRecords.Add(raceDistances.Count(rd => rd > race.Distance));
        }

        var answer = beatenRecords.Aggregate((a, b) => a * b);
        return answer;
    }

    protected override object? ExecutePart2(string[] input)
    {
        var time = ulong.Parse(input[0].Split(':')[1].Replace(" ", string.Empty));
        var distance = ulong.Parse(input[1].Split(':')[1].Replace(" ", string.Empty));

        var lastDistance = (ulong)0;
        var beatenRecordsCount = 0;
        for (ulong speed = 0; speed < time; speed++)
        {
            var raceDistance = speed * (time - speed);
            if (raceDistance > distance)
            {
                beatenRecordsCount++;
            }
            else if (raceDistance < lastDistance)
            {
                break;
            }

            lastDistance = raceDistance;
        }

        var answer = beatenRecordsCount;
        return answer;
    }
}
