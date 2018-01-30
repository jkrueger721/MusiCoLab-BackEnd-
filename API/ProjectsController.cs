﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusiCoLab2.Models;
using MusiCoLab2.Services;


namespace MusiCoLab2.API
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private ProjectService _service;
        public ProjectsController(ProjectService service)
        {
            _service = service;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var projects = _service.GetProjects();
            return Ok(projects);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetProejct")]
        public IActionResult GetById(long id)
        {
            var item = _service.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Project item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _service.Add(item);

            return CreatedAtRoute("GetProject", item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Project projectUpdate)
        {
            if (projectUpdate == null || projectUpdate.Id != id)
            {
                return BadRequest();
            }

            var project = _service.Find(id);
            if (project == null)
            {
                return NotFound();
            }

           // project.IsComplete = projectUpdate.IsComplete;
            project.Name = projectUpdate.Name;

            _service.Update(project);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var project = _service.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            _service.Remove(id);
            return new NoContentResult();
        }
    }
}
