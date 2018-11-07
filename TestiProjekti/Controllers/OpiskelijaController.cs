using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestiProjekti.Models;

namespace TestiProjekti.Controllers
{
    public class OpiskelijaController : Controller
    {

        // GET: Opiskelija
        //Listaus Opiskelijoista
        TestiKantaEntities entities;
        public ActionResult Index()
        {
            entities = new TestiKantaEntities();
            IEnumerable<OpiskelijaMalli> OpiskelijaLIstaus =
                (
                from Listaus in entities.Opiskelija
                select new OpiskelijaMalli
                {
                    OpiskelijaID = Listaus.OpiskelijaID,
                    Koodi = Listaus.Koodi,
                    Nimi = Listaus.Nimi,
                    Ikä = Listaus.Ikä,
                    Osoite = Listaus.Osoite,
                    Maa = Listaus.Maa,
                    Email = Listaus.Email,
                    Postinumero = Listaus.Postinumero
                }).ToList();
            return View(OpiskelijaLIstaus);
        }

        public ActionResult Create()
        {
            return View(new OpiskelijaMalli());
        }

        //Opiskelian lisääminen
        [HttpPost]
        public ActionResult Create(OpiskelijaMalli oppMalli)
        {
            if (ModelState.IsValid)
            {
                entities = new TestiKantaEntities();

                //Opiskelijakoodin tarkistus, koodi on uniikki
                bool KoodiOlemassa = entities.Opiskelija.Any(m => m.Koodi == oppMalli.Koodi);
                if (KoodiOlemassa)
                {
                    //Mikäli koodi on jo käytössä niin käyttäjä saa siitä ilmoituksen
                    ModelState.AddModelError("Koodi", string.Format("Koodi: {0} on jo olemassa", oppMalli.Koodi));
                    return View(oppMalli);
                }
                //Jos kaikki menee hyvin, niin tiedot tallennetaan tietokantaan

                Opiskelija Opp = new Opiskelija();
                Opp.OpiskelijaID = oppMalli.OpiskelijaID;
                Opp.Koodi = oppMalli.Koodi;
                Opp.Nimi = oppMalli.Nimi;
                Opp.Ikä = oppMalli.Ikä;
                Opp.Osoite = oppMalli.Osoite;
                Opp.Maa = oppMalli.Maa;
                Opp.Email = oppMalli.Email;
                Opp.Postinumero = oppMalli.Postinumero;
                entities.Opiskelija.Add(Opp);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(oppMalli);
            }
        }

        //Muokkaus osio
        public ActionResult Muokkaa(int? OpiskelijaID)
        {
            entities = new TestiKantaEntities();

            //Tarkistetaan löytyykö id:tä

            bool OnkoOlemassa = entities.Opiskelija.Any(m => m.OpiskelijaID == OpiskelijaID);

            //Jos id:tä ei löydy käyttäjä saa viestin "Ei löytynyt"
            if(!OnkoOlemassa)
            {
                return View("Ei löytynyt");
            }

            OpiskelijaMalli OppMalli = (from op in entities.Opiskelija
                                        where op.OpiskelijaID == OpiskelijaID
                                        select new OpiskelijaMalli
                                        {
                                            OpiskelijaID = op.OpiskelijaID,
                                            Koodi = op.Koodi,
                                            Nimi = op.Nimi,
                                            Ikä = op.Ikä,
                                            Osoite = op.Osoite,
                                            Maa = op.Maa,
                                            Email = op.Email,
                                            Postinumero = op.Postinumero
                                        }).FirstOrDefault();


            return View(OppMalli);
        }

        //Lähetetään muokatut tiedot
        [HttpPost]
        public ActionResult Muokkaa(OpiskelijaMalli oppMalli)
        {
            if (ModelState.IsValid)
            {
                entities = new TestiKantaEntities();

                Opiskelija Oppi = (from op in entities.Opiskelija
                                   where op.OpiskelijaID == oppMalli.OpiskelijaID
                                   select op).FirstOrDefault();

                Oppi.Koodi = oppMalli.Koodi;
                Oppi.Nimi = oppMalli.Nimi;
                Oppi.Ikä = oppMalli.Ikä;
                Oppi.Osoite = oppMalli.Osoite;
                Oppi.Maa = oppMalli.Maa;
                Oppi.Email = oppMalli.Email;
                Oppi.Postinumero = oppMalli.Postinumero;



                //Tallennetaan muutokset ja palataan index sivulle
                  entities.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(oppMalli);
            }
        }

        //Poista osio
        public ActionResult Poista(int? OpiskelijaID)
        {

            entities = new TestiKantaEntities();

            OpiskelijaMalli OppMalli = (from op in entities.Opiskelija
                                        where op.OpiskelijaID == OpiskelijaID
                                        select new OpiskelijaMalli
                                        {
                                            OpiskelijaID = op.OpiskelijaID,
                                            Koodi = op.Koodi,
                                            Nimi = op.Nimi,
                                            Ikä = op.Ikä,
                                            Osoite = op.Osoite,
                                            Maa = op.Maa,
                                            Email = op.Email,
                                            Postinumero = op.Postinumero
                                        }).FirstOrDefault();

            return View(OppMalli);
        }

        [HttpPost]
        public ActionResult Poista(OpiskelijaMalli oppMalli)
        {
            entities = new TestiKantaEntities();
            Opiskelija Oppi = (from op in entities.Opiskelija
                               where op.OpiskelijaID == oppMalli.OpiskelijaID
                               select op).FirstOrDefault();

            entities.Opiskelija.Remove(Oppi);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
