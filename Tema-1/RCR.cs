using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1
{
    internal class RCR
    {
        private string? Message { get; set; }
        private string? Polynomial { get; set; }
        public void Run()
        {
            ReadInput();
            var change = Calculate();
        }

        private string Calculate()
        {
            Console.WriteLine($"In-between Steps for XOR:");
            var addedMessage = Message + new string('0', Polynomial.Length - 1);
            Console.WriteLine($"Message: {addedMessage}");
            while (addedMessage.Length >= Polynomial.Length)
            {
                StringBuilder result = new();
                for (var i = 0; i < Polynomial.Length; i++)
                {
                    var xorResult = addedMessage[i] ^ Polynomial[i];
                    result.Append(xorResult == 0 ? '0' : '1');
                }
                addedMessage = $"{result}{addedMessage[Polynomial.Length..]}";
                addedMessage = addedMessage.TrimStart('0');
                Console.WriteLine($"Message: {addedMessage}");
            }


            return addedMessage;
        }

        private void ReadInput()
        {
            Console.WriteLine("Enter Message:");
            Message = Console.ReadLine();
            Console.WriteLine("Enter Polynomial:");
            Polynomial = Console.ReadLine();
            Verify();
        }

        private void Verify()
        {
            if (Message is null) throw new Exception("Message is null");
            if (Message.Any(bit => bit != '1' && bit != '0'))
            {
                throw new Exception("Unknown Characters");
            }
            if (Polynomial is null) throw new Exception("Polynomial is null");
            if (Polynomial.Any(bit => bit != '1' && bit != '0'))
            {
                throw new Exception("Unknown Characters");
            }
            if(Polynomial.First()=='0') throw new Exception("Polynomial should start with 1");
            if(Polynomial.Length>Message.Length) throw new Exception("Polynomial length is greater than message length");
        }
    }
}
