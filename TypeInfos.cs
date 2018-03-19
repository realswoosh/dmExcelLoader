using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader
{
	public class TypeInfos
	{
		public static Type ConvertRealType(string t)
		{
			t = t.ToLower();

			if (t == "s" || t == "string")
				return typeof(string);
			else if (t == "i" || t == "int" || t == "key")
				return typeof(int);
			else if (t == "b" || t == "bool")
				return typeof(bool);
			else if (t == "strings")
				return typeof(string[]);
			else if (t == "double")
				return typeof(double);
			else if (t == "doubles" || t == "doublearray")
				return typeof(double[]);
			else if (t == "ints" || t == "intarray")
				return typeof(int[]);
			else if (t == "float")
				return typeof(float);
			else if (t == "floats" || t == "floatarray")
				return typeof(float[]);

			return typeof(Enum);
		}

		public static bool IsArray(Type type)
		{
			if (type == typeof(int[]) ||
				type == typeof(float[]) ||
				type == typeof(string[]) ||
				type == typeof(double[]))
				return true;

			return false;
		}

		public static object GetDefaultValue(Type type)
		{
			if (type == typeof(Enum))
				return 0;
			else if (type == typeof(string))
				return "";
			else if (type == typeof(int[]))
				return new int[0];
			else if (type == typeof(float[]))
				return new float[0];
			else if (type == typeof(double[]))
				return new double[0];

			return 0;
		}
		
	}
}
