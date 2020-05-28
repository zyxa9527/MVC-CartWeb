using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication9.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            return View();
        }
        
       [HttpPost]
        public ActionResult Login(Models.User postback)
        {
            
            try
            {
                if (Db.CheckUserData(postback.Name, postback.Password))
                {
                    Session["LoginName"] = postback.Name;
                    Session["LoginPassword"] = postback.Password;
                    TempData["ResultMessage"] = String.Format("{0}登入成功!!" , postback.Name);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ResultMessage"] = "帳號或密碼錯誤";
                    return RedirectToAction("Index","Login");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Login");
                
            }

        }
            /// <summary>
            /// 登出
            /// </summary>
            public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
        public class Db
        {
           
            public static SqlConnection CreateConnection()//192.168.56.1,64814  User ID =zyxa9527;Password = a3727240;
            {
                SqlConnection con = new SqlConnection("Data Source =172.16.5.43;Initial Catalog =Carts;User ID =zyxa9527;Password = a3727240;");
                return con;
            }
           
            public static bool CheckUserData(string name, string password)
            {
                SqlConnection con = Db.CreateConnection();
                try
                {
                   
                    string strSQL = "select userpassword from [User] where username = '" + name + "' and userpassword = '" + password + "'" ;
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return true;
                    }
                    
                    return false;

                }
                
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    
                    return false;
                }
                finally
                {
                    con.Close();
                }
                
            }
        }

    }
}