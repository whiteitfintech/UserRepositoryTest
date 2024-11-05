namespace UserRepositoryTest.Utils;

public interface IToastMessageHandler
{
    void Info(string message, bool autoHide = false, TimeSpan? autoHideDelay = null);
    void Success(string message, bool autoHide = false, TimeSpan? autoHideDelay = null);
    void Danger(string message, bool autoHide = false, TimeSpan? autoHideDelay = null);
    void Warning(string message, bool autoHide = false, TimeSpan? autoHideDelay = null);
}
