using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using User.Domain;
using User.Domain.Entities;
using User.Domain.Events;
using User.Domain.ValueObjects;

namespace User.Infrastructure
{
    public class UserResitory : IUserRepository
    {
        private readonly UserDbContext dbContext;
        private readonly IDistributedCache cache;
        private readonly IMediator mediator;

        public UserResitory(UserDbContext dbContext, IDistributedCache cache, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.cache = cache;
            this.mediator = mediator;
        }

        public async Task AddNewLoginHistory(PhoneNumber phoneNumber, string message)
        {
            UserModel? user = await FindOneAsync(phoneNumber);
            await dbContext.UserLoginHistories.AddAsync(new UserLoginHistory(user?.Id, phoneNumber, message));
        }

        public async Task<UserModel?> FindOneAsync(PhoneNumber phoneNumber)
        {
            return await dbContext.Users.SingleOrDefaultAsync(x => x.PhoneNumber.Number == phoneNumber.Number && x.PhoneNumber.RegionNumber == phoneNumber.RegionNumber);
        }

        public async Task<UserModel?> FindOneAsync(Guid userId)
        {
            return await dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber)
        {
            string cacheKey = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.Number}";
            var code = await cache.GetStringAsync(cacheKey);
            cache.Remove(cacheKey);
            return code;
        }

        public Task PublishEventAsync(UserAccessResultEvent _event)
        {
            return mediator.Publish(_event);
        }

        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            string cacheKey = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.Number}";
            
            return cache.SetStringAsync(cacheKey, code, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }
    }
}
