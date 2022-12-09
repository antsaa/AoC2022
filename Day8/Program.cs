var input = File.ReadAllLines("input.txt");

void Part1()
{
    int[,] matrix = new int[input.Length,input[0].Length];

    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            matrix[i, j] = (int)char.GetNumericValue(input[i][j]);
        }
    }

    int visible = (matrix.GetLength(0) * 2) + (matrix.GetLength(1) * 2) - 4;

    for (int i = 1; i < matrix.GetLength(0) - 1; i++)
    {
        for (int j = 1; j < matrix.GetLength(1) - 1; j++)
        {
            if (Left(i, j, matrix) || Right(i, j, matrix) || Up(i, j, matrix) || Down(i, j, matrix))
            {
                visible++;
            }
        }
    }

    Console.WriteLine(visible);
}

bool Right(int i, int j, int[,] matrix)
{
    var height = matrix[i, j];
    i++;
    while (i < matrix.GetLength(0))
    {
        if (height <= matrix[i, j])
        {
            return false;
        }
        i++;
    }

    return true;
}

bool Left(int i, int j, int[,] matrix)
{
    var height = matrix[i, j];
    i--;
    while(i >= 0)
    {
        if(height <= matrix[i,j])
        {
            return false;
        }
        i--;
    }

    return true;
}

bool Up(int i, int j, int[,] matrix)
{
    var height = matrix[i, j];
    j--;
    while (j >= 0)
    {
        if (height <= matrix[i, j])
        {
            return false;
        }
        j--;
    }

    return true;
}

bool Down(int i, int j, int[,] matrix)
{
    var height = matrix[i, j];
    j++;
    while (j < matrix.GetLength(1))
    {
        if (height <= matrix[i, j])
        {
            return false;
        }
        j++;
    }

    return true;
}

void Part2()
{

}

Part1();
Part2();
