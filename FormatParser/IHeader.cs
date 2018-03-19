using dmExcelLoader.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader.FormatParser
{
	public interface IHeader
	{
		LoaderConfiguration Configuration { get; set; }

		List<HeaderType> Parse(Row[] rows);
	}
}
