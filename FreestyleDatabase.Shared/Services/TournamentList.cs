using System.Collections.Generic;

namespace FreestyleDatabase.Shared.Services
{
    public static class Tournaments
    {
        private static readonly List<string> tourney2020 = new List<string>();
        private static readonly List<string> tourney2019 = new List<string>();
        private static readonly List<string> tourney2018 = new List<string>();

        static Tournaments()
        {
            tourney2020.Add("2020 FloWrestling Burroughs-Valencia");
            tourney2020.Add("2020 Moscow Grand Prix");
            tourney2020.Add("2020 Iran World Team Wrestle-Offs");
            tourney2020.Add("2020 Belarus Championship");
            tourney2020.Add("2020 Poland Open");
            tourney2020.Add("2020 Hawkeye Showdown Open");
            tourney2020.Add("2020 Defense Minister Cup");
            tourney2020.Add("2020 FloWrestling 195 8-man");
            tourney2020.Add("2020 Kiev International Tournament");
            tourney2020.Add("2020 Russian Nationals");
            tourney2020.Add("2020 Bulgarian Championship");
            tourney2020.Add("2020 Iran Premier League");
            tourney2020.Add("2020 Hungarian Championships");
            tourney2020.Add("2020 Pan-American Championships");
            tourney2020.Add("2020 Asian Championships");
            tourney2020.Add("2020 European Championships");
            tourney2020.Add("2020 Cerro Pelado");
            tourney2020.Add("2020 African Championships");
            tourney2020.Add("2019 German League");
            tourney2020.Add("2020 Grand Prix Ivan Yariguin");
            tourney2020.Add("2020 Henri Deglane");
            tourney2020.Add("2020 Matteo Pellicone");
            tourney2020.Add("2020 Takhti Cup");

            tourney2019.Add("2019 World Clubs Cup");
            tourney2019.Add("2019 South East Asian Games");
            tourney2019.Add("2019 Alans");
            tourney2019.Add("2019 Grand Prix of Moscow");
            tourney2019.Add("2019 Iran Premier League");
            tourney2019.Add("2019 Kunayev");
            tourney2019.Add("2019 U23 World Championships");
            tourney2019.Add("2019 World Military Games");
            tourney2019.Add("2019 Akhmat Kadyrov");
            tourney2019.Add("2019 Continental Cup");
            tourney2019.Add("2019 Dmitry Korkin");
            tourney2019.Add("2019 World Championships");
            tourney2019.Add("2019 African Games");
            tourney2019.Add("2019 Junior World Championships");
            tourney2019.Add("2019 Medved");
            tourney2019.Add("2019 Grand Prix of Tbilisi");
            tourney2019.Add("2019 Pan-American Games");
            tourney2019.Add("2019 Poland Open");
            tourney2019.Add("2019 Ion Cornianu & Ladislau Simon");
            tourney2019.Add("2019 Stepan Sargsyan Cup");
            tourney2019.Add("2019 Yasar Dogu");
            tourney2019.Add("2019 Grand Prix of Spain");
            tourney2019.Add("2019 Russian Nationals");
            tourney2019.Add("2019 Canada Cup");
            tourney2019.Add("2019 European Games");
            tourney2019.Add("2019 Matteo Pellicone");
            tourney2019.Add("2019 Macedonian Pearl");
            tourney2019.Add("2019 Grand Prix of Buryatia");
            tourney2019.Add("2019 Outstanding Ukrainian");
            tourney2019.Add("2019 Ali Aliev");
            tourney2019.Add("2019 Asian Championships");
            tourney2019.Add("2019 European Championships");
            tourney2019.Add("2019 Kristjan Palusalu Memorial");
            tourney2019.Add("2019 Mongolia Open");
            tourney2019.Add("2019 African Championships");
            tourney2019.Add("2019 World Cup");
            tourney2019.Add("2019 Dan Kolov");
            tourney2019.Add("2019 Cerro Pelado");
            tourney2019.Add("2018 German League");
            tourney2019.Add("2019 Takhti Cup");
            tourney2019.Add("2019 Henri Deglane");
            tourney2019.Add("2019 Indian Pro League");
            tourney2019.Add("2019 Grand Prix Ivan Yariguin");

            tourney2018.Add("2018 World Clubs Cup");
            tourney2018.Add("2018 Alans");
            tourney2018.Add("2018 Alrosa Cup");
            tourney2018.Add("2018 Akhmat Kadyrov Cup");
            tourney2018.Add("2018 Kunayev");
            tourney2018.Add("2018 Continental Cup");
            tourney2018.Add("2018 U23 World Championships");
            tourney2018.Add("2018 World Championships");
            tourney2018.Add("2018 Ugra Cup");
            tourney2018.Add("2018 Henri Deglane");
            tourney2018.Add("2018 Junior World Championships");
            tourney2018.Add("2018 Medved");
            tourney2018.Add("2018 Poland Open");
            tourney2018.Add("2018 Stepan Sargsyan");
            tourney2018.Add("2018 Dmitry Korkin");
            tourney2018.Add("2018 World University Championships");
            tourney2018.Add("2018 Asian Games");
            tourney2018.Add("2018 Russian Nationals");
            tourney2018.Add("2018 Central American & Carribean Games");
            tourney2018.Add("2018 Yasar Dogu");
            tourney2018.Add("2018 Grand Prix of Spain");
            tourney2018.Add("2018 Canada Cup");
            tourney2018.Add("2018 Tbilisi Grand Prix");
            tourney2018.Add("2018 Mediterranean Games");
            tourney2018.Add("2018 Mongolia Open");
            tourney2018.Add("2018 XI South American Games");
            tourney2018.Add("2018 Matteo Pellicone");
            tourney2018.Add("2018 Oceania Championships");
            tourney2018.Add("2018 World Military Championships");
            tourney2018.Add("2018 Macedonian Pearl Macedonia");
            tourney2018.Add("2018 Ali Aliev");
            tourney2018.Add("2018 Pan-American Championships");
            tourney2018.Add("2018 European Championships");
            tourney2018.Add("2018 Commonwealth Games");
            tourney2018.Add("2018 World Cup");
            tourney2018.Add("2018 Kristjan Paluslau");
            tourney2018.Add("2018 Dan Kolov");
            tourney2018.Add("2018 Central American & Carribean Championships");
            tourney2018.Add("2018 Grand Prix of Buryatia");
            tourney2018.Add("2018 Asian Championships");
            tourney2018.Add("2018 Outstanding Ukrainian");
            tourney2018.Add("2018 Cerro Pelado");
            tourney2018.Add("2018 Takhti Cup");
            tourney2018.Add("2018 African Championships");
            tourney2018.Add("2018 Indian Pro League");
            tourney2018.Add("2018 Ivan Yariguin");
        }

        public static List<string> Events(string year)
        {
            if (year == "2020")
            {
                return tourney2020;
            }
            else if (year == "2019")
            {
                return tourney2019;
            }
            else if (year == "2018")
            {
                return tourney2018;
            }

            return null;
        }
    }
}