namespace NMHTestProject.Data.Entities
{
    public class Site
    {
        public long Id { get; set; } // Primary key

        public virtual ICollection<Article> Articles { get; set; } = default!;

        public DateTimeOffset CreatedAt { get; set; }
    }
}
