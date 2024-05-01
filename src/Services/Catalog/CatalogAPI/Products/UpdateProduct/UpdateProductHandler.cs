﻿using CatalogAPI.Products.GetProducts;
using FluentValidation;
using JasperFx.CodeGeneration.Model;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price): ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price is greater than 0");
        }
    }

    internal class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {

        private readonly IDocumentSession _session;
        public UpdateProductHandler(IDocumentSession session)
        {
            _session = session;
        }
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _session.LoadAsync<Product>(command.Id, cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            product.Name= command.Name;
            product.Category= command.Category;
            product.Description= command.Description;
            product.ImageFile= command.ImageFile;
            product.Price= command.Price;

            _session.Update(product);
            await _session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);

        }
    }
}
