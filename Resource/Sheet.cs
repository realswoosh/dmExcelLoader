using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace dmExcelLoader.Resource
{
	public class Sheet
	{
		public string fileName;

		public string name;
		public int sheetId;
		public string rid;

		public string XmlName
		{
			get
			{
				return string.Format("sheet{0}.xml", Regex.Match(rid, @"[0-9]").Value);
			}
		}
		
		public Worksheet worksheet;
	}
}
