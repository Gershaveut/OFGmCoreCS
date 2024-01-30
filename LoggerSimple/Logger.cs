using System;

namespace OFGmCoreCS.LoggerSimple
{
    /// <summary>
    /// Класс <see cref="Logger"/> предоставляет простой логгер.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Текст лога.
        /// </summary>
        private string logText = "";

        public string LogText
        {
            get { return logText; }
            set { logText = value; LogChange?.Invoke(value); }
        }

        /// <summary>
        /// Делегат обработчика лога.
        /// </summary>
        public delegate void LogChangeHandler(string message);

        public delegate void LogWrittenHandler(string message, LoggerLevel level);

        /// <summary>
        /// Событие изменения лога.
        /// </summary>
        public event LogChangeHandler LogChange;

        public event LogWrittenHandler LogWritten;

        /// <summary>
        /// Вывод в консоль.
        /// </summary>
        public bool consoleOutput;

        /// <summary>
        /// Вывод сообщений с уровнем <see cref="LoggerLevel.Debug"/> в лог.
        /// </summary>
        public bool debug;

        /// <summary>
        /// Префикс, добавляемый в начало записи лога.
        /// </summary>
        public string prefix;

        /// <summary>
        /// Суффикс, добавляемый в конец записи лога.
        /// </summary>
        public string suffix;

        /// <summary>
        /// Создаёт логгер с переданными параметрами.
        /// </summary>
        /// <param name="loggerProperties">Настройки логгера.</param>
        public Logger(Properties loggerProperties)
        {
            consoleOutput = loggerProperties.consoleOutput;
            debug = loggerProperties.debug;
            prefix = loggerProperties.prefix;
            suffix = loggerProperties.suffix;
        }

        /// <summary>
        /// Записывает сообщение в лог с указанным уровнем логирования.
        /// </summary>
        /// <param name="message">Сообщение для записи в лог.</param>
        /// <param name="level">Уровень логирования.</param>
        /// <returns>Записанное сообщение.</returns>
        public string LogWrite(string message, LoggerLevel level)
        {
            message = prefix + $"[{DateTime.Now.ToLongTimeString()}] [{level.ToString().ToUpperInvariant()}] {message}" + suffix;
            
            if (consoleOutput)
                System.Console.WriteLine(message);

            if (logText != "")
                message = "\n" + message;

            if ((level == LoggerLevel.Debug && debug) || level != LoggerLevel.Debug)
                logText += message;

            LogWritten.Invoke(message, level);
            return message;
        }
        
        /// <summary>
        /// Вложенный класс для настройки логгера.
        /// </summary>
        public class Properties
        {
            /// <summary>
            /// Вывод в консоль.
            /// </summary>
            public bool consoleOutput = true;

            /// <summary>
            /// Вывод сообщений с уровнем <see cref="LoggerLevel.Debug"/> в лог.
            /// </summary>
            public bool debug;

            /// <summary>
            /// Префикс, добавляемый в начало записи лога.
            /// </summary>
            public string prefix;

            /// <summary>
            /// Суффикс, добавляемый в конец записи лога.
            /// </summary>
            public string suffix;

            /// <summary>
            /// Значение <see cref="consoleOutput"/>.
            /// </summary>
            /// <param name="consoleOutput">Вывод в консоль.</param>
            /// <returns>Свой экземпляр <see cref="Properties"/>.</returns>
            public Properties ConsoleOutput(bool consoleOutput)
            {
                this.consoleOutput = consoleOutput;
                return this;
            }

            /// <summary>
            /// Включает отладочный режим.
            /// </summary>
            /// <returns>Свой экземпляр <see cref="Properties"/>.</returns>
            public Properties Debug()
            {
                debug = true;
                return this;
            }

            /// <summary>
            /// Устанавливает префикс для каждой записи лога.
            /// </summary>
            /// <param name="text">Текст префикса.</param>
            /// <returns>Свой экземпляр <see cref="Properties"/>.</returns>
            public Properties Prefix(string text)
            {
                prefix = text;
                return this;
            }

            /// <summary>
            /// Устанавливает суффикс для каждой записи лога.
            /// </summary>
            /// <param name="text">Текст суффикса.</param>
            /// <returns>Свой экземпляр <see cref="Properties"/>.</returns>
            public Properties Suffix(string text)
            {
                suffix = text;
                return this;
            }
        }
    }
}
