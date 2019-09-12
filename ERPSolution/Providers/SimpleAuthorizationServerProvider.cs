using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ERP.Model;
using Microsoft.Owin.Security;

namespace ERPSolution.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        //ScUserBLL obBLL = new ScUserBLL();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            string vPassHashSalt = "";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            string LoginId = context.UserName.ToUpper();

            vPassHashSalt = GenerateHashWithSalt(context.Password, LoginId);


            bool vIsUser = new ScUserModel().SignIn().Any(m => m.LOGIN_ID.ToUpper() == LoginId && m.PASSWORD_HASH == vPassHashSalt);




            if (!vIsUser)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ScUserModel User = new ScUserModel().SignIn().Where(m => m.LOGIN_ID.ToUpper() == LoginId && m.PASSWORD_HASH == vPassHashSalt).First();
            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                { 
                    "SC_USER_ID", User.SC_USER_ID.ToString()
                },
                { 
                    "USER_NAME_EN", User.USER_NAME_EN.ToString()
                },
                { 
                    "USER_EMAIL", User.USER_EMAIL.ToString()
                }
            });

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public static string GenerateHashWithSalt(string password, string salt)
        {
            // merge password and salt together
            string sHashWithSalt = password + salt;
            // convert this merged value to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            // use hash algorithm to compute the hash
            System.Security.Cryptography.HashAlgorithm algorithm = new System.Security.Cryptography.SHA256Managed();
            // convert merged bytes to a hash as byte array
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // return the has as a base 64 encoded string
            return Convert.ToBase64String(hash);
        }

    }
}