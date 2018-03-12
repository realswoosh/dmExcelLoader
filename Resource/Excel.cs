using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace dmExcelLoader.Resource
{
	public class Excel
	{
		public string FileName;

		string[] sharedStrings;

		public WorkSheet workSheet;

		public LoaderConfiguration Configuration { get; set; }

		public Excel()
		{

		}

		public void Load(string fileName)
		{
			using (ZipArchive archive = ZipFile.OpenRead(fileName))
			{
				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					
				}

				ZipArchiveEntry entrySharedStrings = archive.Entries.FirstOrDefault(x => x.Name == "sharedStrings.xml");
				
				LoadSharedString(entrySharedStrings);
			}
		}

		void LoadSharedString(ZipArchiveEntry entry)
		{
			using (var stream = entry.Open())
			using (var reader = XmlReader.Create(stream))
			{
				XmlDocument doc = new XmlDocument();
				XmlNamespaceManager tmpNamespaceManager = new XmlNamespaceManager(doc.NameTable);
				tmpNamespaceManager.AddNamespace("x", Configuration.namespaceSharedString);

				doc.Load(reader);

				XmlNodeList nodeList = doc.SelectNodes("//x:si", tmpNamespaceManager);

				sharedStrings = new string[nodeList.Count];

				int i = 0;

				foreach (XmlNode elem in nodeList)
				{
					XmlNodeList multiLine = elem.SelectNodes("x:r/x:t", tmpNamespaceManager);

					string str = "";
					if (multiLine.Count > 0)
					{
						foreach (XmlNode tn in multiLine)
						{
							str += tn.InnerText;
						}
					}
					else
						str = elem.SelectSingleNode("x:t", tmpNamespaceManager).InnerText;

					sharedStrings[i++] = str;
				}
			}
		}
	}
}
