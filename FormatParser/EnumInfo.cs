using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader.FormatParser
{
	public class EnumSheet
	{
		public string sheetName { get; set; }

		public Dictionary<string, List<EnumInfo>> enumInfoDic;
	}

	public class EnumInfo
	{
		public string name;
		public string value;
	}
}
