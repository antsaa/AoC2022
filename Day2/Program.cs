string[] input = File.ReadAllLines("input.txt");

const int win = 6;
const int draw = 3;
const int lose = 0;

void Part1()
{
    int points = 0;

    foreach(var round in input)
    {
        var temp = round.Split(' ');
        int opponent = temp[0][0] - 64;
        int player = temp[1][0] - 87;
        points += (opponent, player) switch
        {
            (1, 1) => player + draw,
            (1, 2) => player + win,
            (1, 3) => player + lose,
            (2, 1) => player + lose,
            (2, 2) => player + draw,
            (2, 3) => player + win,
            (3, 1) => player + win,
            (3, 2) => player + lose,
            (3, 3) => player + draw
        };
    }

    Console.WriteLine(points);
}

void Part2()
{
    int points = 0;

    foreach (var round in input)
    {
        var temp = round.Split(' ');
        int opponent = temp[0][0] - 64;
        int result = temp[1][0] - 87;
        points += (opponent, result) switch
        {
            (1, 1) => 3 + lose,
            (1, 2) => 1 + draw,
            (1, 3) => 2 + win,
            (2, 1) => 1 + lose,
            (2, 2) => 2 + draw,
            (2, 3) => 3 + win,
            (3, 1) => 2 + lose,
            (3, 2) => 3 + draw,
            (3, 3) => 1 + win
        };
    }

    Console.WriteLine(points);
}

Part1();
Part2();