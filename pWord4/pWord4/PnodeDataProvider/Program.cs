using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Reflection;
using pWordLib.dat;
using Microsoft.VisualStudio.Data;

namespace PnodeDataProvider
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
		}
	}

	public class MyDataProv : DataProvider
    {
        //public MyDataProv(StatementBuilder sb) : base(sb)
        //{
        //}

        // add methods for MyDataProv here

        public override Guid Guid => new Guid("some-guid-here");

        public override string DisplayName => "pWord Data Provider";

        public override string ShortDisplayName => "pWord";

        public override string Description => "Data Provider for pWord";

        public override Guid Technology => new Guid("some-other-guid-here");

        public override object CreateObject(Guid dataSource, Type objType)
        {
            if (objType == typeof(pNode))
            {
                return new List<pNode>
                {
                    new pNode("master")
                };
            }
            else
            {
                throw new NotSupportedException($"Object Type: {objType.Name} is not supported");
            }
        }

        public override Guid DeriveDataSource(string connectionString)
        {
            // Implement logic to derive data source
            throw new NotImplementedException();
        }

        public override Assembly GetAssembly(Guid dataSource, string assemblyString)
        {
            // Implement logic to get assembly
            throw new NotImplementedException();
        }

        public override object GetProperty(string name)
        {
            // Implement logic to get property
            throw new NotImplementedException();
        }

        public override Type GetType(Guid dataSource, string typeName)
        {
            // Implement logic to get type
            throw new NotImplementedException();
        }

        public override bool SupportsObject(Guid dataSource, Type objType)
        {
            // Implement logic to check if object is supported
            throw new NotImplementedException();
        }

    }
}
