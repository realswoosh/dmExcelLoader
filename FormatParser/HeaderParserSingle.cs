using dmExcelLoader.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader.FormatParser
{
	public class HeaderParserSingle : IHeader
	{
		public LoaderConfiguration Configuration { get; set; }

		public List<HeaderType> Parse(Row[] rows)
		{
			///  Single Header Format
			///  ex : id:int[:key]
			///  ex : damageFactor:float
			///  ex : name:string

			if (rows.Length != 1)
				throw new Exception("Rows Length Invalid");

			Row row = rows[0];

			List<HeaderType> headerTypeList = new List<HeaderType>();

			foreach (var cell in row.FilledCells)
			{
				if (cell.ReferenceIndex < Configuration.StartCol)
					continue;

				if (cell.Text.Length == 0 || Configuration.PrefixIgnoreColumn.Any(x => x == cell.Text[0]))
					continue;

				string[] typeStr = cell.Text.Split(':');

				var isKey = false;
				if (typeStr.Length == 3 &&
					string.Equals(typeStr[2], "key", StringComparison.OrdinalIgnoreCase))
					isKey = true;

				HeaderType headerType = new HeaderType()
				{
					Reference = cell.Reference,
					ReferenceIndex = cell.ReferenceIndex,
					ValueName = typeStr[0],
					ValueType = typeStr[1],
					ValueRealType = TypeInfos.ConvertRealType(typeStr[1]),
					IsKey = isKey
				};

				headerTypeList.Add(headerType);
			}

			return headerTypeList;
		}
	}
}
