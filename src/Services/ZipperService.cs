namespace StiebelEltronDashboard.Services;
using CsvHelper;

using CsvHelper.Configuration;
using Ionic.Zip;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public static class ZipperService
{
    public static MemoryStream Zip(string csvString,
        MemoryStream memoryStream,
        ZipFile zipFile, string fileName)
    {
        zipFile.AddEntry(fileName, csvString);
        zipFile.Save(memoryStream);
        return memoryStream;
    }

    public static List<T> ReadDataFromZip<T, TMap>(ZipFile zipFile) where TMap : ClassMap<T>
    {
        var myTableData = new List<T>();

        var entry = zipFile.Entries.First();
        using (var stream = entry.OpenReader())
        using (var reader = new StreamReader(stream))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<TMap>();
            myTableData = csv.GetRecords<T>().ToList();
        }

        return myTableData;
    }

    public static string ZipFileName<T>()
    {
        return $"{typeof(T)}.zip";
    }
}

