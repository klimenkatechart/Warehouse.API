namespace Warehouse.Domain
{
    public abstract class BaseEntity<TEntity>
    {
        public string Id { get; set; } = IdGen();

        private static string IdGen()
        {
            Random generator = new Random();
            String id = generator.Next(0, 1000).ToString("D6");
            return $"{typeof(TEntity).Name}{id}";
        }
    }
}
