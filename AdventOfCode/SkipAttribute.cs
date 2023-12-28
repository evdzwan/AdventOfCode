namespace AdventOfCode;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class SkipAttribute(object? answer) : Attribute
{
    public object? Answer { get; } = answer;
}
