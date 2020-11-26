using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreWeb.Data.Ultilities
{
    public class AppConstant
    {
        public const string DbConnectionString = @"Server=(localdb)\mssqllocaldb;Database=PhoneStoreDbContext;Trusted_Connection=True;MultipleActiveResultSets=true";
        public const string DbConnectionStringSQLite = @"Data Source = PhoneStoreDbContext.db";
        public const string DbConnectionStringPostgres = @"Server=ec2-54-156-85-145.compute-1.amazonaws.com;Port=5432;Username=yjvorrfduweaew;Password=32218545f63c9b373e7b1a6f6a76a2f06e3524ecf8fb51c0e4451c79e10f21d3;Database=d370nhld068e28;Timeout=120;Trust Server Certificate=true;Pooling=true;SSL Mode=Require";
    }
}
