using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Interfaces
{
    internal interface ICRUD 
    {
        public bool GetAll(ref List<Wallet> wallets);
        public bool Add(string? holder, decimal balance);
        public bool Delete(int RecordId);
        public bool Update(Wallet Record);
        public T GetAtID<T>(int Id);

    }
}
