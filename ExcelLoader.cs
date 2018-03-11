using dmExcelLoader.Resource;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader
{
	public class ExcelLoader : Loader, ILoader
	{
		public static LoaderConfiguration Configuration { get; set; }
		
		public List<Excel> excelList = new List<Excel>();

		private readonly IHeader headerParser;

		public ExcelLoader()
		{
			if (Configuration.HeaderRowCount > 2)
				headerParser = new HeaderParserMulti();
			else
				headerParser = new HeaderParserSingle();
		}

		public void Load(string path)
		{
			Path = path;
			
			string[] files = Directory.GetFiles(Path, "*.xlsx");
			foreach (var file in files)
			{
				Excel excel = new Excel();
				excel.Load(file);
			}
			
			LoadEnum();
			LoadSheet();
		}

		void LoadEnum()
		{

		}

		void LoadSheet()
		{

		}
	}
}
