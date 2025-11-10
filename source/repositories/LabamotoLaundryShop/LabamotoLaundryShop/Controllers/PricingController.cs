using iTextSharp.text;
using iTextSharp.text.pdf;
using LabamotoLaundryShop.Models;
using LabamotoLaundryShop.Repositories.Implementations;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LabamotoLaundryShop.Controllers
{
    public class PricingController : Controller
    {
        private readonly PricingRepository _repo;

        public PricingController()
        {
            _repo = new PricingRepository(new Data.DapperContext());
        }

        // ==========================
        // INDEX
        // ==========================
        public ActionResult Index(string currentAction = "RegularLaundry")
        {
            var model = new PricingViewModel
            {
                RegularLaundry = _repo.GetAllRegularLaundry(),
                SpecialItems = _repo.GetAllSpecialItems(),
                DryClean = _repo.GetAllDryClean(),
                AddOns = _repo.GetAllAddOns(),
                RushServiceFee = _repo.GetFee("RushService"),
                PickupFee = _repo.GetFee("PickupFee"),
                Discount = _repo.GetFee("Discount"),
                VAT = _repo.GetFee("VAT")
            };
            ViewBag.CurrentAction = currentAction;
            return View(model);
        }

        // ==========================
        // TAB ACTIONS (to fix 404 errors)
        // ==========================
        public ActionResult RegularLaundry()
        {
            var model = new PricingViewModel
            {
                RegularLaundry = _repo.GetAllRegularLaundry(),
                SpecialItems = _repo.GetAllSpecialItems(),
                DryClean = _repo.GetAllDryClean(),
                AddOns = _repo.GetAllAddOns(),
                RushServiceFee = _repo.GetFee("RushService"),
                PickupFee = _repo.GetFee("PickupFee"),
                Discount = _repo.GetFee("Discount"),
                VAT = _repo.GetFee("VAT")
            };
            return View("Index", model); // Reuse Index.cshtml
        }

        public ActionResult SpecialItems()
        {
            var model = new PricingViewModel
            {
                RegularLaundry = _repo.GetAllRegularLaundry(),
                SpecialItems = _repo.GetAllSpecialItems(),
                DryClean = _repo.GetAllDryClean(),
                AddOns = _repo.GetAllAddOns(),
                RushServiceFee = _repo.GetFee("RushService"),
                PickupFee = _repo.GetFee("PickupFee"),
                Discount = _repo.GetFee("Discount"),
                VAT = _repo.GetFee("VAT")
            };
            return View("Index", model);
        }

        public ActionResult DryClean()
        {
            var model = new PricingViewModel
            {
                RegularLaundry = _repo.GetAllRegularLaundry(),
                SpecialItems = _repo.GetAllSpecialItems(),
                DryClean = _repo.GetAllDryClean(),
                AddOns = _repo.GetAllAddOns(),
                RushServiceFee = _repo.GetFee("RushService"),
                PickupFee = _repo.GetFee("PickupFee"),
                Discount = _repo.GetFee("Discount"),
                VAT = _repo.GetFee("VAT")
            };
            return View("Index", model);
        }

        public ActionResult AddOns()
        {
            var model = new PricingViewModel
            {
                RegularLaundry = _repo.GetAllRegularLaundry(),
                SpecialItems = _repo.GetAllSpecialItems(),
                DryClean = _repo.GetAllDryClean(),
                AddOns = _repo.GetAllAddOns(),
                RushServiceFee = _repo.GetFee("RushService"),
                PickupFee = _repo.GetFee("PickupFee"),
                Discount = _repo.GetFee("Discount"),
                VAT = _repo.GetFee("VAT")
            };
            return View("Index", model);
        }

        // ==========================
        // REGULAR LAUNDRY CRUD
        // ==========================
        [HttpPost]
        public ActionResult AddPackage(PricingPackage model)
        {
            if (ModelState.IsValid)
                _repo.AddRegular(model);
            return RedirectToAction("Index", new { actionType = "RegularLaundry" });
        }

        [HttpPost]
        public ActionResult EditPackage(PricingPackage model)
        {
            if (ModelState.IsValid)
                _repo.EditRegular(model);
            return RedirectToAction("Index", new { actionType = "RegularLaundry" });
        }


        [HttpGet]
        public ActionResult TogglePackageStatus(int PackageID)
        {
            _repo.ToggleRegularStatus(PackageID);
            TempData["SuccessMessage"] = "Package updated successfully!";
            return RedirectToAction("Index", new { actionType = "RegularLaundry" });
        }

        // ==========================
        // SPECIAL ITEMS CRUD
        // ==========================
        [HttpPost]
        public ActionResult AddSpecialItem(SpecialItem item)
        {
            if (ModelState.IsValid)
                _repo.AddSpecialItem(item);
            return RedirectToAction("Index", new { currentAction = "SpecialItems" });
        }
        [HttpPost]
        public ActionResult EditSpecialItem(SpecialItem model)
        {
            if (ModelState.IsValid)
                _repo.UpdateSpecialItem(model);
            return RedirectToAction("Index", new { currentAction = "SpecialItems" });
        }

        [HttpPost]
        public ActionResult DeleteSpecialItem(int SpecialItemID)
        {
            _repo.DeleteSpecialItem(SpecialItemID);
            return RedirectToAction("Index", new { currentAction = "SpecialItems" });
        }

        // ==========================
        // DRY CLEAN CRUD
        // ==========================
        [HttpPost]
        public ActionResult AddDryClean(DryCleanItem item)
        {
            if (ModelState.IsValid)
                _repo.AddDryCleanItem(item);
            return RedirectToAction("Index", new { currentAction = "DryClean" });
        }
        [HttpPost]
        public ActionResult EditDryClean(DryCleanItem model)
        {
            if (ModelState.IsValid)
                _repo.UpdateDryClean(model);
            return RedirectToAction("Index", new { currentAction = "DryClean" });
        }

        // ==========================
        // ADD-ONS CRUD
        // ==========================
        [HttpPost]
        public ActionResult AddAddOn(AddOnItem item)
        {
            if (ModelState.IsValid)
                _repo.AddAddOn(item);
            return RedirectToAction("Index", new { currentAction = "AddOns" });
        }
        [HttpPost]
        public ActionResult EditAddOn(AddOnItem model)
        {
            if (ModelState.IsValid)
                _repo.UpdateAddOn(model);
            return RedirectToAction("Index", new { currentAction = "AddOns" });
        }

        // ==========================
        // FEES / SURCHARGES
        // ==========================
        //[HttpPost]
        //public ActionResult UpdateFees(decimal RushServiceFee, decimal PickupFee, decimal Discount, decimal VAT)
        //{
        //_repo.UpdateFee("RushService", RushServiceFee);
        //_repo.UpdateFee("PickupFee", PickupFee);
        //_repo.UpdateFee("Discount", Discount);
        //_repo.UpdateFee("VAT", VAT);
        //return RedirectToAction("Index", new { currentAction = "Fees" }); // Optional: if you have a fees tab
        //}
        [HttpPost]
        public ActionResult UpdateFees(decimal RushServiceFee, decimal PickupFee, decimal FreeDeliveryMinimum, decimal Discount, decimal  LoyaltyDiscount, decimal VAT)
        {
            _repo.UpdateFee("RushService", RushServiceFee);
            _repo.UpdateFee("PickupFee", PickupFee);
            _repo.UpdateFee("DeliveryMinimum", FreeDeliveryMinimum);
            _repo.UpdateFee("Discount", Discount);
            _repo.UpdateFee("LoyaltyDiscount", LoyaltyDiscount);
            _repo.UpdateFee("VAT", VAT);

            TempData["SuccessMessage"] = "Fees updated successfully!";
            return RedirectToAction("Index", new { currentAction = "AddOns" });
        }

        // ========================
        // EXPORT PRICING AS PDF
        // ========================
        public ActionResult ExportPDF()
        {
            var pricingData = _repo.GetAllPricing(); // You can adjust this method based on your repo

            using (MemoryStream stream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                pdfDoc.Add(new Paragraph("Labamoto Laundry Shop - Pricing List"));
                pdfDoc.Add(new Paragraph("Generated on: " + System.DateTime.Now));
                pdfDoc.Add(new Paragraph("\n"));

                PdfPTable table = new PdfPTable(3);
                table.AddCell("Service Name");
                table.AddCell("Price");
                

                foreach (var item in pricingData)
                {
                    table.AddCell(item.PackageName);
                    table.AddCell(item.PricePerKg.ToString("C"));
                    
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                return File(stream.ToArray(), "application/pdf", "PricingList.pdf");
            }
        }

        // ========================
        // IMPORT PRICING FROM CSV
        // ========================
        [HttpPost]
        public ActionResult ImportCSV(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                using (var reader = new StreamReader(file.InputStream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        // Example: assuming CSV columns = ServiceName, Price, Category
                        var pricingItem = new PricingPackage
                        {
                            PackageName = values[0],
                            PricePerKg = decimal.Parse(values[1])
                            
                        };

                        _repo.AddOrUpdatePricing(pricingItem);
                    }
                }

                TempData["SuccessMessage"] = "Pricing imported successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Please select a CSV file to import.";
            }

            return RedirectToAction("Index");
        }
    }
}
