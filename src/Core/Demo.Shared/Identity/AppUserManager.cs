using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppUser> passwordHasher,
            IEnumerable<IUserValidator<AppUser>> userValidators,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<AppUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<AppUser> FindUniqueAsync(string key)
        {
            try
            {
                var user = await FindByPhoneNumberUniqueOrDefaultAsync(key);
                if (user == null)
                {
                    user = await FindByIdcardUniqueOrDefaultAsync(key);
                }
                if (user == null)
                {
                    user = await FindByFullNameUniqueOrDefaultAsync(key);
                }
                if (user == null)
                {
                    user = await FindByEmailAsync( key);
                }
                if (user == null)
                {
                    user = await FindByNameAsync(key);
                }
                return user;
            }
            catch
            {
                return null;
            }
        }

        #region Exists...
        public async Task<bool> ExistsPhoneNumberAsync(string phoneNumber)
        {
            return await Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<bool> ExistsEmailAsync(string email)
        {
            return await Users.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> ExistsFullNameAsync(string fullName)
        {
            return await Users.AnyAsync(x => x.PhoneNumber == fullName);
        }

        public async Task<bool> ExistsUserNameAsync(string userName)
        {
            return await Users.AnyAsync(x => x.UserName == userName);
        }

        public async Task<bool> ExistsIdcardAsync(string idcard)
        {
            return await Users.AnyAsync(x => x.Idcard == idcard);
        }
        #endregion

        #region Find...
        public async Task<AppUser> FindByPhoneNumberUniqueOrDefaultAsync(string phoneNumber)
        {
            try
            {
                return await Users.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            }
            catch
            {
                return null;
            }
        }

        public async Task<AppUser> FindByIdcardUniqueOrDefaultAsync(string idcard)
        {
            try
            {
                return await Users.SingleOrDefaultAsync(x => x.Idcard == idcard);
            }
            catch
            {
                return null;
            }
        }

        public async Task<AppUser> FindByFullNameUniqueOrDefaultAsync(string phoneNumber, bool confirmedOnly = true)
        {
            try
            {
                if (confirmedOnly)
                    return await Users.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber && x.PhoneNumberConfirmed);
                else
                    return await Users.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        public virtual async Task<IdentityResult> VerifyAndConfirmPhoneNumberAsync(AppUser user, string code)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!await VerifyUserTokenAsync(user, Options.Tokens.ChangePhoneNumberTokenProvider, ChangePhoneNumberTokenPurpose + ":" + user.PhoneNumber, code))
            {
                return IdentityResult.Failed(ErrorDescriber.InvalidToken());
            }

            user.PhoneNumberConfirmed = true;
            return await UpdateAsync(user);
        }

        public virtual async Task<(AppUser user, string code)> GenerateChangePhoneNumberTokenAsync(string phoneNumber)
        {
            var user = await FindByPhoneNumberUniqueOrDefaultAsync(phoneNumber);
            if (user == null)
            {
                user = new AppUser { UserName = phoneNumber, PhoneNumber = phoneNumber };
                var identityResult = await CreateAsync(user);
                if (!identityResult.Succeeded)
                {
                    var errMsg = "";
                    foreach (var err in identityResult.Errors)
                    {
                        errMsg += $"[{err.Code}]{err.Description}";
                    }
                    throw new ApplicationException($"Failed on creating a user.{errMsg}");
                }
            }

            var code = await GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            return (user, code);
        }


        public virtual async Task<(AppUser user, IdentityResult result)> VerifyChangePhoneNumberTokenAsync(string phoneNumber, string token)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }
            var user = await FindByPhoneNumberUniqueOrDefaultAsync(phoneNumber);
            if (user == null)
            {
                throw new ArgumentNullException($"User associated with phone number {phoneNumber} not exists!");
            }

            // Make sure the token is valid and the stamp matches
            //var result = VerifyUserTokenAsync(user, Options.Tokens.ChangePhoneNumberTokenProvider, ChangePhoneNumberTokenPurpose + ":" + phoneNumber, token).Result;
            var result = await VerifyAndConfirmPhoneNumberAsync(user, token);
            return (user, result);
        }
    }
}
