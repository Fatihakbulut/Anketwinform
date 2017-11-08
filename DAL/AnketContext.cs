using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class AnketContext:DbContext
    {
        public virtual DbSet<Soru> Sorular { get; set; }
        public virtual DbSet<Kisi> Kisiler { get; set; }
        public virtual DbSet<Cevap> Cevaplar { get; set; }
    }
}
