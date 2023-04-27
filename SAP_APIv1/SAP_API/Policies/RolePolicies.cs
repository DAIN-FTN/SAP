using Microsoft.AspNetCore.Authorization;

public class Policies {
    public const string Admin = "Admin";
    public const string Staff = "Staff";

    public static AuthorizationPolicy AdminPolicy(){
        return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireRole(Admin)
            .Build();
    }

    public static AuthorizationPolicy UserPolicy() {
        return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireRole(Staff)
            .Build();
    }

}
