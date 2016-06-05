// ***********************************************************************
// Assembly         : NALOrder
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

using NALOrder.Model;
using NALOrder.Utilities;
using NALOrder.ViewModel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NALOrder.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            OrderViewModel order = new OrderViewModel();
            order.Products = ProductRepos.GetAll();
            order.Countries = CountryRepos.GetAll();

            //test 
            OrderDetailViewModel orderDetail = new OrderDetailViewModel();
            orderDetail.ProductId = 0;
            orderDetail.Quality = 0;
            orderDetail.Products = order.Products;
            //end test
            order.OrderDetail = new List<OrderDetailViewModel> { orderDetail };

            return View(order);
        }

        [HttpPost]
        public ActionResult Index(OrderViewModel order)
        {
            ViewBag.Error = "Error";

            if (ModelState.IsValid)
            {
                try
                {
                    var saveCustomer = CustomerRepos.AddCustomer(new CustomerDto()
                       {
                           Address = order.Address,
                           CountryId = order.CountryId,
                           Email = order.Email,
                           Name = order.Name,
                           Phone = order.Phone
                       });

                    var saveOrder = OrderRepos.AddOrder(new OrderDto
                    {
                        CustomerId = saveCustomer.ID,
                        OrderDate = DateTime.Now,
                        Status = StatusType.NEW,
                    });

                    //Remove first order
                    if (order.OrderDetail.Count > 0)
                        order.OrderDetail.RemoveAt(0);

                    if (order.OrderDetail.Count > 0)
                        foreach (var item in order.OrderDetail)
                        {
                            decimal price = ProductRepos.GetPrice(item.ProductId);

                            var saveOrderDetail = OrderDetailRepos.Add(new OrderDetailDto
                            {
                                OrderId = saveOrder.ID,
                                ProductId = 1,
                                Quantity = 1,
                                UnitPrice = price,
                            });
                        }
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message, ex);
                    return View();
                }

                //Return page successfully
                return RedirectToAction("ResultOrder", new { status = true });// View(order);
            }

            return View();
        }

        [HttpGet]
        public PartialViewResult NewOrderDetail(int max, int no)
        {
            OrderDetailViewModel order = new OrderDetailViewModel();
            var products = ProductRepos.GetAll();
            order.Products = products;
            order.Max = max;
            order.No = no;

            return PartialView("NewOrderDetail", order);
        }

        [HttpGet]
        public JsonResult GetPrice(int id)
        {
            var product = ProductRepos.Single(id);
            if (product != null)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { data = product.UnitPrice }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet, ActionName("ResultOrder")]
        public ActionResult ResultOrder(bool status)
        {
            return View(status);
        }

        #region
        [Inject]
        public ILogService LogService { get; set; }

        [Inject]
        public ICustomerRepository CustomerRepos { get; set; }

        [Inject]
        public IProductRepository ProductRepos { get; set; }

        [Inject]
        public ICountryRepository CountryRepos { get; set; }

        [Inject]
        public IOrderDetailRepository OrderDetailRepos { get; set; }

        [Inject]
        public IOrderRepository OrderRepos { get; set; }
        #endregion
    }
}