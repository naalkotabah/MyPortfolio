using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace porject.ViewModels
{
    public class HomeViewModel
    {

        public Owner Owner { get; set; }
        public List<Portfolioitem>  Portfolioitems{ get; set; }
    }
}
