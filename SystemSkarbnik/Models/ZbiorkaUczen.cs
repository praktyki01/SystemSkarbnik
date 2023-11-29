namespace SystemSkarbnik.Models
{
    public class ZbiorkaUczen
    {
        public int ID { get; set; }
        public int ZbiorkaID { get; set; }
        public Zbiorka? Zbiorka { get; set; } = null!;
        public int KlasaID { get; set; }
        public Klasa? Klasa { get; set; } = null!;
        public int UczenID { get; set; }
        public Uczen? Uczen { get; set; } = null!;
        public bool CzyZaplacil { get; set; }
        public DateTime KiedyZaplacil {  get; set; }
        
    }
}
