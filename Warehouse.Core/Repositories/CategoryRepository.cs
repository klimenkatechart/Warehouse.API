using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Helpers;

namespace Warehouse.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IOptions<MongoDbConfiguration> settings) : base(settings) { }
    }
}
