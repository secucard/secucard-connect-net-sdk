namespace Secucard.Connect.rest
{
    using System.Text;

    public static class RestHelp
    {
        public static byte[] ToUTF8Bytes(this string s)
        {
            return s == null ? null : Encoding.UTF8.GetBytes(s);
        }
    }
}