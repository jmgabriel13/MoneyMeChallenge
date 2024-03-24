﻿using Domain.Shared;
using MediatR;

namespace Application.Interfaces;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
