static void Part1()
{
    var input = File.ReadAllText("input.txt").Split("\r\n\r\n").Select((x, i) => x.Split("\r\n").Select(int.Parse).Sum());

    Console.WriteLine(input.Max());
}

static void Part2()
{
    var input = File.ReadAllText("input.txt").Split("\r\n\r\n").Select((x, i) => x.Split("\r\n").Select(int.Parse).Sum());

    Console.WriteLine(input.OrderByDescending(x => x).Take(3).Sum());
}

Part1();
Part2();