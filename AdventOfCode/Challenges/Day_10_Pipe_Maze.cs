namespace AdventOfCode.Challenges;

internal sealed class Day_10_Pipe_Maze : Challenge
{
    [Skip(answer: 7_005)]
    protected override async Task<object?> ExecutePart1Async(string[] input)
    {
        var grid = input.Select(line => line.ToList()).ToList();
        var gridSize = grid.Count;

        var startPosition = grid.Select((row, y) => (X: row.IndexOf('S'), Y: y)).First(p => p.X >= 0);
        var steps = new List<List<(int X, int Y)>>();

        await TraverseAsync(grid[startPosition.Y][startPosition.X], startPosition, [], steps);
        return steps.Max(step => step.Count + 1) / 2;

        async Task TraverseAsync(char cell, (int X, int Y) position, List<(int X, int Y)> path, List<List<(int X, int Y)>> steps)
        {
            // prevent back and forth
            if (path.Contains(position))
            {
                return;
            }
            else
            {
                steps.Add(path);
                path.Add(position);
            }

            await Task.WhenAll(
            [
                Task.Run(() =>
                {
                    // left
                    if (position.X > 0 && (cell == 'S' || cell == '-' || cell == 'J' || cell == '7'))
                    {
                        var newPosition = (X: position.X - 1, position.Y);
                        return TraverseAsync(grid[newPosition.Y][newPosition.X], newPosition, [.. path], steps);
                    }
                    return Task.CompletedTask;
                }),
                Task.Run(() =>
                {
                    // right
                    if (position.X < gridSize - 1 && (cell == 'S' || cell == '-' || cell == 'L' || cell == 'F'))
                    {
                        var newPosition = (X: position.X + 1, position.Y);
                        return TraverseAsync(grid[newPosition.Y][newPosition.X], newPosition, [.. path], steps);
                    }
                    return Task.CompletedTask;
                }),
                Task.Run(() =>
                {

                    // top
                    if (position.Y > 0 && (cell == 'S' || cell == '|' || cell == 'L' || cell == 'J'))
                    {
                        var newPosition = (position.X, Y: position.Y - 1);
                        return TraverseAsync(grid[newPosition.Y][newPosition.X], newPosition, [.. path], steps);
                    }
                    return Task.CompletedTask;
                }),
                Task.Run(() =>
                {
                    // bottom
                    if (position.Y < gridSize - 1 && (cell == 'S' || cell == '|' || cell == 'F' || cell == '7'))
                    {
                        var newPosition = (position.X, Y: position.Y + 1);
                        return TraverseAsync(grid[newPosition.Y][newPosition.X], newPosition, [.. path], steps);
                    }
                    return Task.CompletedTask;
                })
            ]);
        }
    }

    [Skip(answer: 417)]
    protected override async Task<object?> ExecutePart2Async(string[] input)
    {
        var grid = input.Select(line => line.ToList()).ToList();
        var gridSize = grid.Count;

        var steps = new List<List<(int X, int Y)>>();
        var startPosition = grid.Select((row, y) => (X: row.IndexOf('S'), Y: y)).First(p => p.X >= 0);
        await TraverseAsync(grid[startPosition.Y][startPosition.X], startPosition, [], steps);

        var mainLoop = steps.MaxBy(step => step.Count) ?? throw new NullReferenceException();
        var enclosedCells = new List<(int X, int Y)>();
        for (var y = 0; y < gridSize; y++)
        {
            var isEnclosed = false;
            for (var x = 0; x < gridSize; x++)
            {
                if (mainLoop.Contains((x, y)) && (grid[y][x] == '|' || grid[y][x] == 'L' || grid[y][x] == 'J'))
                {
                    isEnclosed = !isEnclosed;
                }
                else if (isEnclosed)
                {
                    enclosedCells.Add((x, y));
                }
            }
        }

        var enclosedCount = 0;
        var displayGrid = string.Join(Environment.NewLine, grid.Select((row, y) => new string(row.Select((cell, x) =>
        {
            if (mainLoop.Contains((x, y)))
            {
                return cell switch
                {
                    'F' => '╔',
                    'L' => '╚',
                    '7' => '╗',
                    'J' => '╝',
                    '-' => '═',
                    '|' => '║',
                    'S' => '▶',
                    _ => cell
                };
            }
            else if (enclosedCells.Contains((x, y)))
            {
                enclosedCount++;
                return '■';
            }
            else
            {
                return ' ';
            }
        }).ToArray())));
        return enclosedCount;

        Task TraverseAsync(char cell, (int X, int Y) position, List<(int X, int Y)> path, List<List<(int X, int Y)>> steps)
        {
            // prevent back and forth
            if (path.Contains(position))
            {
                return Task.CompletedTask;
            }
            else
            {
                steps.Add(path);
                path.Add(position);
            }

            var continuationTasks = new List<Task>();
            if (position.X > 0 && (cell == 'S' || cell == '-' || cell == 'J' || cell == '7'))
            {
                // left
                var newPosition = (X: position.X - 1, position.Y);
                continuationTasks.Add(Task.Run(() => TraverseAsync(grid[newPosition.Y][newPosition.X], newPosition, [.. path], steps)));
            }
            if (position.X < gridSize - 1 && (cell == 'S' || cell == '-' || cell == 'L' || cell == 'F'))
            {
                // right
                var newPosition = (X: position.X + 1, position.Y);
                continuationTasks.Add(Task.Run(() => TraverseAsync(grid[newPosition.Y][newPosition.X], newPosition, [.. path], steps)));
            }
            if (position.Y > 0 && (cell == 'S' || cell == '|' || cell == 'L' || cell == 'J'))
            {
                // top
                var newPosition = (position.X, Y: position.Y - 1);
                continuationTasks.Add(Task.Run(() => TraverseAsync(grid[newPosition.Y][newPosition.X], newPosition, [.. path], steps)));
            }
            if (position.Y < gridSize - 1 && (cell == 'S' || cell == '|' || cell == 'F' || cell == '7'))
            {
                // bottom
                var newPosition = (position.X, Y: position.Y + 1);
                continuationTasks.Add(Task.Run(() => TraverseAsync(grid[newPosition.Y][newPosition.X], newPosition, [.. path], steps)));
            }

            return Task.WhenAll(continuationTasks);
        }
    }
}
