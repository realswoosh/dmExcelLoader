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
		[XmlIgnore]
		string reference;

		[XmlAttribute("r")]
		public string Reference
		{
			get { return reference; }
			set
			{
				reference = value;
				string tmp = new Regex("[0-9]+").Match(reference).Value.ToUpper();
				Row = Convert.ToInt32(tmp);
				referenceIndex = GetCellReferenceIndex(value);
			}
		}
		
		[XmlIgnore]
		public int ReferenceIndex
		{
			get { return referenceIndex; }
		}

		[XmlIgnore]
		public int Row { get; set; }
		[XmlAttribute("t")]
		public string tType = "";
		[XmlElement("v")]
		public string Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;

				if (tType.Equals("s"))
				{
					Text = Workbook.sst.si[Convert.ToInt32(this.value)].Text;
				}
				else if (tType.Equals("str"))
				{
					Text = this.value;
				}
				else
				{
					Text = Convert.ToString(value);
				}
			}
		}

		[XmlIgnore]
		string value;
		[XmlIgnore]
		public string Text { get; set; }

		[XmlIgnore]
		int referenceIndex;

		public static int GetCellReferenceIndex(string reference)
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

		public static string GetCellReference(int index)
		{
			int div = index;
			string letter = String.Empty;
			int mod = 0;

			if (div == 0)
			{
				return (char)65 + letter;
			}

			while (div > 0)
			{
				mod = div % 26;
				letter = (char)(65 + mod) + letter;
				div = (div - mod) / 26;
			}
			return letter;
		}
	}
}
