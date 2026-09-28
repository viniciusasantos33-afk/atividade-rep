using Microsoft.EntityFrameworkCore;
using ProdutosCodeFirst.Models;

namespace ProdutosCodeFirst.Contexts;

public class ProdutoContext : DbContext
{
    public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
}