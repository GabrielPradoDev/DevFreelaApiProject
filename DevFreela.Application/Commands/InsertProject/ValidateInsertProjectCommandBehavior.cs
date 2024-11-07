using DevFreela.Application.Models;
using DevFreela.Infrastructure.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class ValidateInsertProjectCommandBehavior :

        IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public ValidateInsertProjectCommandBehavior(DevFreelaDbContext context) 
        {
            _context = context;
        }
        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExistis = _context.Users.Any(u => u.Id == request.IdClient);
            var freelancerExistis = _context.Users.Any(u => u.Id == request.IdFreelancer);

            if(!clientExistis ||  !freelancerExistis)
            {
                return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos");
            }

            return await next();
        }
    }
}
