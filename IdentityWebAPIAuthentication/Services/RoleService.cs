using IdentityWebAPIAuthentication.Data;
using IdentityWebAPIAuthentication.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebAPIAuthentication.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityDbContext _identityDbContext;

        public RoleService(RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager, IdentityDbContext identityDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _identityDbContext = identityDbContext;
        }
        public async Task<List<RoleModel>> GetRolesAsync()
        {
            //var roleList = await _roleManager.Roles.Select(x =>
            //    new RoleModel { Id = Guid.Parse(x.Id), Name = x.Name }).ToListAsync();

            var roleList = await _identityDbContext.Roles.Select(x =>new RoleModel { Id = Guid.Parse(x.Id), Name = x.Name }).ToListAsync();
            return roleList;
        }

        public async Task<List<string>> GetUserRolesAsync(string emailId)
        {
           //var user = await _userManager.FindByEmailAsync(emailId);

           var user = await _identityDbContext.Users.FirstOrDefaultAsync(x => x.Email == emailId);

         //  var userRoles = await _userManager.GetRolesAsync(user);
          var userRoles = await _identityDbContext.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
          
          for (var i = 0; i < userRoles.Count; i++)
          {
              userRoles[i] = _identityDbContext.Roles.FirstOrDefault(x => x.Id == userRoles[i]).Name;
          }
          
          
          return userRoles.ToList();
        }
        public async Task<List<string>> AddRolesAsync(string[] roles)
        {
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
               if(!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    rolesList.Add(role);
                }
            }
            return rolesList;
        }

        public async Task<bool> AddUserRoleAsync(string userEmail, string[] roles)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var exitsRoles = await ExistsRolesAsync(roles);

            if(user != null && exitsRoles.Count == roles.Length)
            {
                var assginRoles = await _userManager.AddToRolesAsync(user, exitsRoles);
                return assginRoles.Succeeded;
            }

            return false;

        }

        private async Task<List<string>> ExistsRolesAsync(string[] roles)
        {
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (roleExist)
                {
                    rolesList.Add(role);
                }
            }
            return rolesList;

        }


        }
}
