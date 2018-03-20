using dmExcelLoader.Resource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace dmExcelLoader
{
	public class BinaryLoader : Loader, ILoader
	{
		public void Load()
		{
			string[] files = Directory.GetFiles(Configuration.PathBinary, "*.dat");

			foreach (string file in files)
			{
				BinaryFormatter formmater = new BinaryFormatter();
	
				Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				
				Excel excel = (Excel)formmater.Deserialize(stream);

				excelList.Add(excel);
			}
		}
	}
}
