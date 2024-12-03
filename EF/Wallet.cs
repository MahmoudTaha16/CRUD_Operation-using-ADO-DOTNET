using EF.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;

namespace EF
{
    class Wallet: ICRUD
    {
        #region
        public Wallet(int id, string? holder, decimal balance)
        {
            Id = id;
            Holder = holder;
            Balance = balance;
        }

        public Wallet()
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

        [Description("This method is used to add record to sql table ,and didn't return anything from the table " +
            "If The Query Is success , the method will return true, else return false")]
        public  bool Add(string? holder, decimal balance)
        {
            #region
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    string sql = $"INSERT INTO [dbo].[Wallets] ([Holder] ,[Balance]) " +
                      $"VALUES (@Holder,@Balance)";
                    SqlCommand cmd = new SqlCommand( sql, sqlConnection);
                   
                    SqlParameter sqlParameter_Holder = new SqlParameter()
                    {
                        ParameterName = "@Holder",
                        DbType = System.Data.DbType.String,
                        Value = holder
                    };
                    SqlParameter sqlParameter_Balance = new SqlParameter()
                    {
                        ParameterName = "@Balance",
                        DbType = System.Data.DbType.Decimal,
                        Value = balance
                    };
                    cmd.Parameters.Add(sqlParameter_Holder);
                    cmd.Parameters.Add(sqlParameter_Balance);
                    sqlConnection.Open();
                    using (cmd)
                    {
                         result = cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return result != -1 || result==0;
            #endregion
        }

        [Description("This method is used to Get All records")]
        public  bool GetAll(ref List<Wallet> wallets)
        {
            #region
            wallets = new List<Wallet>();
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    string sql = $"Select * From Wallets";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.CommandType = CommandType.Text;
                    sqlConnection.Open();
                    using (cmd)
                    {
                       SqlDataReader sqlDataReader= cmd.ExecuteReader();
                        Wallet wallet;
                       
                        while (sqlDataReader.Read())
                        {
                            wallet = new Wallet(
                                     sqlDataReader.GetInt32("Id"),
                                     sqlDataReader.GetString("Holder"),
                                     sqlDataReader.GetDecimal("Balance")
                                    );
                            wallets.Add( wallet );
                        }
                    }
                }
            }
            catch( Exception ex ) 
            {
                ErrorHandling(ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return result != -1;
            #endregion
        }

        [Conditional("DEBUG")]
        [Description("This Method Is Used to display the Error Message if there is any Exception " )]
        private static void ErrorHandling(Exception ex)
        {
            Console.WriteLine($"{ex.Message.ToString()} \n {ex.StackTrace?.ToString()}");
        }
        public override string ToString()
        {
            return $"{Id.ToString().PadRight(5)} {Holder?.ToString().PadRight(20)} {Balance:C}";
        }

        public bool Delete(int RecordId)
        {
            throw new NotImplementedException();
        }

        public bool Update(Wallet Record)
        {
            throw new NotImplementedException();
        }

        public T GetAtID<T>(int Id)
        {
            throw new NotImplementedException();
        }
    }


}
