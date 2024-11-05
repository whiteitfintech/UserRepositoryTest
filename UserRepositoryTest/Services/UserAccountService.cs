namespace UserRepositoryTest.Services;

public class UserAccountService(IUserRepository userRepository) : IUserAccountService
{
    public bool Add(string userName)
    {
        userRepository.AddUser(new UserModel(Guid.NewGuid(), userName));
        return true;
    }

    public bool AddAccount(Guid userId, string accountName)
    {
        var user = userRepository.GetUser(userId);
        if (user is not null)
        {
            user.Accounts.Add(new AccountModel(Guid.NewGuid(), accountName, true));
            return true;
        }

        return false;
    }

    public IEnumerable<AccountDto> GetAccounts(Guid userId)
    {
        var user = userRepository.GetUser(userId);
        return user?.Accounts.Select(x => new AccountDto(x.Name, x.Id, x.IsActive)) ?? [];
    }

    public UserDto? GetUser(Guid userId)
    {
        var user = userRepository.GetUser(userId);
        return user is not null ? new UserDto(user.Name, user.Id) : null;
    }

    public GetUsersResult GetUsers(UserFilterModel filter)
    {
        var users = userRepository.Users.OrderBy(x => x.Name).ToList();
        var usersResult = users.Skip((filter.Page - 1) * filter.Take)
                               .Take(filter.Take)
                               .Select(x => new UserDto(x.Name, x.Id));

        var maxPage = Convert.ToInt32(Math.Ceiling(users.Count / (decimal)filter.Take));

        return new GetUsersResult(usersResult, filter.Page, maxPage);
    }

    public bool SetAccountActive(Guid userId, Guid accountId, bool isActive)
    {
        var user = userRepository.GetUser(userId);
        var account = user?.Accounts.FirstOrDefault(x => x.Id == accountId);
        if (account != null) 
        {
            account.IsActive = isActive;
            return true;
        }

        return false;
    }
}
