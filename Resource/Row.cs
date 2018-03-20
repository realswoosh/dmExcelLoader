using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dmExcelLoader.Resource
{
	[Serializable]
	public class Row
	{
		[XmlAttribute("r")]
		public int r;

		[XmlElement("c")]
		public Cell[] FilledCells;
	}
}