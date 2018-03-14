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

		public ExcelLoader()
		{
			
		}

		public void Load(string path)
		{
			Path = path;

			try
			{
				string[] files = Directory.GetFiles(Path, "*.xlsx");

				files = files.Where(x => x.IndexOf('~') < 0).ToArray();
				
				foreach (var file in files)
				{
					Excel excel = new Excel();
					excel.Load(file);

					excelList.Add(excel);
				}

			}
			catch (DirectoryNotFoundException)
			{
				// Error
				//
				return;
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
