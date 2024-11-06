using Domain;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;


namespace Infra
{
    public partial class DbContext2 : DbContext, IUnitOfWork
    {
        public DbContext2(DbContextOptions<DbContext2> options) : base(options) { }

        public virtual DbSet<Plano> Planos { get; set; }
        public virtual DbSet<Moto> Motos { get; set; }
        public virtual DbSet<Entregador> Entregadores { get; set; }
        public virtual DbSet<Locacao> Locacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgres;Database=dockerdb;Username=postgres;Password=postgres");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moto>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<Entregador>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<Locacao>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<Plano>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
        }

        public void SeedData()
        {

            if (!Motos.Any())
            {
                var motos = new List<Moto>
                {
                    new() { Placa = "AAA-1234", Marca = "Yamaha", Modelo = "MT-07", Ano = 2020 },
                    new() { Placa = "BBB-2345", Marca = "Honda", Modelo = "CB 500", Ano = 2021 },
                    new() { Placa = "CCC-3456", Marca = "Kawasaki", Modelo = "Z400", Ano = 2019 },
                    new() { Placa = "DDD-4567", Marca = "Suzuki", Modelo = "GSX-S750", Ano = 2020 },
                    new() { Placa = "EEE-5678", Marca = "Ducati", Modelo = "Monster", Ano = 2022 },
                    new() { Placa = "FFF-6789", Marca = "BMW", Modelo = "G 310", Ano = 2021 },
                    new() { Placa = "GGG-7890", Marca = "Harley", Modelo = "Iron 883", Ano = 2020 },
                    new() { Placa = "HHH-8901", Marca = "Triumph", Modelo = "Street", Ano = 2019 },
                    new() { Placa = "III-9012", Marca = "Royal Enfield", Modelo = "Interceptor", Ano = 2022 },
                    new() { Placa = "JJJ-0123", Marca = "Aprilia", Modelo = "Tuono", Ano = 2020 },
                    new() { Placa = "KKK-1234", Marca = "KTM", Modelo = "Duke 390", Ano = 2021 },
                    new() { Placa = "LLL-2345", Marca = "Honda", Modelo = "CBR 650", Ano = 2018 },
                    new() { Placa = "MMM-3456", Marca = "Yamaha", Modelo = "MT-09", Ano = 2019 },
                    new() { Placa = "NNN-4567", Marca = "BMW", Modelo = "S 1000", Ano = 2022 },
                    new() { Placa = "OOO-5678", Marca = "Ducati", Modelo = "Panigale", Ano = 2021 },
                    new() { Placa = "PPP-6789", Marca = "Kawasaki", Modelo = "Ninja ZX-6R", Ano = 2020 },
                    new() { Placa = "QQQ-7890", Marca = "Triumph", Modelo = "Tiger 900", Ano = 2021 },
                    new() { Placa = "RRR-8901", Marca = "Suzuki", Modelo = "Hayabusa", Ano = 2022 },
                    new() { Placa = "SSS-9012", Marca = "Harley-Davidson", Modelo = "Street Glide", Ano = 2021 },
                    new() { Placa = "TTT-0123", Marca = "Royal Enfield", Modelo = "Classic 500", Ano = 2019 }
                };
                Motos.AddRange(motos);
                SaveChanges();
            }

            if (!Planos.Any())
            {
                var planos = new List<Plano>
                {
                    new() { Nome = "7 dias", QuantidadeDias = 7, Valor= 30},
                    new() { Nome = "15 dias", QuantidadeDias = 15, Valor = 28 },
                    new() { Nome = "30 dias", QuantidadeDias = 30, Valor = 22 },
                    new() { Nome = "45 dias", QuantidadeDias = 45, Valor = 20 },
                    new() { Nome = "50 dias", QuantidadeDias = 50, Valor = 18 }
                };
                Planos.AddRange(planos);
                SaveChanges();
            }

            if (!Entregadores.Any())
            {
                var entregadores = new List<Entregador>
                {
                    new() { Nome = "Carlos Almeida", CNPJ = "12345678000190", DataNascimento = DateTime.Now.AddYears(-30).ToUniversalTime(), NumeroCNH = "CNH123456", TipoCNH = "AB", ImagemCNH = "imagem1.jpg" },
                    new() { Nome = "Ana Souza", CNPJ = "22345678000190", DataNascimento = DateTime.Now.AddYears(-25).ToUniversalTime(), NumeroCNH = "CNH234567", TipoCNH = "A", ImagemCNH = "imagem2.jpg" },
                    new() { Nome = "Pedro Silva", CNPJ = "32345678000190", DataNascimento = DateTime.Now.AddYears(-28).ToUniversalTime(), NumeroCNH = "CNH345678", TipoCNH = "B", ImagemCNH = "imagem3.jpg" },
                    new() { Nome = "Mariana Ferreira", CNPJ = "42345678000190", DataNascimento = DateTime.Now.AddYears(-32).ToUniversalTime(), NumeroCNH = "CNH456789", TipoCNH = "AB", ImagemCNH = "imagem4.jpg" },
                    new() { Nome = "João Oliveira", CNPJ = "52345678000190", DataNascimento = DateTime.Now.AddYears(-29).ToUniversalTime(), NumeroCNH = "CNH567890", TipoCNH = "A", ImagemCNH = "imagem5.jpg" },
                    new() { Nome = "Fernanda Lima", CNPJ = "62345678000190", DataNascimento = DateTime.Now.AddYears(-35).ToUniversalTime(), NumeroCNH = "CNH678901", TipoCNH = "B", ImagemCNH = "imagem6.jpg" },
                    new() { Nome = "Ricardo Santos", CNPJ = "72345678000190", DataNascimento = DateTime.Now.AddYears(-27).ToUniversalTime(), NumeroCNH = "CNH789012", TipoCNH = "AB", ImagemCNH = "imagem7.jpg" },
                    new() { Nome = "Paula Rocha", CNPJ = "82345678000190", DataNascimento = DateTime.Now.AddYears(-31).ToUniversalTime(), NumeroCNH = "CNH890123", TipoCNH = "A", ImagemCNH = "imagem8.jpg" },
                    new() { Nome = "Thiago Gonçalves", CNPJ = "92345678000190", DataNascimento = DateTime.Now.AddYears(-26).ToUniversalTime(), NumeroCNH = "CNH901234", TipoCNH = "B", ImagemCNH = "imagem9.jpg" },
                    new() { Nome = "Camila Barbosa", CNPJ = "12345678000191", DataNascimento = DateTime.Now.AddYears(-34).ToUniversalTime(), NumeroCNH = "CNH012345", TipoCNH = "AB", ImagemCNH = "imagem10.jpg" },
                    new() { Nome = "Luiz Martins", CNPJ = "22345678000191", DataNascimento = DateTime.Now.AddYears(-29).ToUniversalTime(), NumeroCNH = "CNH123450", TipoCNH = "A", ImagemCNH = "imagem11.jpg" },
                    new() { Nome = "Patrícia Antunes", CNPJ = "32345678000191", DataNascimento = DateTime.Now.AddYears(-33).ToUniversalTime(), NumeroCNH = "CNH234561", TipoCNH = "B", ImagemCNH = "imagem12.jpg" },
                    new() { Nome = "Rodrigo Santos", CNPJ = "42345678000191", DataNascimento = DateTime.Now.AddYears(-28).ToUniversalTime(), NumeroCNH = "CNH345672", TipoCNH = "AB", ImagemCNH = "imagem13.jpg" },
                    new() { Nome = "Juliana Moura", CNPJ = "52345678000191", DataNascimento = DateTime.Now.AddYears(-30).ToUniversalTime(), NumeroCNH = "CNH456783", TipoCNH = "A", ImagemCNH = "imagem14.jpg" },
                    new() { Nome = "Gustavo Lima", CNPJ = "62345678000191", DataNascimento = DateTime.Now.AddYears(-35).ToUniversalTime(), NumeroCNH = "CNH567894", TipoCNH = "B", ImagemCNH = "imagem15.jpg" },
                    new() { Nome = "Sabrina Castro", CNPJ = "72345678000191", DataNascimento = DateTime.Now.AddYears(-27).ToUniversalTime(), NumeroCNH = "CNH678905", TipoCNH = "AB", ImagemCNH = "imagem16.jpg" },
                    new() { Nome = "Felipe Almeida", CNPJ = "82345678000191", DataNascimento = DateTime.Now.AddYears(-32).ToUniversalTime(), NumeroCNH = "CNH789016", TipoCNH = "A", ImagemCNH = "imagem17.jpg" },
                    new() { Nome = "Renata Costa", CNPJ = "92345678000191", DataNascimento = DateTime.Now.AddYears(-33).ToUniversalTime(), NumeroCNH = "CNH890127", TipoCNH = "B", ImagemCNH = "imagem18.jpg" },
                    new() { Nome = "Diego Souza", CNPJ = "12345678000192", DataNascimento = DateTime.Now.AddYears(-26).ToUniversalTime(), NumeroCNH = "CNH901238", TipoCNH = "AB", ImagemCNH = "imagem19.jpg" },
                    new() { Nome = "Larissa Ramos", CNPJ = "22345678000192", DataNascimento = DateTime.Now.AddYears(-34).ToUniversalTime(), NumeroCNH = "CNH012349", TipoCNH = "A", ImagemCNH = "imagem20.jpg" }
                };
                Entregadores.AddRange(entregadores);
                SaveChanges();
            }


        }
    }
}