using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Interfaces
{
    internal interface ITransaction
    {
        public bool TransactionBalance(int Id1, int Id2, decimal BalanceToTransferFrom1_2, ref string Message);
    }
}
