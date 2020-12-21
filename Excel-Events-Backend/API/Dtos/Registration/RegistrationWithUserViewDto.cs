namespace API.Dtos.Registration
{
    public class RegistrationWithUserViewDto:Models.Registration
    {
        public UserForViewDto User { get; set; }
    }
}