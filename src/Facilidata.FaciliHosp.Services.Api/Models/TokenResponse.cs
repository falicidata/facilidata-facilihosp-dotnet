namespace Facilidata.FaciliHosp.Services.Api.Models
{
    public class TokenResponse
    {
        public TokenResponse(string email, string usuarioId, string created, string expiration, string token)
        {
            Email = email;
            UsuarioId = usuarioId;
            Created = created;
            Expiration = expiration;
            Token = token;
        }

        public string Email { get; set; }
        public string UsuarioId { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string Token { get; set; }
    }
}
