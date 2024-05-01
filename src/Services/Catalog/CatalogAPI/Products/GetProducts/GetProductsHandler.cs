using Marten.Linq.QueryHandlers;
using Marten.Pagination;

namespace CatalogAPI.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> products);
    internal class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly IDocumentSession _session;

        public GetProductsQueryHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
            return new GetProductsResult(products);

        }
    }
}
