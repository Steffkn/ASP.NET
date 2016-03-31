namespace DDS.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using DDS.Data.Models;
    using Services.Data;

    public class DiplomasController : Controller
    {
        private readonly IDiplomasService diplomas;

        public DiplomasController(IDiplomasService diplomas)
        {
            this.diplomas = diplomas;
        }

        // GET: Diplomas
        public ActionResult Index()
        {
            return this.View(this.diplomas.GetAll());
        }

        // GET: Diplomas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int intId = id ?? 0;
            Diploma diploma = this.diplomas.GetById(intId);

            if (diploma == null)
            {
                return this.HttpNotFound();
            }

            return this.View(diploma);
        }

        // GET: Diplomas/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Diplomas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description")] Diploma diploma)
        {
            if (this.ModelState.IsValid)
            {
                this.diplomas.Create(diploma);
                return this.RedirectToAction("Index");
            }

            return this.View(diploma);
        }

        // GET: Diplomas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int intId = id ?? 0;
            Diploma diploma = this.diplomas.GetById(intId);
            if (diploma == null)
            {
                return this.HttpNotFound();
            }

            return this.View(diploma);
        }

        // POST: Diplomas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsDeleted,Description")] Diploma diploma)
        {
            if (this.ModelState.IsValid)
            {
                this.diplomas.Edit(diploma);
                return this.RedirectToAction("Index");
            }

            return this.View(diploma);
        }

        // GET: Diplomas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int intId = id ?? 0;
            Diploma diploma = this.diplomas.GetById(intId);
            if (diploma == null)
            {
                return this.HttpNotFound();
            }

            return this.View(diploma);
        }

        // POST: Diplomas/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diploma diploma = this.diplomas.GetById(id);
            this.diplomas.Delete(diploma);
            return this.RedirectToAction("Index");
        }

        // protected override void Dispose(bool disposing)
        // {
        //     if (disposing)
        //     {
        //         this.diplomas.Dispose();
        //     }
        //     base.Dispose(disposing);
        // }
    }
}
