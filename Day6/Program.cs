var input = File.ReadAllText("input.txt");

void Part1()
{
    var marker = input.Skip(3).Select((_, i) => new { Index = i + 4, Collection = input[i..(i + 4)] }).First(x => x.Collection.Distinct().Count() == 4);
    Console.WriteLine(marker.Index);
}

void Part2()
{
    var marker = input.Skip(13).Select((_, i) => new { Index = i + 14, Collection = input[i..(i + 14)] }).First(x => x.Collection.Distinct().Count() == 14);
    Console.WriteLine(marker.Index);
}

Part1();
Part2();
