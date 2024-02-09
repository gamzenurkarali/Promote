using Microsoft.EntityFrameworkCore;

namespace Promote.website.Models
{
    public class Context : DbContext
    {
        internal readonly object InformationForms;

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Admin>? admins { get; set; }
        public DbSet<AboutPage>? aboutPages { get; set; }
        public DbSet<ContactForm>? contactForms { get; set; }
        public DbSet<ContactPage>? contactPages { get; set; }
        public DbSet<HomePage>? homePages { get; set; }
        public DbSet<Layout>? layouts { get; set; }
        public DbSet<Product>? products { get; set; }
        public DbSet<ProductDetailPage>? productDetailPages { get; set; }
        public DbSet<ProductListPage>? productLists { get; set; }
        public DbSet<Sublink>? sublinks { get; set; }

        public DbSet<PasswordResetToken>? passwordResetTokens { get; set; }
        public DbSet<RelevantDocument>? relevantDocuments { get; set; }
        public DbSet<İnformationForm>? İnformationForm { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Fee)
                .HasColumnType("decimal(18,2)"); // 18 total digits, 2 decimal places

            // Diğer model konfigürasyonlarını ekleyebilirsiniz...
        }
    }
}
