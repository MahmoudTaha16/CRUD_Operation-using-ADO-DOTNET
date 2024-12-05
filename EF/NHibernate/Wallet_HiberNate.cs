using EF.Dapper;
using EF.Interfaces;
using Microsoft.Extensions.Configuration;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Linq;

namespace EF.NHibernate
{
    internal class Wallet_HiberNate : ICRUD_NHibernate, IDocumenation
    {
        public Wallet_HiberNate(int id, string? holder, decimal balance)
        {
            Id = id;
            Holder = holder;
            Balance = balance;
        }

        public Wallet_HiberNate()
        {
        }
        public override string ToString()
        {
            return $"{Id.ToString().PadRight(5)} {Holder?.ToString().PadRight(20)} {Balance:C}";
        }

        public virtual int Id { get; set; }
        public virtual string? Holder { get; set; }
        public virtual decimal Balance { get; set; }

        [Description("Add a record ")]
        public void Add(string? holder, decimal balance)
        {
            #region
            using (var session =CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Wallet_HiberNate wallet_HiberNate = new Wallet_HiberNate(-1, holder, balance);
                    session.Save(wallet_HiberNate);
                    transaction.Commit();
                }
            }
            #endregion
        }

        [Description("Delete a record ")]
        public void Delete(int RecordId)
        {
            #region
            using (var session =CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                   var wallet=  session.Get<Wallet_HiberNate>(RecordId);
                    session.Delete(wallet);
                    transaction.Commit();
                }
            }
            #endregion
        }



        [Description("Return all recorda ")]

        public void GetAll(ref List<Wallet_HiberNate> wallets)
        {
            #region
            using (var session = CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Console.WriteLine(10000);
                 
                    var walletsQuery = session.Query<Wallet>();
                    //Console.WriteLine(walletsQuery.MappedAs<>());

                    Console.WriteLine(walletsQuery.Count()) ;
                    foreach (var wallet in walletsQuery)
                    {
                        //wallets.Add(wallet);
                    }
                }
            }
            #endregion
        }
        [Description("Return a record using Id")]

        public Wallet_HiberNate? GetAtID(int Id)
        {
            #region
            Wallet_HiberNate? wallet_HiberNate = null; ;
            using (var session =CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    wallet_HiberNate = session.Query<Wallet_HiberNate>().ToList().Where(x=>x.Id==Id).FirstOrDefault();
                }
            }
            return wallet_HiberNate;
            #endregion
        }
        [Description("Check if a record is exist")]

        public bool IsExist(int Id)
        {
            #region
            Wallet_HiberNate? wallet_HiberNate;
            using (var session = CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    wallet_HiberNate = session.Query<Wallet_HiberNate>().ToList().Where(x => x.Id == Id).FirstOrDefault();
                }
            }
            #endregion

            return wallet_HiberNate !=null;
        }
        [Description("Update The REcord Using NHIBERNATE")]
        public void Update(Wallet_HiberNate Record)
        {
            #region
            using (var session = CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(Record);
                }
            }
            #endregion
        }

        [Description("Documet this class")]
        public string Document()
        {
            #region
            StringBuilder stringBuilder = new StringBuilder();
            Type ObjectType = typeof(Wallet_HiberNate);

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

        //lets create the session in this position
        //it's a normal session to interact with the data
        public static ISession CreateSession()
        {
            #region
            var config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
            var constr = config.GetSection("ConnString").Value;
            var mapper = new ModelMapper();


            //list all of type mappings from assmbly
            mapper.AddMappings(typeof(Wallet).Assembly.ExportedTypes);

            //compile class mapping
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            //optional to display the cofiuration file XML
            //Console.WriteLine(domainMapping.AsString());

            //allow  the application to specify properties
            //and mapping documents to be used when creating

            var hbConfig = new Configuration();

            hbConfig.DataBaseIntegration(c =>
            {
                //strategy to interact with provider
                c.Driver<MicrosoftDataSqlClientDriver>();
                //dialect NHibernate uses to build synatx to rdbms
                c.Dialect<MsSql2012Dialect>();
                c.ConnectionString = constr;

                //log sql statement to console
                c.LogSqlInConsole = true;

                //formatted logged sql statement
                c.LogFormattedSql = true;
                hbConfig.AddMapping(domainMapping);
            });
            //instantiate a new ISessionFactory (use settings ,properties and mapping)
            var SessionFactory = hbConfig.BuildSessionFactory();
            var session = SessionFactory.OpenSession();
            return session;
            #endregion

        }
    }
    internal class Wallet
    {
        public virtual int Id { get; set; }
        public virtual string? Holder { get; set; }
        public virtual decimal Balance { get; set; }
    }
}
