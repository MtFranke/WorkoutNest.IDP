using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace WorkoutNest.IDP;

public static class Config
{

    internal const string Id = "user_id";
    internal const string UUID = "F185D000-BAD0-4BA8-AC53-88D6124B81F3";
    
    
    public static List<TestUser> Users =>
    [
        new TestUser
        {
            SubjectId = UUID,
            Username = "MtFranke",
            Password = "asd",
            Claims =
            {
                new Claim(JwtClaimTypes.Name, "Mateusz Franke"),
                new Claim(JwtClaimTypes.GivenName, "Mateusz"),
                new Claim(JwtClaimTypes.FamilyName, "Franke"),
                new Claim(JwtClaimTypes.Email, "mateusz.franke@proton.me"),
                new Claim(Id, UUID)

            }
        }
    ];


    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "workout-nest",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                // scopes that client has access to
                AllowedScopes = { "workout-nest.user", "openid", "profile" },
                
            }
        
        };
    
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new(name: "workout-nest.user") 
        };

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource
            {
                Name = "workout-nest",
                ApiSecrets = { new Secret("secret".Sha256()) },
                UserClaims = {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    Id
                },
                Description = "Backend services od WorkoutNest",
                DisplayName = "WorkoutNest API",
                Enabled = true,
                Scopes = { "workout-nest.user" },
            }
        };
    }
}