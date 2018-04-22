using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels.Areas.Administration.Resources
{
    public class IndexViewModel
    {
        public IEnumerable<Resource> Resources { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
