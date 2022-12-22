var input = File.ReadAllLines("input.txt").Select((x, i) => (long.Parse(x), i)).ToList();

void Part1()
{
    input = Move(input);
    int zeroIndex = input.IndexOf(input.Find(x => x.Item1 == 0));
    Console.WriteLine(
        GetValue(input, 1000 + zeroIndex) +
        GetValue(input, 2000 + zeroIndex) +
        GetValue(input, 3000 + zeroIndex)
    );
}

void Part2()
{
    var decryptionKey = 811589153;
    input = File.ReadAllLines("input.txt").Select((x, i) => (long.Parse(x) * decryptionKey, i)).ToList();
    List<(long, int)> mixed = new(input);
    for(int i = 0; i < 10; i++)
    {
        mixed = Move(input, mixed);
    }

    int zeroIndex = mixed.IndexOf(mixed.Find(x => x.Item1 == 0));
    Console.WriteLine(
        GetValue(mixed, 1000 + zeroIndex) +
        GetValue(mixed, 2000 + zeroIndex) +
        GetValue(mixed, 3000 + zeroIndex)
    );
}

List<(long val, int i)> Move(List<(long val, int i)> startNumbers, List<(long val, int i)>? result = null)
{
    result = result == null ? new(startNumbers) : result;
    foreach (var number in startNumbers)
    {
        int oldIndex = result.IndexOf(number);
        int newIndex = (int)((oldIndex + number.val) % (result.Count - 1));
        if (newIndex <= 0 && oldIndex + number.val != 0) newIndex = result.Count - 1 + newIndex;
        result.RemoveAt(oldIndex);
        result.Insert(newIndex, number);
    }
    return result;
}

long GetValue(List<(long, int)> list, int i) => list[i % list.Count].Item1;

Part1();
Part2();