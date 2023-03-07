namespace TodoListBack.Models
{
    public class Job
    {
        public long id { get; set; }
        public long parent_id { get; set; } = default;
        public string? name { get; set; }
        public DateTime add_date { get; set; } = DateTime.UtcNow; 
        public DateTime done_date { get; set; } = default;
        public bool is_done { get; set; } = default;
        public bool is_delete { get; set; } = default;
    }
}
