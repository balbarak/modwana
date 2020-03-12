﻿using Modwana.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Domain.Services
{
    public interface IBlogService
    {
        Task Add(Blog entity);

        Task Save(Blog entity);

        Task Delete(string id);
    }
}
