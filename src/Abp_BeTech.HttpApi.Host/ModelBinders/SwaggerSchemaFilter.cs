using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Abp_BeTech.ModelBinders
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod == "POST" && context.ApiDescription.RelativePath.Contains("Create"))
            {

                operation.RequestBody = new OpenApiRequestBody
                {
                    Description = "Product with specifications",
                    Content = new Dictionary<string, OpenApiMediaType>
                {
                    { "multipart/form-data", new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties =
                                {
                                    { "productWithSpecificationsDto", new OpenApiSchema
                                        {
                                             Type = "object",
                                Properties =
                                {
                                    { "CategoryId", new OpenApiSchema { Type = "integer" } },
                                    { "Description", new OpenApiSchema { Type = "string" } },
                                    { "Brand", new OpenApiSchema { Type = "string" } },
                                    { "ModelName", new OpenApiSchema { Type = "string" } },
                                    { "Price", new OpenApiSchema { Type = "number", Format = "decimal" } },
                                    { "DiscountValue", new OpenApiSchema { Type = "number", Format = "decimal" } },
                                    { "DiscountedPrice", new OpenApiSchema { Type = "number", Format = "decimal" } },
                                    { "Warranting", new OpenApiSchema { Type = "string" } },
                                    { "Quantity", new OpenApiSchema { Type = "integer" } },
                                    { "DateAdded", new OpenApiSchema { Type = "string", Format = "date-time" } },
                                    { "Images", new OpenApiSchema
                                        {
                                            Type = "array",
                                            Items = new OpenApiSchema
                                            {

                                               Type = "string",
                                                Format = "binary" // to represent file uploads
                                            }
                                        }
                                    }
                                }
                                        }
                                    },
                                    { "productCategorySpecificatoinDto", new OpenApiSchema
                                        {
                                            Type = "array",
                                            Items = new OpenApiSchema
                                            {
                                                Type = "object",
                                                Properties =
                                                {
                                                    { "SpecificationId", new OpenApiSchema { Type = "integer" } },
                                                    { "Value", new OpenApiSchema { Type = "string" } },
                                                    // أضف خصائص أخرى لـ ProductCategorySpecificatoinDto هنا
                                                }
                                            }
                                        }
                                    }
                                     , { "file", new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "binary"
                                        }
                                }
                            }
                        }
                    }
                }
                }
                };
             
            }

        }
    }

}
