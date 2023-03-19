namespace StiebelEltronDashboard.Services;
using CsvHelper;
using Ionic.Zip;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public static class ZipperService
{
    public static MemoryStream ToCsvAndZip<T>(IEnumerable<T> data,
        MemoryStream memoryStream,
        ZipFile zipFile, string fileName)
    {
        // Convert the data to CSV format using CsvHelper
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(data);
        var csvString = writer.ToString();

        zipFile.AddEntry(fileName, csvString);
        zipFile.Save(memoryStream);
        return memoryStream;
    }

    public static string CsvFileName<T>()
    {
        return $"{typeof(T)}.csv";
    }

    public static string ZipFileName<T>()
    {
        return $"{typeof(T)}.zip";
    }
}

