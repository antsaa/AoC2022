var monkeys = File.ReadAllLines("input.txt").Chunk(7).Select(ParseMonkeys).ToList();

void Part1()
{
    // round
    for (int i = 0; i < 20; i++)
    {
        // turn
        foreach (var monkey in monkeys)
        {
            monkey.Turn(monkeys);
        }
        Console.WriteLine($"After round {i + 1}, the monkeys are holding items with these worry levels");
        monkeys.ForEach(x => Console.WriteLine($"Monkey {x.Number}: {string.Join(',', x.Items)}"));
        Console.WriteLine();
    }
    Console.WriteLine(monkeys.Select(x => x.Inspected).OrderByDescending(x => x).Take(2).Aggregate((x, y) => x * y));
}

void Part2()
{
    monkeys = File.ReadAllLines("input.txt").Chunk(7).Select(ParseMonkeys).ToList();

    var factor = monkeys
        .Select(m => m.DivisibleBy)
        .Aggregate((f1, f2) => f1 * f2);

    // round
    for (int i = 0; i < 10_000; i++)
    {
        // turn
        foreach (var monkey in monkeys)
        {
            monkey.Turn(monkeys, false, factor);
        }
    }
    Console.WriteLine(monkeys.Select(x => x.Inspected).OrderByDescending(x => x).Take(2).Aggregate((x, y) => x * y));
}

Part1();
Part2();

Monkey ParseMonkeys(string[] lines)
{
    var monkey = new Monkey
    {
        Number = (int)char.GetNumericValue(lines[0][7]),
        Items = new Queue<ulong>(lines[1].Split(':')[1].Split(',').Select(ulong.Parse)),
        DivisibleBy = int.Parse(lines[3].Split(' ')[^1]),
        TestTrueThrowTo = int.Parse(lines[4].Split(' ')[^1]),
        TestFalseThrowTo = int.Parse(lines[5].Split(' ')[^1])
    };

    var op = lines[2].Split('=')[1].TrimStart().Split(' ');
    monkey.Operation = (op[1], op[2]) switch
    {
        ("*", "old") => (ulong old) => old * old,
        ("+", "old") => (ulong old) => old + old,
        ("+", _) => (ulong old) => old + ulong.Parse(op[2]),
        ("*", _) => (ulong old) => old * ulong.Parse(op[2])
    };

    return monkey;
}

delegate ulong Operation(ulong old);

class Monkey
{
    public void Turn(List<Monkey> monkeys, bool applyRelief = true, int factor = 1)
    {
        while(Items.TryDequeue(out ulong item))
        {
            Inspected++;

            ulong newWorryLevel = Operation.Invoke(item);

            if(applyRelief)
            {
                newWorryLevel = (ulong)Math.Floor(newWorryLevel / 3.0);
            }
            else
            {
                newWorryLevel %= (ulong)factor;
            }
            

            var destinationMonkey = Test(newWorryLevel);

            monkeys[destinationMonkey].Items.Enqueue(newWorryLevel);
            
        }
    }

    public int Number { get; set; }
    public Queue<ulong> Items { get; set; } = new Queue<ulong>();
    public Operation Operation { get; set; }
    public int DivisibleBy { get; set; }
    public int TestTrueThrowTo { get; set; }
    public int TestFalseThrowTo { get; set; }
    public int Test(ulong worryLevel) => worryLevel % (ulong)DivisibleBy == 0 ? TestTrueThrowTo : TestFalseThrowTo;
    public int Inspected { get; private set; } = 0;
}