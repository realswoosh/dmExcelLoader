using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelFormatGenerator.FormatParser.Template
{
	public class ClassTemplate : Template
	{
		public static string Base = @"namespace #namespace
{
	public class #classname
	{
#classmemberfield
	}
}";
		public List<HeaderType> HeaderTypeList { get; set; }

		public void Save()
		{
			string className = Configuration.PrefixDataClass + SheetName;

			string savePath = Configuration.PathOutputClass;
			string fileName = className + ".cs";
			string fullPath = savePath + fileName;

			Directory.CreateDirectory(savePath);

			string code = Base;
			code = code.Replace("#namespace", Configuration.NamespaceDataClass);
			code = code.Replace("#classname", className);

			code = MemberField(code);

			File.WriteAllText(fullPath, code);
		}

		string MemberField(string code)
		{
			List<string> memberField = new List<string>();

			foreach (var headerType in HeaderTypeList)
			{
				string valueType = "";
				string valueName = headerType.ValueName;

				if (headerType.ValueRealType == typeof(Enum))
					valueType = $"{Configuration.NamespaceDataClass}.{headerType.ValueType}";
				else
					valueType = headerType.ValueRealType.ToString();
					
				string member = $"\t\t{valueType} {valueName};";

				memberField.Add(member);
			}

			string tmpFields = string.Join("\r\n", memberField);
			code = code.Replace("#classmemberfield", tmpFields);
			return code;
		}
	}
}
