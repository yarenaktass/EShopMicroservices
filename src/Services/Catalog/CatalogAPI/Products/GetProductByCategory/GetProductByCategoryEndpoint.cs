﻿
namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));
                var response = result.Adapt <GetProductCategoryResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProductByCatagory")
                .Produces<GetProductCategoryResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Category")
                .WithDescription("Get Product By Category");
        }
    }
}
