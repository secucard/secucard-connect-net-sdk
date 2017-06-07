namespace Secucard.Connect.Client
{
    using System;
    using System.Diagnostics;

    public static class SecucardTrace
    {
        internal static void EmptyLine()
        {
            Trace.WriteLine(string.Empty);
        }

        internal static void Info(string fmt, params object[] param)
        {
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            var name = string.Empty;
            if (type != null) name = type.Name;
            var source = string.Format("{0}.{1}", name, method.Name);

            Trace.WriteLine(string.Format("{0} : {1} : {2}", "info".PadRight(8), source, string.Format(fmt, param)));
        }

        internal static void InfoSource(string source, string fmt, params object[] param)
        {
            Trace.WriteLine(string.Format("{0} : {1} : {2}", "info".PadRight(8), source, string.Format(fmt, param)));
        }

        internal static void Error(string source, string fmt, params object[] param)
        {
            Trace.WriteLine(string.Format("{0} : {1} : {2}", "error".PadRight(8), source, string.Format(fmt, param)));
        }


        internal static void Exception(Exception ex)
        {
            Trace.WriteLine(string.Format("{0} : {1} : {2}", "error".PadRight(8), GetSource(),
                string.Format("{0}\n{1}", ex.Message, ex.StackTrace)));
        }

        private static string GetSource()
        {
            var frame = new StackFrame(2);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            var name = string.Empty;
            if (type != null) name = type.Name;
            var source = string.Format("{0}.{1}", name, method.Name);
            return source;
        }
    }
}