using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MYData>(options => {
    options.UseSqlServer("Data Source=.;Initial Catalog=WebAPIIS;Integrated Security=True");
}) ;

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<MYData>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(crosoption =>
{
    crosoption.AddPolicy("mypolicy", crosPolicyBuilder =>
    {
        crosPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors("mypolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
