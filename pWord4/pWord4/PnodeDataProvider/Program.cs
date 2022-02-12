using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.Data;
using Microsoft.VisualStudio.Data.AdoDotNet;
using Microsoft.VisualStudio.Data.Interop;
using pWordLib.dat;

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
		public override Guid Guid
		{
			get
			{
				Guid guid = new(4, 2, 2, 4, 4, 2, 2, 4, 42, 97, 10);
				return guid;
			}
		}

		public override string DisplayName => "pWord Data Proiver";

		public override string ShortDisplayName => "pWord";

		public override string Description => "Data Provider for pWord";

		public override Guid Technology
		{
			get
			{
				Guid guid = new(4, 2, 2, 4, 4, 2, 2, 4, 42, 97, 11);
				return guid;
			}
		}

		public override object CreateObject(Guid dataSource, Type objType)
		{
			// TODO: fix pWord so it returns a pNode of master pNodes instead of a list 
			if (objType == typeof(pWordLib.dat.pNode)) {
				return new List<pNode> {
					new pNode("master")
					};
			}
			else
			{
				throw new Exception($"Object Type: {objType.GetType()} is not allowed");
			}
		}

		public override Guid DeriveDataSource(string connectionString)
		{
			// find a pNode data source as a file and retrieve its Guid unique guid stored in the master master file
			throw new NotImplementedException();
		}

		public override Assembly GetAssembly(Guid dataSource, string assemblyString)
		{
			throw new NotImplementedException();
		}

		public override object GetProperty(string name)
		{
			throw new NotImplementedException();
		}

		public override Type GetType(Guid dataSource, string typeName)
		{
			throw new NotImplementedException();
		}

		public override bool SupportsObject(Guid dataSource, Type objType)
		{
			throw new NotImplementedException();
		}
	}
}
