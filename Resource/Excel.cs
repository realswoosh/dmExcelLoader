using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace dmExcelLoader.Resource
{
	[Serializable]
	public class Excel
	{
		public string FileName { get; set; }

		public SharedStrings sst;

		Workbook workbook = new Workbook();

		public List<Sheet> sheetList;

		public Excel()
		{

		}

		public void Load(string fileName)
		{
			FileName = fileName;

			FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

			using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read))
			{
				foreach (ZipArchiveEntry entry in archive.Entries)
				{

				}

				ZipArchiveEntry entryWorkbook = archive.Entries.FirstOrDefault(x => x.Name == "workbook.xml");
				ZipArchiveEntry entrySharedStrings = archive.Entries.FirstOrDefault(x => x.Name == "sharedStrings.xml");

				LoadSharedString(entrySharedStrings);
				LoadWorkbook(entryWorkbook);

				ZipArchiveEntry[] entrySheet =
					archive.Entries.Where(x => Regex.IsMatch(x.FullName, @"xl/worksheets/sheet", RegexOptions.IgnoreCase)).ToArray();

				LoadSheet(entrySheet);
			}
		}

		void LoadWorkbook(ZipArchiveEntry entry)
		{
			using (var stream = entry.Open())
			using (var reader = XmlReader.Create(stream))
			{
				XmlDocument doc = new XmlDocument();
				XmlNamespaceManager tmpNamespaceManager = new XmlNamespaceManager(doc.NameTable);
				tmpNamespaceManager.AddNamespace("x", LoaderNamespace.XmlNamespace);

				doc.Load(reader);

				XmlNodeList nodeList = doc.GetElementsByTagName("sheet");

				sheetList = new List<Sheet>();

				foreach (XmlNode node in nodeList)
				{
					string name = node.Attributes["name"].Value;
					int sheetId = Convert.ToInt32(node.Attributes["sheetId"].Value);
					string rid = node.Attributes["id", LoaderNamespace.XmlNamespaceR].Value;

					Sheet sheet = new Sheet()
					{
						fileName = FileName,
						name = name,
						sheetId = sheetId,
						rid = rid
					};

					sheetList.Add(sheet);
				}
			}
		}

		void LoadSharedString(ZipArchiveEntry entry)
		{
			sst = DeserializedZipEntry<SharedStrings>(entry);

			Workbook.sst = sst;
		}

		void LoadSheet(ZipArchiveEntry[] entrys)
		{
			foreach (var entry in entrys)
			{
				LoadSheet(entry);
			}
		}

		void LoadSheet(ZipArchiveEntry entry)
		{
			var sheet = sheetList.FirstOrDefault(x => x.XmlName == entry.Name);
			sheet.worksheet = DeserializedZipEntry<Worksheet>(entry);
		}

		private static T DeserializedZipEntry<T>(ZipArchiveEntry ZipArchiveEntry)
		{
			using (Stream stream = ZipArchiveEntry.Open())
				return (T)new XmlSerializer(typeof(T)).Deserialize(XmlReader.Create(stream));
		}
	}
}
