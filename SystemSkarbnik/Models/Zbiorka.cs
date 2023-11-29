namespace SystemSkarbnik.Models
{
    public class Zbiorka
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }    
        public decimal Kwota { get; set; }
        public DateTime DataOd {  get; set; }
        public DateTime DataDo { get; set; }
        public int KlasaID { get; set; }
        public Klasa? Klasa { get; set; } = null!;
        public int SkarbnikID { get; set; }
        public Skarbnik? Skarbnik { get; set; } = null!;

        public ICollection<ZbiorkaUczen> ZbiorkaUczen { get; } = new List<ZbiorkaUczen>();
    }
}
