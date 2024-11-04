
namespace UserRepositoryTest.Services;

public class UserAccountService(IUserRepository userRepository) : IUserAccountService
{
    public GetUsersResult GetUsers(UserFilterModel filter)
    {
        var users = userRepository.Users.OrderBy(x => x.Name).ToList();
        var usersResult = users.Skip((filter.Page - 1) * filter.Take).Take(filter.Take);
        var maxPage = users.Count() / filter.Take;

        return new GetUsersResult(usersResult, filter.Page, maxPage);
    }
}
