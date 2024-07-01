namespace NMHTestProject.Data.Entities
{
    public class Article
    {
        public long Id { get; set; } // Primary key

        public string Title { get; set; } = default!; // Index

        public virtual ICollection<Author> Author { get; set; } = default!; // Many-To-Many        

        public virtual Site Site { get; set; } = default!;  // One-To-Many
    }
}
