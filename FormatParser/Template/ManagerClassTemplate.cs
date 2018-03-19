using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelFormatGenerator.FormatParser.Template
{
	public class ManagerClassTemplate : Template
	{
		public static string Base = @"using System.Collections.Generic;
using System.Reflection;

using dmExcelLoader.Resource;

namespace Az.Resource
{
	public class ResourceDatabase
	{
#membervalues

		public void Load(Dictionary<string, Sheet> sheetList)
		{
#loadfunction
		}

		T InnerLoad<T>(Row row) where T : class, new()
		{
			T t = new T();

			FieldInfo[] fieldinfos = t.GetType().GetFields();

			foreach (FieldInfo fi in fieldinfos)
			{
				Column column = row.FilledCells.Find(x => x.HeaderName == fi.Name);
				
				if (column.RealType == typeof(System.Enum))
					fi.SetValue(t, column.Value);
				else
					fi.SetValue(t, column.GetValue());
			}

			return t;
		}

		KeyValuePair<int, T> InnerLoadKeyValue<T>(Row row) where T : class, new()
		{
			T t = InnerLoad<T>(row);

			FieldInfo[] fieldInfos = t.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
			if (fieldInfos.Length == 0)
				throw new System.Exception();

			int key = -1;

			foreach (FieldInfo fi in fieldInfos)
			{
				if (fi.GetCustomAttributes(typeof(Az.Resource.Key), false).Length > 0)
				{
					key = (int)fi.GetValue(t);
					break;
				}
			}
			
			return new KeyValuePair<int, T>(key, t);
		}
#classmethodfield
	}
}";
		public void Save(List<GeneratorSheet> genSheetList)
		{
			string savePath = Configuration.PathOutputResourceClass;
			string fileName = Configuration.ResourceManagerFileName;
			string fullPath = savePath + fileName;

			string code = Base;

			Directory.CreateDirectory(savePath);

			File.WriteAllText(fullPath, code);
		}
	}
}
