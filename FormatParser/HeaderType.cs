using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader.FormatParser
{
	public class HeaderType
	{
		public string Reference { get; set; }
		public string ValueName { get; set; }
		public string ValueType { get; set; }
		public Type ValueRealType { get; set; }

		public int ReferenceIndex { get; set; }
		public bool IsKey { get; set; }
	}
}
