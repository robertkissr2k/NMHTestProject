namespace NMHTestProject.Data.Entities
{
    public class Author
    {
        public long Id { get; set; } // Primary key

        public string Name { get; set; } = default!; // Unique index

        public virtual ICollection<Article> Articles { get; set; } = default!;

        public virtual Image Image { get; set; } = default!;  // One-To-One
    }
}
