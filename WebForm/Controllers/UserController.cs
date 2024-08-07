using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
using System.Text.Json;
using WebForm.Data;
using WebForm.Models;

namespace WebForm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly WebFormDbContext dbContext;

        public UserController(WebFormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("user/get")]
        public async Task<IActionResult> GetUser()
        {
            var users = await dbContext.users.ToListAsync();
            return View(users);
        }

        [HttpPost("user/addUser")]
        public async Task<IActionResult> Add([FromForm] AddUsers addUsers)
        {
            var newUser = new User
            {
                Id = addUsers.Id,
                FirstName = addUsers.FirstName,
                LastName = addUsers.LastName,
                Age = addUsers.Age,
                PhoneNumber = addUsers.PhoneNumber,
                AlternatePhoneNumber = addUsers.AlternatePhoneNumber,
                Email = addUsers.Email,
                Password = addUsers.Password,
                ConfirmPassword = addUsers.ConfirmPassword,
                Gender = addUsers.Gender,
                MaritalStatus = addUsers.MaritalStatus
            };
            dbContext.users.Add(newUser);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(GetUser));
        }
        [HttpGet("user/addUser")]
        public IActionResult Add()
        {
            return View();
        }



        [HttpGet("user/updateUser/{id:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id)
        {
            var existingUser = await dbContext.users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            var model = new UpdateUser
            {
                Id = existingUser.Id,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                Age = existingUser.Age,
                PhoneNumber = existingUser.PhoneNumber,
                AlternatePhoneNumber = existingUser.AlternatePhoneNumber,
                Email = existingUser.Email,
                Password = existingUser.Password,
                ConfirmPassword = existingUser.ConfirmPassword,
                Gender = existingUser.Gender,
                MaritalStatus = existingUser.MaritalStatus
            };

            return View(model);
        }

        [HttpPost("user/updateUser/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromForm] UpdateUser updateUser)
        {
            if (id != updateUser.Id)
            {
                return BadRequest();
            }

            var existingUser = await dbContext.users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = updateUser.FirstName;
            existingUser.LastName = updateUser.LastName;
            existingUser.Age = updateUser.Age;
            existingUser.PhoneNumber = updateUser.PhoneNumber;
            existingUser.AlternatePhoneNumber = updateUser.AlternatePhoneNumber;
            existingUser.Email = updateUser.Email;
            existingUser.Password = updateUser.Password;
            existingUser.ConfirmPassword = updateUser.ConfirmPassword;
            existingUser.Gender = updateUser.Gender;
            existingUser.MaritalStatus = updateUser.MaritalStatus;

            dbContext.users.Update(existingUser);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(GetUser));
        }

        [HttpGet("user/deleteUser/{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await dbContext.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost("user/deleteUser/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed([FromRoute] int id)
        {
            var user = await dbContext.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            dbContext.users.Remove(user);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(GetUser));
        }







        [HttpGet("user")]
        public async Task<IActionResult> DownloadGrid()
        {
            var users = await dbContext.users.ToListAsync();

           
            using (var package = new ExcelPackage())
            {
              
                var worksheet = package.Workbook.Worksheets.Add("Report");

              
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "FirstName";
                worksheet.Cells["C1"].Value = "LastName";
                worksheet.Cells["D1"].Value = "Age";
                worksheet.Cells["E1"].Value = "PhoneNumber";
                worksheet.Cells["F1"].Value = "AlternatePhoneNumber";
                worksheet.Cells["G1"].Value = "Email";
                worksheet.Cells["H1"].Value = "Password";
                worksheet.Cells["I1"].Value = "ConfirmPassword";
                worksheet.Cells["J1"].Value = "Gender";
                worksheet.Cells["K1"].Value = "MaritalStatus";

                int row = 2;
                foreach (var item in users)
                {
                    worksheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                    worksheet.Cells[string.Format("B{0}", row)].Value = item.FirstName;
                    worksheet.Cells[string.Format("C{0}", row)].Value = item.LastName;
                    worksheet.Cells[string.Format("D{0}", row)].Value = item.Age;
                    worksheet.Cells[string.Format("E{0}", row)].Value = item.PhoneNumber;
                    worksheet.Cells[string.Format("F{0}", row)].Value = item.AlternatePhoneNumber;
                    worksheet.Cells[string.Format("G{0}", row)].Value = item.Email;
                    worksheet.Cells[string.Format("H{0}", row)].Value = item.Password;
                    worksheet.Cells[string.Format("I{0}", row)].Value = item.ConfirmPassword;
                    worksheet.Cells[string.Format("J{0}", row)].Value = item.Gender;
                    worksheet.Cells[string.Format("K{0}", row)].Value = item.MaritalStatus;
                    row++;
                }
               
                worksheet.Cells["A:K"].AutoFitColumns();
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UsersReport.xlsx");
            }
        }
      
    }
}



            



      


 
