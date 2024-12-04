using EF.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace EF
{
    class Wallet: ICRUD,IDocumenation,ITransaction
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
            "If The Query Is success , the method will return true, else return false,  sql text command")]
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
                        Direction = ParameterDirection.Input,
                        Value = holder
                    };
                    SqlParameter sqlParameter_Balance = new SqlParameter()
                    {
                        ParameterName = "@Balance",
                        DbType = System.Data.DbType.Decimal,
                        Direction = ParameterDirection.Input,
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

        [Description("This method is used to Get All records,sql text command")]
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

        [Description("This Method Is Used to Delete Delete a record using the given id ,sql text command")]
        public bool Delete(int RecordId)
        {
            #region
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    string sql = @"DELETE FROM [dbo].[Wallets]
                                    WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                    SqlParameter sqlParameter_Id = new SqlParameter()
                    {
                        ParameterName = "@Id",
                        DbType = System.Data.DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = RecordId
                    };

                    cmd.Parameters.Add(sqlParameter_Id);
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
                try
                {
                    sqlConnection.Close();
                }
                catch
                { }
            }

            return result > 0;
            #endregion
        }

        [Description("This Method Is Used to update a record using the given id ,sql text command")]
        public bool Update(Wallet Record)
        {
            #region
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    string sql = @"UPDATE [dbo].[Wallets]
                                   SET [Holder] = @Holder
                                      ,[Balance] = @Balance
                                 WHERE Id=@Id
                                  ";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                    SqlParameter sqlParameter_Id = new SqlParameter()
                    {
                        ParameterName = "@Id",
                        DbType = System.Data.DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = Record.Id
                    };


                    SqlParameter sqlParameter_Holder = new SqlParameter()
                    {
                        ParameterName = "@Holder",
                        DbType = System.Data.DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = Record.Holder
                    };
                    SqlParameter sqlParameter_Balance = new SqlParameter()
                    {
                        ParameterName = "@Balance",
                        DbType = System.Data.DbType.Decimal,
                        Direction = ParameterDirection.Input,
                        Value = Record.Balance
                    };
                    cmd.Parameters.Add(sqlParameter_Id);
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

            return result > 0;
            #endregion
        }

        [Description("This Method Is Used to Return a record using the given id using the sql text command ")]
        public Wallet GetAtID(int Id)
        {
            #region

            Wallet wallet = new Wallet();
            wallet.Id = -1;

            SqlConnection sqlConnection = new SqlConnection(ConnString);
            try
            {
                using (sqlConnection)
                {
                    string sql = @"Select * from wallets
                                    WHERE Id=@Id
                                  ";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.CommandType = CommandType.Text;
                    SqlParameter sqlParameter_Id = new SqlParameter()
                    {
                        ParameterName = "@Id",
                        DbType = System.Data.DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = Id
                    };

                    cmd.Parameters.Add(sqlParameter_Id);

                    sqlConnection.Open();
                    using (cmd)
                    {

                       SqlDataReader sqlDataReader= cmd.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            wallet.Id = sqlDataReader.GetInt32("Id");
                            wallet.Holder = sqlDataReader.GetString("Holder");
                            wallet.Balance = sqlDataReader.GetDecimal("Balance");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch { }
            }
            return wallet;
            #endregion
        }

        [Description("This Method Is Used to Check if a record is exist or not ,using the given id, using the sql text command ")]
        public bool IsExist(int Id)
        {
            #region
            bool IsExist=false;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            try
            {
                using (sqlConnection)
                {
                    string sql = @"Select 0 from wallets
                                    WHERE Id=@Id
                                  ";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.CommandType = CommandType.Text;
                    SqlParameter sqlParameter_Id = new SqlParameter()
                    {
                        ParameterName = "@Id",
                        DbType = System.Data.DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = Id
                    };

                    cmd.Parameters.Add(sqlParameter_Id);

                    sqlConnection.Open();
                    using (cmd)
                    {
                        object result= cmd.ExecuteScalar();
                        IsExist = result is null ? false : true;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch { }
            }
            return IsExist;
            #endregion
        }

        [Description("This method is used to Get All records,sql Stored Procedure command")]
        public bool GetAll_UsingProcedure(ref List<Wallet> wallets)
        {
            #region
            wallets = new List<Wallet>();
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    string sql = $"sp_GetAllWallets";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    using (cmd)
                    {
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();
                        Wallet wallet;

                        while (sqlDataReader.Read())
                        {
                            wallet = new Wallet(
                                     sqlDataReader.GetInt32("Id"),
                                     sqlDataReader.GetString("Holder"),
                                     sqlDataReader.GetDecimal("Balance")
                                    );
                            wallets.Add(wallet);
                        }
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

            return result != -1;
            #endregion


        }

        [Description("This method is used to add record to sql table ,and didn't return anything from the table " +
            "If The Query Is success , the method will return true, else return false,  sql StoredProcedure command")]
        public bool Add_UsingProcedure(string? holder, decimal balance)
        {
            #region
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    string sql = $"Add_UsingProcedure";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.CommandType= CommandType.StoredProcedure;
                    SqlParameter sqlParameter_Holder = new SqlParameter()
                    {
                        ParameterName = "@Holder",
                        DbType = System.Data.DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = holder
                    };
                    SqlParameter sqlParameter_Balance = new SqlParameter()
                    {
                        ParameterName = "@Balance",
                        DbType = System.Data.DbType.Decimal,
                        Direction = ParameterDirection.Input,
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

            return result > 0;
            #endregion
        }

        [Description("This Method Is Used to delete a record , using the sql Stored Procedure command ")]
        public bool Delete_UsingProcedure(int RecordId)
        {
            #region
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    string sql = @"sp_DeleteRcord";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter sqlParameter_Id = new SqlParameter()
                    {
                        ParameterName = "@Id",
                        DbType = System.Data.DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = RecordId
                    };

                    cmd.Parameters.Add(sqlParameter_Id);
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
                try
                {
                    sqlConnection.Close();
                }
                catch
                { }
            }

            return result > 0;
            #endregion
        }

        [Description("This Method Is Used to update a record using the given id ,sql Stored Procedure command")]
        public bool Update_UsingProcedure(Wallet Record)
        {
            #region
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    string sql = @"sp_UpdateRecord ";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.CommandType= CommandType.StoredProcedure;
                    SqlParameter sqlParameter_Id = new SqlParameter()
                    {
                        ParameterName = "@Id",
                        DbType = System.Data.DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = Record.Id
                    };

                    SqlParameter sqlParameter_Holder = new SqlParameter()
                    {
                        ParameterName = "@Holder",
                        DbType = System.Data.DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = Record.Holder
                    };

                    SqlParameter sqlParameter_Balance = new SqlParameter()
                    {
                        ParameterName = "@Balance",
                        DbType = System.Data.DbType.Decimal,
                        Direction = ParameterDirection.Input,
                        Value = Record.Balance
                    };
                    cmd.Parameters.Add(sqlParameter_Id);
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
                try
                {
                    sqlConnection.Close();
                }
                catch {  }
                
            }

            return result > 0;
            #endregion
        }

        [Description("This Method Is Used to Return a record using the given id using the sql Procedure command ")]
        public Wallet GetAtID_UsingProcedure(int Id)
        {
            #region

            Wallet wallet = new Wallet();
            wallet.Id = -1;

            SqlConnection sqlConnection = new SqlConnection(ConnString);
            try
            {
                using (sqlConnection)
                {
                    string sql = @"[sp_ReturnRecord]";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter sqlParameter_Id = new SqlParameter()
                    {
                        ParameterName = "@Id",
                        DbType = System.Data.DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = Id
                    };

                    cmd.Parameters.Add(sqlParameter_Id);

                    sqlConnection.Open();
                    using (cmd)
                    {
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            wallet.Id = sqlDataReader.GetInt32("Id");
                            wallet.Holder = sqlDataReader.GetString("Holder");
                            wallet.Balance = sqlDataReader.GetDecimal("Balance");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch { }
            }
            return wallet;
            #endregion
        }

        [Description("This Method Is Used to Add a record , using the sql Procedure command " +
            "If The Operation is successfully , the id will be return , else the id will be -1")]
        public int Add_andReturnID__UsingProcedure(string? holder, decimal balance)
        {
            #region
            int Id = -1;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            object result;
            try
            {
                using (sqlConnection)
                {
                    string sql = $"[sp_AddWallet_returnId]";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                    //SqlParameter sqlParameter_Id = new SqlParameter()
                    //{
                    //    ParameterName = "@Id",
                    //    DbType = System.Data.DbType.Int32,
                    //    Direction = ParameterDirection.Output,
                     
                    //}; 
                    SqlParameter sqlParameter_Holder = new SqlParameter()
                    {
                        ParameterName = "@Holder",
                        DbType = System.Data.DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = holder
                    };
                    SqlParameter sqlParameter_Balance = new SqlParameter()
                    {
                        ParameterName = "@Balance",
                        DbType = System.Data.DbType.Decimal,
                        Direction = ParameterDirection.Input,
                        Value = balance
                    };
                    //cmd.Parameters.Add(sqlParameter_Id);
                    cmd.Parameters.Add(sqlParameter_Holder);
                    cmd.Parameters.Add(sqlParameter_Balance);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    using (cmd)
                    {
                        result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            if (int.TryParse(result.ToString(), out Id)) { };
                        }

                        //Id = (int)sqlParameter_Id.Value;
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

            return Id;
            #endregion
        }
        [Description("This method is used to Get All records and return the values inside a datatable,sql text command")]
        public DataTable GetAll_UsingdataAdapter()
        {
            #region
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConnString);
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
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataTable);
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

            return dataTable;
            #endregion
        }

        public string Document()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Type ObjectType = typeof(Wallet);

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
                    string privateorPublic=item.IsPrivate ? "Private" : "Public";
                    stringBuilder.AppendLine(
                        $"{description} \n {privateorPublic} {item.ReturnType}   {item.Name} " +
                         $"( {String.Join(" , ", item.GetParameters().Select(x => x.ParameterType.ToString() + "  " + x.Name).ToList())})"
                        );

                    stringBuilder.AppendLine();


                }

            }
            return stringBuilder.ToString();

        }
        [Description("This method used to transfer balance from first id into the second id ")]
        public bool TransactionBalance(int Id1, int Id2, decimal BalanceToTransferFrom1_2,ref string Message)
        {
            #region

            Wallet wallet1 = null;
            Wallet wallet2 = null;
           if( !(IsExist(Id1)))
           {
               Message = "Wallet 1  is not exist";
               return false;
           }
            wallet1 = GetAtID(Id1);

           if ( !(IsExist(Id2)))
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
            wallet1.Balance-=BalanceToTransferFrom1_2;
            wallet2.Balance+=BalanceToTransferFrom1_2;


            SqlConnection sqlConnection = new SqlConnection(ConnString);
            int result = 0;
           
            string sql = @"sp_UpdateRecord ";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);

            SqlTransaction sqlTransaction;
            try
            {
                using (sqlConnection)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter sqlParameter_Id = new SqlParameter()
                    {
                        ParameterName = "@Id",
                        DbType = System.Data.DbType.Int32,
                        Direction = ParameterDirection.Input,
                        Value = wallet1.Id
                    };

                    SqlParameter sqlParameter_Holder = new SqlParameter()
                    {
                        ParameterName = "@Holder",
                        DbType = System.Data.DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = wallet1.Holder
                    };

                    SqlParameter sqlParameter_Balance = new SqlParameter()
                    {
                        ParameterName = "@Balance",
                        DbType = System.Data.DbType.Decimal,
                        Direction = ParameterDirection.Input,
                        Value = wallet1.Balance
                    };
                    cmd.Parameters.Add(sqlParameter_Id);
                    cmd.Parameters.Add(sqlParameter_Holder);
                    cmd.Parameters.Add(sqlParameter_Balance);
                 
                    sqlConnection.Open();
                    sqlTransaction = sqlConnection.BeginTransaction();

                   
                    try
                    {
                        using (cmd)
                        {
                            cmd.Transaction = sqlTransaction;
                            result = cmd.ExecuteNonQuery();
                        }

                        //second wallet
                        sql = @"sp_UpdateRecord ";
                        cmd = new SqlCommand(sql, sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        sqlParameter_Id = new SqlParameter()
                        {
                            ParameterName = "@Id",
                            DbType = System.Data.DbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = wallet2.Id
                        };

                        sqlParameter_Holder = new SqlParameter()
                        {
                            ParameterName = "@Holder",
                            DbType = System.Data.DbType.String,
                            Direction = ParameterDirection.Input,
                            Value = wallet2.Holder
                        };

                        sqlParameter_Balance = new SqlParameter()
                        {
                            ParameterName = "@Balance",
                            DbType = System.Data.DbType.Decimal,
                            Direction = ParameterDirection.Input,
                            Value = wallet2.Balance
                        };
                        cmd.Parameters.Add(sqlParameter_Id);
                        cmd.Parameters.Add(sqlParameter_Holder);
                        cmd.Parameters.Add(sqlParameter_Balance);

                        using (cmd)
                        {
                            cmd.Transaction = sqlTransaction;
                            result = cmd.ExecuteNonQuery();
                            sqlTransaction?.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString()+"\n"+ex.StackTrace?.ToString());
                        try
                        {
                            sqlTransaction?.Rollback();
                        }
                        catch (Exception)
                        {
                        }

                    }
                    //cmd.Transaction.Connection.BeginTransaction();







                }
            }
            catch (Exception ex)
            {
               
                ErrorHandling(ex);
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch { }

            }

            return result > 0;
            #endregion
        }
    }


}
