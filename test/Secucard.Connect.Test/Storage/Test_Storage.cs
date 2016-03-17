/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Secucard.Connect.Test.Storage
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Storage;

    [TestClass]
    public class Test_Storage
    {
        [TestMethod]
        [DeploymentItem("temp", "temp")]
        [TestCategory("storage")]
        public void Test_MemoryDataStorage()
        {
            var storage = new MemoryDataStorage();
            storage.Save("test1", "Teststring");
            var s = storage.Get<string>("test1");
            Assert.AreEqual("Teststring", s);
            storage.Clear();
            var s2 = storage.Get<string>("test1");
            Assert.IsNull(s2);
            storage.Save("test1", "Teststring");

            var bytes = storage.Serialize();
            File.WriteAllBytes(@"test.bin", bytes);

            var bytesRead = File.ReadAllBytes(@"test.bin");
            Assert.AreEqual(bytesRead.Length, bytes.Length);
            var storage2 = MemoryDataStorage.Deserialize(new MemoryStream(bytesRead));
            Assert.AreEqual(storage2.Get<string>("test1"), "Teststring");

            storage.Save("test1", "Teststring");
            storage.SaveToFile("test2.bin");
            var storeFromFile = MemoryDataStorage.LoadFromFile("test2.bin");
            Assert.AreEqual(storage.Size(), storeFromFile.Size());
        }
    }
}