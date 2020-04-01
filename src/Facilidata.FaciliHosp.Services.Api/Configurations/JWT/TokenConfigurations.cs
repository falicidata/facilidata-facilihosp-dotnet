namespace Facilidata.FaciliHosp.Services.Api.Configurations.JWT
{

    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}