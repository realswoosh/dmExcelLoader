using dmExcelLoader.Resource;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader.FormatParser
{
	public class HeaderParserMulti : IHeader
	{
		public LoaderConfiguration Configuration { get; set; }

		public List<HeaderType> Parse(Row[] rows)
		{
			if (rows.Length != 2)
				throw new Exception("Rows Length Invalid");

			if (rows[0].FilledCells.Length != 
				rows[1].FilledCells.Length)
				throw new Exception("FielledCell Length Different");

			List<HeaderType> headerTypeList = new List<HeaderType>();

			Row row = rows[0];
			Row rowType = rows[1];

			for (int i = 0; i < row.FilledCells.Length; i++)
			{
				Cell cell = row.FilledCells[i];
				Cell cellType = rowType.FilledCells[i];

				if (cell.ReferenceIndex < Configuration.StartCol)
					continue;

				if (cell.Text.Length == 0 || Configuration.PrefixIgnoreColumn.Any(x => x == cell.Text[0]))
					continue;

				string[] typeStr = cellType.Text.Split(':');

				var isKey = false;
				if (typeStr.Length == 2 &&
					string.Equals(typeStr[1], "key", StringComparison.OrdinalIgnoreCase))
					isKey = true;

				HeaderType headerType = new HeaderType()
				{
					Reference = cell.Reference,
					ValueName = cell.Text,
					ValueType = typeStr[0],
					ValueRealType = TypeInfos.ConvertRealType(typeStr[0]),
					IsKey = isKey
				};

				headerTypeList.Add(headerType);
			}

			return headerTypeList;

		}
	}
}
