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
		public List<Excel> excelList = new List<Excel>();

		private IHeader headerParser;

		public ExcelLoader()
		{
			
		}

		public void Load(string path)
		{
			Path = path;

			headerParser = HeaderParser.Create(Configuration);

			string[] files = Directory.GetFiles(Path, "*.xlsx");
			foreach (var file in files)
			{
				Excel excel = new Excel();
				excel.Configuration = Configuration;
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
