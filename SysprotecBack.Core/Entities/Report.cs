namespace SysprotecBack.Core.Entities
{
    public partial class Report
    {
        public int Id { get; set; }

        public int IdService { get; set; }

        public int IdStatus { get; set; }

        public int IdUser { get; set; }

        public string Observation { get; set; } = null!;

        public DateTime CreationOn { get; set; }

        public virtual Service Service { get; set; } = null!;

        public virtual Status Status { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}