namespace esWMS.Client.Services
{
    public class AuthenticationService
    {
        private bool _loged = false;

        public void Login() => _loged = true;
    }
}
