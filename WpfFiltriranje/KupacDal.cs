using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFiltriranje
{
    static class KupacDal
    {
        public static List<KupacView> Filtriraj(string pretraga)
        {
            List<KupacView> listaKupaca = new List<KupacView>();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT KupacId, ImeKompanije,Drzava");
            sb.AppendLine("FROM Prodaja.Kupci");
            sb.AppendLine("WHERE ImeKompanije LIKE '%'+@Firma+'%'");
            sb.AppendLine("COLLATE Latin1_General_100_CI_AI");

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnTSQL2018))
            {
                using (SqlCommand komanda = new SqlCommand(sb.ToString(),konekcija))
                {

                    try
                    {
                        komanda.Parameters.AddWithValue("@Firma",pretraga);
                        konekcija.Open();
                        SqlDataReader dr = komanda.ExecuteReader();
                        while (dr.Read())
                        {
                            KupacView k = new KupacView {
                                Id = dr.GetInt32(0),
                                Firma = dr.GetString(1),
                                Drzava = dr.GetString(2)
                            };

                            listaKupaca.Add(k);
                        }
                        return listaKupaca;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }
    }
}
