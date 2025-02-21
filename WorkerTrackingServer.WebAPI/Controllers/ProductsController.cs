using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerTrackingServer.Application.Features.Admin.Products.CreateProduct;
using WorkerTrackingServer.Application.Features.Admin.Products.DeleteProductById;
using WorkerTrackingServer.Application.Features.Admin.Products.GetAllProduct;
using WorkerTrackingServer.Application.Features.Admin.Products.GetProductById;
using WorkerTrackingServer.Application.Features.Admin.Products.UpdateProduct;
using WorkerTrackingServer.Application.Features.Admin.Products.UpdateProductStatus;
using WorkerTrackingServer.WebAPI.Abstractions;

namespace WorkerTrackingServer.WebAPI.Controllers;

public sealed class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllProductQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateStatus(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateProductStatusCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteProductByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
