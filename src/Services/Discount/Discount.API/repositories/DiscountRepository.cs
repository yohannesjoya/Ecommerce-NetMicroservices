using Dapper;
using Discount.API.Dtos;
using Discount.API.Entities;
using Npgsql;
using System.Collections.Generic;

namespace Discount.API.repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        //  ------------------------- 1. GetDiscount -------------------------
        public async Task<Coupon> GetDiscount(string productname)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName=@ProductName", new { ProductName = productname });


            if (coupon is null) {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }

            return coupon;
        }


        //  ------------------------- 2. CreateDiscount -------------------------
        public async Task<bool> CreateDiscount(CreateCouponDto coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
             var affectedRows = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)", new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affectedRows == 0) return false;
            return true;

        }


        // ------------------------- 3. UpdateDiscount -------------------------
        public async Task<bool> UpdateDiscount(UpdateCouponDto coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            string query = "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(query, new { ProductName = coupon.ProductName, Description = coupon.Description, Amount= coupon.Amount, Id=coupon.Id });


            if (affectedRows == 0) return false;
            return true;
        }

        // ------------------------- 4. DeleteDiscount -------------------------
        public async Task<bool> DeleteDiscount(string productname)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affectedRows = await connection.ExecuteAsync("DELETE From Coupon WHERE ProductName=@ProductName", new { ProductName = productname});

            if (affectedRows == 0) return false;
            return true;
        }
    }
}
