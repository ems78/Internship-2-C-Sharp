using SvjetskoPrvenstvo;
using System.Collections;

Dictionary<string, (string position, int rating)> playersInfo = new Dictionary<string, (string position, int rating)>(new stringEqualityComparer())
{
    { "Ivo Grbic", ("GK", 74)},
    { "Ivica Ivusic", ("GK", 72)},
    { "Luka Modric", ("MF", 88)},
    { "Marcelo Brozovic", ("MF", 86)},
    { "Mateo Kovacevic", ("MF", 84)},
    { "Ivan Perisic", ("MF", 84)},
    { "Ivan Rakitic", ("MF", 82)},
    { "Mario Pasalic", ("MF", 81)},
    { "Duje Caleta-Car", ("DF", 78)},
    { "Dejan Lovren", ("DF", 78)},
    { "Josko Gvardiol", ("DF", 81)},
    { "Domagoj Vida", ("DF", 76)},
    { "Josip Sutalo", ("DF", 75)},
    { "Josip Juranovic", ("DF", 75)},
    { "Andrej Kramaric", ("FW", 82)},
    { "Ante Rebic", ("FW", 80)},
    { "Ante Budimir", ("FW", 76)},
    { "Nikola Kalinic", ("FW", 74)},
    { "Petar Musa", ("FW", 74)},
    { "Damir Kreilach", ("FW", 73)}
};

var gk = new List<(string name, int rating)>();
var mf = new List<(string name, int rating)>();
var df = new List<(string name, int rating)>();
var fw = new List<(string name, int rating)>();

var ekipa = new List<(string name, int rating)>();


void PrintLine()
{
    Console.WriteLine("------------------------------------\n");
}


// dijalog za potvrdu akcije
bool AreYouSure(string typeOfConfirmation)   //nadodat u sve akcije      
{
    Console.Write($"Jeste li sigurni da želite {typeOfConfirmation}? (y/n): ");
    var confirm = Console.ReadLine();
    confirm = confirm?.ToLower();
    if (confirm == "y")
    {
        return true;
    }
    return false;
}


// metoda za navigiranje po izborniku
int UnosIzbora(string typeOfChoice)
{
    int choice;
    bool flag = false;

    do
    {
        Console.Write($"\nUnesite {typeOfChoice}: ");
        flag = int.TryParse(Console.ReadLine(), out choice);
    } while (flag == false);

    return choice;
}


void ReturnToIzbornik()
{
    Console.Write("\n\n0 - Povratak na izbornik\n");
    PrintLine();

    int back;
    while ((back = UnosIzbora("broj za izbor")) != 0)
    {
        Console.WriteLine("Opcija ne postoji!");
    }
    Izbornik();
}


void Ispis(Dictionary<string, (string position, int rating)> playersInfo)
{
    PrintLine();
    foreach (var player in playersInfo)
    {
        Console.WriteLine($"{player.Key}\t{player.Value.position}\t{player.Value.rating}");
    }
}


void Ispis2(IOrderedEnumerable<KeyValuePair<string, (string position, int rating)>> players)
{
    PrintLine();
    foreach (var player in players)
    {
        Console.WriteLine($"{player.Key}\t{player.Value.position}\t{player.Value.rating}");
    }
}


// metoda za razvrstavanje igrača u liste za pozicije
void organisePlayers(List<(string name, int rating)> playersOnPosition, string positionName, bool printPlayers)
{
    playersOnPosition.Clear();

    foreach (var player in playersInfo.Keys)
    {
        if (playersInfo[player].position.Equals(positionName))
        {
            playersOnPosition!.Add((player, playersInfo[player].rating));
        }
    }
    playersOnPosition!.Sort((a, b) => b.rating.CompareTo(a.rating));  // sortiranje po ratingu

    if (printPlayers)
    {
        foreach (var player in playersOnPosition)
        {
            Console.WriteLine($"{player.name}\t{player.rating}");
        }
    }
}


// main izbornik
void Izbornik()
{
    bool repeat = true;
    do
    {
        Console.Clear();
        Console.WriteLine("1 - Odradi trening\n2 - Odigraj utakmicu\n3 - Statistika\n4 - Kontrola igraca\n0 - Izlaz iz aplikacije\n\n");
        int choice = UnosIzbora("broj za izbor");
        switch (choice)
        {
            // Odradi trening
            case 1: 
                if (AreYouSure("odraditi trening"))
                {
                    OdradiTrening(playersInfo);
                    repeat = false;
                    break;
                }
                break;

            // Odigraj utakmicu
            case 2:
                if (AreYouSure("odigrati utakmicu"))
                {
                    OdigrajUtakmicu(playersInfo);
                    repeat = false;
                    break;
                }
                break;

            // Statistika
            case 3:
                repeat = false;
                Statistika(playersInfo);
                break;

            // Kontrola igraca
            case 4:
                KontrolaIgraca(playersInfo);
                repeat = false;
                break;

            // izlaz iz aplikacije
            case 0:
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Izbor mora biti 0, 1, 2, 3 ili 4.");
                choice = UnosIzbora("broj za izbor");
                break;
        }
    } while (repeat);

}


void OdradiTrening(Dictionary<string, (string position, int rating)> playersInfo)
{
    Console.Clear();
    Random randomNum = new Random();
    foreach (var player in playersInfo)
    {
        float percentage = randomNum.Next(-5, 5) / 100f;
        var oldRating = player.Value.rating;                                                               
        int newRating = (int)(oldRating * (1 + percentage));  
        if (newRating >= 100)  
        {   
            newRating = 100;
        }
        else if (newRating <= 0)
        {
            newRating = 0;
        }
        Console.Write($"{player.Key} ({player.Value.position}) - old rating: {oldRating}\tnew rating: {newRating}\n");
        playersInfo[player.Key] = (player.Value.position, newRating);       
    }
    ReturnToIzbornik();
}


void OdigrajUtakmicu(Dictionary<string, (string position, int rating)> playersInfo)  
{
    if (playersInfo.Count < 26)
    {
        Console.WriteLine("Nema dovoljno igrača!");
        if (AreYouSure("vratiti se na izbornik?"))
        {
            ReturnToIzbornik();
        }
    }
    organisePlayers(gk!, "GK", false);
    if (gk!.Count < 1)
    {
        Console.WriteLine("Nema dovoljno igrača! (GK)");
        if (AreYouSure("vratiti se na izbornik?"))
        {
            ReturnToIzbornik();
        }
    }
    organisePlayers(df!, "DF", false);
    if (df!.Count < 4)
    {
        Console.WriteLine("Nema dovoljno igrača! (DF)");
        if (AreYouSure("vratiti se na izbornik?"))
        {
            ReturnToIzbornik();
        }
    }
    organisePlayers(mf!, "MF", false);
    if (mf!.Count < 3)
    {
        Console.WriteLine("Nema dovoljno igrača! (MF)");
        if (AreYouSure("vratiti se na izbornik?"))
        {
            ReturnToIzbornik();
        }
    }
    organisePlayers(fw!, "FW", false);
    if (fw!.Count < 3)
    {
        Console.WriteLine("Nema dovoljno igrača! (FW)");
        if (AreYouSure("vratiti se na izbornik?"))
        {
            ReturnToIzbornik();
        }
    }

    ekipa!.Add(gk[0]);
    
    for (int i = 0; i < 4; i++)
    {
        ekipa!.Add(df[i]);
    }
    
    for (int i = 0; i < 3; i++)
    {
        ekipa!.Add(df[i]);
        ekipa!.Add(fw[i]);
    }

    Random randomInt = new Random();
    int golEkipa = randomInt.Next(0, 7);
    int golProtivnici = randomInt.Next(0, 7);

    List<string> strijelci = new List<string>();
    for (int i = 0; i < golEkipa; i++)
    {
        int pozicijaStrijelca = randomInt.Next(0, fw.Count - 1);
        strijelci.Add(fw[pozicijaStrijelca].name);
    }                                                                                       // <---------------------------------------------------------

}


void Statistika(Dictionary<string, (string position, int rating)> playersInfo)
{
    Console.Clear();
    Console.WriteLine("1 - Ispis svih igrača");
    PrintLine();

    int choice;
    while ((choice = UnosIzbora("broj za izbor")) != 1)
    {
        Console.WriteLine("Krivi unos!");
    }

    bool repeat = true;
    do
    {
        Console.Clear();
        Console.WriteLine($"Ispis svih igrača\n\n1 - Ispis onako kako su spremljeni\n2 - Ispis po rating uzlazno\n3 - Ispis po ratingu silazno\n" +
            $"4 - Ispis igrača po imenu i prezimenu\n5 - Ispis igrača po ratingu\n6 - Ispis igrača po poziciji\n7 - Ispis trenutno prvih 11 igrača\n" +
            $"8 - Ispis strijelaca i koliko golova imaju\n9 - Ispis svih rezultata ekipe\n10 - Ispis rezultat svih ekipa\n11 - Ispis tablice grupe");

        choice = UnosIzbora("broj za izbor");

        switch (choice)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("Ispis igrača onako kako su spremljeni:\n\nIme\tPozicija\tRating");
                Ispis(playersInfo);
                ReturnToIzbornik();
                repeat = false;
                break;
            case 2:
                Console.Clear();
                Console.WriteLine("Ispis igrača po ratingu uzlazno:\n\nIme\tPozicija\tRating");
                var sortedRatingAscending = from player in playersInfo orderby player.Value.rating ascending select player;
                Ispis2(sortedRatingAscending);
                ReturnToIzbornik();
                repeat = false;
                break;
            case 3:
                Console.Clear();
                Console.WriteLine("Ispis igrača po ratingu silazno:\n\nIme\tPozicija\tRating");
                var sortedRatingDescending = from player in playersInfo orderby player.Value.rating descending select player;
                Ispis2(sortedRatingDescending);
                ReturnToIzbornik();
                repeat = false;
                break;
            case 4:
                Console.Clear();
                Console.WriteLine("Ispis igrača po imenu i prezimenu:\n\nIme\tPozicija\tRating");
                var sortedName = from player in playersInfo orderby player.Key ascending select player;
                Ispis2(sortedName);
                ReturnToIzbornik();
                repeat = false;
                break;

            // po ratingu??
            case 5:         //     <-------------------------------------------------------------

            // po poziciji
            case 6:
                Console.Clear();
                Console.WriteLine("Ispis igrača po poziciji:\n\nIme\tPozicija\tRating\n");
                PrintLine();

                Console.WriteLine("GK igrači:\nIme\t\tRating\n");
                organisePlayers(gk!, "GK", true);

                Console.WriteLine("\n\nMF igrači:\nIme\t\tRating\n");
                organisePlayers(mf!, "MF", true);

                Console.WriteLine("\n\nDF igrači:\nIme\t\tRating\n");
                organisePlayers(df!, "DF", true);

                Console.WriteLine("\n\nFW igrači:\nIme\t\tRating\n");
                organisePlayers(fw!, "FW", true);

                ReturnToIzbornik();
                repeat = false;
                break;
            
            // top 11 po pozicijama
            case 7:
                Console.Clear();
                Console.WriteLine("Ispis top 11 igrača po pozicijama:");
                PrintLine();

                organisePlayers(gk!, "GK", false);
                Console.WriteLine($"GK - {gk![0].name}\t{gk[0].rating}");

                organisePlayers(df!, "DF", false);
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine($"DF - {df![i].name}\t{df[i].rating}");
                }

                organisePlayers(mf!, "MF", false);
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"MF - {mf![i].name}\t{mf[i].rating}");
                }

                organisePlayers(fw!, "FW", false);
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"FW - {fw![i].name}\t{fw[i].rating}");
                }

                ReturnToIzbornik();
                repeat = false;
                break;

            // ispis strijelaca i koliko golova imaju             
            case 8:
                Console.Clear();
                Console.WriteLine("Ispis strijelaca i koliko golova imaju:\n\nIme\tGolovi");
                PrintLine();

                Random randomInt = new Random();
                organisePlayers(fw!, "FW", false);
                foreach (var player in fw!)
                {
                    int brojGolova = randomInt.Next(0,300);
                    Console.WriteLine($"{player.name}\t{brojGolova}");
                }

                ReturnToIzbornik();
                repeat = false;
                break;

            // svi rezultati ekipe ??          //     <-------------------------------------------------
            case 9:

                Console.Clear();
                Console.WriteLine("Ispis rezultata ekipe:");

                ReturnToIzbornik();
                repeat = false;
                break;

            // rezultat svih ekipa ??                //     <-------------------------------------------------
            case 10:
                Console.Clear();
                Console.WriteLine("Ispis rezultata svih ekipa:");

                ReturnToIzbornik();
                repeat = false;
                break;

            // tablica grupe ??                  //     <-------------------------------------------------
            case 11:
                Console.Clear();
                Console.WriteLine("Ispis tablice grupe:");

                ReturnToIzbornik();
                repeat = false;
                break;

            default:
                break;
        }
    } while (repeat);

}


void KontrolaIgraca(Dictionary<string, (string position, int rating)> playersInfo)
{
    Console.Clear();

    Console.WriteLine("1 - Unos novog igrača\n2 - Brisanje igrača\n3 - Uređivanje igrača\n0 - Izlaz iz kontrole igrača");

    int choice = UnosIzbora("broj za izbor");
    bool repeat = true;
    do
    {
        switch (choice)
        {
            // unos novog igrača
            case 1:
                Console.Clear();
                Console.WriteLine("Unos novog igrača");
                PrintLine();
                if (playersInfo.Count is 26)
                {
                    Console.WriteLine("Ekipa je popunjena.");
                    ReturnToIzbornik();
                    break;
                }
                else 
                {
                    var newPlayerName = "";
                    while (true)
                    {
                        Console.Write("Unesite ime i prezime novog igrača: ");
                        newPlayerName = Console.ReadLine();

                        if (newPlayerName is not "")  
                        {
                            if (playersInfo.ContainsKey(newPlayerName!))
                            {
                                Console.WriteLine("Igrač je već unesen!");
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    
                    var newPlayerPosition = "";
                    while (newPlayerPosition is "")
                    {
                        Console.Write("\nUnesite poziciju novog igrača: ");
                        newPlayerPosition = Console.ReadLine();
                    }
                    newPlayerPosition = newPlayerPosition!.ToUpper();
                    string[] pozicije = { "GK", "MF", "DF", "FW" };
                    while (!pozicije.Contains(newPlayerPosition))
                    {
                        Console.Write("\nUnesite ispravnu poziciju (GK, MF, DF ili FW):");
                        newPlayerPosition = Console.ReadLine();
                    }
                    
                    var newPlayerRating = UnosIzbora("rating novog igrača [1-100]");
                    while (newPlayerRating < 0 || newPlayerRating > 100)
                    {
                        newPlayerRating = UnosIzbora("ispravan rating [1-100]");
                    }

                    if (AreYouSure("unjeti novog igrača")) // bez dodatnog uvjeta daje warning
                    {
                        playersInfo.Add(newPlayerName!, (newPlayerPosition!, newPlayerRating));
                    }                    
                }
                repeat= false;
                break;

            // brisanje igraca
            case 2:                 
                Console.Clear();
                Console.WriteLine("Brisanje igrača");
                PrintLine();
                Console.WriteLine("1 - Brisanje igrača unosom imena i prezimena");

                while (UnosIzbora("broj za izbor") is not 1)
                {
                    Console.WriteLine("Ne postoji ta opcija.");
                }

                Console.Clear();
                Console.WriteLine("Brisanje igrača unosom imena i prezimena");
                PrintLine();

                var playerToDelete = "";
                while (true)
                {
                    Console.Write("Unesite ime i prezime igrača kojeg želite izbrisati: ");
                    playerToDelete = Console.ReadLine();

                    if (playerToDelete is not "")   
                    {
                        if (playersInfo.ContainsKey(playerToDelete!))
                        {
                            break;
                        }
                    }
                }

                if (AreYouSure("izbrisati ovog igrača"))
                {
                    playersInfo.Remove(playerToDelete!);
                }
                repeat = false;
                break;

            // uredivanje igraca
            case 3:                  
                Console.Clear();
                Console.WriteLine("Uređivanje igrača");
                PrintLine();

                var playerName = "";
                while (true)
                {
                    Console.WriteLine("Upišite ime i prezime igrača kojeg želite urediti: ");
                    playerName = Console.ReadLine();

                    if (playerName is not "")
                    {
                        if (!playersInfo.ContainsKey(playerName!))
                        {
                            Console.WriteLine("Igrač nije u popisu!");
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                Console.Clear();
                Console.WriteLine($"Uredi igrača {playerName}");
                PrintLine();
                Console.WriteLine("1 - Uredi ime i prezime igrača\n2 - Uredi poziciju igrača (GK, DF, MF ili FW)\n3 - Uredi rating igrača [1 - 100]\n0 - povratak na izbornik");

                var repeat2 = true;
                do
                {
                    switch (UnosIzbora("broj za izbor"))
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine($"Uredi ime i prezime igrača {playerName}");
                            PrintLine();
                            Console.Write("Unesite novo ime i prezime: ");
                            var newName = "";
                            while (newName is "")
                            {
                                newName = Console.ReadLine();
                            }

                            if (AreYouSure($"promijeniti ime igrača {playerName}"))
                            {
                                var temp = (playersInfo[playerName!].position, playersInfo[playerName!].rating);
                                playersInfo.Remove(playerName!);
                                playersInfo.Add(newName!, temp);
                            }

                            repeat2 = false;
                            break;

                        // uredi poziciju igrača
                        case 2:
                            Console.Clear();
                            Console.WriteLine($"Uredi poziciju igrača {playerName} (GK, DF, MF ili FW)"); 
                            PrintLine();
                            Console.Write("Unesite novu poziciju: ");
                            //var newPosition = "";
                            string[] pozicije = { "GK", "DF", "MF", "FW" };
                            var newPosition = "";
                            while (true)
                            {
                                newPosition = Console.ReadLine();
                                if (newPosition is not "")
                                {
                                    newPosition = newPosition!.ToUpper();
                                    if (pozicije.Contains(newPosition))
                                    {   
                                        break;
                                    }
                                    Console.Write("\nNeispravan unos pozicije!\nUnesite novu poziciju: ");
                                }
                            }

                            if (AreYouSure($"promijeniti poziciju igrača {playerName}"))
                            {
                                var temp = (newPosition, playersInfo[playerName!].rating);
                                playersInfo[playerName!] = temp!;
                            }

                            repeat2 = false;
                            break;

                        // uredi rating igrača
                        case 3:
                            Console.Clear();
                            Console.WriteLine($"Uredi rating igrača {playerName}");
                            PrintLine();

                            var newRating = 0;
                            while (true)
                            {
                                newRating = UnosIzbora("novi rating [1 - 100]");
                                if (newRating > 0 && newRating < 101)   
                                {
                                    break;
                                }
                                Console.WriteLine("\nNeispravan unos ratinga.");
                            }

                            if (AreYouSure($"promijeniti rating igraču {playerName}"))
                            {
                                var temp = (playersInfo[playerName!].position, newRating);
                                playersInfo[playerName!] = temp;
                            }

                            repeat2 = false;
                            break;

                        case 0:
                            if (AreYouSure("vratiti na izbornik"))
                            {
                                repeat2 = false;
                                ReturnToIzbornik();
                                break;
                            }                            
                            break;

                        default:
                            break;
                    }
                } while (repeat2);
                repeat = false;
                break;

            case 0:
                Izbornik();
                repeat= false;
                break;

            default:
                choice= UnosIzbora("broj za izbor");
                break;
        }
    } while (repeat);
    KontrolaIgraca(playersInfo);
}


Izbornik();


