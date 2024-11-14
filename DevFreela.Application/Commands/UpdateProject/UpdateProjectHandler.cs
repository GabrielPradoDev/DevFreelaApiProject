using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject
{
    internal class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IProjectRepository _repository;
        public UpdateProjectHandler(IMediator mediator, IProjectRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.IdProject);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
            }
            project.Update(request.Title, request.Description, request.TotalCost);

            await _repository.Update(project);
            return ResultViewModel.Success();
        }
    }
}
