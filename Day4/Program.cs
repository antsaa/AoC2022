var input = File.ReadAllLines("input.txt");

void Part1()
{
    int count = 0;

    foreach(var line in input)
    {
        var (p1, p2) = ParseRow(line);

        var overlaps = p1.Intersect(p2).ToArray();

        if (p1.Length >= p2.Length)
        {
            if (overlaps.Length == p2.Length)
            {
                count++;
            }
        }
        else
        {
            if (overlaps.Length == p1.Length)
            {
                count++;
            }
        }
    }

    Console.WriteLine(count);
}

void Part2()
{
    int count = 0;

    foreach (var line in input)
    {
        var (p1, p2) = ParseRow(line);

        var overlaps = p1.Intersect(p2).ToArray();

        if(overlaps.Any())
        {
            count++;
        }
    }

    Console.WriteLine(count);
}

(int[] p1, int[] p2) ParseRow(string line)
{
    var assignmentPair = line.Split(',');
    var p1start = int.Parse(assignmentPair[0].Split('-')[0]);
    var p1end = int.Parse(assignmentPair[0].Split('-')[1]);
    var p2start = int.Parse(assignmentPair[1].Split('-')[0]);
    var p2end = int.Parse(assignmentPair[1].Split('-')[1]);

    var p1 = FillSegment(p1start, p1end);
    var p2 = FillSegment(p2start, p2end);

    return (p1, p2);
}

int[] FillSegment(int start, int end) => Enumerable.Range(start, end - start + 1).ToArray();

Part1();
Part2();