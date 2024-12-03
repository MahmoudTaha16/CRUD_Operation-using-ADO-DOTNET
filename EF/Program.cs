using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.NetworkInformation;

namespace EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
           List<Wallet> wallets=new List<Wallet>();


            if (Wallet.GetAll(ref wallets))
            {
                Print(wallets.Select(x=>x.ToString()));
            }
            else
            {
                Console.WriteLine( "Error");
            }

            Console.WriteLine(  "Ad New Record");
            if (Wallet.Add("Mahmoud", 40000))
            {
                Console.WriteLine("Record has been added Successfully");

            }
            else
            {
                Console.WriteLine("Error while adding the record");
            }
                ;


        }

        static void Print(IEnumerable<string> Data)
        {
            foreach (var item in Data) {
                Console.WriteLine(item);
            }
        }

    }


}
