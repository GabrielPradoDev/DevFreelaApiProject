using DevFreela.Application.Models;
using DevFreela.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeletProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public DeleteProjectHandler(DevFreelaDbContext context)
        {
            _context = context;

        }
        async Task<ResultViewModel> IRequestHandler<DeletProjectCommand, ResultViewModel>.Handle(DeletProjectCommand request, CancellationToken cancellationToken)
        {
            var project =await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
            }

            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChangesAsync();
            return ResultViewModel.Success();
        }
    }
}
