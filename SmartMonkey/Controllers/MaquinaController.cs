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
            ViewBag.idInstituicao = new SelectList(db.Instituicao, "idInstituicao", "nomeFantasia");
            return View();
        }

        // POST: Maquina/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "modelo,processador,memoriaRam,discoRigido,idInstituicao")] Maquina maquina)
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

        public ActionResult Delimitadores()
        {
            ViewBag.listaInstituicao = new SelectList(db.Instituicao.ToList(), "idInstituicao", "nomeFantasia");
            ViewBag.listaMaquina = new SelectList(db.Maquina.ToList(),"idMaquina","modelo");
            return View();
        }

        [HttpPost]
        public ActionResult buscarMaquinaPorInstituicao(int idInstituicao)
        {
            List<Maquina> maquinas = new List<Maquina>();
            maquinas = db.Maquina.Where(m => m.idInstituicao == idInstituicao).ToList();
            SelectList maquina = new SelectList(maquinas, "idMaquina", "modelo");
            return Json(maquina);
        }

        [HttpPost]
        public ActionResult buscarDelimitadoresPorMaquina(int idMaquina)
        {
            System.Diagnostics.Debug.WriteLine(idMaquina);
            List<double> delimitadores = new List<double>();
            Maquina maquina = new Maquina();
            maquina = db.Maquina.Find(idMaquina);
            if(maquina.delimitCpu != null && maquina.delimitRam != null && maquina.delimitHd != null)
            {
                delimitadores.Add(maquina.delimitCpu.Value);
                delimitadores.Add(maquina.delimitRam.Value);
                delimitadores.Add(maquina.delimitHd.Value);

                System.Diagnostics.Debug.WriteLine("AOOOO COWBOY DE ESTRADA");
                System.Diagnostics.Debug.WriteLine("ID MAQUINA = " + maquina.idMaquina);
                System.Diagnostics.Debug.WriteLine("CPU: " + maquina.delimitCpu + " / HD: " + maquina.delimitHd + " / RAM: " + maquina.delimitRam);

            }

            return Json(delimitadores);

        }

        [HttpPost]
        public ActionResult salvarDelimitadores([Bind(Include = "idMaquina, delimitCpu, delimitRam, delimitHd")]Maquina maquina)
        {
            Maquina maquinaEditada = db.Maquina
                .Where(w => w.idMaquina == maquina.idMaquina)
                .SingleOrDefault();
            System.Diagnostics.Debug.WriteLine("ID MAQUINA = " + maquina.idMaquina);

            if (maquinaEditada != null)
            {
                System.Diagnostics.Debug.WriteLine("máquinaEditada não é nulo ---------");

                System.Diagnostics.Debug.WriteLine("CPU: "+maquina.delimitCpu+" / HD: "+maquina.delimitHd+" / RAM: "+maquina.delimitRam);
                maquinaEditada.delimitCpu = maquina.delimitCpu;
                maquinaEditada.delimitHd = maquina.delimitHd;
                maquinaEditada.delimitRam = maquina.delimitRam;

                if (ModelState.IsValid)
                {
                    db.Entry(maquinaEditada).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }

                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }

                
    }
        

