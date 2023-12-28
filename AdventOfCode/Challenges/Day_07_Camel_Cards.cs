using System.Text.RegularExpressions;

namespace AdventOfCode.Challenges;

#pragma warning disable SYSLIB1045
internal sealed class Day_07_Camel_Cards : Challenge
{
    protected override object? ExecutePart1(string[] input)
    {
        var cardValues = new Dictionary<char, int> { { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 }, { 'T', 10 }, { 'J', 11 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 } };
        var hands = input.Select(line =>
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var cards = parts[0];
            return new { Cards = cards, Bid = int.Parse(parts[1]), Category = GetHandCategory(new string(cards.OrderByDescending(card => cardValues[card]).ToArray())), Value = GetHandValue(cards) };
        }).OrderBy(hand => hand.Category).ThenBy(hand => hand.Value);

        var answer = hands.Select((hand, index) => hand.Bid * (index + 1)).Sum();
        return answer;

        int GetHandCategory(string cards)
        {
            var duplicates = Regex.Matches(cards, @"(.)\1+");
            return duplicates.Count switch
            {
                1 when duplicates[0].Captures[0].Length == 5 => 6,
                1 when duplicates[0].Captures[0].Length == 4 => 5,
                2 when duplicates.Any(m => m.Captures[0].Length == 3) && duplicates.Any(m => m.Captures[0].Length == 2) => 4,
                1 when duplicates[0].Captures[0].Length == 3 => 3,
                2 when duplicates.All(m => m.Captures[0].Length == 2) => 2,
                1 when duplicates[0].Captures[0].Length == 2 => 1,
                _ => 0
            };
        }

        int GetHandValue(string cards)
        {
            return int.Parse(string.Join(string.Empty, cards.Select(card => cardValues[card].ToString("00"))));
        }
    }

    protected override object? ExecutePart2(string[] input)
    {
        var cardValues = new Dictionary<char, int> { { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 }, { 'T', 10 }, { 'J', 1 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 } };
        var hands = input.Select(line =>
        {
            var cardsAndBet = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var cards = cardsAndBet[0];
            var bet = int.Parse(cardsAndBet[1]);

            return new { Cards = cards, Category = GetHandCategory(cards), Value = GetHandValue(cards), Bet = bet };
        }).OrderBy(hand => hand.Category).ThenBy(hand => hand.Value);

        var answer = hands.Select((hand, index) => hand.Bet * (index + 1)).Sum();
        return answer;

        static int GetHandCategory(string cards)
        {
            var groupedCards = cards.GroupBy(card => card);

            if (groupedCards.Any(g => g.Count() == 5) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 4) && groupedCards.Any(g => g.Key == 'J')) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 3) && groupedCards.Any(g => g.Key == 'J' && g.Count() >= 2)) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 2) && groupedCards.Any(g => g.Key == 'J' && g.Count() >= 3)) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 1) && groupedCards.Any(g => g.Key == 'J' && g.Count() >= 4)))
            {
                return 6; // five of a kind
            }
            else if (groupedCards.Any(g => g.Count() == 4) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 3) && groupedCards.Any(g => g.Key == 'J')) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 2) && groupedCards.Any(g => g.Key == 'J' && g.Count() >= 2)) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 1) && groupedCards.Any(g => g.Key == 'J' && g.Count() >= 3)))
            {
                return 5; // four of a kind
            }
            else if ((groupedCards.Any(g => g.Count() == 3) && groupedCards.Any(g => g.Count() == 2)) ||
                (groupedCards.Count(g => g.Key != 'J' && g.Count() == 2) == 2 && groupedCards.Any(g => g.Key == 'J')))
            {
                return 4; // full house
            }
            else if (groupedCards.Any(g => g.Count() == 3) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 2) && groupedCards.Any(g => g.Key == 'J')) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 1) && groupedCards.Any(g => g.Key == 'J' && g.Count() >= 2)))
            {
                return 3; // three of a kind
            }
            else if (groupedCards.Count(g => g.Count() == 2) == 2)
            {
                return 2; // two pair
            }
            else if (groupedCards.Any(g => g.Count() == 2) ||
                (groupedCards.Any(g => g.Key != 'J' && g.Count() == 1) && groupedCards.Any(g => g.Key == 'J')))
            {
                return 1; // one pair
            }

            return 0;
        }

        int GetHandValue(string cards)
        {
            return int.Parse(string.Join(string.Empty, cards.Select(card => cardValues[card].ToString("00"))));
        }
    }
}
