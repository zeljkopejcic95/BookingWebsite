﻿namespace Interfaces;

public interface ILoggerManager
{
    void LogInfo(string message);
    void LogWarn(string message);
    void LogError(string message);
}
