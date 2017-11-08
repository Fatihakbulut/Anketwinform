using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    [Table("tblCevaplar")] //db de oluşacak tablo adını belirliyoruz.
    public class Cevap
    {
        [Key]
        public int CevapID { get; set; }
        [ForeignKey("CevabiVerenKisi")] //Bu FK nın dolduracağı virtal navigation property CevabıVerenKisi'dir.
        public int KisiID { get; set; }
        [ForeignKey("Sorusu")]
        public int SoruID { get; set; }
        public Yanit Yanit { get; set; }

        public virtual Kisi CevabiVerenKisi { get; set; }
        public virtual Soru Sorusu { get; set; }
    }

    [Table("tblSorular")]
    public class Soru
    {
        [Key]
        public int SoruID { get; set; }

        [Required] //Soru Cümlesi zorunlu olmasını ifade eder.
        public string SoruCumlesi { get; set; }
    }

    [Table("tblKisiler")]
    public class Kisi
    {
        [Key]
        public int KisiID { get; set; }

        [Required]
        public string AdSoyad { get; set; }
    }

    public enum Yanit
    {
        Hayir,Evet
    }
}
