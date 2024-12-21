using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormsApp.Models;

// [Bind("Name", "Price")]
public class Product
{
    // [BindNever]
    [Display(Name = "Ürün Id")]
    public int ProductId { get; set; }

    [Required]
    [Display(Name = "Ürün Adı")]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0, 1000000)]
    [Display(Name = "Ürün Fiyatı")]
    public decimal? Price { get; set; }

    [Display(Name = "Resim")]
    public string Image { get; set; } = string.Empty;

    [Display(Name = "Aktif Mi ?")]
    public bool IsActive { get; set; }

    [Required]
    [Display(Name = "Kategori")]
    public int? CategoryId { get; set; }

}