using DataLayer;
using DataLayer.Repository.Implementation;
using DataLayer.Repository.Interface;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CadreRepository : Repository<Cadre>, ICadreRepository
    {
        public CadreRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
