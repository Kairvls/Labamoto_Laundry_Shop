using LabamotoLaundryShop.Models;
using LabamotoLaundryShop.Repositories.Implementations;
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
        public ActionResult Index()
        {
            return RedirectToAction("RegularLaundry");
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditPackage(PricingPackage model)
        {
            if (ModelState.IsValid)
                _repo.UpdateRegular(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult TogglePackageStatus(int PackageID)
        {
            _repo.ToggleRegularStatus(PackageID);
            return RedirectToAction("Index");
        }

        // ==========================
        // SPECIAL ITEMS CRUD
        // ==========================
        [HttpPost]
        public ActionResult AddSpecialItem(SpecialItem item)
        {
            if (ModelState.IsValid)
            {
                _repo.AddSpecialItem(item);
                return RedirectToAction("SpecialItems");
            }
            return RedirectToAction("SpecialItems");
        }
        [HttpPost]
        public ActionResult EditSpecialItem(SpecialItem model)
        {
            if (ModelState.IsValid)
                _repo.UpdateSpecialItem(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteSpecialItem(int SpecialItemID)
        {
            _repo.DeleteSpecialItem(SpecialItemID);
            return RedirectToAction("Index");
        }

        // ==========================
        // DRY CLEAN CRUD
        // ==========================
        [HttpPost]
        public ActionResult AddDryClean(DryCleanItem item)
        {
            if (ModelState.IsValid)
            {
                _repo.AddDryCleanItem(item);
                return RedirectToAction("DryClean");
            }
            return RedirectToAction("DryClean");
        }
        [HttpPost]
        public ActionResult EditDryClean(DryCleanItem model)
        {
            if (ModelState.IsValid)
                _repo.UpdateDryClean(model);
            return RedirectToAction("Index");
        }

        // ==========================
        // ADD-ONS CRUD
        // ==========================
        [HttpPost]
        public ActionResult AddAddOn(AddOnItem item)
        {
            if (ModelState.IsValid)
            {
                _repo.AddAddOn(item);
                return RedirectToAction("AddOns");
            }
            return RedirectToAction("AddOns");
        }
        [HttpPost]
        public ActionResult EditAddOn(AddOnItem model)
        {
            if (ModelState.IsValid)
                _repo.UpdateAddOn(model);
            return RedirectToAction("Index");
        }

        // ==========================
        // FEES / SURCHARGES
        // ==========================
        [HttpPost]
        public ActionResult UpdateFees(decimal RushServiceFee, decimal PickupFee, decimal Discount, decimal VAT)
        {
            _repo.UpdateFee("RushService", RushServiceFee);
            _repo.UpdateFee("PickupFee", PickupFee);
            _repo.UpdateFee("Discount", Discount);
            _repo.UpdateFee("VAT", VAT);
            return RedirectToAction("Index");
        }
    }
}
