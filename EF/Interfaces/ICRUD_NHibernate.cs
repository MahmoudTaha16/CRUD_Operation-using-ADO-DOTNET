using EF.Ado;
using EF.NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Interfaces
{
    internal interface ICRUD_NHibernate
    {
        public void GetAll(ref List<Wallet_HiberNate> wallets);
        public void Add(string? holder, decimal balance);
        public void Delete(int RecordId);
        public void Update(Wallet_HiberNate Record);
        public Wallet_HiberNate? GetAtID(int Id);
        public bool IsExist(int Id);


    }
}
