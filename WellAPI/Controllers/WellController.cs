using Microsoft.AspNetCore.Mvc;
using WellAPI.Models;
using WellAPI.Data;
using System;
using System.Linq;
using AutoMapper;
using WellAPI.DTO;
using System.Collections.Generic;
using WellAPI.Repository;

namespace WellAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellController : Controller
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private IWellRepository _wellRepository;

        public WellController(ApiContext context, IMapper mapper, IWellRepository wellRepository)
        {
            _context = context;
            _mapper = mapper;
            _wellRepository = wellRepository;
        }

        [HttpPost]
        public JsonResult Create(WellDTO well)
        {

            var w = _mapper.Map<Well>(well);
            w.CreatedAt = DateTime.UtcNow;
            w.UpdatedAt = DateTime.UtcNow;

            _wellRepository.Add(w);

            _wellRepository.Save();
            return new JsonResult(Ok(w));
        }

        [HttpPut]
        public JsonResult Edit(WellDTO well)
        {
            var WellInDb = _wellRepository.GetById(well.Id);
            if (WellInDb == null)
                return new JsonResult(NotFound());

            //_mapper.Map(well, WellInDb);

            WellInDb.Name = well.Name;

            WellInDb.UpdatedAt = DateTime.UtcNow;

            _wellRepository.Save();

            return new JsonResult(Ok(_mapper.Map<WellDTO>(WellInDb)));
        }

        [HttpGet("{id}")]
        public JsonResult Get(long id)
        {
            var result = _wellRepository.GetById(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(_mapper.Map<WellDTO>(result)));
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(long id)
        {
            var result = _wellRepository.GetById(id);

            if (result == null)
                return new JsonResult(NotFound());

            _wellRepository.Delete(id);
            _wellRepository.Save();

            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult getAll()
        {
            var result = _wellRepository.GetAll();

            return new JsonResult(Ok(_mapper.Map<IEnumerable<WellDTO>>(result)));
        }
    }
}
