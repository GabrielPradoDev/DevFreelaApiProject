using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _context;
      //  private readonly IMediator _mediator;
        public ProjectRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Project project)
        {
            await _context.Projects.AddAsync(project);
            _context.SaveChanges();

            return project.Id;
        }

        public async Task AddComment(ProjectComment comment)
        {
            _context.ProjectComments.AddAsync(comment);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Projects.AnyAsync(p=> p.Id == id);
        }

        public async Task<List<Project>> GetAll()
        {
            var projects = await _context.Projects
                 .Include(p => p.Client)
                 .Include(p => p.Freelancer)
                 .Where(p => !p.IsDeleted)
                 .ToListAsync();

            return projects;
        }

        public async Task<Project> GetById(int id)
        {
            return await _context.Projects
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> GetDatailsById(int id)
        {
             var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
