using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFiltriranje
{
    static class ProizvodDal
    {
        public static List<ProizvodView> PrikaziProizvode(decimal min,decimal max)
        {
            List<ProizvodView> listaProizvoda = new List<ProizvodView>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT ProizvodId,NazivProizvoda, JedinicnaCena");
            sb.AppendLine("FROM Proizvodnja.Proizvodi");
            sb.AppendLine("WHERE JedinicnaCena BETWEEN @min and @max");
            sb.AppendLine("order by JedinicnaCena");
            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnTSQL2018))
            {
                using (SqlCommand komanda = new SqlCommand(sb.ToString(),konekcija))
                {

                    try
                    {
                        komanda.Parameters.AddWithValue("@min", min);
                        komanda.Parameters.AddWithValue("@max", max);
                        konekcija.Open();
                        using (SqlDataReader dr = komanda.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ProizvodView p = new ProizvodView { 
                                    Id = dr.GetInt32(0),
                                    Naziv = dr.GetString(1),
                                    Cena = dr.GetDecimal(2)
                                };

                                listaProizvoda.Add(p);
                            }
                        }
                        return listaProizvoda;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        public static decimal NadjiMinimum(int min = 1)
        {
            string upit = "";
            decimal rez = 0;

            if (min == 1)
            {
                upit = "SELECT MIN(JedinicnaCena) FROM Proizvodnja.Proizvodi";
            }
            else
            {
                upit = "SELECT MAX(JedinicnaCena) FROM Proizvodnja.Proizvodi";
            }

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnTSQL2018))
            {
                using (SqlCommand komanda = new SqlCommand(upit,konekcija))
                {
                    try
                    {
                        konekcija.Open();
                        rez = (decimal)komanda.ExecuteScalar();
                        return rez;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }
        }
    }
}
