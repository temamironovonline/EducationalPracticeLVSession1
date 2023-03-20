using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace EducationalPracticeLVSession1
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private int _countTry = 0;
        private DispatcherTimer _timer;
        private Users _user;
        private void signInUser_Click(object sender, RoutedEventArgs e)
        {
            if (loginInput.Text == "")
            {
                MessageBox.Show("Поле 'Логин' не может быть пустым", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (passwordInput.Password == "")
            {
                MessageBox.Show("Поле 'Пароль' не может быть пустым", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                _user = DataBaseClass.dataBaseEntities.Users.FirstOrDefault(x => x.UserLogin == loginInput.Text);

                if (_countTry >= 1)
                {
                    CaptchaFieldWindow captchaFieldWindow = new CaptchaFieldWindow();
                    captchaFieldWindow.ShowDialog();

                    if (captchaFieldWindow.ReturnErrorCode == 1)
                    {
                        signInUser.IsEnabled = false;
                        signInGuest.IsEnabled = false;

                        _timer = new DispatcherTimer();
                        _timer.Interval = new TimeSpan(0, 0, 10);
                        _timer.Tick += new EventHandler(Timer_Tick);

                        MessageBox.Show("Повторить попытку можно через 10 секунд", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                        _timer.Start();
                    }
                }

                if (_user == null)
                {
                    MessageBox.Show("Пользователь не найден", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    _countTry++;
                }
                else if (_user.UserPassword != passwordInput.Password)
                {
                    MessageBox.Show("Неверный пароль. Повторите ввод", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    _countTry++;
                }
                else
                {
                    SignInRegisteredUser();
                }
            }
        }

        

        private void SignInRegisteredUser()
        {
            MessageBox.Show("Добро пожаловать", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            FrameNavigation.frame.Navigate(new ProductsList(_user));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            signInUser.IsEnabled = true;
            signInGuest.IsEnabled = true;
            _timer.Stop();
        }

        private void signInGuest_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigation.frame.Navigate(new ProductsList());
            
        }
    }
}
