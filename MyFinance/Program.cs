using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyFinance;
using MyFinance.Models;
using MyFinance.Services.IRepository;
using MyFinance.Services.Repository;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    { 
        options.AddSecurityDefinition("oauth2",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });


builder.Services.AddDbContext<MyFinanceContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyFinanceContext>();

//Can remove this block later//
builder.Services.AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme).Configure(options =>
{
    options.BearerTokenExpiration = TimeSpan.FromSeconds(1800);
});
//

builder.Services.AddHttpContextAccessor();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                      });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//builder.Services.AddSingleton<ILoanCalculationService, LoanCalculationService>();
//DB services registration
builder.Services.AddScoped<IMemberRepositoryService, MemberRepositoryService>();
builder.Services.AddScoped<IAddressRepositoryService, AddressRepositoryService>();
builder.Services.AddScoped<ILoanRepositoryService, LoanRepositoryService>();
builder.Services.AddScoped<INomineeRepositoryService, NomineeRepositoryService>();
builder.Services.AddScoped<IBankDetailsRepositoryService, BankDetailsRepositoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();
app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.Use(async (context, next) =>
{
    // Intercept POST /register
    if (context.Request.Path.Equals("/register", StringComparison.OrdinalIgnoreCase) &&
        context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
    {
        // Check if the user is authenticated
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Authentication required to register users.");
            return;
        }

        // Check if user is in Admin role
        if (!context.User.IsInRole("Admin"))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Only admins can register new users.");
            return;
        }
    }

    // If checks pass (or it's not /register), continue the pipeline
    await next.Invoke();
});

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var role = "Admin";
    if (!await roleManager.RoleExistsAsync(role))
    {
        await roleManager.CreateAsync(new IdentityRole(role));
    }

}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var email = builder.Configuration.GetValue<string>("AdminUser:Email");
    var password = builder.Configuration.GetValue<string>("AdminUser:Password");
    //var email = "Admin@gmail.com";
    //var password = "password";

    if (email != null && password != null && await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser() { Email=email, UserName=email};

        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();
