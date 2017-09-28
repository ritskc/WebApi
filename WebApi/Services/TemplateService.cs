using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.IServices;
using WebApi.Models;

namespace WebApi.Services
{
    public class TemplateService : ITemplateService
    {
       

        public IEnumerable<Template> Get()
        {
            List<Template> templateList = new List<Template>();
         
            for(int i=0;i<5;i++)
            {
                    
                var template = new Template(i + 1, "template" + i + 1, true);
                templateList.Add(template);
            }

            return templateList.ToList();
        }

        public Template Get(int id)
        {
            return this.Get().FirstOrDefault(f=>f.Id == id);           
        }

        public Template Save(Template template)
        {
            var templates = this.Get();
            if (templates != null)
            {
                var _fleet = templates.SingleOrDefault(m => m.Id == template.Id);
                if (_fleet != null)
                {
                    templates.ToList().Remove(template);
                }
            }
            else
            {
                templates = new List<Template>();
            }

            templates.ToList().Add(template);
            return template;
        }

        public void Delete(int id)
        {
            var templates = this.Get();
            if (templates != null)
            {
                var template = templates.SingleOrDefault(m => m.Id == id);
                if (template != null)
                {
                    templates.ToList().Remove(template);                   
                }
            }
           
        }
    }
}
