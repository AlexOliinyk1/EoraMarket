using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EoraMarketplace.Services.Email
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //todo: implement email sending here
            return Task.FromResult(0);
        }
    }
}
