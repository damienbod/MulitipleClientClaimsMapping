using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect("t1", options => // Duende IdentityServer 
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    builder.Configuration.GetSection("IdentityServerSettings").Bind(options);
    options.Authority = builder.Configuration["IdentityServerSettings:Authority"];
    options.ClientId = builder.Configuration["IdentityServerSettings:ClientId"];
    options.ClientSecret = builder.Configuration["IdentityServerSettings:ClientSecret"];
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name"
    };
    options.CallbackPath = "/signin-oidc-t1";
    options.SignedOutCallbackPath = "/signout-callback-oidc-t1";
    options.MapInboundClaims = false;
})
.AddOpenIdConnect("t2", options => // OpenIddict server 
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    builder.Configuration.GetSection("IdentityProviderSettings").Bind(options);
    options.Authority = builder.Configuration["IdentityProviderSettings:Authority"];
    options.ClientId = builder.Configuration["IdentityProviderSettings:ClientId"];
    options.ClientSecret = builder.Configuration["IdentityProviderSettings:ClientSecret"];
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name"
    };
    options.CallbackPath = "/signin-oidc-t2";
    options.SignedOutCallbackPath = "/signout-callback-oidc-t2";
});

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

IdentityModelEventSource.ShowPII = true;
//JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
