using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingRoomAPI.Domain.Models.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public DateTime? Deleted_At { get; set; }
        public bool Active { get; set; }
    }
}
