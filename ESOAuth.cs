using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EastSidazFantasy
{
    public class ESOAuth : OAuthBase
    {
        public string GenerateURL(string sURL, string sConsumerKey, string sSignature, string sToken, string sSecret)
        {
            string normURL, normParams;
            string sResult = this.GenerateSignature(new Uri(sURL), sConsumerKey, sSignature, sToken, sSecret, "GET", clsStatic.GenerateTimeStamp(), clsStatic.GenerateNonce(), out normURL, out normParams);
            sResult = sResult.Replace("=", "%3D");
            sResult = sResult.Replace("+", "%2B");
            sResult = sResult.Replace("/", "%2F");
            return normURL + "?" + normParams + "&oauth_signature=" + sResult;
        }

        public string GenerateGetRequestToken(string sConsumerKey, string sSignature, string sCallback)
        {
            string sURL = "https://api.login.yahoo.com/oauth/v2/get_request_token?" +
                              OAuthNonceKey + "=" + clsStatic.GenerateNonce() +
                              "&" + OAuthTimestampKey + "=" + clsStatic.GenerateTimeStamp() +
                              "&" + OAuthConsumerKeyKey + "=" + sConsumerKey +
                              "&" + OAuthSignatureMethodKey + "=" + PlainTextSignatureType +
                              "&" + OAuthSignatureKey + "=" + sSignature + "%26" +
                              "&" + OAuthVersionKey + "=" + OAuthVersion +
                              "&" + XOAuthLangPrefKey + "=en-us" +
                              "&" + OAuthCallbackKey + "=" + sCallback;

            return sURL;
        }

        public string GenerateGetToken(string sConsumerKey, string sSignature, string sSecret, string sToken, string sVerifier)
        {
            string sURL = "https://api.login.yahoo.com/oauth/v2/get_token?" +
                          OAuthConsumerKeyKey + "=" + sConsumerKey +
                          "&" + OAuthSignatureMethodKey + "=" + PlainTextSignatureType +
                          "&" + OAuthVersionKey + "=" + OAuthVersion +
                          "&" + OAuthVerifierKey + "=" + sVerifier +
                          "&" + OAuthTokenKey + "=" + sToken +
                          "&" + OAuthNonceKey + "=" + clsStatic.GenerateNonce() +
                          "&" + OAuthTimestampKey + "=" + clsStatic.GenerateTimeStamp() +
                          "&" + OAuthSignatureKey + "=" + sSignature + "%26" + sSecret;

            return sURL;
        }

        public string GenerateRefreshToken(string sConsumerKey, string sSignature, string sSecret, string sToken, string sSessionHandle)
        {
            string sURL = "https://api.login.yahoo.com/oauth/v2/get_token?" +
                          OAuthNonceKey + "=" + clsStatic.GenerateNonce() +
                          "&" + OAuthConsumerKeyKey + "=" + sConsumerKey +
                          "&" + OAuthSignatureMethodKey + "=" + PlainTextSignatureType +
                          "&" + OAuthSignatureKey + "=" + sSignature + "%26" + sSecret +
                          "&" + OAuthVersionKey + "=" + OAuthVersion +
                          "&" + OAuthTokenKey + "=" + sToken +
                          "&" + OAuthTimestampKey + "=" + clsStatic.GenerateTimeStamp() +
                          "&" + OAuthSessionHandleKey + "=" + sSessionHandle;

            return sURL;
        }
    }
}
