﻿using SvjetskoPrvenstvo;
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

string[] positions = { "GK", "MF", "DF", "FW" };
var gk = new List<(string name, int rating)>();
var mf = new List<(string name, int rating)>();
var df = new List<(string name, int rating)>();
var fw = new List<(string name, int rating)>();
var ekipa = new List<(string name, int rating)> ();

/*
var gk = new Dictionary<string, (string position, int rating)>();
var mf = new Dictionary<string, (string position, int rating)>();
var df = new Dictionary<string, (string position, int rating)>();
var fw = new Dictionary<string, (string position, int rating)>();
var ekipa = new Dictionary<string, (string position, int rating)>();
*/

void PrintLine()
{
    Console.WriteLine("------------------------------------\n");
}


// dijalog za potvrdu akcije
bool AreYouSure(string typeOfConfirmation)     
{
    Console.Write($"Jeste li sigurni da želite {typeOfConfirmation}? (y/n): ");
    var confirm = Console.ReadLine();
    if (confirm?.ToLower() == "y")
    {
        return true;
    }
    return false;
}


// metoda za navigiranje po izborniku
int UnosIzbora(string typeOfChoice)
{
    int choice;
    do
    {
        Console.Write($"\nUnesite {typeOfChoice}: ");
    } while (!int.TryParse(Console.ReadLine(), out choice));
    return choice;
}


void ReturnToIzbornik()
{
    Console.Write("\n\n0 - Povratak na izbornik\n");
    PrintLine();

    int back;
    while ((back = UnosIzbora("broj za izbor")) is not 0)
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


void Ispis2(IOrderedEnumerable<KeyValuePair<string, (string position, int rating)>> players)  // mogu li se ikako ispis i ispis2 spojit?
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
    playersOnPosition!.Sort((a, b) => b.rating.CompareTo(a.rating));
    //var sortedPlayersOnPostion = playersOnPosition.Values.OrderBy<>

    if (printPlayers)
    {
        foreach (var player in playersOnPosition)
        {
            Console.WriteLine($"{player.name}\t{player.rating}");
        }
    }
}


void CalculateNewRating(int minValue, int maxValue, bool printResults)
{
    Random randomNum = new Random();
    foreach (var player in playersInfo)
    {
        float percentage = randomNum.Next(minValue, maxValue) / 100f;
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

        if (printResults)
        {
            Console.Write($"{player.Key} ({player.Value.position}) - old rating: {oldRating}\tnew rating: {newRating}\n");
        }
        playersInfo[player.Key] = (player.Value.position, newRating);
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
        switch (UnosIzbora("broj za izbor"))
        {
            case 1: // Odradi trening
                if (AreYouSure("odraditi trening"))
                {
                    OdradiTrening();
                    repeat = false;
                    break;
                }
                break;

            case 2:  // Odigraj utakmicu
                if (AreYouSure("odigrati utakmicu"))
                {
                    OdigrajUtakmicu();
                    repeat = false;
                    break;
                }
                break;

            case 3: // Statistika
                repeat = false;
                Statistika();
                break;

            case 4:  // Kontrola igraca
                KontrolaIgraca();
                repeat = false;
                break;

            case 0:
                Environment.Exit(0);
                break;

            default:
                break;
        }
    } while (repeat);

}


void OdradiTrening()
{
    Console.Clear();
    CalculateNewRating(-5, -5, true);
    ReturnToIzbornik();
}


void CheckPlayers(List<(string name, int rating)> playersOnPosition, int minNumber, string positionName)
{
    organisePlayers(playersOnPosition!, positionName, false);
    if (playersOnPosition!.Count < minNumber)
    {
        Console.WriteLine($"Nema dovoljno igrača na poziciji {positionName}!");
        if (AreYouSure("vratiti se na izbornik"))
        {
            ReturnToIzbornik();
        }
    }
}


void OdigrajUtakmicu()  
{
    if (playersInfo.Count < 20) // 26
    {
        Console.WriteLine("Nema dovoljno igrača!");
        ReturnToIzbornik();
    }

    CheckPlayers(gk!, 1, "GK");
    CheckPlayers(df!, 1, "DF");
    CheckPlayers(mf!, 1, "MF");
    CheckPlayers(fw!, 1, "FW");

    ekipa!.Add(gk![0]);
    
    for (int i = 0; i < 4; i++)
    {
        ekipa.Add(df![i]);
    }
    
    for (int i = 0; i < 3; i++)
    {
        ekipa.Add(df![i]);
        ekipa.Add(fw![i]);
    }

    Random randomInt = new Random();
    int golEkipa = randomInt.Next(0, 7);
    int golProtivnici = randomInt.Next(0, 7);

    List<string> strijelci = new List<string>();
    for (int i = 0; i < golEkipa; i++)
    {
        int pozicijaStrijelca = randomInt.Next(0, fw!.Count - 1);
        strijelci.Add(fw[pozicijaStrijelca].name);
    }                                                                                   // <---------------------------------------------------------

    // mijenjanje ratinga - napravit metodu ?? 
    //           -strijelci +5%

    //           -ostatak  +-2% (pobjeda/poraz)


    // izlistat sve rezultate trenutnog kola skupine (6 utakmica)
    ReturnToIzbornik();
}


void Statistika()
{
    Console.Clear();
    Console.WriteLine("1 - Ispis svih igrača");
    PrintLine();

    //int choice;
    while (UnosIzbora("broj za izbor") != 1)
    {
        Console.WriteLine("Krivi unos!");
    }

    bool repeat = true;
    do
    {
        Console.Clear();
        Console.WriteLine("Ispis svih igrača\n\n1 - Ispis onako kako su spremljeni\n2 - Ispis po rating uzlazno\n3 - Ispis po ratingu silazno\n" +
            "4 - Ispis igrača po imenu i prezimenu\n5 - Ispis igrača po ratingu\n6 - Ispis igrača po poziciji\n7 - Ispis trenutno prvih 11 igrača\n" +
            "8 - Ispis strijelaca i koliko golova imaju\n9 - Ispis svih rezultata ekipe\n10 - Ispis rezultat svih ekipa\n11 - Ispis tablice grupe\n12 - Povratak na izbornik");

        switch (UnosIzbora("broj za izbor"))
        {
            case 1:
                Console.Clear();
                Console.WriteLine("Ispis igrača onako kako su spremljeni:\n\nIme\tPozicija\tRating");
                Ispis(playersInfo);
                ReturnToIzbornik();
                break;
            case 2:
                Console.Clear();
                Console.WriteLine("Ispis igrača po ratingu uzlazno:\n\nIme\tPozicija\tRating");
                var sortedRatingAscending = from player in playersInfo orderby player.Value.rating ascending select player;
                Ispis2(sortedRatingAscending);
                ReturnToIzbornik();
                break;
            case 3:
                Console.Clear();
                Console.WriteLine("Ispis igrača po ratingu silazno:\n\nIme\tPozicija\tRating");
                var sortedRatingDescending = from player in playersInfo orderby player.Value.rating descending select player;
                Ispis2(sortedRatingDescending);
                ReturnToIzbornik();
                break;
            case 4:
                Console.Clear();
                Console.WriteLine("Ispis igrača po imenu i prezimenu:\n\nIme\tPozicija\tRating");
                var sortedName = from player in playersInfo orderby player.Key ascending select player;
                Ispis2(sortedName);
                ReturnToIzbornik();
                break;

            
            case 5: // po ratingu??        <-------------------------------------------------------------


            case 6: // po poziciji
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
                break;
            
            case 7:  // top 11 po pozicijama
                Console.Clear();
                Console.WriteLine("Ispis top 11 igrača po pozicijama:");  // problem kad nema dovoljno ljudi
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
                break;

            case 8: // ispis strijelaca i koliko golova imaju  
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
                break;

            case 9: // svi rezultati ekipe ??          //     <-------------------------------------------------

                Console.Clear();
                Console.WriteLine("Ispis rezultata ekipe:");

                ReturnToIzbornik();
                break;

            case 10: // rezultat svih ekipa ??                //     <-------------------------------------------------
                Console.Clear();
                Console.WriteLine("Ispis rezultata svih ekipa:");

                ReturnToIzbornik();
                break;

            case 11: // tablica grupe ??                  //     <-------------------------------------------------
                Console.Clear();
                Console.WriteLine("Ispis tablice grupe:");

                ReturnToIzbornik();
                break;

            case 0:
                ReturnToIzbornik();
                break;

            default:
                break;
        }
    } while (repeat);

}


void KontrolaIgraca()
{
    bool repeat = true;
    do
    {
        Console.Clear();
        Console.WriteLine("1 - Unos novog igrača\n2 - Brisanje igrača\n3 - Uređivanje igrača\n0 - Izlaz iz kontrole igrača");
        switch (UnosIzbora("broj za izbor"))
        {
            case 1:  // unos novog igrača
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
                    while (newPlayerRating < 1 || newPlayerRating > 100)
                    {
                        newPlayerRating = UnosIzbora("ispravan rating [1-100]");
                    }

                    if (AreYouSure("unjeti novog igrača")) 
                    {
                        playersInfo.Add(newPlayerName!, (newPlayerPosition!, newPlayerRating));
                    }                    
                }
                repeat= false;
                break;

            case 2: // brisanje igraca      
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

            case 3: // uredivanje igraca             
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

                        case 2: // uredi poziciju igrača
                            Console.Clear();
                            Console.WriteLine($"Uredi poziciju igrača {playerName} (GK, DF, MF ili FW)"); 
                            PrintLine();
                            Console.Write("Unesite novu poziciju: ");
                            var newPosition = "";
                            while (true)
                            {
                                newPosition = Console.ReadLine();
                                if (newPosition is not "")
                                {
                                    newPosition = newPosition!.ToUpper();
                                    if (positions.Contains(newPosition))
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

                        case 3:  // uredi rating igrača
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
                break;
        }
    } while (repeat);
    KontrolaIgraca();
}

Izbornik();