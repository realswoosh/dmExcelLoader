using System;
using System.Linq;
using System.Xml.Serialization;

namespace dmExcelLoader.Resource
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot("sst", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class SharedStrings
	{
		[XmlElement("si")]
		public Si[] si;

		public SharedStrings()
		{
		}
	}

	public class Si
	{
		[XmlElement("t")]
		public string t;
		[XmlElement("r")]
		public Sirow[] r;

		public string Text
		{
			get
			{
				if (string.IsNullOrEmpty(t) == false)
					return t;

				return r.Select(x => x.t).ToArray()
										 .Aggregate((current, next) => current + next);
			}
		}
	}

	public class Sirow
	{
		public string t;
	}
}
