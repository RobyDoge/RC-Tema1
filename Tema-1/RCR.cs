using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1
{
    internal class RCR
    {
        private string? InitialMessage { get; set; }
        private string? Polynomial { get; set; }
        public void Run()
        {
            ReadInput();
            var change = Calculate();
            var sendMessage=ModifyMessage(change);
            sendMessage = CorruptMessage(sendMessage);
            VerifyMessage(sendMessage);
        }

        private void VerifyMessage(string sendMessage)
        {
            Console.WriteLine($"Received Message: {sendMessage}");
            while (sendMessage.Length >= Polynomial.Length)
            {
                StringBuilder result = new();
                for (var i = 0; i < Polynomial.Length; i++)
                {
                    var xorResult = sendMessage[i] ^ Polynomial[i];
                    result.Append(xorResult == 0 ? '0' : '1');
                }
                sendMessage = $"{result}{sendMessage[Polynomial.Length..]}";
                
                if(sendMessage.Contains('1'))
                    sendMessage = sendMessage.TrimStart('0');
                Console.WriteLine($"New Message: {sendMessage}");
            }
            Console.WriteLine(sendMessage.Any(bit => bit == '1') ? "Message is corrupted" : "Message is not corrupted");
        }

        private static string CorruptMessage(string sendMessage)
        {
            var random = new Random();
            var position = random.Next(0, sendMessage.Length - 1);
            var oppositeChar = sendMessage[position] == '0' ? '1' : '0';
            sendMessage = sendMessage.Remove(position, 1).Insert(position, oppositeChar.ToString());
            Console.WriteLine($"Correct corrupted bit's position is: {position}");
            return sendMessage;
        }

        private string ModifyMessage(string change)
        {
            var neededSymbols = Polynomial.Length-1 - change.Length;
            if (neededSymbols > 0)
            {
                change = new string('0', neededSymbols)+ change;
            }
            Console.WriteLine($"InitialMessage after XOR: {change}");
            return InitialMessage+ change;
        }

        private string Calculate()
        {
            Console.WriteLine($"In-between Steps for XOR:");
            var addedMessage = InitialMessage + new string('0', Polynomial.Length - 1);
            Console.WriteLine($"Initial Message: {addedMessage}");
            while (addedMessage.Length >= Polynomial.Length)
            {
                StringBuilder result = new();
                for (var i = 0; i < Polynomial.Length; i++)
                {
                    var xorResult = addedMessage[i] ^ Polynomial[i];
                    result.Append(xorResult == 0 ? '0' : '1');
                }
                addedMessage = $"{result}{addedMessage[Polynomial.Length..]}";

                if (addedMessage.Contains('1'))
                    addedMessage = addedMessage.TrimStart('0');
                Console.WriteLine($"New Message: {addedMessage}");
            }
            return addedMessage;
        }

        private void ReadInput()
        {
            Console.WriteLine("Enter InitialMessage:");
            InitialMessage = Console.ReadLine();
            Console.WriteLine("Enter Polynomial:");
            Polynomial = Console.ReadLine();
            Verify();
        }

        private void Verify()
        {
            if (InitialMessage is null) throw new Exception("InitialMessage is null");
            if (InitialMessage.Any(bit => bit != '1' && bit != '0'))
            {
                throw new Exception("Unknown Characters");
            }
            if (Polynomial is null) throw new Exception("Polynomial is null");
            if (Polynomial.Any(bit => bit != '1' && bit != '0'))
            {
                throw new Exception("Unknown Characters");
            }
            if(Polynomial.First()=='0') throw new Exception("Polynomial should start with 1");
            if(Polynomial.Length>InitialMessage.Length) throw new Exception("Polynomial length is greater than message length");
        }
    }
}
