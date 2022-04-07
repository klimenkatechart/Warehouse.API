
using MongoDB.Bson;
using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Ammount { get; set; }
        public ProductStockStatus StockStatus { get; set; } 
        public string CategoryId { get; set; } = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
    }
}
