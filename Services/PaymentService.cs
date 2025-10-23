using E_Commerce.Data;
using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_Commerce.Services
{
    public class PaymentService
    {
        private ECommerceDataContext _context;

        public PaymentService(ECommerceDataContext context) 
        {
            _context = context;
        }

        public async Task<Cart> Cart(int userId)
        {
            var cart = await _context
                    .Carts
                    .FirstOrDefaultAsync(c => c.UserId == userId && !c.Closed);

            return cart;
        }

        public async Task<long> TotalValue(int userId)
        {
            decimal subTotalValue = await _context.Carts
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.Items)
                .SumAsync(i => i.Product.Price * i.Quantity);

            decimal taxEstimate = subTotalValue / 10; // 10%
            decimal shippingEstimate = subTotalValue / 50; // 20%
            long totalValue = Convert.ToInt64((subTotalValue + taxEstimate + shippingEstimate) * 100);

            return totalValue;
        }
    }
}
