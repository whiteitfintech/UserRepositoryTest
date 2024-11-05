namespace UserRepositoryTest.Services;

public interface IInputModal
{
    public Task<string?> Show(string title, string propertyName);
}
