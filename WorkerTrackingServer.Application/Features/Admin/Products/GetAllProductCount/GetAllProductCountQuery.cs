using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.DTOs;

namespace WorkerTrackingServer.Application.Features.Admin.Products.GetAllProductCount;
public sealed record GetAllProductCountQuery() : IRequest<Result<List<GetAllProductCountDto>>>;
