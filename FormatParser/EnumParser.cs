using dmExcelLoader.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dmExcelLoader.FormatParser
{
	public class EnumParser
	{
		public LoaderConfiguration Configuration { get; set; }

		public Dictionary<string, EnumSheet> enumSheetDic = new Dictionary<string, EnumSheet>();

		public string GetEnumName(string text)
		{
			string[] splitHeader = text.Split(':');

			if (splitHeader.Length != 2)
				return "";

			return splitHeader[1];
		}

		public bool IsEnumFormat(string text)
		{
			string[] splitHeader = text.Split(':');
			if (splitHeader.Length < 2)
				return false;

			if (splitHeader[0] != Configuration.EnumCellPrefix)
				return false;

			return true;
		}

		public void Parse(Sheet sheet)
		{
			var enumPrefix = Configuration.EnumCellPrefix + ":";
			var enumHeader = sheet.worksheet.Rows.SelectMany(x => x.FilledCells)
											 	 .Where(x => x.Text != null &&
															 x.Text.Contains(enumPrefix));

			Dictionary<string, List<EnumInfo>> enumInfoDic = new Dictionary<string, List<EnumInfo>>();

			foreach (var header in enumHeader)
			{
				if (IsEnumFormat(header.Text) == false)
					continue;

				List<EnumInfo> enumInfoList = new List<EnumInfo>();

				Cell currentCell = header;
	
				while (true)
				{
					string tmpCurrentReference = Cell.GetCellReference(currentCell.ReferenceIndex);
					string nextRow = $"{tmpCurrentReference}{currentCell.Row + 1}";

					var cell = sheet.worksheet.Rows.SelectMany(x => x.FilledCells)
												   .FirstOrDefault(x => x.Reference == nextRow);

					if (cell != null &&
						cell.Text != null &&
						cell.Text != "" && IsEnumFormat(cell.Text) == false)
					{
						string tmpReference = Cell.GetCellReference(cell.ReferenceIndex + 1);
						string value = $"{tmpReference}{cell.Row}";
						var cellValue = sheet.worksheet.Rows.SelectMany(x => x.FilledCells)
															.FirstOrDefault(x => x.Reference == value);

						EnumInfo ei = new EnumInfo()
						{
							name = cell.Text,
							value = cellValue.Text
						};

						enumInfoList.Add(ei);
					}
					else
						break;

					currentCell = cell;
				}

				string enumName = GetEnumName(header.Text);

				enumInfoDic.Add(enumName, enumInfoList);
			}

			enumSheetDic.Add(sheet.name, new EnumSheet() {
					sheetName   = sheet.name,
					enumInfoDic = enumInfoDic
			});
		}
	}
}
