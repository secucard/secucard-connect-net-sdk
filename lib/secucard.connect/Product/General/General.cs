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

/**
 * Holds service references and service type constants for "general" product.
 */

namespace Secucard.Connect.Product.General
{
    public class General
    {
        public AccountDevicesService Accountdevices { get; set; }
        public AccountsService Accounts { get; set; }
        public MerchantsService Merchants { get; set; }
        public NewsService News { get; set; }
        public PublicMerchantsService Publicmerchants { get; set; }
        public StoresService Stores { get; set; }
        public TransactionsService Transactions { get; set; }
    }
}