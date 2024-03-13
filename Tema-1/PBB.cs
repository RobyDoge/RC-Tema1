namespace Tema_1;

internal class PBB
{
    private string? InitialMessage { get; set; }

    public void Run()
    {
        Console.WriteLine("Enter the Message:");
        InitialMessage = Console.ReadLine();
        Verify();
        var matrix = CreateMatrix();
        PrintMatrix(matrix);
        var corruptedMessage = CorruptMessage(matrix);
        FindCorruptedBit(corruptedMessage);
    }

    private void FindCorruptedBit(string message)
    {
        var newMatrix = StringToMatrix(message);
        var corruptedRow = FindCorruptedRow(newMatrix);
        var corruptedColumn = FindCorruptedColumn(newMatrix);
        var corruptedPosition = corruptedRow * 8 + corruptedColumn;
        Console.WriteLine($"The corrupted bit is at position {corruptedPosition}");
    }

    private static bool[,] StringToMatrix(string message)
    {
        var numRows = (int)Math.Ceiling((double)message.Length / 8);
        var matrix = new bool[numRows, 8];

        for (var i = 0; i < message.Length; i++)
        {
            var row = i / 8;
            var col = i % 8;
            matrix[row, col] = message[i] == '1';
        }
        //Console.WriteLine("Matrix:");
        //PrintMatrix(matrix);

        return matrix;
    }

    private static int FindCorruptedColumn(bool[,] newMatrix)
    {
        for (var i = 0; i < newMatrix.GetLength(1); i++)
            if (newMatrix[newMatrix.GetLength(0) - 1, i] != SumOnColumn(i,newMatrix))
                return i;

        throw new Exception("No corrupted column found");
    }

    private static int FindCorruptedRow(bool[,] newMatrix)
    {
        for (var i = 0; i < newMatrix.GetLength(0); i++)
            if (newMatrix[i, 7] != SumOnRow(i,newMatrix))
                return i;

        throw new Exception("No corrupted row found");
    }

    private static string CorruptMessage(bool[,] matrix)
    {
        var random = new Random();
        var stringMatrix = MatrixToString(matrix);
        var position = random.Next(0, stringMatrix.Length - 1);
        var oppositeChar = stringMatrix[position] == '0' ? '1' : '0';
        stringMatrix = stringMatrix.Remove(position, 1).Insert(position, oppositeChar.ToString());
        Console.WriteLine($"Correct corrupted bit's position is: {position}");
        return stringMatrix;
    }

    private static string MatrixToString(bool[,] matrix)
    {
        var result = "";
        for (var i = 0; i < matrix.GetLength(0); i++)
        for (var j = 0; j < matrix.GetLength(1); j++)
            result += matrix[i, j] ? "1" : "0";
        return result;
    }

    private static void PrintMatrix(bool[,] Matrix)
    {
        Console.WriteLine("Matrix:");
        for (var i = 0; i < Matrix.GetLength(0); i++)
        {
            for (var j = 0; j < Matrix.GetLength(1); j++) Console.Write(Matrix[i, j] ? "1" : "0");
            Console.WriteLine();
        }
    }

    private bool[,] CreateMatrix()
    {
        var matrix = new bool[InitialMessage.Length / 7 + 1, 8];
        var noRows = 0;
        var j = 0;
        foreach (var c in InitialMessage)
        {
            if (c == '1')
                matrix[noRows, j] = true;
            else matrix[noRows, j] = false;

            if (noRows + 1 == InitialMessage.Length / 7) matrix[noRows + 1, j] = SumOnColumn(j, matrix);
            ++j;
            if (j != 7) continue;
            matrix[noRows, j] = SumOnRow(noRows, matrix);
            ++noRows;
            j = 0;
        }

        matrix[noRows, 7] = SumOnRow(noRows,matrix);
        return matrix;
    }

    private static bool SumOnColumn(int j, bool[,] matrix)
    {
        var sum = 0;
        var aux = matrix.GetLength(0);
        for (var i = 0; i < matrix.GetLength(0)-1; i++)
            if (matrix[i, j])
                ++sum;
        return sum % 2 != 0;
    }

    private static bool SumOnRow(int noRow, bool[,] matrix)
    {
        var sum = 0;
        for (var i = 0; i < 7; i++)
            if (matrix[noRow, i])
                ++sum;
        return sum % 2 != 0;
    }

    private void Verify()
    {
        if (InitialMessage is null) throw new Exception("InitialMessage is null");
        if (InitialMessage.Length % 7 != 0)
            throw new Exception($"There Are Missing {7 - InitialMessage.Length % 7} Bits");

        if (InitialMessage.Any(bit => bit != '1' && bit != '0')) throw new Exception("Unknown Characters");
    }
}