using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coffe.Dal;
using Coffe.Models;

namespace Coffe.Controllers
{
    public class OrdersController : Controller
    {
        private OrderDal db = new OrderDal();
        private ItemsDal idb = new ItemsDal();
        private UserDal udb = new UserDal();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Userid,Itemid,Chair,Price,IsDone")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }
        public ActionResult CreateForClient([Bind(Include = "Id,Userid,Itemid,Chair,Price,IsDone")] Order order)
        {
            bool check = true;
            if (ModelState.IsValid)
            {
                foreach(Order o in db.Orders.ToList())
                {
                    if(o.Chair == order.Chair)
                    {
                        check = false;
                    }
                }
                if (!check && order.Chair != 0)
                {
                    ViewBag.m = "Chair is already taken!";
                    return View("EditClient",order);
                }
                Items items = idb.Items.Find(order.Itemid);
                order.Price = items.price;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View("EditClient",order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        public ActionResult EditOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Userid,Itemid,Chair,Price,IsDone")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                User a = udb.Users.Find(Session["id"]);
                if (a.isAdmin)
                    return RedirectToAction("Index");
                return RedirectToAction("Cart", a.Uid);
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            User a = udb.Users.Find(Session["id"]);
            if(a.isAdmin)
                return RedirectToAction("Index");
            return RedirectToAction("Cart",a.Uid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult CreateOrder(int? id, string username)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Items item = idb.Items.Find(id);
            User user = udb.Users.Where(x => x.Username == username).FirstOrDefault();

            Order order = new Order() { Itemid = item.Id, Price = item.price, Userid = user.Uid };
            return View("EditClient", order);
        }
        public ActionResult Cart(string id)
        {
            if(id == null)
            {
                id = Session["id"].ToString(); 
            }
            int i = int.Parse(id);
            return View(db.Orders.Where(x => x.Userid == i).ToList());
        }
        
    }
}
