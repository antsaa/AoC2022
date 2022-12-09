var input = File.ReadAllLines("input.txt");

void Part1()
{
    var steps = ParseSteps(input);
    var visited = new HashSet<Point> { new Point(0, 0) };

    var headPos = new Point(0, 0);
    var tailPos = new Point(0, 0);

    foreach (var step in steps)
    {
        for(int i = 0; i < step.Steps; i++)
        {
            // move head
            headPos = MoveHead(step, headPos);

            // move tail
            tailPos = Follow(tailPos, headPos);

            visited.Add(tailPos);
        }
    }

    Console.WriteLine(visited.Count);
}

void Part2()
{
    var steps = ParseSteps(input);
    var visited = new HashSet<Point> { new Point(0, 0) };

    var headPos = new Point(0, 0);
    Point[] knots = Enumerable.Range(0, 9).Select(_ => new Point(0, 0)).ToArray();

    foreach (var step in steps)
    {
        for (int i = 0; i < step.Steps; i++)
        {
            // move head
            headPos = MoveHead(step, headPos);

            knots[0] = Follow(knots[0], headPos);

            for(int k = 1; k < knots.Length; k++)
            {
                knots[k] = Follow(knots[k], knots[k - 1]);
            }

            visited.Add(knots[^1]);
        }
    }

    Console.WriteLine(visited.Count);
}

static Point MoveHead((string Direction, int Steps) step, Point headPos) => step switch
{
    ("R", _) => headPos with { X = headPos.X + 1 },
    ("L", _) => headPos with { X = headPos.X - 1 },
    ("U", _) => headPos with { Y = headPos.Y + 1 },
    ("D", _) => headPos with { Y = headPos.Y - 1 },
    _ => throw new InvalidOperationException()
};

static Point Follow(Point position, Point target)
{
    var deltaX = target.X - position.X;
    var deltaY = target.Y - position.Y;

    if(Math.Abs(deltaX) > 1 || Math.Abs(deltaY) > 1)
    {
        return position with { X = position.X + Math.Sign(deltaX), Y = position.Y + Math.Sign(deltaY) };
    }

    return position;
}

static List<(string Direction, int Steps)> ParseSteps(string[] lines) => lines!.Select(x => (x.Split(' ')[0], int.Parse(x.Split(' ')[1]))).ToList();

Part1();
Part2();

record Point(int X, int Y);