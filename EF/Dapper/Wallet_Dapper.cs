using Dapper;
using EF.Ado;
using EF.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EF.Dapper
{
    internal class Wallet_Dapper: ICRUD_Dapper,IDocumenation
    {
        #region
        public Wallet_Dapper(int id, string? holder, decimal balance)
        {
            Id = id;
            Holder = holder;
            Balance = balance;
        }

        public Wallet_Dapper()
        {
        }

        public int Id { get; set; }
        public string? Holder { get; set; }
        public decimal Balance { get; set; }
        #endregion

        static string? _ConnString;
        public static string? ConnString
        {
            get
            {
                if (_ConnString == null)
                {
                    _ConnString = new ConfigurationBuilder().AddJsonFile("" +
                        "D:\\Programming\\EF\\CRUD_Operation-using-ADO-DOTNET\\EF\\appSettings.json")
                                                  .Build().GetSection("ConnString").Value;
                }
                return _ConnString;
            }
            set
            {
                _ConnString = value;
            }
        }

        [Description("This method is used to return the data as a type")]
        public bool GetAll(ref List<Wallet_Dapper> wallets)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"Select * From Wallets";
            //Here the results will be returned as dapper row 
            List<Wallet_Dapper> resultAsTyped = db.Query<Wallet_Dapper>(sql).ToList();
            wallets = resultAsTyped;
            return resultAsTyped.Count()>0;
            #endregion
        }
        [Description("This method is used to return the data as a dapper row , it uses the dynamic object")]
        public bool GetAll_AsDynamic(ref IEnumerable<object?> wallets)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"Select * From Wallets";
            //Here the results will be returned as dapper row 
            var resultAsDynamic = db.Query(sql);
            wallets = resultAsDynamic;
            return resultAsDynamic.Count() > 0;
            #endregion
        }

        [Description("This method is used to return the data as a type, using a stored Procedure")]

        public bool GetAll_UsingProcedure(ref List<Wallet_Dapper> wallets)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"sp_GetAllWallets";
            //Here the results will be returned as dapper row 
            var result = db.Query<Wallet_Dapper>(sql);
            wallets = result.ToList();

            return wallets.Count() > 0;
            #endregion
        }
        [Description("This method is used to Add A Record into  the data ")]
        public bool Add(string? holder, decimal balance)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"Insert Into Wallets (Holder,Balance) values (@Holder,@Balance)";
            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Holder = holder,
                Balance = balance
            };
            int result = -1;
            result= db.Execute(sql, parameters);
            return result > 0;
            #endregion
        }
        [Description("This method is used to Add A Record into  the data, using A stored procedure ")]
        public bool Add_UsingProcedure(string? holder, decimal balance)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"sp_AddWallet";
            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Holder = holder,
                Balance = balance
            };
            int result = -1;
            result = db.Execute(sql, parameters);
            return result > 0;
            #endregion
        }

        [Description("This method is used to Add A Record into  the data,And return the new ID using A stored procedure ")]
        public int Add_andReturnID__UsingProcedure(string? holder, decimal balance)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"sp_AddWallet_returnId";
            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Holder = holder,
                Balance = balance
            };
            int result = -1;
            result = db.ExecuteScalar<int>(sql, parameters);
            return result;
            #endregion
        }

        [Description("This method is used to Delete A Record From The data")]
        public bool Delete(int RecordId)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"Delete From Wallets Where Id=@Id";
            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Id = RecordId
            };
            int result = -1;
            result = db.Execute(sql, parameters);
            return result>0;
            #endregion
        }

        [Description("This method is used to Delete A Record From The data, using A stored procedure ")]
        public bool Delete_UsingProcedure(int RecordId)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"sp_DeleteRecord";
            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Id = RecordId
            };
            int result = -1;
            result = db.Execute(sql, parameters);
            return result > 0;
            #endregion
        }

        [Description("This method is used to Upadte A Record From The dat")]
        public bool Update(Wallet_Dapper Record)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"Update Wallets 
                        Set Holder=@Holder,
                        Balance=@Balance
                        Where Id=@Id
                        ";
            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Id = Record.Id,
                Holder = Record.Holder,
                Balance = Record.Balance,
            };
            int result = -1;
            result = db.Execute(sql, parameters);
            return result > 0;
            #endregion


        }
        [Description("This method is used to Update A Record From The data, using A stored procedure ")]

        public bool Update_UsingProcedure(Wallet_Dapper Record)
        {
           
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"sp_UpdateRecord";
            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Id = Record.Id,
                Holder = Record.Holder,
                Balance = Record.Balance,
            };
            int result = -1;
            result = db.Execute(sql, parameters);
            return result > 0;
            #endregion

        }
        [Description("This method is used to Get A Record From The data")]
        public Wallet_Dapper GetAtID(int Id)
        {
            #region
            Wallet_Dapper wallet_Dapper =new Wallet_Dapper();
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"Select * From Wallets Where Id=@Id";
            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Id = Id
            };
            wallet_Dapper = db.Query<Wallet_Dapper>(sql, parameters).Single();
            return wallet_Dapper ;
            #endregion
        }
        [Description("This method is used to Get A Record From The data, using A stored procedure ")]
        public Wallet_Dapper GetAtID_UsingProcedure(int Id)
        {
            #region
            Wallet_Dapper wallet_Dapper = new Wallet_Dapper();
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"sp_UpdateRecord";
            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Id = Id
            };
            wallet_Dapper = db.Query<Wallet_Dapper>(sql, parameters).Single();
            return wallet_Dapper;
            #endregion
        }

        [Description("This method is used to Check if a record is Exist or not ")]
        public bool IsExist(int Id)
        {
            #region
            IDbConnection db = new SqlConnection(ConnString);
            var sql = @"Select 1 from wallets where Id=@Id";

            //Here the results will be returned as dapper row 
            var parameters = new
            {
                Id = Id
            };
            int result = -1;
            result = db.ExecuteScalar<int>(sql, parameters);
            return result>0;
            #endregion
        }

        public override string ToString()
        {
            return $"{Id.ToString().PadRight(5)} {Holder?.ToString().PadRight(20)} {Balance:C}";
        }

        [Description("This method is used Make a transaction between two records using dapper")]
        public bool TransactionBalance(int Id1, int Id2, decimal BalanceToTransferFrom1_2, ref string Message)
        {
            #region
            BitArray binarryArray =new BitArray(2);
            IDbConnection db = new SqlConnection(ConnString);
            using (TransactionScope transaction=new TransactionScope())
            {
                Wallet_Dapper? wallet1 = null;
                Wallet_Dapper? wallet2 = null;
                if (!IsExist(Id1))
                {
                    Message = "Wallet 1  is not exist";
                    return false;
                }
                wallet1 = GetAtID(Id1);

                if (!IsExist(Id2))
                {
                    Message = "Wallet 2  is not exist";
                    return false;
                }
                wallet2 = GetAtID(Id2);


                if (BalanceToTransferFrom1_2 > wallet1.Balance)
                {
                    Message = "The amount is greater than the available balance";
                    return false;
                }
                wallet1.Balance -= BalanceToTransferFrom1_2;
                wallet2.Balance += BalanceToTransferFrom1_2;


                //Decrease the first wallet
                var sql = @"
                    Update Wallets 
                    Set Balance=@Balance Where Id=@Id
                        ";
                var parameters = new
                {
                    Id = Id1,
                    Balance = wallet1.Balance,
                };
               int result=  db.Execute(sql, parameters);
                if (result > 0)
                {
                    binarryArray[1] = true;
                }
                else
                {
                    binarryArray[1] = false;
                }

                //Increase the Second wallet

                parameters = new
                {
                    Id = Id2,
                    Balance = wallet2.Balance,
                };
                result = db.Execute(sql, parameters);
                if (result > 0)
                {
                    binarryArray[2] = true;
                }
                else
                {
                    binarryArray[2] = false;
                }

                transaction.Complete();


                
            }
            if (binarryArray.Cast<bool>().All(x => x.Equals(true)))
            {

                return true;
            }
            return false;

            #endregion
        }

        [Description("This method is used to (Document) descripe the usage of the mathods")]
        public string Document()
        {
            #region
            StringBuilder stringBuilder = new StringBuilder();
            Type ObjectType = typeof(Wallet_Dapper);

            if (ObjectType != null)
            {
                var methods = ObjectType.GetMethods();
                //Console.WriteLine( $"{methods.Length}" );
                foreach (var item in methods)
                {
                    //string description = String.Join(" , ", item.GetParameters().Select(x => x.ParameterType.ToString() + x.Name).ToList());
                    string description = "";
                    foreach (var attr in item.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>())
                    {
                        description = attr.Description;
                    }
                    if (description == "") continue;
                    string privateorPublic = item.IsPrivate ? "Private" : "Public";
                    stringBuilder.AppendLine(
                        $"{description} \n {privateorPublic} {item.ReturnType}   {item.Name} " +
                         $"( {string.Join(" , ", item.GetParameters().Select(x => x.ParameterType.ToString() + "  " + x.Name).ToList())})"
                        );

                    stringBuilder.AppendLine();
                }
            }
            return stringBuilder.ToString();
            #endregion
        }
    }
}
