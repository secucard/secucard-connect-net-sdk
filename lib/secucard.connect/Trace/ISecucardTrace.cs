namespace Secucard.Connect.Trace
{
    using System;

    public interface ISecucardTrace
    {
        void Error(Exception e);
        void Info(string fmt, params object[] param);
        void Error(string fmt, params object[] param);
    }
}