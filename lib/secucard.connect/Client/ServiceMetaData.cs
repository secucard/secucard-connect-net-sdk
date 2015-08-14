﻿/*
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

namespace Secucard.Connect.Client
{
    /// <summary>
    /// Meta data describing the service.
    /// </summary>
    public class ServiceMetaData<T>
    {
        public readonly string Product;
        public readonly string Resource;
        public string AppId;
        public T ResourceType;

        public string ProductResource
        {
            get { return string.Format("{0}.{1}", Product, Resource); }
        }

        public ServiceMetaData(string product, string resource)
        {
            Product = product;
            Resource = resource;
        }
    }
}