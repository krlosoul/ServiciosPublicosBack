namespace SysprotecBack.Core.Entities
{
    public partial class Service
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}