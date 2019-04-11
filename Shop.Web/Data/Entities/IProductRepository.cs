﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data.Entities
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        IQueryable GetAllWithUsers();

    }
}
