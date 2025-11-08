using LabamotoLaundryShop.Models;
using LabamotoLaundryShop.Services.Interfaces;
using LabamotoLaundryShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LabamotoLaundryShop.Controllers
{
    public class OwnerController : Controller
    {
        // Owner Dashboard Page
        private readonly IOrderService _orderService;

        public OwnerController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Dashboard()
        {
            // Check if logged in
            if (Session["OwnerUsername"] == null)
                return RedirectToAction("OwnerLogin", "Account");

            ViewBag.OwnerName = Session["OwnerUsername"].ToString();

            var viewModel = new OwnerDashboardViewModel
            {
                TotalIncomeToday = _orderService.GetTotalIncomeToday(),
                TotalIncomeWeekly = _orderService.GetTotalIncomeWeekly(),
                TotalIncomeMonthly = _orderService.GetTotalIncomeMonthly(),
                ActiveOrders = _orderService.GetActiveOrdersCount(),
                TodaysOrders = _orderService.GetTodaysOrdersCount(),
                QueuedCount = _orderService.GetOrdersCountByStatus("Queued"),
                WashingCount = _orderService.GetOrdersCountByStatus("Washing"),
                DryingCount = _orderService.GetOrdersCountByStatus("Drying"),
                ReadyCount = _orderService.GetOrdersCountByStatus("Ready"),
                Alerts = new List<AlertViewModel>
        {
            new AlertViewModel { Message = "⚠️ 3 inventory items below minimum stock level", Link = "/Owner/Inventory" },
            new AlertViewModel { Message = "📈 Weekly revenue up 15% from last week", Link = "/Owner/Reports" },
            new AlertViewModel { Message = "👥 Staff schedule for next week needs approval", Link = "/Owner/Staff" }
        }
            };

            return View(viewModel);
        }
    }
}
