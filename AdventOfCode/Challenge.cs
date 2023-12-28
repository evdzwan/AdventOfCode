using System.Reflection;

namespace AdventOfCode;

internal abstract class Challenge : IDisposable
{
    public async Task<(object? Part1, object? Part2)> ExecuteAsync()
    {
        var input = await File.ReadAllLinesAsync($"Resources/{GetType().Name}.txt");
        var parts = await Task.WhenAll(TryExecutePart1Async(input), TryExecutePart2Async(input));
        return (parts[0], parts[1]);
    }

    void IDisposable.Dispose() => OnDispose();

    protected virtual object? ExecutePart1(string[] input) => throw new NotImplementedException();

    protected virtual Task<object?> ExecutePart1Async(string[] input) => Task.FromResult(ExecutePart1(input));

    protected virtual object? ExecutePart2(string[] input) => throw new NotImplementedException();

    protected virtual Task<object?> ExecutePart2Async(string[] input) => Task.FromResult(ExecutePart2(input));

    protected virtual void OnDispose() { }

    private async Task<object?> TryExecutePart1Async(string[] input)
    {
        if (GetType().GetMethod(nameof(ExecutePart1), BindingFlags.NonPublic | BindingFlags.Instance)?.GetCustomAttribute<SkipAttribute>() is { } executeAttribute)
        {
            return executeAttribute.Answer;
        }

        if (GetType().GetMethod(nameof(ExecutePart1Async), BindingFlags.NonPublic | BindingFlags.Instance)?.GetCustomAttribute<SkipAttribute>() is { } executeAsyncAttribute)
        {
            return executeAsyncAttribute.Answer;
        }

        try
        {
            return await ExecutePart1Async(input);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    private async Task<object?> TryExecutePart2Async(string[] input)
    {
        if (GetType().GetMethod(nameof(ExecutePart2), BindingFlags.NonPublic | BindingFlags.Instance)?.GetCustomAttribute<SkipAttribute>() is { } executeAttribute)
        {
            return executeAttribute.Answer;
        }

        if (GetType().GetMethod(nameof(ExecutePart2Async), BindingFlags.NonPublic | BindingFlags.Instance)?.GetCustomAttribute<SkipAttribute>() is { } executeAsyncAttribute)
        {
            return executeAsyncAttribute.Answer;
        }

        try
        {
            return await ExecutePart2Async(input);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
