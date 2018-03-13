using dmExcelLoader.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader
{
	public class HeaderParser
	{
		public static IHeader Create(LoaderConfiguration configuration)
		{
			if (configuration.HeaderRowCount > 2)
				return new HeaderParserMulti();
			else
				return new HeaderParserSingle();

			throw new Exception("eee");
		}
	}
}
