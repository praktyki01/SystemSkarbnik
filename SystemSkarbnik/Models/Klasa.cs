namespace SystemSkarbnik.Models
{
    public class Klasa
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }    
        public string Opis { get; set; }
        public ICollection<Skarbnik> Skarbnik { get; } = new List<Skarbnik>();
        public ICollection<Uczen> Uczen { get; } = new List<Uczen>();
        public ICollection<Zbiorka> Zbiorkas { get; } = new List<Zbiorka>();
        public ICollection<ZbiorkaUczen> ZbiorkaUczens { get; } = new List<ZbiorkaUczen>();

    }
}
