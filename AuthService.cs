using SRS_Hotels.Data.src.Modules.Authentication.Features.Login;
using SRS_Hotels.Data.src.Modules.Authentication.Features.SignUp;
using SRS_Hotels.Web.Areas.Auth.ViewModels;
using System.Net.Http.Json;

namespace SRS_Hotels.Web.Services;

public class AuthService
{
    private readonly IHttpClientFactory _factory;
    private readonly IHttpContextAccessor _context;

    public AuthService(IHttpClientFactory factory, IHttpContextAccessor context)
    {
        _factory = factory;
        _context = context;
    }

    // SIGN UP
    public async Task<SignUpResponse?> SignUpAsync(RegisterViewModel model)
    {
        var client = _factory.CreateClient("Api");

        var request = new SignUpRequest(
            model.Email,
            model.Password,
            model.FullName,
            model.PhoneNumber
        );

        var response = await client.PostAsJsonAsync("api/auth/signup", request);

        if (!response.IsSuccessStatusCode)
            return new SignUpResponse(false, "Error en API");

        return await response.Content.ReadFromJsonAsync<SignUpResponse>();
    }

    // LOGIN (JWT)
    public async Task<LoginResponse?> LoginAsync(LoginViewModel model)
    {
        var client = _factory.CreateClient("Api");

        var request = new LoginRequest(model.Email, model.Password);

        var response = await client.PostAsJsonAsync("api/auth/login", request);

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result?.Successful == true && result.Token != null)
        {
            _context.HttpContext!.Session.SetString("JWT", result.Token);
        }

        return result;
    }
}