using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApis.BasicAuthen;
using WebApis.Interface;
using WebApis.Models;
using WebApis.Services;


var builder = WebApplication.CreateBuilder(args);

var constantSection = builder.Configuration.GetSection("Constant");
ConstantModel model = constantSection.Get<ConstantModel>();
Constants.Constant = model;

byte[] key = Encoding.ASCII.GetBytes(model.Secret);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "Neversitup Test APIs", Version = "v1" });

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(x =>
 {
     x.RequireHttpsMetadata = false;
     x.SaveToken = true;
     x.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(key),
         ValidateIssuer = false,
         ValidateAudience = false,
         ValidateLifetime = false,
         RequireExpirationTime = false
     };
 });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
               options => options
               .SetIsOriginAllowed(x => _ = true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
           );

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<BasicAuthMiddleware>();

app.MapControllers();

app.Run();
