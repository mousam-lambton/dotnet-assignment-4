using Microsoft.EntityFrameworkCore;

namespace Assignment4.Models
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
    }
}