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
 * Defines operations to access the necessary authentication details provided by the client which request authentication.
 * Administration of confidential data is delegated to the client through this interface.
 */

namespace Secucard.Connect.Auth
{
    using Secucard.Connect.Auth.Model;

    public interface IClientAuthDetails {
        /**
   * Returns the credentials needed to obtain an new access token and refresh token.
   * The returned type depends on the authentication type used with the client. Should never return null.
   */
        OAuthCredentials GetCredentials();

        /**
   * Returns the client credentials needed to obtained an access token with an existing refresh token.
   * Should never return null.
   */
        ClientCredentials GetClientCredentials();

        /**
   * Returns the current Oauth token data passed by {@link #onTokenChanged(com.secucard.connect.auth.model.Token)}
   * or null if no token is available yet.
   */
        Token GetCurrent();

        /**
   * Called when a new token was obtained.
   * The client must persist the given token in a way that calls to {@link #getCurrent()} can return this token anytime.
   */
        void OnTokenChanged(Token token);
    }
}
