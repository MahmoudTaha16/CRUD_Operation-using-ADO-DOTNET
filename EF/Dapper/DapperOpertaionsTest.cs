using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EF.Dapper
{
    internal class DapperOpertaionsTest
    {
        public static void Tests()
        {

            Wallet_Dapper wallet_Dapper = new Wallet_Dapper();


            ////Dynamic->return data as dapper rows
            //IEnumerable<object?> wallet_Dappers = new List<object?>();
            ////return data as dapper rows
            //wallet_Dapper.GetAll_AsDynamic(ref wallet_Dappers);




            //Dynamic->return data as dapper rows
            List<Wallet_Dapper> wallet_Dappers = new List<Wallet_Dapper>();

            ////return data as Wallet rows
            //wallet_Dapper.GetAll(ref wallet_Dappers);
            //Print(wallet_Dappers.Select(x=>x.ToString()));

            //return data as Wallet rows
            Console.WriteLine("<------------------------All Records--------------------->");
            wallet_Dapper.GetAll_UsingProcedure(ref wallet_Dappers);
            Print(wallet_Dappers.Select(x => x.ToString()));


            //Console.WriteLine("<------------------------Insert A record --------------------->");
            //wallet_Dapper.Add("Takwa", 30000);



            //Console.WriteLine("<------------------------Insert A record Using a stored procedure--------------------->");
            //wallet_Dapper.Add_UsingProcedure("Mahmoud", 30000);

            //Console.WriteLine("<------------------------Insert A record Using a stored procedure and return the New Id--------------------->");
            //Console.WriteLine(wallet_Dapper.Add_andReturnID__UsingProcedure("Mahmoud", 30000).ToString());;

            Console.WriteLine("<------------------Documentation---------------------------->");
            Console.WriteLine(wallet_Dapper.Document());
            Console.WriteLine("<------------------Documentation---------------------------->");

            Console.WriteLine("<------------------------All Records--------------------->");

            wallet_Dapper.GetAll_UsingProcedure(ref wallet_Dappers);
            Print(wallet_Dappers.Select(x => x.ToString()));

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
