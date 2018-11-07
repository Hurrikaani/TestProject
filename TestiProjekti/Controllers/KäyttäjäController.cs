using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestiProjekti.DBModel;
using TestiProjekti.Models;

namespace TestiProjekti.Controllers
{
    public class KäyttäjäController : Controller
    {

        TestiKantaEntities1 entities = new TestiKantaEntities1();
        // GET: Käyttäjä
        public ActionResult Index()
        {
            return View();
        }
        //Rekisteröinti osio
        public ActionResult Register()
        {
            // Luodaan olio "käyttäjä"
            KäyttäjäMalli käyttäjä = new KäyttäjäMalli();
            //palautetaan olio
            return View(käyttäjä);
        }
        //Post osio
        [HttpPost]
        public ActionResult Register(KäyttäjäMalli käyttäjä)
        {
            
            if (ModelState.IsValid)
            {
                //Jos email ei ole sama niin tiedot tallennetaan kantaan
                if (!entities.Käyttäjät.Any(m => m.Email == käyttäjä.Email))
                {

                    Käyttäjät käyt = new DBModel.Käyttäjät();
                    käyt.Luotu = DateTime.Now;
                    käyt.Email = käyttäjä.Email;
                    käyt.Etunimi = käyttäjä.Etunimi;
                    käyt.Sukunimi = käyttäjä.Sukunimi;
                    käyt.Salasana = käyttäjä.Salasana;
                    entities.Käyttäjät.Add(käyt);
                    entities.SaveChanges();
                    //Jos kaikki menee hyvin niin käyttäjä rekisteröidään tietokantaan
                    ViewBag.success = "Tiedot tallennettu kantaan";
                    käyttäjä.HyväksyttyViesti = "Tiedot lisätty tietrokantaan onnistuneesti";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Ilmoitus käyttäjälle mikäli email on varattu
                    ModelState.AddModelError("Error", "Email on jo käytössä");
                    return View();
                }
            }
            //Palautetaan Register näkymä
            return View("Register");
        }

        //Loggaus osio
        public ActionResult Login()
        {
            //Luodaan olio "LogModel"
            LoginModel LogModel = new LoginModel();
            //Palautetaan olio
            return View(LogModel);
        }
        [HttpPost]
        public ActionResult Login(LoginModel LogModel)
        {
            //JOs kaikki onnistuu  ja email / salsana täsmää niin aloitetaan istunto ja siirrytään index sivustolle
            if (ModelState.IsValid)
            {
                if(entities.Käyttäjät.Where(m => m.Email == LogModel.Email && m.Salasana == LogModel.Salasana).FirstOrDefault() == null)
                {
                    ModelState.AddModelError("Error", "Email ja salasana eivät täsmää");
                    return View();
                }
                else
                {
                    Session["Email"] = LogModel.Email;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        //Uloskirjautuminen
        public ActionResult Logout()
        {
           //Suljetaan istunto ja siirrytään index sivulle
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
    }
}