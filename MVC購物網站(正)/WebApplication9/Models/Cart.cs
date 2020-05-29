using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    [Serializable]
    public class Cart : IEnumerable<CartItem> //購物車類別
    {
        //建構值
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        //儲存所有商品
        private List<CartItem> cartItems;

        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }
        //取得商品總價
        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0.0m;
                foreach (var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }
        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            //判斷相同Id的CartItem是否已經存在購物車內
            if (findItem == default(Models.CartItem))
            {   //不存在購物車內，則新增一筆
                using (Models.CartsEntities db = new CartsEntities())
                {
                    var product = (from s in db.Products
                                   where s.Id == ProductId
                                   select s).FirstOrDefault();
                    if (product != default(Models.Products))
                    {
                        this.AddProduct(product);
                    }
                }
            }
            else
            {   //存在購物車內，則將商品數量累加
                findItem.Quantity += 1;
            }
            return true;
        }
        private bool AddProduct(Products product)
        {
            //將Product轉為CartItem
            var cartItem = new Models.CartItem()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                DefaultImageURL = product.DefaultImageURL,
                Quantity = 1
            };

            //加入CartItem至購物車
            this.cartItems.Add(cartItem);
            return true;
        }

        //移除一筆Product，使用ProductId
        public bool RemoveProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            //判斷相同Id的CartItem是否已經存在購物車內
            if (findItem == default(Models.CartItem))
            {
                //不存在購物車內，不需做任何動作
            }
            else
            {   //存在購物車內，將商品移除
                this.cartItems.Remove(findItem);
            }
            return true;
        }
        //清空購物車
        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }

        public List<Models.OrderDetails> ToOrderDetailList(int orderId)
        {
            var result = new List<Models.OrderDetails>();
            foreach (var cartItem in this.cartItems)
            {
                result.Add(new Models.OrderDetails()
                {
                    Name = cartItem.Name,
                    Price = string.Format("{0}",cartItem.Price),
                    Quantity = string.Format("{0}", cartItem.Quantity),
                    OrderId = orderId
                });
            }
            return result;
        }




        IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }
    }
}