using dmExcelLoader;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader.FormatParser
{
	public class HeaderFactory
	{
		public static IHeader Create(LoaderConfiguration configuration)
		{
			if (configuration.Header > 2)
				return new HeaderParserMulti()
				{
					Configuration = configuration
				};
			else
				return new HeaderParserSingle()
				{
					Configuration = configuration
				};

			throw new Exception("eee");
		}
	}
}
