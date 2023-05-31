﻿using DataLayer;
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
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
