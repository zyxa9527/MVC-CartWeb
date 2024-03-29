﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplication9.Models
{

    
    public class Operation
    {
        [WebMethod(EnableSession = true)] //啟用Session
        public static Models.Cart GetCurrentCart() //取得目前Session中的Cart物件
        {
            if (System.Web.HttpContext.Current != null) //確認System.Web.HttpContext.Current是否為空
            {
                //如果Session["Cart"]不存在，則新增一個空的Cart物件
                if (System.Web.HttpContext.Current.Session["Cart"] == null)
                {
                    var order = new Cart();
                    System.Web.HttpContext.Current.Session["Cart"] = order;
                }

                //回傳Session["Cart"]
                return (Cart)System.Web.HttpContext.Current.Session["Cart"];
            }
            else
            {
                throw new InvalidOperationException("System.Web.HttpContext.Current為空，請檢查");
            }
        }
    }
}