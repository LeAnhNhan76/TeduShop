using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeduShop.Common.Constants;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        #region Variables and Properties

        private IProductService _productService;
        private ApplicationUserManager _userManager;
        private IOrderService _orderService;

        #endregion Variables and Properties

        #region Constructors

        public ShoppingCartController(IProductService productService
            , ApplicationUserManager userManager
            , IOrderService orderService
            , IErrorService errorService) : base(errorService)
        {
            this._productService = productService;
            UserManager = userManager;
            this._orderService = orderService;
        }

        #endregion Constructors

        #region Methods

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            if (Session[Constant.Session_ShoppingCart] == null)
                Session[Constant.Session_ShoppingCart] = new List<ShoppingCartViewModel>();
            return View();
        }

        public JsonResult GetAll()
        {
            if (Session[Constant.Session_ShoppingCart] == null)
                Session[Constant.Session_ShoppingCart] = new List<ShoppingCartViewModel>();
            var cart = (List<ShoppingCartViewModel>)Session[Constant.Session_ShoppingCart];
            return Json(new
            {
                data = cart,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[Constant.Session_ShoppingCart];
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                var product = _productService.GetById(productId);
                newItem.Product = Mapper.Map<ProductViewModel>(product);
                newItem.Quantity = 1;
                cart.Add(newItem);
            }

            Session[Constant.Session_ShoppingCart] = cart;
            return Json(new
            {
                status = true,
                countInCart = cart.Count
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);

            var cartSession = (List<ShoppingCartViewModel>)Session[Constant.Session_ShoppingCart];
            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    if (item.ProductId == jitem.ProductId)
                    {
                        item.Quantity = jitem.Quantity;
                    }
                }
            }

            Session[Constant.Session_ShoppingCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[Constant.Session_ShoppingCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[Constant.Session_ShoppingCart] = cartSession;
                return Json(new
                {
                    status = true,
                    countInCart = cartSession.Count
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[Constant.Session_ShoppingCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            if (Session[Constant.Session_ShoppingCart] == null)
            {
                return Redirect("/gio-hang.html");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new {
                    data = user,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateOrder(string orderVM)
        {
            try
            {
                if (!string.IsNullOrEmpty(orderVM))
                {
                    var model = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderVM);
                    var orderNew = Mapper.Map<Order>(model);
                    orderNew.PaymentStatus = Constant.PaymentStatus_Pending;
                    orderNew.CreatedDate = DateTime.Now;
                    orderNew.CreatedBy = User.Identity.Name;
                    if (Request.IsAuthenticated)
                    {
                        orderNew.CustomerId = User.Identity.GetUserId();
                        orderNew.CreatedBy = User.Identity.GetUserName();
                    }
                    var cart = (List<ShoppingCartViewModel>)Session[Constant.Session_ShoppingCart];
                    List<OrderDetail> orderDetails = new List<OrderDetail>();
                    foreach (var item in cart)
                    {
                        var detail = new OrderDetail()
                        {
                            ProductID = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = item.Product.Promotion.HasValue ? item.Product.Promotion : item.Product.Price
                        };
                        orderDetails.Add(detail);
                    }
                    if(_orderService.Create(orderNew, orderDetails))
                    {
                        Session[Constant.Session_ShoppingCart] = null;
                        return Json(new
                        {
                            status = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                LogError(ex);
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Methods
    }
}