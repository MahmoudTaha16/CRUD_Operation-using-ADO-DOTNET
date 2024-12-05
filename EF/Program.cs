using EF.Ado;
using EF.Dapper;
using EF.NHibernate;
using NHibernate.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.NetworkInformation;
using System.Reflection;

namespace EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ;

            //Console.WriteLine(WalletMapping.CreateSession().MappedAs);
            NHibernateTests.Tests();

        }

    }


}
