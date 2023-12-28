using System.Collections.Frozen;

namespace AdventOfCode.Challenges;

internal sealed class Day_08_Haunted_Wasteland : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var instructions = input[0];
        var elements = input.Skip(2).Select(line =>
        {
            var elementParts = line.Split('=', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var targetParts = elementParts[1].Split((char[])['(', ',', ')'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            return new { Source = elementParts[0], Targets = targetParts };
        }).ToDictionary(element => element.Source, element => element.Targets);

        var element = "AAA";
        var steps = 0;
        while (true)
        {
            var targets = elements[element];
            element = targets[instructions[steps++ % instructions.Length] == 'L' ? 0 : 1];

            if (element == "ZZZ")
            {
                break;
            }
        }

        return steps;
    }

    [Skip(answer: 14_265_111_103_729)]
    protected override object? ExecutePart2(string[] input)
    {
        var instructions = input[0].Select(instruction => instruction == 'L' ? 0 : 1).ToArray();
        var bufferSize = (uint)(instructions.Length * 1_000);

        var nodes = input.Skip(2).Select((line, index) =>
        {
            var nodeParts = line.Split('=', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var source = nodeParts[0];
            var targets = nodeParts[1].Split((char[])['(', ',', ')'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            return new
            {
                Source = source,
                Targets = targets
            };
        }).ToFrozenDictionary(node => node.Source, node => node.Targets);

        var nodesWithMeta = nodes.ToFrozenDictionary(node => node.Key, node =>
        {
            var zIndexes = GetZIndexes(nodes, instructions, bufferSize, node.Key, out var finalNode);
            return new
            {
                Targets = node.Value,
                FinalNode = finalNode,
                ZIndexes = zIndexes
            };
        });

        var steps = 0ul;
        var activeNodes = nodesWithMeta.Where(node => node.Key.EndsWith('A')).Select(node => node.Value).ToArray();
        while (true)
        {
            var matchingIndexes = activeNodes.Select(node => node.ZIndexes).Aggregate((a, b) => a.Intersect(b).ToArray());
            if (matchingIndexes.Length > 0)
            {
                steps += matchingIndexes[0];
                break;
            }

            activeNodes = activeNodes.Select(node => nodesWithMeta[node.FinalNode]).ToArray();
            steps += bufferSize;
        }
        return steps;

        static uint[] GetZIndexes(FrozenDictionary<string, string[]> nodes, int[] instructions, uint bufferSize, string node, out string finalNode)
        {
            var zIndexes = new List<uint>();
            for (var i = 0u; i < bufferSize; i++)
            {
                if (node.EndsWith('Z'))
                {
                    zIndexes.Add(i);
                }

                node = nodes[node][instructions[i % instructions.Length]];
            }

            finalNode = node;
            return [.. zIndexes];
        }
    }
}
