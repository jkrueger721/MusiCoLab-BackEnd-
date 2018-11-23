
using Microsoft.EntityFrameworkCore;
using MusiCoLab2.Modals;
using MusiCoLab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusiCoLab2.Services
{
    public class ProjectService : IProjectService
    {
        private UserContext _db;
        public ProjectService(UserContext db)
        {
            _db = db;
        }

        public List<Project> GetProjects()
        {
            var projects = _db.Projects
                .Include( p => p.ProjectOwner)
               .Include( p => p.ProjectContributors)
                .ToList();
            return projects;
        }
        public void Add(AddProjectVM vm)
        {
            vm.Project.ProjectContributors = new List<ProjectContributor>();
            var user = _db.Users.FirstOrDefault( u => u.Id == vm.UserId);
            vm.Project.ProjectOwner = user;
           
            _db.Projects.Add(vm.Project);
            _db.SaveChanges();
        }

        public Project Find(long id)
        {
            return _db.Projects
                .Include(p => p.ProjectOwner)
               .Include(p => p.ProjectContributors)
               .FirstOrDefault(project => project.Id == id);
        }
        public Project FindWithOwner(long id)
        {
            return _db.Projects.Include("ProjectOwner").FirstOrDefault(project => project.Id == id);
        }
        public void Remove(long key)
        {
            var projectEntity = _db.Projects.First(project => project.Id == key);
            _db.Projects.Remove(projectEntity);
            _db.SaveChanges();
        }
        public void AddProjectContributor(ProjectContributor projectContributor)
        {
            
            if(!_db.ProjectContributors.Any(pc => pc.UserId == projectContributor.UserId && pc.ProjectId == projectContributor.ProjectId))
            {
                _db.ProjectContributors.Add(projectContributor);
            }
        }
        public void Update(Project item)
        {
            _db.Projects.Update(item);
            _db.SaveChanges();
        }
    }
}
