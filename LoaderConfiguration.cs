using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader
{
	public class LoaderConfiguration
	{
		public Int32 StartRow { get; set; }
		public Int32 StartCol { get; set; }

		/*
		 Header = 1
		 +--------------------+------
		 + NAME:type:moreinfo |...
		 +--------------------+------

		 Header = 2
		 +--------------+------
		 + NAME         |...
		 +--------------+------
		 + type:moreinfo
		*/


		public Int32 Header { get; set; }

		/// <summary>
		/// Path : Excel File Path
		/// </summary>
		public string Path { get; set; }

		public Int32 HeaderRowCount { get; set; }

		public char PrefixIgnoreColumn { get; set; } = '#';
				
		public string[] DescriptionSheetName { get; set; } = new[] { "Description" };
		public string[] EnumSheetName { get; set; } = new[] { "Enum" };
		
		public static LoaderConfiguration Defaultconfiguration => new LoaderConfiguration() { StartRow = 0, StartCol = 1 };
	}
}
