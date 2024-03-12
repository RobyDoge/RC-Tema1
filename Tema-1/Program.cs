
using Tema_1;

Console.WriteLine("0. Biti de paritate bidimensionala;\n1. CRC");
var choice=Console.ReadLine();
while (true)
{
    switch (choice)
    {
        default:
            Console.WriteLine("Incorrect Choice. Pls select a correct one");
            choice = Console.ReadLine(); 
            break;
        case "0":
            BitParBid();
            return;
        case "1":
            RCR();
            return;
    }

}

void BitParBid()
{
    PBB pBB = new PBB();
    pBB.Run();
}

void RCR()
{

}