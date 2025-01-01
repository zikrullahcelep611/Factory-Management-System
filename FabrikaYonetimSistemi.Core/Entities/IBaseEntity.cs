namespace FabrikaYonetimSistemi.Core.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
