using Microsoft.AspNetCore.Mvc;
using WellAPI.Models;
using WellAPI.Data;
using System;
using System.Linq;
using AutoMapper;
using WellAPI.DTO;
using System.Collections.Generic;

namespace WellAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellController : Controller
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public WellController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult Create(WellDTO well)
        {

            var w = _mapper.Map<Well>(well);
            w.CreatedAt = DateTime.UtcNow;
            w.UpdatedAt = DateTime.UtcNow;

            _context.Wells.Add(w);

            _context.SaveChanges();
            return new JsonResult(Ok(w));
        }

        [HttpPut]
        public JsonResult Edit(WellDTO well)
        {
            var WellInDb = _context.Wells.FirstOrDefault(w => w.Id == well.Id);
            if (WellInDb == null)
                return new JsonResult(NotFound());

            //_mapper.Map(well, WellInDb);

            WellInDb.Name = well.Name;

            WellInDb.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return new JsonResult(Ok(_mapper.Map<WellDTO>(WellInDb)));
        }

        [HttpGet("{id}")]
        public JsonResult Get(long id)
        {
            var result = _context.Wells.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(_mapper.Map<WellDTO>(result)));
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(long id)
        {
            var result = _context.Wells.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Wells.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult getAll()
        {
            var result = _context.Wells.ToList();

            return new JsonResult(Ok(_mapper.Map<IEnumerable<WellDTO>>(result)));
        }
    }
}
