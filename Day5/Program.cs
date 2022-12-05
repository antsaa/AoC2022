using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

void Part1()
{
    var stacks = ParseStacks();
    var instructions = ParseInstructions();

    foreach(var instruction in instructions)
    {
        for(int i = 0; i < instruction.Count; i++)
        {
            stacks[instruction.Destination].Push(stacks[instruction.Source].Pop());
        }
    }

    Console.WriteLine(new string(stacks.Select(x => x.Peek()).ToArray()));
}

void Part2()
{
    var stacks = ParseStacks();
    var instructions = ParseInstructions();

    foreach (var instruction in instructions)
    {
        var chars = string.Empty;
        for (int i = 0; i < instruction.Count; i++)
        {
            chars += stacks[instruction.Source].Pop();
        }

        foreach(var c in chars.Reverse())
        {
            stacks[instruction.Destination].Push(c);
        }
    }

    Console.WriteLine(new string(stacks.Select(x => x.Peek()).ToArray()));
}

List<Stack<char>> ParseStacks()
{
    var stacks = new List<Stack<char>>
    {
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
    };

    foreach(var line in input[..8].Reverse())
    {
        for(int i = 0, j = 1; i <= 8; i++, j+=4)
        {
            if (char.IsLetter(line[j]))
                stacks[i].Push(line[j]);
        }
    }

    return stacks;
}

List<MoveInstrucion> ParseInstructions() => input[10..]
    .Select(
        x => {
            var matches = Regex.Match(x, @"^move (\d+) from (\d+) to (\d+)$");
            return new MoveInstrucion(int.Parse(matches.Groups[1].Value), int.Parse(matches.Groups[2].Value) - 1, int.Parse(matches.Groups[3].Value) - 1);
        })
    .ToList();

Part1();
Part2();

record MoveInstrucion(int Count, int Source, int Destination);