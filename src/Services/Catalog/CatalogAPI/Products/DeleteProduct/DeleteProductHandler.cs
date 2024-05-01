using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        }
    }
    internal class DeleteProductHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {

        private readonly IDocumentSession _session;

        public DeleteProductHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _session.Delete<Product>(command.Id);
            await _session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);  

        }
    }
}
