using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IProjectRepository _repository;
        public DeleteProjectHandler(IMediator mediator, IProjectRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        async Task<ResultViewModel> IRequestHandler<DeleteProjectCommand, ResultViewModel>.Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project =await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
            }

            project.SetAsDeleted();
            _repository.Update(project);
            return ResultViewModel.Success();
        }
    }
}
