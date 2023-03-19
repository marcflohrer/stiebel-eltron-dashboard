namespace StiebelEltronDashboard.Services;
using CsvHelper;
using Ionic.Zip;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public static class CsvService
{
    public static string ToCsvString<T>(IEnumerable<T> data)
    {
        // Convert the data to CSV format using CsvHelper
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(data);
        var csvString = writer.ToString();
        return csvString;
    }

    public static string CsvFileName<T>()
    {
        return $"{typeof(T)}.csv";
    }
}

