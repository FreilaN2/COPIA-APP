using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace SpinningTrainer.ViewModels
{
    public class UserProfileViewModel : ViewModelBase
    {
        private string userName;
        private string userEmail;
        private string userPhone;
        private string userProfileImage;

        public UserProfileViewModel()
        {
            // Simulate loading user data, replace with actual data retrieval
            LoadUserData();
            LogoutCommand = new Command(OnLogout);
        }

        private void LoadUserData()
        {
            // Here you would load the actual data from your session or data source
            UserName = "John Doe";
            UserEmail = "johndoe@example.com";
            UserPhone = "+1234567890";
            UserProfileImage = "default_profile.png"; // Placeholder image
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string UserEmail
        {
            get => userEmail;
            set => SetProperty(ref userEmail, value);
        }

        public string UserPhone
        {
            get => userPhone;
            set => SetProperty(ref userPhone, value);
        }

        public string UserProfileImage
        {
            get => userProfileImage;
            set => SetProperty(ref userProfileImage, value);
        }

        public ICommand LogoutCommand { get; }

        private async void OnLogout()
        {
            // Perform logout actions
            await Shell.Current.GoToAsync("//LoginView");
        }
    }
}
