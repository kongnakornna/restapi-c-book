using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Helpers
{
    public class CsvOptions
    {
        public string LineSeperator { get; set; }
        public string ColumnSeperator { get; set; }
        public bool FirstRowIsColumm { get; set; }
        public CsvOptions()
        {
            this.LineSeperator = "\n";
            this.ColumnSeperator = ",";
            this.FirstRowIsColumm = true;
        }
    }

    public static class CSV
    {
        public static IEnumerable<T> ToObject<T>(StreamReader reader, Func<string[], T> Map, CsvOptions options)
        {
            var csvData = reader.ReadToEnd();
            return ToObject(csvData, Map, options);
        }

        public static IEnumerable<T> ToObject<T>(string CsvData, Func<string[], T> Map, CsvOptions? options)
        {
            if (CsvData == "")
                throw new Exception("CSV Text Not Found");

            var setting = options == null ? new CsvOptions() : options;

            string[] lines = CsvData.Split(setting.LineSeperator);

            //=== Populate Data
            int startIndex = (setting.FirstRowIsColumm ? 1 : 0);
            var ret = new List<T>();

            for (int i = startIndex; i < lines.Length - 1 - startIndex; i++)
                if (!string.IsNullOrEmpty(lines[i]))
                    ret.Add(Map(lines[i].Split(setting.ColumnSeperator)));
            return ret;
        }

        public static string ToString<T>(IEnumerable<T> Data, StreamWriter output, Func<T, string> Export, CsvOptions? options , Func<CsvOptions, string> ?WriteHeader)
        {
            using (var mem = new MemoryStream())
            {
                var writer = new StreamWriter(mem);
                WriteCsv<T>(Data, writer, Export, options, WriteHeader);
                writer.Flush();
                var reader = new StreamReader(mem);
                var csvData = reader.ReadToEnd();
                return csvData;
            }
        }

        public static string ToString(DataTable dataTable, string lineSep = "\n", string columnSep = ",", bool firstRowIsColumnName = true)
        {
            string csv = "";
            if (firstRowIsColumnName)
            {
                foreach (DataColumn dcol in dataTable.Columns)
                {
                    if (csv != "")
                        csv += columnSep;
                    csv += "\"" + dcol.ColumnName.ToString() + "\"";
                }
            }

            csv += lineSep;
            foreach (DataRow dr in dataTable.Rows)
            {
                string data = "";
                foreach (DataColumn dcol in dataTable.Columns)
                {
                    if (data != "")
                        data += columnSep;
                    data += "\"" + dr[dcol].ToString().Replace("\"", "'") + "\"";
                }
                csv += data + lineSep;
            }
            return csv;
        }

        public static void WriteCsv<T>(IEnumerable<T> Data, string fileName, Func<T, string> Export, CsvOptions? options, Func<CsvOptions, string>? WriteHeader)
        {
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                WriteCsv(Data, outputFile, Export, options, WriteHeader);
            }
        }

        public static void WriteCsv<T>(IEnumerable<T> Data, StreamWriter output, Func<T, string> Export, CsvOptions? options, Func<CsvOptions, string>? WriteHeader )
        {
            var setting = options == null ? new CsvOptions() : options;

            if (WriteHeader != null)
                output.WriteLine(WriteHeader(setting));

            foreach (var item in Data)
                output.WriteLine(Export(item));
        }
    }
}
