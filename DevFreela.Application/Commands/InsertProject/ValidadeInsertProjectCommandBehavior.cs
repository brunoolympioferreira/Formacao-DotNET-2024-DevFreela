﻿using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;
public class ValidadeInsertProjectCommandBehavior : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
{
    private readonly DevFreelaDbContext _context;
    public ValidadeInsertProjectCommandBehavior(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
    {
        var clientExists = _context.Users.Any(u => u.Id == request.IdClient);
        var freelancerExists = _context.Users.Any(u => u.Id == request.IdFreelancer);

        if (!clientExists || !freelancerExists)
        {
            return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos");
        }

        return await next();
    }
}