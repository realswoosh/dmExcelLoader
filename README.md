# dmExcelLoader
dmExcelLoader is simple project for excel contents.

### Installing

Clone project or install nuget package https://www.nuget.org/packages/dmExcelLoader/


### Running
```
string currentPath = Directory.GetCurrentDirectory();
			
var config = LoaderConfiguration.Defaultconfiguration;

config.Path = $"{currentPath}/../../ExcelData";
						
ExcelLoader excelLoader = new ExcelLoader();
excelLoader.Configuration = config;

// excel load
excelLoader.Load();  

// formatted using 
excelLoader.FormatParse();
// transform binary data
excelLoader.Transform();
```

more information.

download https://github.com/realswoosh/dmContentsBuilder


