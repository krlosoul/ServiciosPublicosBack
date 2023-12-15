namespace SysprotecBack.Core.Entities
{
    public partial class Status
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}