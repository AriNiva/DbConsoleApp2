using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int j = 0, i = 0, k = 0;
            string sarakeNimi = "", sarakeArvo = "";

            //Tässä tulee olla oman serverisi osoite ja tietokannan nimi
            string connStr = "";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = "123";
            while (sql.ToUpper() != "X")
            {
                Console.Write("SQL> ");
                sql = Console.ReadLine();
                if (sql.ToUpper() == "X") break;
                if (sql == "") continue;
                SqlCommand cmd = new SqlCommand(sql, conn);//Command-olio, mikä suorittaa annetun SQL-lauseen
                SqlDataReader reader = cmd.ExecuteReader();//DataReader-olio, mikä lukee kyselyn tulokset rivi kerrallaan
                DataTable schemaTable = reader.GetSchemaTable();
                foreach (DataRow rivi in schemaTable.Rows) //Pystysuunnassa alaspäin kaikki rivit käydään läpi 
                {
                    foreach (DataColumn column in schemaTable.Columns) //Joka rivin kohdalla käydään leveyssuunnassa läpi mitä kolumneja/sarakkeita schemaTablessa on
                    {
                        if (column.ColumnName == "ColumnName")
                        {
                            j++;
                            sarakeNimi = rivi[column].ToString();
                            sarakeNimi = (sarakeNimi.PadRight(15).Substring(0, 15) + "|");
                            Console.Write(sarakeNimi);
                        }
                        /* Tämä if - lause tarkistaa onko tietyn sarakkeen nimi "ColumnName". Jos näin on, se suorittaa seuraavat toiminnot:
                        1. Lisää j:n arvoa yhdellä 
                        2. Asettaa SarakeNimi:n arvoksi kyseisen sarakkeen arvon merkkijonona
                        3. Muokkaa SarakeNimi:ä niin, että se on vähintään 15 merkkiä pitkä(täyttämällä loput välilyönneillä) ja leikkaa sen sitten 15 merkkiin
                        4. Tulostaa sarakeNimi:n konsoliin */
                    }
                }
                Console.WriteLine();
                while (reader.Read())
                {
                    i++;
                    for (k = 0; k < j; k++)
                    {
                        sarakeArvo = reader.GetValue(k).ToString();
                        sarakeArvo = (sarakeArvo.PadRight(15).Substring(0, 15) + "|");
                        if (k < j - 1)
                        {
                            Console.Write(sarakeArvo);
                        }
                        else
                            Console.WriteLine(sarakeArvo);
                    }
                }

                Console.ReadLine();

                //resurssien vapautus, suljetaan tietokantayhteydet ja lukijat
                reader.Close();
                cmd.Dispose();
                schemaTable.Dispose();
                j = 0;
                i = 0;
                
            }
            //resurssien vapautus
            conn.Dispose();
        }
    }
}
