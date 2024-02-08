using System;
using System.IO;

namespace OFGmCoreCS.Util
{
    public class FileLogger
    {
        public string loggerPath;
        public string fileName;
        public string format = ".log";

        private string fileText;

        public string FileText
        {
            get { return fileText; }
            set { fileText = value; FileChange?.Invoke(value); }
        }

        public delegate void ChangeHandler(string text);
        public delegate void WrittenHandler(string text);
        public delegate void SaveHandler(FileInfo fileInfo);

        public event ChangeHandler FileChange;
        public event WrittenHandler FileWritten;
        public event SaveHandler FileSave;

        public FileLogger(string fileName, string loggerPath)
        {
            this.loggerPath = loggerPath;
            this.fileName = fileName == "" ? fileName : fileName + "-";
        }

        public FileLogger(string fileName) : this(fileName, Directory.GetCurrentDirectory())
        {

        }

        public FileLogger() : this("")
        {

        }

        public void SaveFile()
        {
            string file = Path.Combine(loggerPath, fileName + DateTime.Now.ToShortDateString() + format);

            File.WriteAllText(file, File.ReadAllText(file) + Environment.NewLine + FileText);
            
            FileSave?.Invoke(new FileInfo(file));
        }

        public void SaveLatest()
        {
            string file = Path.Combine(loggerPath, "latest" + format);

            File.WriteAllText(file, FileText);

            FileSave?.Invoke(new FileInfo(file));
        }

        public void Write(string text)
        {
            FileText += text;

            FileWritten?.Invoke(FileText);

            SaveLatest();
        }
    }
}
