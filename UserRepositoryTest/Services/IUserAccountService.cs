namespace UserRepositoryTest.Services;

public interface IUserAccountService
{
    public GetUsersResult GetUsers(UserFilterModel filter);
}

public record UserFilterModel(int Page, int Take);

public record GetUsersResult(IEnumerable<UserModel> Users, int CurrentPage, int MaxPage);
