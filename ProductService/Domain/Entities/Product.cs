using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Domain.Entities;

[Table("Product")]
public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
}
