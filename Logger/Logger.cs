using System;

namespace OFGmCoreCS.Logger
{
    /// <summary>
    /// Класс <see cref="Logger"/> предоставляет простой логгер.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Текст лога.
        /// </summary>
        public string logText = "";

        /// <summary>
        /// Делегат обработчика лога.
        /// </summary>
        public delegate void LogHandler(string message);

        /// <summary>
        /// Событие изменения лога.
        /// </summary>
        public event LogHandler LogChange;

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
        public Logger(LoggerProperties loggerProperties)
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
                Console.WriteLine(message);

            if ((level == LoggerLevel.Debug && debug) || level != LoggerLevel.Debug)
                logText += "\n" + message;

            LogChange?.Invoke(message);
            return message;
        }

        /// <summary>
        /// Вложенный класс для настройки логгера.
        /// </summary>
        public class LoggerProperties
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
            /// <returns>Свой экземпляр <see cref="LoggerProperties"/>.</returns>
            public LoggerProperties ConsoleOutput(bool consoleOutput)
            {
                this.consoleOutput = consoleOutput;
                return this;
            }

            /// <summary>
            /// Включает отладочный режим.
            /// </summary>
            /// <returns>Свой экземпляр <see cref="LoggerProperties"/>.</returns>
            public LoggerProperties Debug()
            {
                debug = true;
                return this;
            }

            /// <summary>
            /// Устанавливает префикс для каждой записи лога.
            /// </summary>
            /// <param name="text">Текст префикса.</param>
            /// <returns>Свой экземпляр <see cref="LoggerProperties"/>.</returns>
            public LoggerProperties Prefix(string text)
            {
                prefix = text;
                return this;
            }

            /// <summary>
            /// Устанавливает суффикс для каждой записи лога.
            /// </summary>
            /// <param name="text">Текст суффикса.</param>
            /// <returns>Свой экземпляр <see cref="LoggerProperties"/>.</returns>
            public LoggerProperties Suffix(string text)
            {
                suffix = text;
                return this;
            }
        }
    }
}
