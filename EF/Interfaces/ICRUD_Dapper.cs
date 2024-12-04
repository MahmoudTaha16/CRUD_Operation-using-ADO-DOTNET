using EF.Ado;
using EF.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Interfaces
{
   
    internal interface ICRUD_Dapper: ITransaction
    {
        public bool GetAll(ref List<Wallet_Dapper> wallets);
        public bool GetAll_UsingProcedure(ref List<Wallet_Dapper> wallets);
        public bool Add(string? holder, decimal balance);
        public bool Add_UsingProcedure(string? holder, decimal balance);
        public int Add_andReturnID__UsingProcedure(string? holder, decimal balance);
        public bool Delete(int RecordId);
        public bool Delete_UsingProcedure(int RecordId);
        public bool Update(Wallet_Dapper Record);
        public bool Update_UsingProcedure(Wallet_Dapper Record);
        public Wallet_Dapper GetAtID(int Id);
        public Wallet_Dapper GetAtID_UsingProcedure(int Id);
        public bool IsExist(int Id);

    }

}
