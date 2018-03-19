using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader.FormatParser
{
	public class FormatSheet
	{
		public string SheetName { get; set; }

		public List<HeaderType> HeaderTypeList { get; set; }
		public List<Resource.Row> rowList = new List<Resource.Row>();
	}
}
