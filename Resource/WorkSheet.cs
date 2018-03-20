using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dmExcelLoader.Resource
{
	[Serializable()]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot("worksheet", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class Worksheet
	{
		[Serializable]
		public class Dimension
		{
			[XmlAttribute("ref")]
			public string _ref;
		}
		
		public Dimension dimension;

		[XmlArray("sheetData")]
		[XmlArrayItem("row")]
		public Row[] Rows;
	}

	
}
