using System;

namespace IfiNavetNotifier.Logger
{
    public interface ILogger
    {
        void Informtion(string info);

        void Warning(string warning);

        void Debug(string info);

        void Exception(string info, Exception ex);
    }
}