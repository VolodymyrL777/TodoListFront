using Microsoft.AspNetCore.Mvc;
using TodoListBack.Models;
using TodoListBack.Repositories;

namespace TodoListBack.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class JobController : ControllerBase
    {
        private JobContext _jobContext;

        public JobController()
        {
            _jobContext = new JobContext();
        }

        // GET: <JobController>
        [HttpGet(Name = "GetJob")]
        public IEnumerable<Job> Get()
        {
            var res = _jobContext.Jobs.Select(x => x).Where(x => x.is_delete == false && x.parent_id == 0).ToList();

            return res;
        }

        // GET <JobController>/5
        [HttpGet("{id}")]
        public IEnumerable<Job> Get(long id)
        {
            var res = _jobContext.Jobs.Select(x => x).Where(x => x.parent_id == id && x.is_delete == false && x.is_done == false).ToList();
            return res;
        }

        // POST <JobController>
        [HttpPost]
        public void Post(Job job)
        {
            _jobContext.Add(job);
            _jobContext.SaveChanges();
        }

        // PUT <JobController>/5
        [HttpPut("{id}")]
        public void Put(long id, Job job)
        {
            Job? res = _jobContext.Jobs.Select(x => x).Where(x => x.id == id).FirstOrDefault();
            if (res != null)
            {
                res.done_date = job.done_date;
                res.is_done = job.is_done;
                res.name = job.name;
                res.is_delete = job.is_delete;


                _jobContext.Update(res);
                _jobContext.SaveChanges();
            }
        }

        // DELETE <JobController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Job? res = _jobContext.Jobs.Select(x => x).Where(x => x.id == id).FirstOrDefault();
            if (res != null)
            {
                res.is_delete = true;
                _jobContext.Update(res);
                _jobContext.SaveChanges();
            }
        }
    }
}
