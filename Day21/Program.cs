using Day21;
using System.Linq.Expressions;

var input = File.ReadAllLines("input.txt").Select(ParseMonkeys).ToList();
Dictionary<string, long> knownMonkeyValues = input.OfType<YellingMonkey>().ToDictionary(x => x.Name, x => x.Number);

void Part1()
{
    while(!knownMonkeyValues.ContainsKey("root"))
    {
        foreach(var monkey in input.OfType<CalculatingMonkey>())
        {
            if(knownMonkeyValues.TryGetValue(monkey.Monkey1, out long number1) && knownMonkeyValues.TryGetValue(monkey.Monkey2, out long number2))
            {
                long result = monkey.Operation switch
                {
                    "+" => number1 + number2,
                    "*" => number1 * number2,
                    "-" => number1 - number2,
                    "/" => number1 / number2
                };

                knownMonkeyValues.TryAdd(monkey.Name, result);
            }
        }
    }

    Console.WriteLine(knownMonkeyValues["root"]);
}

void Part2()
{
    var expressions = new Dictionary<string, Expression>();
    var humanParam = Expression.Parameter(typeof(long), "humn");

    foreach(var monkey in input.OfType<YellingMonkey>())
    {
        Expression expr;
        if(monkey.Name == "humn")
        {
            expr = humanParam;
        }
        else
        {
            expr = Expression.Constant(monkey.Number, typeof(long));
        }

        expressions.Add(monkey.Name, expr);
    }

    while (expressions.Count != input.Count)
    {
        foreach(var monkey in input.OfType<CalculatingMonkey>().Where(x => !expressions.ContainsKey(x.Name)))
        {
            if(expressions.TryGetValue(monkey.Monkey1, out var left) && expressions.TryGetValue(monkey.Monkey2, out var right))
            {
                var expr = monkey switch
                {
                    (_, _, _, "+") => Expression.Add(left, right),
                    (_, _, _, "-") => Expression.Subtract(left, right),
                    (_, _, _, "*") => Expression.Multiply(left, right),
                    (_, _, _, "/") => Expression.Divide(left, right),
                    ("root", _, _, _) => Expression.Equal(left, right)
                };

                expressions.Add(monkey.Name, expr);
            }
        }
    }

    var root = expressions["root"];
    root = new SimpleAlgebraicSolver().Visit(root);

    var lambda = Expression.Lambda<Func<long>>(((BinaryExpression)root).Right).Compile();

    Console.WriteLine(lambda());
}


Part1();
Part2();

Monkey ParseMonkeys(string line)
{
    var name = line[..4];

    if (char.IsLetter(line[6]))
    {
        var monkey1 = line[6..10];
        var op = line[11];
        var monkey2 = line[13..];

        return new CalculatingMonkey(name, monkey1, monkey2, op.ToString());
    }
    else
    {
        return new YellingMonkey(name, int.Parse(line[5..]));
    }
}

public abstract record Monkey(string Name);
public record YellingMonkey(string Name, long Number) : Monkey(Name);
public record CalculatingMonkey(string Name, string Monkey1, string Monkey2, string Operation) : Monkey(Name);
