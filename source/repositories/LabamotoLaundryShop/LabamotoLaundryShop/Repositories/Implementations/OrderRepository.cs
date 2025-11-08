using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using LabamotoLaundryShop.Data;
using LabamotoLaundryShop.Models;
using LabamotoLaundryShop.Repositories.Interfaces;

namespace LabamotoLaundryShop.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public decimal GetTotalIncome(DateTime startDate, DateTime endDate)
        {
            using (var connection = _context.CreateConnection())
            {
                string sql = @"SELECT IFNULL(SUM(TotalAmount),0) 
                               FROM ORDERS 
                               WHERE OrderDate BETWEEN @StartDate AND @EndDate;";
                return connection.ExecuteScalar<decimal>(sql, new { StartDate = startDate, EndDate = endDate });
            }
        }

        public int GetActiveOrdersCount()
        {
            using (var connection = _context.CreateConnection())
            {
                string sql = @"SELECT COUNT(*) 
                               FROM ORDERS 
                               WHERE Status != 'Completed';";
                return connection.ExecuteScalar<int>(sql);
            }
        }

        public int GetOrdersCountByStatus(string status)
        {
            using (var connection = _context.CreateConnection())
            {
                string sql = @"SELECT COUNT(*) 
                               FROM ORDERS 
                               WHERE Status = @Status;";
                return connection.ExecuteScalar<int>(sql, new { Status = status });
            }
        }

        public int GetOrdersCountByDate(DateTime date)
        {
            using (var connection = _context.CreateConnection())
            {
                string sql = @"SELECT COUNT(*) 
                               FROM ORDERS 
                               WHERE DATE(OrderDate) = @Date;";
                return connection.ExecuteScalar<int>(sql, new { Date = date.Date });
            }
        }

        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            using (var connection = _context.CreateConnection())
            {
                string sql = @"SELECT * 
                               FROM ORDERS 
                               WHERE Status = @Status;";
                return connection.Query<Order>(sql, new { Status = status }).ToList();
            }
        }
    }
}
