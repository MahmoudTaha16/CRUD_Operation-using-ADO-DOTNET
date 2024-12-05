using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.NHibernate
{
    internal class WalletMapping:ClassMapping<Wallet>
    {
        public WalletMapping()
        {
            //first we must tell the NHIBERNATE how to mapping the table
            Id(x => x.Id, c =>
            {
                c.Generator(Generators.Identity);
                c.Type(NHibernateUtil.Int32);
                c.Column("Id");
                c.UnsavedValue(0);
            });

            Property(x => x.Holder, c =>
            {
                c.Type(NHibernateUtil.AnsiString);
                c.Column("Holder");
                c.NotNullable(true);
            });
            
            Property(x => x.Balance, c =>
            {
                c.Type(NHibernateUtil.Decimal);
                c.Column("Balance");
              
            });
            Table("Wallets");
        }


    }
}
