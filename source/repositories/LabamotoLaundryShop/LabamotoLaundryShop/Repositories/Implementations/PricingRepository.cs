using System.Collections.Generic;
using System.Linq;
using Dapper;
using LabamotoLaundryShop.Data;
using LabamotoLaundryShop.Models;

namespace LabamotoLaundryShop.Repositories.Implementations
{
    public class PricingRepository
    {
        private readonly DapperContext _context;

        public PricingRepository(DapperContext context)
        {
            _context = context;
        }

        // ======================
        // REGULAR LAUNDRY
        // ======================
        public IEnumerable<PricingPackage> GetAllRegularLaundry()
        {
            using (var conn = _context.CreateConnection())
            {
                return conn.Query<PricingPackage>("SELECT * FROM PRICING_PACKAGES ORDER BY PackageID ASC").ToList();
            }
        }

        public void AddRegular(PricingPackage model)
        {
            using (var conn = _context.CreateConnection())
            {
                conn.Execute("INSERT INTO PRICING_PACKAGES (PackageName, PricePerKg, MinimumKg, Status) VALUES (@PackageName,@PricePerKg,@MinimumKg,@Status)", model);
            }
        }

        public void UpdateRegular(PricingPackage model)
        {
            using (var conn = _context.CreateConnection())
            {
                conn.Execute("UPDATE PRICING_PACKAGES SET PackageName=@PackageName, PricePerKg=@PricePerKg, MinimumKg=@MinimumKg, Status=@Status WHERE PackageID=@PackageID", model);
            }
        }

        public void ToggleRegularStatus(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                conn.Execute("UPDATE PRICING_PACKAGES SET Status = CASE WHEN Status='Active' THEN 'Inactive' ELSE 'Active' END WHERE PackageID=@id", new { id });
            }
        }

        // ======================
        // SPECIAL ITEMS
        // ======================
        public IEnumerable<SpecialItem> GetAllSpecialItems()
        {
            using (var conn = _context.CreateConnection())
            {
                return conn.Query<SpecialItem>("SELECT * FROM SPECIAL_ITEMS ORDER BY SpecialItemID ASC").ToList();
            }
        }

        public void AddSpecialItem(SpecialItem item)
        {
            var sql = @"INSERT INTO special_items 
                (ItemName, PricePerPiece, Category, ProcessingTime, Status)
                VALUES (@ItemName, @PricePerPiece, @Category, @ProcessingTime, 'Active')";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, item);
            }
        }

        public void UpdateSpecialItem(SpecialItem model)
        {
            using (var conn = _context.CreateConnection())
            {
                conn.Execute(@"UPDATE SPECIAL_ITEMS SET ItemName=@ItemName, Type=@Type, Category=@Category, PricePerPiece=@PricePerPiece, ProcessingTime=@ProcessingTime, Status=@Status WHERE SpecialItemID=@SpecialItemID", model);
            }
        }

        public void DeleteSpecialItem(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                conn.Execute("DELETE FROM SPECIAL_ITEMS WHERE SpecialItemID=@id", new { id });
            }
        }

        // ======================
        // DRY CLEAN
        // ======================
        public IEnumerable<DryCleanItem> GetAllDryClean()
        {
            using (var conn = _context.CreateConnection())
            {
                return conn.Query<DryCleanItem>("SELECT * FROM DRYCLEAN_ITEMS ORDER BY DryCleanItemID ASC").ToList();
            }
        }

        public void AddDryCleanItem(DryCleanItem item)
        {
            var sql = @"INSERT INTO dryclean_items 
                (ItemName, PricePerPiece, ProcessingTime, Status)
                VALUES (@ItemName, @PricePerPiece, @ProcessingTime, 'Active')";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, item);
            }
        }

        public void UpdateDryClean(DryCleanItem model)
        {
            using (var conn = _context.CreateConnection())
            {
                conn.Execute(@"UPDATE DRYCLEAN_ITEMS SET ItemName=@ItemName, Type=@Type, PricePerPiece=@PricePerPiece, ProcessingTime=@ProcessingTime, Status=@Status WHERE DryCleanItemID=@DryCleanItemID", model);
            }
        }

        // ======================
        // ADD-ONS
        // ======================
        public IEnumerable<AddOnItem> GetAllAddOns()
        {
            using (var conn = _context.CreateConnection())
            {
                return conn.Query<AddOnItem>("SELECT * FROM ADDON_SERVICES ORDER BY AddOnID ASC").ToList();
            }
        }

        public void AddAddOn(AddOnItem item)
        {
            var sql = @"INSERT INTO addon_services 
                (ServiceName, Price, Status, PriceType)
                VALUES (@ServiceName, @Price, 'Active', @PriceType)";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, item);
            }
        }

        public void UpdateAddOn(AddOnItem model)
        {
            using (var conn = _context.CreateConnection())
            {
                conn.Execute("UPDATE ADDON_SERVICES SET ServiceName=@ServiceName, Price=@Price, Status=@Status WHERE AddOnID=@AddOnID", model);
            }
        }

        // ======================
        // FEES / SURCHARGES
        // ======================
        public decimal GetFee(string feeName)
        {
            using (var conn = _context.CreateConnection())
            {
                var result = conn.QueryFirstOrDefault<string>("SELECT SettingValue FROM BUSINESS_SETTINGS WHERE SettingKey=@feeName", new { feeName });
                return decimal.TryParse(result, out var value) ? value : 0;
            }
        }

        public void UpdateFee(string feeName, decimal amount)
        {
            using (var conn = _context.CreateConnection())
            {
                conn.Execute("UPDATE BUSINESS_SETTINGS SET SettingValue=@amount WHERE SettingKey=@feeName", new { feeName, amount });
            }
        }
    }
}
