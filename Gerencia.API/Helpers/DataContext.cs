namespace Gerencia.API.Helpers;

using Gerencia.API.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
 private readonly IConfiguration _configuration;

 public DataContext(IConfiguration configuration){
    _configuration = configuration;
 }

 protected override void OnConfiguring(DbContextOptionsBuilder options)
 {
  // connect to sqlite database
  options.UseSqlite(_configuration.GetConnectionString("WebApiDatabase"));
 }

 public DbSet<User> Users { get; set; }
 public DbSet<LoginHistoric> LoginHistorics { get; set; }
}