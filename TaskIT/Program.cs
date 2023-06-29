using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskIT.Controllers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

 
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "editRecord",
        pattern: "/Home/EditRecord/{id}",
        defaults: new { controller = "Home",action = "EditRecord" });

    endpoints.MapControllerRoute(
        name: "deleteRecord",
        pattern: "/Home/DeleteRecord/{id}",
        defaults: new { controller = "Home",action = "DeleteRecord" });

    endpoints.MapControllerRoute(
        name: "addRecord",
        pattern: "/Home/AddRecord/{id}",
        defaults: new { controller = "Home",action = "AddRecord" });

    endpoints.MapControllerRoute(
        name: "thirdTable",
        pattern: "/Home/LastTable/{id}",
        defaults: new { controller = "Home",action = "CreateThirdTable" });


    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
