using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader
{
	[AttributeUsage(AttributeTargets.Class)]
	public class KeyPairDictionary : Attribute
	{
		public Type typeKey;
		public Type typeValue;

		public KeyPairDictionary(Type k, Type v)
		{
			typeKey = k;
			typeValue = v;
		}
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class Key : Attribute
	{
		public Type type { get; set; }
		public Key(Type t)
		{
			type = t;
		}
	}
}
