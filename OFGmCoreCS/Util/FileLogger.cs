using System;
using System.IO;
using System.Threading;

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

            Directory.CreateDirectory(loggerPath);
        }

        public FileLogger(string fileName) : this(fileName, Path.Combine(Directory.GetCurrentDirectory(), "log"))
        {

        }

        public FileLogger() : this("")
        {

        }

        public void SaveFile(string text, bool addText)
        {
            FileText = text;
            string file = Path.Combine(loggerPath, fileName + DateTime.Now.ToShortDateString() + format);

            try
            {
                File.WriteAllText(file, (File.Exists(file) && addText ? File.ReadAllText(file) + Environment.NewLine : "") + text);
            }
            catch 
            {
                Thread.Sleep(100);
                SaveFile(text, addText);
            }

            FileSave?.Invoke(new FileInfo(file));
        }

        public void SaveFile(bool addText)
        {
            SaveFile(FileText, addText);
        }

        public void SaveFile()
        {
            SaveFile(FileText, true);
        }

        public void SaveLatest(string text)
        {
            FileText = text;
            string file = Path.Combine(loggerPath, "latest" + format);

            try
            {
                File.WriteAllText(file, text);
            }
            catch
            {
                Thread.Sleep(100);
                SaveLatest(text);
            }

            FileSave?.Invoke(new FileInfo(file));
        }

        public void SaveLatest()
        {
            SaveLatest(FileText);
        }

        public void Write(string text)
        {
            FileText += text;

            FileWritten?.Invoke(FileText);

            SaveLatest();
        }

        public void WriteLine(string text)
        {
            Write(Environment.NewLine + text);
        }
    }
}
