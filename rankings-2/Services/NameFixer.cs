﻿using System;
using System.Globalization;
using System.Collections.Generic;

namespace rankings2.Services
{
    public static class NameFixer
    {
        public static string FirstCharToUpper(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.  
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.  
            // ... Uppercase the lowercase letters following spaces.  
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static string FixAmerican(string newAmericanName)

        {
            Dictionary<string, string> americans = new Dictionary<string, string>()
        {
            {"Jordan Ernest Burroughs", "Jordan Burroughs"},
            {"Jordan Michael Oliver", "Jordan Oliver"},
            {"Daton Duain Fix", "Daton Fix"},
            {"Tyler Lee Graff", "Tyler Graff"},
            {"James Malcolm Green", "James Green"},
            {"Klye Douglas Dake", "Kyle Dake"},
            {"Nicholas Edward Gwiazdowski", "Nick Gwiazdowski"},
            {"Anthony Robert Nelson", "Tony Nelson"},
            {"Michael Joseph Kosoy", "Mike Kosoy"},
            {"Nicholas Gwiazdowski", "Nick Gwiazdowski"},
            {"Zachery William Rey", " Zach Rey"},
            {"Gable Dan Steveson", "Gable Steveson"},
            {"Mason Mark Parris", "Mason Parris"},
            {"Youssif Ibrahim Hemida", "Youssif Hemida"},
            {"Daniel Gregor Clifton Kerkvliet Jr", "Daniel Kerkvliet"},
            {"Thomas Patrick Gilman", "Thomas Gilman"},
            {"Joseph Daniel Colon", "Joe Colon"},
            {"Stevan Andria Micic", " Stevan Micic"},
            {"Darian Toi Cruz", "Darian Cruz"},
            {"Frank Vincent Perrelli Iv", "Frank Perrelli"},
            {"Jack Michael Mueller", "Jack Mueller"},
            {"Nicholas Megaludis", "Nico Megaludis"},
            {"Anthony Joseph Ramos", "Tony Ramos"},
            {"Cory John Clark", "Cory Clark"},
            {"Sean Christian Fausz", "Sean Fausz"},
            {"Gabriel Robert Tagg", "Gabe Tagg"},
            {"Joshua Pavel Saunders", "Josh Saunders"},
            {"Alec Christopher Hoover", "Alec Hoover"},
            {"Jonathan R Morrison ", "Jon Morrison"},
            {"Charles Goldsberry Tucker", "Chaz Tucker"},
            {"Zain Allen Retherford", "Zain Retherford"},
            {"Joseph Christopher Mc Kenna", "Joey McKenna"},
            {"Joseph Christopher McKenna", "Joey McKenna"},
            {"Bernard Walter Futrell", "BJ Futrell"},
            {"Evan Richard Henderson", "Evan Henderson"},
            {"John Michael Diakomihalis", "Yianni Diakomihalis"},
            {"Domonick Petro Demas", "Dom Demas"},
            {"Logan Jeffery Stieber", "Logan Stieber"},
            {"Jayson Douglas Ness", "Jayson Ness"},
            {"Joshua Michael Kindig", "Josh Kindig"},
            {"Andrew James Hoover", "Andrew Hoover"},
            {"Brandon Everett Callow", "Brandon Callow"},
            {"Spencer John Dusi", "Spencer Dusi"},
            {"John Andrew Simmons", "Andy Simmons"},
            {"Jaydin Selsor Eierman", "Jaydin Eierman"},
            {"Yahya Abdullah Thomas", "Yahya Thomas"},
            {"Colton James McCrystal", "Colton McCrystal"},
            {"Dean Jacob Heil", "Dean Heil"},
            {"Anthony James Ashnault", "Anthony Ashnault"},
            {"Jason Lyle Chamberlain", "Jason Chamberlain"},
            {"Frank Aniello Molinaro", "Frank Molinaro"},
            {"Austin Alexander Victor", "Austin Victor"},
            {"Brandon Alexander Barton", "Brandon Barton"},
            {"Jarrad Keith Lasko", "Jarrad Lasko"},
            {"Brady Gary Berge", "Brady Berge"},
            {"Brayton Edward Lee", "Brayton Lee"},
            {"Hayden Michael Hidlay", "Hayden Hidlay"},
            {"Alec William Pantaleo", "Alec Pantaleo"},
            {"Brandon John Sorenson", "Brandon Sorenson"},
            {"Logan Wesley Massa", "Logan Massa"},
            {"Isaiah Alexander Martinez", "Isaiah Martinez"},
            {"Alexander Landon Smythe", "Alex Smythe"},
            {"David Aaron Carr", "David Carr"},
            {"Mekhi Kevin Lewis", "Mekhi Lewis"},
            {"Jason Michael Nolf", "Jason Nolf"},
            {"Alexander David Dieringer", "Alex Dieringer"},
            {"Muhamed Mustafa Mc Bryde", "Muhamed McBryde"},
            {"Muhamed Mustafa McBryde", "Muhamed McBryde"},
            {"David Vincent Mc Fadden", "David McFadden "},
            {"David Vincent McFadden", "David McFadden"},
            {"Aaron Marquel Brooks", "Aaron Brooks"},
            {"Daniel Joseph Novak", "Daniel Novak"},
            {"Seth Matthew Winkle", "Seth Winkle"},
            {"Kyle Dylan Roberts", "Kyle Roberts"},
            {"David Morris Taylor", "David Taylor"},
            {"Mark John Hall Ii", "Mark Hall"},
            {"Mark John Hall", "Mark Hall"},
            {"James Patrick Downey", "Pat Downey"},
            {"James Patrick Downey Iii", "Pat Downey"},
            {"Samuel Joseph Brooks", "Sammy Brooks"},  
            {"Nicholas Joseph Heflin", "Nick Heflin"},
            {"Trent Niemond Hidlay", "Trent Hidlay"},
            {"Lou John Perez", "Lou Perez"},
            {"Christian Brian Hipsher", "Christian Hipsher"},
            {"Matthew Christopher Ferraro", "Matthew Ferraro"},  
            {"Kenneth Rashad Courts", "Kenny Courts"},
            {"J’den Michael Tbory Cox", "J’den Cox"},
            {"Michael Justin Macchiavello", "Mike Macchiavello"},
            {"Bo Dean Nickal", "Bo Nickal"},
            {"Lucas John Davison", "Lucas Davison"},  
            {"Christian William Brunner", "Christian Brunner"},
            {"Jacob Alexander Warner", "Jacob Warner"},
            {"Kyle Frederick Snyder", "Kyle Snyder"},
            {"Benjamin Errol Provisor", "Ben Provisor"},
            {"Ty Ryan Jack Walz", "Ty Walz"},  
            {"Hayden Nicholas Zillmer", "Hayden Zillmer"},
            {"Kyven Ross Gadson", "Kyven Gadson"},
            {"Kollin Raymond Moore", "Kollin Moore"},
            {"Zachery Samuel Elam ", "Zach Elam"},
            {"Nathan Joseph Burak", "Nathan Burak"},  
            {"Micah Leo Burak", "Micah Burak"},
            {"Franklin Gomez Matos", "Franklin Gomez"},
            {"Frank Chamizo Marquez", "Frank Chamizo"},
            {"Jeandry Garzon Caballero", "Geandry Garzon"},
            {"Jeandry Garzon", "Geandry Garzon"},
            {"Geandry Garzon Caballero", "Geandry Garzon"},
            {"Alejandro Enrique Valdes Tobier", "Alejandro Valdes"},
            {"Alejandro Valdes Tobier", "Alejandro Valdes"},
            {"Bajrang Bajrang", "Bajrang Punia"},
            {"Reineri Andreu Ortega", "Reineri Andreu"},
            {"Reineris Salas Perez", "Reineris Salas"},
            {"Alan Lavrentevitch Khugaev", "Alan Khugaev"},
            {"Bililal Makhov", "Bilyal Makhov"},
            {"Oleksandr Khotsianivskyi", "Alexander Khotsianivski"},
            {"Anzor Ruslanovitch Khizriev", "Anzor Khizriev"},
            {"Said Gamidovitch Gamidov", "Said Gamidov"},
            {"Yudenny Alpajon Estevez", "Yudenny Alpajon"},
            {"Alexander Bogomoev", "Aleksander Bogomoev"},
            {"Aleksandr Bogomoev", "Aleksander Bogomoev"},
            {"Nachyn Sergeevitch Kuular", "Nachyn Kuular"},
            {"Haji Aliyev", "Haji Aliev"},
            {"Zavur Uguev", "Zaur Uguev"},
            {"Nurislam (artas) Sanayev (sanaa)", "Nurislam Sanayev"},
            {"Ismail Hajiyev", "Ismail Hajiev"},
            {"Khalid Iakhiev", "Khalid Yakhiev"},
            {"Ertine Mortuy Ool", "Ertine Mortuy-Ool"},
            {"Ertine Mortuy-ool", "Ertine Mortuy-Oo"},
            {"Ilias Bekbulatov", "Ilyas Bekbulatov"},
            {"Bekkhan Goigereev", "Bekhan Goigereev"},
            {"Eduard Grigoriev", "Eduard Grigorev"},
            {"Ismail Muskaev", "Ismail Musukaev"},
            {"Iszmail Muszukajev", "Ismail Musukaev "},
            {"Tulga Tumur Ochir", "Tulga Tumur-Ochir"},
            {"Tulga Tumur-ochir", "Tulga Tumur-Ochir"},
            {"Peiman Bioukagha Biabani", "Pejman Biabani"},
            {"Peiman Biabani", "Pejman Biabani"},
            {"Peyman Biabani", "Pejman Biabani"},
            {"Hassan Aliazam Yazdanicharati", "Hassan Yazdani"},
            {"Amir Maghsoudi", "Amir Magsoudi"},
            {"Amirmohammad Babak Yazdanicherati", "Amir Yazdani"},
            {"Domenic Michael Abou Nader", "Domenic Abounader"},
            {"Domenic Michael Abounader", "Domenic Abounader"},
            {"Myles Nazem Amine", "Myles Amine"},
            {"Jaime Yusept Espinal", "Jaime Espinal"},
            {"Uladislau Andreyeu", "Vladislav Andreev"},
            {"Kumar Ravi", "Ravi Kumar"},
            {"Reza Ahmadali Atrinagharchi", "Reza Atri"},
            {"Hamed Zarrin", "Hamid Zarrin"},
            {"Hamid Zarrin Peydar", "Hamidreza Zarrin"},
            {"Hamidreza Zarrin Peykar", "Hamidreza Zarrin"},
            {"Abbas Fourotan", "Abbas Foroutanrami"},
            {"Arshak Mohebbi", "Arashk Mohebbi"},
            {"Yadollah Mohammadkazem Mohebi", "Yadollah Mohebbi"},
            {"Yadollah Mohebi", "Yadollah Mohebbi"},
            {"Ali Khalil Shabanibengar", "Ali Shabani"},
            {"Sajad Gholamhossein Azizi", "Sajad Azizi"},
            {"Mojtaba Mohammadshafie Goleij", "Mojtaba Goleij"},
            {"Alireza Mohammad Karimimachiani", "Alireza Karimi"},
            {"Ali Reza Abbasali Abdollahi", " Ali Reza Abdollahi"},
            {"Hossein Lotfali Shahbazigazvar", "Hossein Shahbazi"},
            {"Seyedabolfazl Seyedabbasali Hashemijouybari", "Seyed Hashemi"},
            {"Kamran Ghasempour", "Kamran Ghasempour"},
            {"Ezzatollah Abbas Akbarizarinkolaei ", "Ezzatollah Akbari"},
            {"Sajjad Saberali Gholami", "Sajjad Gholami "},
            {"Reza Alireza Afzalipaemami", "Reza Afzali"},
            {"Ali Bakhtiar Savadkouhi", "Ali Savadkouhi"},
            {"Navid Morad Zanganeh", "Navid Zanganeh"},
            {"Mostafa Mohabbali Hosseinkhani", "Mostafa Hosseinkhani "},
            {"Mohammad Ashghar Nokhodilarimi", "Mohammad Nokhodi"},
            {"Yones Aliakbar Emamichoghaei", "Yones Emami"},
            {"Amirhossein Morteza Gholi Kavousi", "Amirhossein Kavousi"},
            {"Mohmmadsadegh Biglar Firouzpourbandpei", "Mohmmad Firouzpour"},
            {"Amirhossein Ali Hosseini", "Amirhossein Hosseini"},
            {"Amirhossein Azim Maghsoudi", "Amir Magsoudi"},
            {"Morteza Hassanali Ghiasi Cheka", "Morteza Ghiasi "},
            {"Behnam Eshagh Ehsanpoor", "Behnam Ehsanpoor"},
            {"Mohammadbagher Esmaeil Yakhkeshi", "Mohammad Yakhkeshi"}

        };

              foreach (var pair in americans)
                {
                string name = pair.Key;

                if (newAmericanName.Contains(name))
                {
                    return pair.Value;
                }
               
                }

            return newAmericanName;



        }
    }
}
