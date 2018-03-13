using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dmExcelLoader.Resource
{
	public class Cell
	{
		[XmlAttribute("r")]
		public string CellRefernce
		{
			get
			{
				return index.ToString();
			}
			set
			{
				index = GetColumnIndex(value);
			}
		}

		[XmlAttribute("t")]
		public string tType = "";
		[XmlAttribute("v")]
		public string Value
		{
			get
			{
				return realValue;
			}
			set
			{
				realValue = value;

				if (tType.Equals("s"))
				{
					objValue = Workbook.sst.si[Convert.ToInt32(realValue)].Text;
					realType = typeof(string);
				}
				else if (tType.Equals("str"))
				{
					objValue = realValue;
					realType = typeof(string);
				}
				else
				{
					objValue = value;
				}
			}
		}

		[XmlIgnore]
		public object objValue;

		private string realValue;
		private Type realType;

		[XmlIgnore]
		public int index;

		private int GetColumnIndex(string reference)
		{
			string letter = new Regex("[A-Za-z]+").Match(reference).Value.ToUpper();

			int tmpIndex = 0;

			for (int i = 0; i < letter.Length; i++)
			{
				tmpIndex *= 26;
				tmpIndex += (letter[i] - 'A' + 1);
			}
			return tmpIndex - 1;
		}

	}
}
