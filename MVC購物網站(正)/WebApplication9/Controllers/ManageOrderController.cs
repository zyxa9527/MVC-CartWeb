﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication9.Controllers
{
    public class ManageOrderController : Controller
    {
        // GET: ManageOrder
        public ActionResult Index()
        {
            try
            {
                string loginName = Session["LoginName"].ToString();
                ViewBag.Message = "當前登入使用者：" + loginName;
            }
            catch
            {

            }
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                //取得Order中所有資料
                var result = (from s in db.Orders
                              select s).ToList();

                return View(result);
            }
        }

        public ActionResult Details(int id)
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                //取得OrderId為傳入id的所有商品列表
                var result = (from s in db.OrderDetails
                              where s.OrderId == id
                              select s).ToList();

                if (result.Count == 0)
                {   //如果商品數目為零，代表該訂單異常(無商品)，則導回商品列表
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }
        public ActionResult SerachByUserName(string UserName)
        {
            //儲存查詢出來的UserId
            string searchUserId = null;
            //searchUserId = Session["LoginName"].ToString();
            searchUserId = UserName;
            //如果有存在UserId
            if (!String.IsNullOrEmpty(searchUserId))
            {   //則將此UserId的所有訂單找出
                using (Models.CartsEntities db = new Models.CartsEntities())
                {
                    var result = (from s in db.Orders
                                  where s.UserId == searchUserId
                                  select s).ToList();

                    //回傳 結果 至Index()的View
                    return View("Index", result);
                }
            }
            else
            {   //回傳 空結果 至Index()的View
                return View("Index", new List<Models.Order>());
            }

        }
    }
}