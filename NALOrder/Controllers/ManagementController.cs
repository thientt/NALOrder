using NALOrder.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NALOrder.ViewModel;
using PagedList;

namespace NALOrder.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagementController : Controller
    {
        // GET: Management
        public ActionResult Index(int? page, string sortOrder = "DateSortOrder")
        {
            FilterPageViewModel obj = new FilterPageViewModel
            {
                SortOrder = sortOrder,
                Page = page,
            };
            return View(obj);
        }

        public PartialViewResult GetOrder(string sortOrder, int? page)
        {
            ViewBag.DateSortOrder = sortOrder == "DateSortOrder" ? "DateSortOrder_Desc" : "DateSortOrder";
            ViewBag.CustomerSortOrder = sortOrder == "CustomerSortOrder" ? "CustomerSortOrder_Desc" : "CustomerSortOrder";
            ViewBag.CurrentSort = sortOrder;

            List<ManagementOrderViewModel> data = new List<ManagementOrderViewModel>();

            //Get order
            var orders = OrderRepos.GetAll();
            foreach (var order in orders)
            {
                var total = OrderDetailRepos.TotalOrder(order.ID);
                data.Add(new ManagementOrderViewModel
                {
                    Status = order.Status,
                    Country = order.Customer.Country.Name,
                    Customer = order.Customer.Name,
                    OrderDate = order.OrderDate,
                    Id = order.ID,
                    Total = total
                });
            }
            var dashboards = SortOrder(sortOrder, data);
            int pageNumber = page ?? 1;
            return PartialView(dashboards.ToPagedList(pageNumber, 25));
        }

        [HttpGet]
        public ActionResult OrderDetail(int id)
        {
            var data = GetInforOrder(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult OrderDetail(int id, string submit)
        {
            var status = String.Empty;

            switch (submit)
            {
                case "Reject":
                    status = StatusType.REJECTED;
                    break;
                case "Approve":
                    status = StatusType.APPROVED;
                    break;

            }
            var result = OrderRepos.UpdateStatus(id, status);

            TempData["Type"] = status;
            if (result == SaveResult.SUCCESS)
                TempData["Status"] = "OK";

            else
                TempData["Status"] = "FAIL";

            var data = GetInforOrder(id);

            return View(data);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="masters"></param>
        /// <returns></returns>
        private List<ManagementOrderViewModel> SortOrder(string sortOrder, List<ManagementOrderViewModel> masters)
        {
            switch (sortOrder)
            {
                case "DateSortOrder":
                    return masters.OrderBy(x => x.OrderDate).ToList();
                case "DateSortOrder_Desc":
                    return masters.OrderByDescending(x => x.OrderDate).ToList();
                case "CustomerSortOrder":
                    return masters.OrderBy(x => x.Customer).ToList();
                case "CustomerSortOrder_Desc":
                    return masters.OrderByDescending(x => x.Customer).ToList();
                default:
                    return masters;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private OrderViewModel GetInforOrder(int id)
        {
            //get customer
            OrderDto order = OrderRepos.Single(id);

            OrderViewModel data = new OrderViewModel();

            //Information customer
            data.Id = id;
            data.Name = order.Customer.Name;
            data.Address = order.Customer.Address;
            data.Email = order.Customer.Email;
            data.Phone = order.Customer.Phone;
            data.CounntryName = order.Customer.Country.Name;
            data.DateOrder = order.OrderDate;
            data.Status = order.Status;

            //Information order detail
            List<OrderDetailDto> orderDetail = OrderDetailRepos.GetByOrder(id).ToList();
            if (data.OrderDetail == null)
                data.OrderDetail = new List<OrderDetailViewModel>();
            for (int i = 0; i < orderDetail.Count(); i++)
            {
                var item = orderDetail[i];
                data.OrderDetail.Add(new OrderDetailViewModel
                {
                    No = i + 1,
                    Quality = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    ProductName = item.Product.ProductName,
                    Total = item.Total,
                });
            }

            return data;
        }

        [Inject]
        public IOrderRepository OrderRepos { get; set; }

        [Inject]
        public IOrderDetailRepository OrderDetailRepos { get; set; }

        [Inject]
        public ICustomerRepository CustomerRepos { get; set; }
    }
}