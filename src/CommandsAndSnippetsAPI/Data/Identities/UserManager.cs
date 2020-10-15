using System;
using System.Collections.Generic;
using System.Linq;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    public class UserManager : UserManager<User>
    {
        private readonly UsersRepo _userRepo;

        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store,
            optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
            logger)
        {
            _userRepo = (UsersRepo) store;
        }

        public override IQueryable<User> Users { get; }
    }
}