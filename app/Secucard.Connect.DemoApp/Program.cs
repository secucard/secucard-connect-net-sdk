﻿namespace Secucard.Connect.DemoApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // SMART examples
            _01_smart_transaction.simple.Run();

            // Payment examples
            _02_client_payments.Run_Sample.runAllSamples();
        }
    }
}