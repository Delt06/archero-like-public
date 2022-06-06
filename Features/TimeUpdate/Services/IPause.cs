namespace Features.TimeUpdate.Services
{
    public interface IPause
    {
        void RequestPause();
        void RequestResume();
    }
}