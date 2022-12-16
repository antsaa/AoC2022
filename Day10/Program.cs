var input = File.ReadAllLines("input.txt").Select(ParseInstructions).ToList();

void Part1()
{
    int[] register = new int[input.Count * 2];
    Array.Fill(register, 0);
    register[0] = 1;

    int currentCycle = 0;
    foreach(var instruction in input)
    {
        switch (instruction) {

            case AddInstruction add:
                currentCycle += add.Cycles;
                register[currentCycle] = add.Number;
                break;

            case NoOp noop:
                currentCycle++;
                break;
        }
    }

    var check20th = register[..20].Sum() * 20;
    var check60th = register[..60].Sum() * 60;
    var check100th = register[..100].Sum() * 100;
    var check140th = register[..140].Sum() * 140;
    var check180th = register[..180].Sum() * 180;
    var check220th = register[..220].Sum() * 220;

    Console.WriteLine(check20th + check60th + check100th + check140th + check180th + check220th);
}

void Part2()
{

}

Part1();
Part2();

IInstruction ParseInstructions(string line)
{
    return line.Split(' ')[0] switch
    {
        "noop" => new NoOp(),
        "addx" => new AddInstruction(int.Parse(line.Split(' ')[1]))
    };
}

interface IInstruction
{
    public int Cycles { get; }
};

record AddInstruction(int Number) : IInstruction
{
    public int Cycles => 2;
}

record NoOp() : IInstruction
{
    public int Cycles => 1;
}
