namespace SysprotecBack.Core.Dtos.Report
{
    public class ReportDto
    {
        public int Id { get; set; }
        public int IdService { get; set; }
        public string? ServiceName { get; set; }
        public int IdStatus { get; set; }
        public string? StatusName { get; set; }
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public string? Observation { get; set; }
        public DateTime CreationOn { get; set; }
    }

    public class ReportFilterDto
    {
        public int? IdService { get; set; }
        public int? IdStatus { get; set; }
        public int? IdUser { get; set; }
    }

    public class ReportCreateDto
    {
        public int IdService { get; set; }
        public int IdStatus { get; set; }
        public int IdUser { get; set; }
        public string? Observation { get; set; }
    }

    public class ReportUpdateDto
    {
        public int Id { get; set; }
        public int IdService { get; set; }
        public int IdStatus { get; set; }
        public int IdUser { get; set; }
        public string? Observation { get; set; }
    }

    public class ReportDeleteDto
    {
        public int Id { get; set; }
    }
}
