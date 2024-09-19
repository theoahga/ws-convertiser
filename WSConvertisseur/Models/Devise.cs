namespace WSConvertisseur.Models
{
    public class Devise
    {
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

        private string? nomDevise;

		public string? NomDevise
		{
			get { return nomDevise; }
			set { nomDevise = value; }
		}

		private double taux;

		public double Taux
		{
			get { return taux; }
			set { taux = value; }
		}

        public Devise()
        {
			Id = 0;
			NomDevise = null;
			Taux = 0;
        }

        public Devise(int id, string? nomDevise, double taux)
        {
            Id = id;
            NomDevise = nomDevise;
            Taux = taux;
        }
    }
}
