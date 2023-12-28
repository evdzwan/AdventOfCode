using AdventOfCode;
using AdventOfCode.Challenges;
using System.Diagnostics;
using System.IO;

Console.WriteLine("Advent of Code 2023");
Console.WriteLine();

var answers = await Task.WhenAll(ExecuteChallengeAsync<Day_01_Trebuchet>(),
                                 ExecuteChallengeAsync<Day_02_Cube_Conundrum>(),
                                 ExecuteChallengeAsync<Day_03_Gear_Ratios>(),
                                 ExecuteChallengeAsync<Day_04_Scratchcards>(),
                                 ExecuteChallengeAsync<Day_05_Fertilizer>(),
                                 ExecuteChallengeAsync<Day_06_Wait_For_It>(),
                                 ExecuteChallengeAsync<Day_07_Camel_Cards>(),
                                 ExecuteChallengeAsync<Day_08_Haunted_Wasteland>(),
                                 ExecuteChallengeAsync<Day_09_Mirage_Maintenance>(),
                                 ExecuteChallengeAsync<Day_10_Pipe_Maze>(),
                                 ExecuteChallengeAsync<Day_11_Cosmic_Expansion>(),
                                 ExecuteChallengeAsync<Day_12_Hot_Springs>());

foreach ((var challenge, var part1, var part2, var time) in answers)
{
    PrintChallenge(challenge, part1, part2, time);
}

static async Task<(string Challenge, object? Part1, object? Part2, TimeSpan time)> ExecuteChallengeAsync<TChallenge>() where TChallenge : Challenge, new()
{
    using var challenge = new TChallenge();
    var startTime = Stopwatch.GetTimestamp();
    (var part1, var part2) = await challenge.ExecuteAsync();
    return (challenge.GetType().Name, part1, part2, Stopwatch.GetElapsedTime(startTime));
}

static void PrintAnswers(object? part1, object? part2)
{
    Console.Write("\tPart 1: ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(part1 is Exception ex1 ? ex1.Message : part1);
    Console.ResetColor();

    Console.Write("\tPart 2: ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(part2 is Exception ex2 ? ex2.Message : part2);
    Console.ResetColor();
}

static void PrintChallenge(string challenge, object? part1, object? part2, TimeSpan time)
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write(challenge);
    Console.ResetColor();
    Console.WriteLine($" took {(long)time.TotalMilliseconds}ms");

    PrintAnswers(part1, part2);
    Console.WriteLine();
}
