using EF.Ado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Interfaces
{
    internal interface ICRUD_Ado : ITransaction
    {
        public bool GetAll(ref List<Wallet_Ado> wallets);
        public bool GetAll_UsingProcedure(ref List<Wallet_Ado> wallets);
        public bool Add(string? holder, decimal balance);
        public bool Add_UsingProcedure(string? holder, decimal balance);
        public int Add_andReturnID__UsingProcedure(string? holder, decimal balance);
        public bool Delete(int RecordId);
        public bool Delete_UsingProcedure(int RecordId);
        public bool Update(Wallet_Ado Record); 
        public bool Update_UsingProcedure(Wallet_Ado Record);
        public Wallet_Ado GetAtID(int Id);
        public Wallet_Ado GetAtID_UsingProcedure(int Id);
        public bool IsExist(int Id);
        public DataTable GetAll_UsingdataAdapter();


    }
}
