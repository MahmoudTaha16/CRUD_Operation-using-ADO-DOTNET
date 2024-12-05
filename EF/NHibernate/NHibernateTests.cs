using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.NHibernate
{
    internal class NHibernateTests
    {
        public static void Tests()
        {

            Wallet_HiberNate wallet_HiberNate = new Wallet_HiberNate();

            List<Wallet_HiberNate> wallet_HiberNates = new List<Wallet_HiberNate>();
                
                wallet_HiberNate.GetAll(ref wallet_HiberNates);
            Print(wallet_HiberNates.Select(x=>x.ToString()));
        }

        static void Print(IEnumerable<string> Data)
        {
            foreach (var item in Data)
            {
                Console.WriteLine(item);
            }
        }

    }
}
