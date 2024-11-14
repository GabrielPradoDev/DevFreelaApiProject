using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CompleteProject
{
    internal class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IProjectRepository _repository;
        public CompleteProjectHandler(IMediator mediator, IProjectRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
            }

            project.Complete();
            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
