using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AppDbContext db;
        public AccountController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpPost("[action]")]
        public ResponseApiModel Login([FromBody]User user)
        {
            ResponseApiModel responseAPIModel = new ResponseApiModel();
            try
            {
                if (user.Email != null && user.Password != null)
                {
                    var querylogin = (from lg in db.Users
                                      where lg.Email == user.Email && lg.Password == user.Password
                                      select new { lg.Email, lg.Password }).FirstOrDefault();
                    if (querylogin != null)
                    {
                        responseAPIModel.Data = querylogin;
                        responseAPIModel.Status = APIStatus.Success;
                        responseAPIModel.Message = "User Login Successful";
                    }
                    else
                    {
                        responseAPIModel.Status = APIStatus.Failed;
                        responseAPIModel.Message = "Invalid Credentials";
                    }
                }
                else
                {
                    responseAPIModel.Status = APIStatus.Failed;
                    responseAPIModel.Message = "Please enter username and password";
                }

            }
            catch (Exception ex)
            {
                responseAPIModel.Status = APIStatus.Failed;
                responseAPIModel.Message = ex.Message;
            }

            return responseAPIModel;
        }
    }
}