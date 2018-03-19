using dmExcelLoader.FormatParser;
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
		public List<FormatSheet> formatSheetList = new List<FormatSheet>();

		public LoaderConfiguration Configuration { get; set; }
		public EnumParser enumParser;

		private List<Excel> excelList;

		public ExcelLoader()
		{
			
		}

		public void Load()
		{
			excelList = new List<Excel>();

			Path = Configuration.Path;

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
		}

		public void FormatParse()
		{
			var headerParser = HeaderFactory.Create(Configuration);

			enumParser = new EnumParser()
			{
				Configuration = Configuration
			};

			foreach (var excel in excelList)
			{
				foreach (var sheet in excel.sheetList)
				{
					if (Configuration.IsDescription(sheet.name))
						continue;

					if (Configuration.IsEnum(sheet.name))
						enumParser.Parse(sheet);
					else
					{
						Row[] headerRow;

						if (Configuration.Header == 1)
							headerRow = new[] { sheet.worksheet.Rows[Configuration.StartRow] };
						else
							headerRow = new[] { sheet.worksheet.Rows[Configuration.StartRow],
												sheet.worksheet.Rows[Configuration.StartRow + 1] };

						var headerTypeList = headerParser.Parse(headerRow);

						int maxHeaderReference = headerTypeList.Max(x => x.ReferenceIndex);

						List<Row> dataRow = new List<Row>();

						for (int i = Configuration.StartRow + headerRow.Length;
								 i < sheet.worksheet.Rows.Length;
								 i++)
						{
							Row row = sheet.worksheet.Rows[i];

							List<Cell> cellList = new List<Cell>();

							foreach (var c in row.FilledCells)
							{
								if (c.ReferenceIndex < Configuration.StartCol)
									continue;

								if (c.ReferenceIndex > maxHeaderReference)
									break;

								cellList.Add(c);
							}

							dataRow.Add(new Row()
							{
								r = row.r,
								FilledCells = cellList.ToArray()
							});
						}

						string[] names = sheet.name.Split('_');
						string tmpName = "";
						if (names.Length > 1)
							tmpName = names[0];
						else
							tmpName = sheet.name;

						FormatSheet formatSheet = formatSheetList.Find(x => x.SheetName == tmpName);

						if (formatSheet == null)
						{
							formatSheet = new FormatSheet();
							formatSheet.SheetName = tmpName;
							formatSheet.HeaderTypeList = headerTypeList;

							formatSheetList.Add(formatSheet);
						}

						formatSheet.rowList.AddRange(dataRow);
					}
				}
			}
		}
	}
}
