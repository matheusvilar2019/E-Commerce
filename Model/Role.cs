﻿namespace E_Commerce.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public IList<User> Users { get; set; }
    }
}
