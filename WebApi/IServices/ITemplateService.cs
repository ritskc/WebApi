using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.IServices
{
    public interface ITemplateService
    {
        IEnumerable<Template> Get();
        Template Get(int id);
        Template Save(Template template);
        void Delete(int Id);
    }
}
