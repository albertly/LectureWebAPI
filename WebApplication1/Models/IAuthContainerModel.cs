namespace WebApplication1.Models
{
    public interface IAuthContainerModel
    {
        #region Members
        string SecretKey { get; set; }
        string SecurityAlgorithm { get; set; }
        int ExpireMinutes { get; set; }

        System.Security.Claims.Claim[] Claims { get; set; }
        #endregion
    }
}
