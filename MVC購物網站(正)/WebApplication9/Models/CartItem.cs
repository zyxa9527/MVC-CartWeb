﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    [Serializable]
    public class CartItem
    {
        public int Id { get; set; }

        //商品名稱
        public string Name { get; set; }

        //商品購買時價格
        public decimal Price { get; set; }

        //商品購買數量
        public int Quantity { get; set; }

        //商品小計
        public decimal Amount
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }

        public string DefaultImageURL { get; set; }

    }
}