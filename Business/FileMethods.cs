using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NichiconJP_FCT_Support_WIP.Entity;

namespace NichiconJP_FCT_Support_WIP.Business
{
    public static class FileMethods
    {

        public static Encoding GetEncoding(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw Error.Argument("invalid file path!");
            }
            return FileMethods.GetEncodingCore(filePath);
        }

        private static Encoding GetEncodingCore(string filePath)
        {
            Encoding @default = Encoding.Default;
            byte[] numArray = new byte[5];
            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fileStream.Read(numArray, 0, 5);
                fileStream.Close();
            }
            if (numArray[0] == 239 && numArray[1] == 187 && numArray[2] == 191)
            {
                @default = Encoding.UTF8;
            }
            else if (numArray[0] == 254 && numArray[1] == 255)
            {
                @default = Encoding.Unicode;
            }
            else if (numArray[0] == 0 && numArray[1] == 0 && numArray[2] == 254 && numArray[3] == 255)
            {
                @default = Encoding.UTF32;
            }
            else if (numArray[0] == 43 && numArray[1] == 47 && numArray[2] == 118)
            {
                @default = Encoding.UTF7;
            }
            return @default;
        }

        private static void ParseTextRow(string line, char delimiter, ICollection<string[]> collection)
        {
            List<string> strs = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                char chr = line[i];
                stringBuilder.Append(chr);
                if (chr == delimiter)
                {
                    string str = stringBuilder.ToString();
                    if (!str.StartsWith("\"") || str.EndsWith("\""))
                    {
                        string str1 = str.TrimStart(new char[] { '\"' });
                        char[] chrArray = new char[] { '\"' };
                        strs.Add(str1.TrimEnd(chrArray));
                    }
                }
            }
            collection.Add(strs.ToArray());
        }

        public static IEnumerable<string[]> ParseTextRows(string filePath, char delimiter)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw Error.Argument("invalid file path!");
            }
            if (delimiter == 0)
            {
                throw Error.Argument("invalid delimiter!");
            }
            List<string[]> strArrays = new List<string[]>();
            string[] strArrays1 = FileMethods.ReadTextLines(filePath);
            for (int i = 0; i < (int)strArrays1.Length; i++)
            {
                FileMethods.ParseTextRow(strArrays1[i], delimiter, strArrays);
            }
            return strArrays;
        }

        //public static byte[] ReadBinary(string filePath)
        //{
        //    byte[] numArray;
        //    if (string.IsNullOrEmpty(filePath))
        //    {
        //        throw Error.Argument("invalid file path!");
        //    }
        //    using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        //    {
        //        byte[] numArray1 = new byte[checked((IntPtr)fileStream.Length)];
        //        fileStream.Read(numArray1, 0, (int)fileStream.Length);
        //        fileStream.Close();
        //        numArray = numArray1;
        //    }
        //    return numArray;
        //}

        public static string ReadText(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw Error.Argument("invalid file path!");
            }
            return FileMethods.ReadText(filePath, FileMethods.GetEncodingCore(filePath));
        }

        public static string ReadText(string filePath, Encoding encoding)
        {
            string str;
            if (string.IsNullOrEmpty(filePath))
            {
                throw Error.Argument("invalid file path!");
            }
            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, encoding))
                {
                    string end = streamReader.ReadToEnd();
                    streamReader.Close();
                    str = end;
                }
            }
            return str;
        }

        public static string[] ReadTextLines(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw Error.Argument("invalid file path!");
            }
            return FileMethods.ReadTextLines(filePath, FileMethods.GetEncodingCore(filePath));
        }

        public static string[] ReadTextLines(string filePath, Encoding encoding)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw Error.Argument("invalid file path!");
            }
            List<string> strs = new List<string>();
            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, encoding))
                {
                    while (!streamReader.EndOfStream)
                    {
                        strs.Add(streamReader.ReadLine());
                    }
                    streamReader.Close();
                }
            }
            return strs.ToArray();
        }

        public static void SerializeXml(string filePath, object obj)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw Error.Argument("invalid file path!");
            }
            if (obj == null)
            {
                throw Error.ArgumentNull("obj");
            }
            using (FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                (new XmlSerializer(obj.GetType())).Serialize(fileStream, obj);
                fileStream.Flush();
                fileStream.Close();
            }
        }

        public static void WriteBinary(string filePath, byte[] binary)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw Error.Argument("invalid file path!");
            }
            if (binary == null)
            {
                throw Error.ArgumentNull("binary");
            }
            using (FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(binary);
                    binaryWriter.Flush();
                    binaryWriter.Close();
                }
            }
        }

        public static void WriteText(string filePath, string text, Encoding encoding)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw Error.Argument("invalid file path!");
            }
            if (string.IsNullOrEmpty(text))
            {
                throw Error.Argument("invalid text!");
            }
            if (encoding == null)
            {
                throw Error.ArgumentNull("encoding");
            }
            using (FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, encoding))
                {
                    streamWriter.Write(text);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                fileStream.Close();
            }
        }

        public static void CreateFileLog(string boardNo, string boardState)
        {
            string strDateTime = SingletonHelper.PVSInstance.GetDateTime().ToString("yyMMddHHmmss");
            string fileName = $"{strDateTime}_{boardNo}.txt";
            string path = Path.Combine(SystemSetting.outputLog, fileName);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            using (TextWriter tw = new StreamWriter(path))
            {
                string value = string.Format("{0}|{1}|{2}|{3}|{4}",
                    "IT-Tool",
                    boardNo,
                    strDateTime,
                    boardState,
                    Entity.SystemSetting.stationNo
                    );
                tw.WriteLine(value);
                tw.Close();
            }
        }
    }
}
