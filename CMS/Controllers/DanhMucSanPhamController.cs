﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("danh-muc-san-pham")]
    public class DanhMucSanPhamController : Controller
    {
        private LioaEntities db = new LioaEntities();

        // GET: DanhMucSanPham
        [Route()]
        public ActionResult Index()
        {
            ViewBag.Title = "Danh mục sản phẩm";

            //Check lượt truy cập
            ClientAccess client = new ClientAccess()
            {
                ipClient = Server.HtmlEncode(Request.UserHostAddress).ToString(),
                time = DateTime.Now
            };

            var temp = db.ClientAccess.Where(p => p.ipClient == client.ipClient && p.time.Value.Day == client.time.Value.Day && p.time.Value.Month == client.time.Value.Month && p.time.Value.Year == client.time.Value.Year);

            if (temp.Count() < 2)
            {
                db.ClientAccess.Add(client);
            }
            else
            {
                db.ClientAccess.Remove(temp.FirstOrDefault());
                db.ClientAccess.Add(client);
            }

            db.SaveChanges();

            return View();
        }

        [Route("{alias}-{id:int}")]
        public ActionResult Show(string alias, int id)
        {
            var model = db.CategoryProduct.Where(p => p.idCategory == id && p.alias == alias).FirstOrDefault();

            if (model == null)
            {
                return HttpNotFound();
            }

            //SEO
            ViewBag.Title = model.title;
            ViewBag.Description = model.metadescription;
            ViewBag.Keywords = model.metakewords;
            ViewBag.Robots = model.robots;
            ViewBag.Image = model.image;

            //Check lượt truy cập
            ClientAccess client = new ClientAccess()
            {
                ipClient = Server.HtmlEncode(Request.UserHostAddress).ToString(),
                time = DateTime.Now
            };

            var temp = db.ClientAccess.Where(p => p.ipClient == client.ipClient && p.time.Value.Day == client.time.Value.Day && p.time.Value.Month == client.time.Value.Month && p.time.Value.Year == client.time.Value.Year);

            if (temp.Count() < 2)
            {
                db.ClientAccess.Add(client);
            }
            else
            {
                db.ClientAccess.Remove(temp.FirstOrDefault());
                db.ClientAccess.Add(client);
            }

            db.SaveChanges();

            return View(model);
        }

    }
}