using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1
{
    internal class PBB
    {
        private bool[,] Matrix { get; set; }
        private string? Message { get; set; }
        public void Run()
        {
            Console.WriteLine("Introdu mesajul");
            Message = Console.ReadLine();
            Verify();
            Matrix = CreateMatrix();
            PrintMatrix();
            CorruptMessage();
            FindCorruptedBit();
        }

        private void FindCorruptedBit()
        {
            var newMatrix = CreateMatrix();
            var corruptedRow = FindCorruptedRow(newMatrix);
            var corruptedColumn = FindCorruptedColumn(newMatrix);
            var corruptedPosition = corruptedRow * 7 + corruptedColumn;
            Console.WriteLine($"The corrupted bit is at position {corruptedPosition}");
        }

        private int FindCorruptedColumn(bool[,] newMatrix)
        {
            for (int i = 0; i < newMatrix.GetLength(1); i++)
            {
                if (newMatrix[newMatrix.GetLength(0) - 1, i] != Matrix[Matrix.GetLength(0) - 1, i])
                    return i;
            }

            throw new Exception("No corrupted column found");

        }

        private int FindCorruptedRow(bool[,] newMatrix)
        {
            for (var i = 0; i < newMatrix.GetLength(0); i++)
            {
                if (newMatrix[i, 7] != Matrix[i, 7])
                    return i;
            }

            throw new Exception("No corrupted row found");
        }

        private void CorruptMessage()
        {
            var random = new Random();
            var position = random.Next(0, Message.Length);
            var oppositeChar = Message[position] == '0' ? '1' : '0';
            Message = Message.Remove(position, 1).Insert(position, oppositeChar.ToString());
            Console.WriteLine($"Correct corrupted bit's position is: {position}");
        }

        private void PrintMatrix()
        {
            Console.WriteLine("Matrix:");
            for (var i = 0; i < Matrix.GetLength(0); i++)
            {
                for (var j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write(Matrix[i, j] ? "1" : "0");
                }
                Console.WriteLine();
            }
        }

        private bool[,] CreateMatrix()
        {
            bool[,] matrix = new bool[Message.Length / 7 + 1,8 ];
            var noRows =0;
            var j=0;
            foreach(var c in Message)
            {
                if (c == '1')
                    matrix[noRows, j] = true;
                else matrix[noRows,j] = false;

                if (noRows+1 == Message.Length / 7)
                {
                    matrix[noRows + 1, j] = SumOnColumn( noRows, j,matrix);
                }
                ++j;
                if (j != 7) continue;
                matrix[noRows, j]=SumOnRow( noRows,matrix);
                ++noRows;
                j = 0;

            }
            matrix[noRows,7] = false;
            return matrix;
        }

        private bool SumOnColumn( int noRows, int j, bool[,] matrix)
        {
            var sum = 0;
            for(var i=0; i <= noRows;i++)
            {
                if (matrix[i, j])
                    ++sum;
            }
            return sum % 2 != 0;
        }

        private bool SumOnRow(int noRow, bool[,]matrix)
        {
            var sum=0;
            for(var i=0;i<7;i++)
            {
                if (matrix[noRow, i])
                    ++sum;
            }
            return sum % 2!=0;

        }

        private void Verify()
        {
            if(Message is null) throw new Exception("Message is null");
            if (Message.Length % 7 != 0) throw new Exception($"There Are Missing {7-Message.Length % 7} Bits");

            if (Message.Any(bit => bit != '1' && bit != '0'))
            {
                throw new Exception("Unknown Characters");
            }

        }
    }
}
