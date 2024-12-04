using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Ado
{
    internal class Ado_Operations
    {
        static void Tests()
        {
            List<Wallet_Ado> wallets = new List<Wallet_Ado>();

            Wallet_Ado wallet = new Wallet_Ado();

            if (wallet.GetAll(ref wallets))
            {
                Print(wallets.Select(x => x.ToString()));
            }
            else
            {
                Console.WriteLine("Error");
            }
            Console.WriteLine("<----------------------->");

            //<----------------------------------------------------------------------->

            //Console.WriteLine(  "Add New Record");
            //if (wallet.Add("Mahmoud", 40000))
            //{
            //    Console.WriteLine("Record has been added Successfully");

            //}
            //else
            //{
            //    Console.WriteLine("Error while adding the record");
            //}
            //    ;

            //<----------------------------------------------------------------------->
            //Console.WriteLine(  "Delete Record");

            //if (wallet.Delete(1))
            //{
            //    Console.WriteLine("Record has been deleted Successfully");

            //}
            //else
            //{
            //    Console.WriteLine("Error while deleting the record");
            //};

            //<----------------------------------------------------------------------->

            //Console.WriteLine(  "Update Record");
            //if(wallet.Update(new Wallet(2, "Mostafa", 10000)))
            //{
            //    Console.WriteLine("Record has been Updated Successfully");
            //}
            //else
            //{
            //    Console.WriteLine("Error while Up[dating the record");
            //};

            //<----------------------------------------------------------------------->

            //Console.WriteLine("Return a Record");

            //wallet = wallet.GetAtID(2);

            //Console.WriteLine(wallet);


            ////<----------------------------------------------------------------------->

            //Console.WriteLine("Check if a Record Is Exist");

            //if (wallet.IsExist(1))
            //{
            //  Console.WriteLine("Record Is Exist");
            //}
            //else
            //{
            //    Console.WriteLine("Record Is Not Found");
            //}

            //Console.WriteLine("<----------------------->");
            //Console.WriteLine(wallet);

            //int Id= wallet.Add_andReturnID__UsingProcedure("Abdo", 80000);
            //if (Id!=-1)
            //{
            //    Console.WriteLine($"Record Add with Id ={Id}");
            //}
            //else
            //{
            //    Console.WriteLine("Error");
            //}


            Console.WriteLine("<----------------------->");
            Console.WriteLine("<----------------------->");
            //Console.WriteLine("<-----------------------------Documentation->>>>>>>>>>>>>>>>>>>>>>>>");

            Console.WriteLine(wallet.Document());




            //Console.WriteLine("<-----------------------------Transfering->>>>>>>>>>>>>>>>>>>>>>>>");



            //string ErrorMessage = "";

            //if (wallet.TransactionBalance(2, 4, 1000, ref ErrorMessage))
            //{
            //    Console.WriteLine("amount has been transfered correctly");
            //}
            //else
            //{
            //    Console.WriteLine(ErrorMessage);
            //}



            Console.WriteLine("<----------------------->");
            if (wallet.GetAll_UsingProcedure(ref wallets))
            {
                Print(wallets.Select(x => x.ToString()));
            }
            else
            {
                Console.WriteLine("Error");
            }



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
