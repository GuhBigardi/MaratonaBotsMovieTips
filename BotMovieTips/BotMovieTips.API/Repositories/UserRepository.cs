using BotMovieTips.API.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BotMovieTips.API.Repositories
{
    public class UsersRepository
    {
        private IConfiguration _configuration;

        public UsersRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User Find(string userID)
        {
            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                _configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);

            return new User()
            {
                UserID = userID,
                AccessKey = tokenConfigurations.Token
            };
        }
    }
}