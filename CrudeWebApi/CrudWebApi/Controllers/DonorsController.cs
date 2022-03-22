using CrudWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrudWebApi.Controllers
{
    public class DonorsController : ApiController
    {
        static List<DonorViewModel> donors = new List<DonorViewModel>()
        {
            new DonorViewModel
            {
            Id = 1,
            FirstName = "Dejan",
            LastName = "Stirjan",
            DonNumber = "6",
            ReferentCode = Guid.NewGuid(),
            },
            new DonorViewModel
            {
            Id = 2,
            FirstName = "Ivan",
            LastName = "Kovac",
            DonNumber = "5",
            ReferentCode = Guid.NewGuid(),
            },
            new DonorViewModel
            {
            Id = 3,
            FirstName = "Marko",
            LastName = "Hrastinski",
            DonNumber = "12",
            ReferentCode = Guid.NewGuid(),
            }
        };


        [HttpGet]
        public HttpResponseMessage Point()
        {
            if (donors == null || donors.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no donors at list");
            }
            return Request.CreateResponse(HttpStatusCode.OK,donors);
        }

        //    //    using (var ctx = new Donors_DBEntities())
        //    //    {
        //    //        donor = ctx.Donors.Select(s => new DonorViewModel()
        //    //        {
        //    //            Id = s.id,
        //    //            FirstName = s.firstname,
        //    //            LastName = s.lastname,
        //    //            DonNumber = s.donnumber,
        //    //        }).ToList<DonorViewModel>();
        //    //    }

        //    //    if (donor.Count == 0)
        //    //    {
        //    //        return NotFound();
        //    //    }

        //    //    return Ok(donor);

        //    //}
        [HttpGet()]
        public HttpResponseMessage Point(int id)
        {
            var specificDonor = donors.Find(c => c.Id == id);
            if (donors == null || donors.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"There is no donors at list");
            }
            else if (specificDonor == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,$"There is no donor with  id '{id}' at list");
            }
                return Request.CreateResponse(HttpStatusCode.OK,specificDonor);
        }
        //    // GET api/values/5
        //    [HttpGet()]
        //    public IHttpActionResult Point(int id)
        //    {
        //        IList<DonorViewModel> donor = null;

        //        using (var ctx = new Donors_DBEntities())
        //        {
        //            donor = ctx.Donors.Where(c => c.id == id).Select(s => new DonorViewModel()
        //            {
        //                Id = s.id,
        //                FirstName = s.firstname,
        //                LastName = s.lastname,
        //                DonNumber = s.donnumber,
        //            }).ToList<DonorViewModel>();
        //        }

        //        if (donor.Count == 0)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(donor);

    [HttpPost]
    public HttpResponseMessage Add(DonorViewModel donor)
        {
            if (donors.Count > 0)
            {
                donor.Id = donors.Max(s => s.Id) + 1;
            }
            else
            {
                donor.Id = 1;
            }
            int id = donor.Id;
            donors.Add(donor);
            donor.ReferentCode = Guid.NewGuid();
            HttpResponseMessage responseMessageOk = Request.CreateResponse(HttpStatusCode.Created, donor);
            return Request.CreateResponse(HttpStatusCode.OK,$"You succsessfully add donor with id '{id}' at list");

        }


        [HttpPut]
        public HttpResponseMessage Change(DonorViewModel donor)
        {
            var existingDonor = donors.Find(s => s.Id == donor.Id);

            if (existingDonor == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no donor with  id '{donor.Id}' at list");
            }

            existingDonor.Id = donor.Id;
            existingDonor.FirstName = donor.FirstName;
            existingDonor.LastName = donor.LastName;
            existingDonor.DonNumber = donor.DonNumber;

            HttpResponseMessage responseMessageOk = Request.CreateResponse(HttpStatusCode.OK, donor);
            return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully change donor with id '{donor.Id}' at list");             
        }

        //    // PUT api/values/5
        //    [HttpPut]
        //    public IHttpActionResult Change(DonorViewModel donor)
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest("Not a valid model");

        //        using (var ctx = new Donors_DBEntities())
        //        {
        //            var existingDonor = ctx.Donors.Where(s => s.id == donor.Id)
        //                                                    .FirstOrDefault<Donor>();

        //            if (existingDonor != null)
        //            {
        //                existingDonor.firstname = donor.FirstName;
        //                existingDonor.lastname = donor.LastName;
        //                existingDonor.donnumber = donor.DonNumber;

        //                ctx.SaveChanges();
        //            }
        //            else
        //            {
        //                return BadRequest("There is no donor with that id ");
        //            }
        //        }

        //        return Ok();
        //    }


        [HttpDelete]

        public HttpResponseMessage Remove(int id)
        {
            var specificDonor = donors.Find(c => c.Id == id);
            donors.Remove(specificDonor);
            if (specificDonor == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no donor with  id '{id}' at list");
            }
            else
            {
                HttpResponseMessage responseMessageOk = Request.CreateResponse(HttpStatusCode.OK, specificDonor);
                return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully remove donor with id '{specificDonor.Id}' at list");
            }
        }
        //    // DELETE api/values/5
        //    [HttpDelete]
        //    public IHttpActionResult Remove(int id)
        //    {
        //        if (id <= 0)
        //            return BadRequest("Not a valid donor id");
        //                  using (var ctx = new Donors_DBEntities())
        //        {
        //            var existingDonor = ctx.Donors.Where(s => s.id == id)
        //                                                      .FirstOrDefault<Donor>();
        //            if (existingDonor == null)

        //            {
        //                return BadRequest("Not a valid donor id");
        //            }
        //            else
        //            {
        //                var donor = ctx.Donors
        //                    .Where(s => s.id == id)
        //                    .FirstOrDefault();

        //                ctx.Entry(donor).State = System.Data.Entity.EntityState.Deleted;
        //                ctx.SaveChanges();
        //            }
        //        }

        //        return Ok();
        //    }
        //}
    }
}

