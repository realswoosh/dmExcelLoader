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
		 +----------------+------
		 + NAME           |...
		 +----------------+------
		 + type:moreinfo| |
		 +----------------+
		*/

		public Int32 Header { get; set; } = 1;

		/// <summary>
		/// Path : Excel File Path
		/// </summary>
		public string Path { get; set; }

		public char[] PrefixIgnoreColumn { get; set; } = new[] { '#' };
		public string PrefixDataClass { get; set; } = "Data_";

		public string[] DescriptionSheetName { get; set; } = new[] { "Description" };
		public string[] EnumSheetName { get; set; } = new[] { "Enum" };

		// Must Be Fill Value
		// 
		public string EnumCellPrefix = "enum";

		public bool IsEnumCombine { get; set; } = true;

		public string NamespaceDataClass { get; set; } = "dmGameData.Resource";
		
		public string NamespaceResourceManager { get; set; } = "dmGameData.ResourceManager";

		public string PathClass { get; set; } = "OutputResource";
		public string PathClassResourceManager { get; set; } = "OutputResourceManager";

		public string AddProjectPath { get; set; } = "../../../dmExcelLoader/";
		public string AddProjectName { get; set; } = "dmExcelLoader.csproj";

		public string ResourceManagerFileName = "ResourceDatabase.cs";

		public bool IsAutoAdd = true;

		public bool IsSheetGroup { get; set; } = true;

		public static LoaderConfiguration Defaultconfiguration => new LoaderConfiguration() { StartRow = 0, StartCol = 0 };

		public bool IsDescription(string sheetName)
		{
			if (DescriptionSheetName.Any(x => sheetName.Contains(x)))
				return true;

			return false;
		}

		public bool IsEnum(string sheetName)
		{
			if (EnumSheetName.Any(x => sheetName.Contains(x)))
				return true;

			return false;
		}
	}
}
