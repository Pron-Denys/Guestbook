using Microsoft.EntityFrameworkCore;
using Guestbook.Models;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<GuestbookContext>(options => options.UseSqlServer(connection));
builder.Services.AddSession();
builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseSession();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Comments}/{action=Index}/{id?}");


app.Run();
