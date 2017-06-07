namespace Secucard.Connect.Storage
{
    using System;

    [Serializable]
    public class StorageItem
    {
        public long? Ticks { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
    }
}