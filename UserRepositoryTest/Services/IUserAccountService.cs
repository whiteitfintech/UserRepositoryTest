namespace UserRepositoryTest.Services;

public interface IUserAccountService
{
    public UserDto? GetUser(Guid userId);
    public GetUsersResult GetUsers(UserFilterModel filter);
    public IEnumerable<AccountDto> GetAccounts(Guid userId);

    public bool Add(string userName);
    public bool AddAccount(Guid userId, string accountName);
    public bool SetAccountActive(Guid userId, Guid accountId, bool isActive);
}

public record UserFilterModel(int Page, int Take);

public record UserDto(string Name, Guid Id);
public record AccountDto(string Name, Guid Id, bool IsActive)
{
    public bool IsActive { get; set; } = IsActive;
}
public record GetUsersResult(IEnumerable<UserDto> Users, int CurrentPage, int MaxPage);
