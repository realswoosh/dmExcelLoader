using dmExcelLoader.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader
{
	public class Loader
	{
		public LoaderConfiguration Configuration { get; set; }

		protected List<Excel> excelList = new List<Excel>();

		public string Path { get; set; }
				
	}
}
