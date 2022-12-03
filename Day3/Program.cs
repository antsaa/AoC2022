using System.Diagnostics;

var input = File.ReadAllLines("input.txt");

string priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

void Part1()
{
    int scores = 0;

    foreach(var line in input)
    {
        var middle = line.Length / 2;
        (string comp1, string comp2) = (line[..middle], line[middle..]);

        var common = comp1.Intersect(comp2).Single();

        scores += GetPriorityScore(common);
    }

    Console.WriteLine(scores);
}

void Part2()
{
    int scores = 0;

    var groups = input.Chunk(3);

    foreach(var group in groups)
    {
        var common = group[0].Intersect(group[1]).Intersect(group[2]).Single();

        scores += GetPriorityScore(common);
    }

    Console.WriteLine(scores);
}

int GetPriorityScore(char chr) => priorities.IndexOf(chr) + 1;

Part1();
Part2();