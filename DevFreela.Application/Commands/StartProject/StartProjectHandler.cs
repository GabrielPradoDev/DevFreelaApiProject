using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.StartProject
{
    internal class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IProjectRepository _repository;
        public StartProjectHandler(IMediator mediator, IProjectRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
            }

            project.Start();
            await _repository.Update(project);


            return ResultViewModel.Success();
        }
    }
}
