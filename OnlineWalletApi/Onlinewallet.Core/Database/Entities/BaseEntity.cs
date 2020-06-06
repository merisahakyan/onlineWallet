using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Onlinewallet.Core.Database.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }
}
