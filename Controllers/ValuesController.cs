using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Web.Http;

namespace VerifyAzureAD.Controllers
{
    public class ValuesController : ApiController
    {
        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [Route("api/Values/getauth")]
        public Task<string> Get()
        {
            var st = GetAccessToken();
            return st;
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        public async Task<string> GetAccessToken()
        {
            // Parse parameters
            string username = "swamy.987@staffjmsorg.onmicrosoft.com";
            string password = "Electronic20";
            string clientId = "3a5465cc-af87-4237-a7ea-4fa6569e0eb4";
            string tenant = "staffjmsorg.onmicrosoft.com";

            // Open connection
            string authority = "https://login.microsoftonline.com/" + tenant;
            string[] scopes = new string[] { "user.read" };
            IPublicClientApplication app;
            app = PublicClientApplicationBuilder.Create(clientId)
                  .WithAuthority(authority)
                  .Build();
            var securePassword = new SecureString();
            foreach (char c in password.ToCharArray())  // you should fetch the password
                securePassword.AppendChar(c);  // keystroke by keystroke
            var result = await app.AcquireTokenByUsernamePassword(scopes, username, securePassword).ExecuteAsync(); ;

            // Return
            return result.IdToken;
        }
    }
}
