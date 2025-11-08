using System;
using System.Collections.Generic;
using LabamotoLaundryShop.Models;

namespace LabamotoLaundryShop.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        decimal GetTotalIncome(DateTime startDate, DateTime endDate);
        int GetActiveOrdersCount();
        int GetOrdersCountByStatus(string status);
        int GetOrdersCountByDate(DateTime date);
        IEnumerable<Order> GetOrdersByStatus(string status);
    }
}
