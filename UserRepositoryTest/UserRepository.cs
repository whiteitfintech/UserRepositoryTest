using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace UserRepositoryTest
{
    public class AccountModel : INotifyPropertyChanged, IEquatable<AccountModel>
    {
        private Guid _Id;
        private string _Name;
        private bool _IsActive;
        public Guid Id
        {
            get => _Id;
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get => _Name;
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsActive
        {
            get => _IsActive;
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged();
                }
            }
        }
        public AccountModel(Guid id, string name, bool isActive)
        {
            _Id = id;
            _Name = name;
            _IsActive = isActive;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public bool Equals(AccountModel? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id == other.Id;
        }
    }
    public class UserModel : INotifyPropertyChanged, IEquatable<UserModel>
    {
        private Guid _Id;
        private string _Name;
        private ObservableCollection<AccountModel> _Accounts = new ObservableCollection<AccountModel>();
        public Guid Id
        {
            get => _Id;
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get => _Name;
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<AccountModel> Accounts => _Accounts;

        public UserModel(Guid id, string name)
        {
            _Id = id;
            _Name = name;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public bool Equals(UserModel? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id == other.Id;
        }
        public bool AddAccount(AccountModel account)
        {
            lock (_Accounts)
            {
                if (_Accounts.Contains(account))
                    return false;
                _Accounts.Add(account);
            }
            return true;
        }
        public AccountModel? GetAccount(Guid id)
        {
            lock (_Accounts)
                return _Accounts.FirstOrDefault(x => x.Id == id);
        }
    }
    public interface IUserRepository
    {
        public ObservableCollection<UserModel> Users { get; }
        public bool AddUser(UserModel user);
        public UserModel? GetUser(Guid id);
    }
    public class UserRepository : IUserRepository
    {
        private ObservableCollection<UserModel> _Users = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> Users => _Users;
        public UserRepository()
        {
            for (int i = 1; i <= 100; i++)
            {
                var user = new UserModel(Guid.NewGuid(), $"TestUser_{i}");
                for (int j = 1; j <= RandomNumberGenerator.GetInt32(5) + 1; j++)
                    user.AddAccount(new AccountModel(Guid.NewGuid(), $"TestAccount_{j}", RandomNumberGenerator.GetInt32(2) == 1));
                AddUser(user);
            }
        }
        public bool AddUser(UserModel user)
        {
            lock (_Users)
            {
                if (_Users.Contains(user))
                    return false;
                _Users.Add(user);
            }
            return true;
        }
        public UserModel? GetUser(Guid Id)
        {
            lock (_Users)
                return _Users.FirstOrDefault(x => x.Id == Id);
        }
    }
}
