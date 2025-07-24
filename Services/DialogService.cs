namespace BlazorApp1.Services
{
    public class CustomDialogService
    {
        private Func<string, Task<bool>>? _callback;

        public void Register(Func<string, Task<bool>> confirmCallback)
        {
            _callback = confirmCallback;
        }

        public Task<bool> ConfirmAsync(string message)
        {
            if (_callback == null)
                throw new InvalidOperationException("ConfirmDialog not registered.");

            return _callback.Invoke(message);
        }
    }
}
