using OFGmCoreCS.Util;
using System;
using System.Diagnostics;

namespace OFGmCoreCS.LoggerSimple
{
    public class Logger
    {
        private string text = "";

        public string Text
        {
            get { return text; }
            set { text = value; LogChange?.Invoke(value); }
        }

        public delegate void ChangeHandler(string message);
        public delegate void WrittenHandler(string message, LoggerLevel level);

        public event ChangeHandler LogChange;
        public event WrittenHandler LogWritten;

        public FileLogger fileLogger;

        public bool consoleOutput;
        public bool messageMod;
        public bool debug;

        public string prefix;
        public string suffix;

        public Logger(Properties loggerProperties)
        {
            consoleOutput = loggerProperties.consoleOutput;
            debug = loggerProperties.debug;
            prefix = loggerProperties.prefix;
            suffix = loggerProperties.suffix;
            messageMod = loggerProperties.messageMod;

            LogWritten += SaveLatest;

            Write(Utils.OFGmCoreCS, LoggerLevel.Debug);
        }

        public Logger(Properties loggerProperties, FileLogger fileLogger) : this(loggerProperties)
        {
            this.fileLogger = fileLogger;
        }

        public string Write(string message, LoggerLevel level)
        {
            if (messageMod)
                message = prefix + $"[{DateTime.Now.ToLongTimeString()}] [{level.ToString().ToUpperInvariant()}] {message}" + suffix;
            
            if (Text != "")
                message = Environment.NewLine + message;

            if ((level == LoggerLevel.Debug && debug) || level != LoggerLevel.Debug)
            {
                if (consoleOutput)
                {
                    Console.WriteLine(message);
                    Debug.WriteLine(message);
                }

                Text += message;
                LogWritten?.Invoke(message, level);
            }
            
            return message;
        }

        public void SaveLatest(string text, LoggerLevel level)
        {
            if (fileLogger != null)
            {
                fileLogger.Write(text);
            }
        }

        public class Properties
        {
            internal bool consoleOutput = true;
            internal bool messageMod = true;
            internal bool debug;

            internal string prefix;
            internal string suffix;

            public Properties ConsoleOutput(bool consoleOutput)
            {
                this.consoleOutput = consoleOutput;
                return this;
            }

            public Properties MessageMod(bool messageMod)
            {
                this.messageMod = messageMod;
                return this;
            }

            public Properties Debug()
            {
                debug = true;
                return this;
            }

            public Properties Prefix(string text)
            {
                prefix = text;
                return this;
            }

            public Properties Suffix(string text)
            {
                suffix = text;
                return this;
            }
        }
    }
}
