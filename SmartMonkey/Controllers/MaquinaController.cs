using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartMonkey;

namespace SmartMonkey.Controllers
{
    public class MaquinaController : Controller
    {
        private Entities db = new Entities();

        // GET: Maquina
        public ActionResult Index()
        {
            var maquina = db.Maquina.Include(m => m.Instituicao);
            return View(maquina.ToList());
        }

        // GET: Maquina/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        // GET: Maquina/Create
        public ActionResult Create()
        {
            ViewBag.idInstituicao = new SelectList(db.Instituicao, "idInstituicao", "razaoSocial");
            return View();
        }

        // POST: Maquina/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "modelo,processador,memoriaRam,discoRigido,delimitCpu,delimitRam,delimitHd,idInstituicao")] Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                db.Maquina.Add(maquina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idInstituicao = new SelectList(db.Instituicao, "idInstituicao", "razaoSocial", maquina.idInstituicao);
            return View(maquina);
        }

        // GET: Maquina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            ViewBag.idInstituicao = new SelectList(db.Instituicao, "idInstituicao", "razaoSocial", maquina.idInstituicao);
            return View(maquina);
        }

        // POST: Maquina/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMaquina,modelo,processador,memoriaRam,discoRigido,delimitCpu,delimitRam,delimitHd,idInstituicao")] Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idInstituicao = new SelectList(db.Instituicao, "idInstituicao", "razaoSocial", maquina.idInstituicao);
            return View(maquina);
        }

        // GET: Maquina/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        // POST: Maquina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maquina maquina = db.Maquina.Find(id);
            db.Maquina.Remove(maquina);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
