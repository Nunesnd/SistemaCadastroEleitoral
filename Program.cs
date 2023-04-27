using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SistemaCadastroEleitoral.Infraestrutura.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//################################################################################################################

//Config de conexão com o banco para rodar as migrations
string connString = builder.Configuration.GetConnectionString("MinhaConexao");
builder.Services.AddDbContext<BancoContext>(options => options.UseSqlServer("connString"));

//################################################################################################################

JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
builder.Services.AddDbContext<BancoContext>(options => options.UseSqlServer(jAppSettings["ConnectionStrings"]["ConexaoSql"].ToString()));

//################################################################################################################

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
