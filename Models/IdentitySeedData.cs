using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Models
{
	public static class IdentitySeedData
	{
		private const string adminUser = "Admin";
		private const string adminPassword = "Kx0aMmC1518!";

		public static async void EnsurePopulated(IApplicationBuilder app)
		{
			AppIdentityDbContext context = app.ApplicationServices
				.CreateScope().ServiceProvider
				.GetRequiredService<AppIdentityDbContext>();

			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}

			UserManager<IdentityUser> userManager = app.ApplicationServices
				.CreateScope().ServiceProvider
				.GetRequiredService<UserManager<IdentityUser>>();

			IdentityUser user = await userManager.FindByNameAsync(adminUser);

			if (user == null)
			{
				user = new IdentityUser("Admin");
				user.Email = "ermakovbox@gmail.com";
				user.PhoneNumber = "8 (930) 004-21-21";
				await userManager.CreateAsync(user, adminPassword);
			}
		}
	}
}