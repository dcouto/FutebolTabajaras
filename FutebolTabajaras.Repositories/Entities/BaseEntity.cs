using System;

namespace FutebolTabajaras.Repositories.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public BaseEntity()
        {

        }

        public BaseEntity(int id, DateTime createdDate)
        {
            ID = id;
            CreatedDate = createdDate;
        }
    }
}
