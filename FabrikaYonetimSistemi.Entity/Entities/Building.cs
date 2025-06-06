﻿using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Building : BaseEntity
    {
        public string Name { get; set; }
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }

        public ICollection<Storage> Storages { get; set; } = new List<Storage>();
    }
}
