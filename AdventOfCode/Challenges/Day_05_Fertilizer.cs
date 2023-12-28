namespace AdventOfCode.Challenges;

internal sealed class Day_05_Fertilizer : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var seeds = input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(ulong.Parse).ToArray();
        var seedToSoilMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var soilToFertilizerMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var fertilizerToWaterMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var waterToLightMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var lightToTemperatureMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var temperatureToHumidityMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var humidityToLocationMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var currentMap = (List<(ulong Destination, ulong Source, ulong Length)>?)null;

        foreach (var line in input.Skip(1))
        {
            if (line.Contains("seed-to-soil", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = seedToSoilMap;
            }
            else if (line.Contains("soil-to-fertilizer", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = soilToFertilizerMap;
            }
            else if (line.Contains("fertilizer-to-water", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = fertilizerToWaterMap;
            }
            else if (line.Contains("water-to-light", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = waterToLightMap;
            }
            else if (line.Contains("light-to-temperature", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = lightToTemperatureMap;
            }
            else if (line.Contains("temperature-to-humidity", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = temperatureToHumidityMap;
            }
            else if (line.Contains("humidity-to-location", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = humidityToLocationMap;
            }

            if (currentMap is not null)
            {
                var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (values.Length == 3)
                {
                    currentMap.Add((ulong.Parse(values[0]), ulong.Parse(values[1]), ulong.Parse(values[2])));
                }
            }
        }

        var locations = seeds.Select(s => GetMappedValue(s, seedToSoilMap))
                             .Select(s => GetMappedValue(s, soilToFertilizerMap))
                             .Select(f => GetMappedValue(f, fertilizerToWaterMap))
                             .Select(w => GetMappedValue(w, waterToLightMap))
                             .Select(l => GetMappedValue(l, lightToTemperatureMap))
                             .Select(t => GetMappedValue(t, temperatureToHumidityMap))
                             .Select(h => GetMappedValue(h, humidityToLocationMap));

        var lowestLocation = locations.Min();
        return lowestLocation;

        static ulong GetMappedValue(ulong value, IReadOnlyList<(ulong Destination, ulong Source, ulong Length)> map)
        {
            foreach (var (destination, source, length) in map)
            {
                if (value >= source && value < source + length)
                {
                    return destination + value - source;
                }
            }

            return value;
        }
    }

    [Skip(answer: 41_222_968)]
    protected override async Task<object?> ExecutePart2Async(string[] input)
    {
        var seedsWithLength = input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(ulong.Parse).ToArray();
        var seedToSoilMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var soilToFertilizerMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var fertilizerToWaterMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var waterToLightMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var lightToTemperatureMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var temperatureToHumidityMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var humidityToLocationMap = new List<(ulong Destination, ulong Source, ulong Length)>();
        var currentMap = (List<(ulong Destination, ulong Source, ulong Length)>?)null;

        foreach (var line in input.Skip(1))
        {
            if (line.Contains("seed-to-soil", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = seedToSoilMap;
            }
            else if (line.Contains("soil-to-fertilizer", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = soilToFertilizerMap;
            }
            else if (line.Contains("fertilizer-to-water", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = fertilizerToWaterMap;
            }
            else if (line.Contains("water-to-light", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = waterToLightMap;
            }
            else if (line.Contains("light-to-temperature", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = lightToTemperatureMap;
            }
            else if (line.Contains("temperature-to-humidity", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = temperatureToHumidityMap;
            }
            else if (line.Contains("humidity-to-location", StringComparison.OrdinalIgnoreCase))
            {
                currentMap = humidityToLocationMap;
            }

            if (currentMap is not null)
            {
                var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (values.Length == 3)
                {
                    currentMap.Add((ulong.Parse(values[0]), ulong.Parse(values[1]), ulong.Parse(values[2])));
                }
            }
        }

        var tasks = new List<Task<ulong>>();
        for (var i = 0; i < seedsWithLength.Length; i += 2)
        {
            var index = i;
            tasks.Add(Task.Run(() =>
            {
                var lowestLocation = ulong.MaxValue;
                ulong seed, soil, fertilizer, water, light, temperature, humidity, location;
                var skips = new ulong[7];

                var value = seedsWithLength[index];
                var length = seedsWithLength[index + 1];
                for (ulong j = 0; j < length; j++)
                {
                    seed = value + j;

                    soil = GetMappedValue(seed, seedToSoilMap, out skips[0]);
                    fertilizer = GetMappedValue(soil, soilToFertilizerMap, out skips[1]);
                    water = GetMappedValue(fertilizer, fertilizerToWaterMap, out skips[2]);
                    light = GetMappedValue(water, waterToLightMap, out skips[3]);
                    temperature = GetMappedValue(light, lightToTemperatureMap, out skips[4]);
                    humidity = GetMappedValue(temperature, temperatureToHumidityMap, out skips[5]);
                    location = GetMappedValue(humidity, humidityToLocationMap, out skips[6]);

                    if (location < lowestLocation)
                    {
                        lowestLocation = location;
                    }

                    var skip = skips.Min();
                    if (skip > 0)
                    {
                        j += skip;
                    }
                }

                return lowestLocation;
            }));
        }

        var lowestLocation = (await Task.WhenAll(tasks)).Min();
        return lowestLocation;

        static ulong GetMappedValue(ulong value, List<(ulong Destination, ulong Source, ulong Length)> map, out ulong skip)
        {
            foreach (var (destination, source, length) in map)
            {
                if (value >= source && value < source + length)
                {
                    skip = source + length - value;
                    return destination + value - source;
                }
            }

            skip = 0;
            return value;
        }
    }
}
