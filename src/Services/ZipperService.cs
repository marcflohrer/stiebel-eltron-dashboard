namespace StiebelEltronDashboard.Services;
using Ionic.Zip;
using System.IO;

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

    public static string ZipFileName<T>()
    {
        return $"{typeof(T)}.zip";
    }
}

