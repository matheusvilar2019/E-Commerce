﻿namespace E_Commerce.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }        
        public List<Cart> Carts { get; set; }
    }
}
