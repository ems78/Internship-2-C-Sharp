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

string[] positions = { "GK", "MF", "DF", "FW" };
var gk = new Dictionary<string, (string position, int rating)>();
var mf = new Dictionary<string, (string position, int rating)>();
var df = new Dictionary<string, (string position, int rating)>();
var fw = new Dictionary<string, (string position, int rating)>();
var ekipa = new Dictionary<string, (string position, int rating)>();
var rezultatiEkipe = new List<(int rezEkipe, int rezProtivnika)>();
var bodoviOstali = new Dictionary<string, (int bodovi, int golRazlika)>()
{
    {"Belgium", (0, 0)},       
    {"Canada", (0, 0)},
    {"Marocco", (0, 0)},
    {"Croatia", (0, 0)}
};   
var protivnici = new List<(bool odigrano, int kolo, string fristTeam, string secondTeam, int rezultatFitstTeam, int rezultatSecondTeam)> 
{
    (false, 1,  "Marocco", "Croatia", 0, 0),
    (false, 1, "Canada", "Belgium", 0, 0),
    (false, 2, "Croatia", "Canada", 0, 0),
    (false, 2, "Belgium", "Marocco", 0, 0),
    (false, 3, "Croatia", "Belgium", 0, 0),
    (false, 3, "Canada", "Marocco", 0, 0),
};


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
    while ((back = UnosIzbora("0 za povratak na izbornik")) is not 0)
    {
        Console.WriteLine("Opcija ne postoji!");
    }
    Izbornik();
}


void Ispis<T>(T playersInfo) where T : Dictionary<string, (string position, int rating)> 
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


void CalculateNewRating(Dictionary<string, (string position, int rating)> listOfPlayers, int minValue, int maxValue, bool printResults)
{
    Random randomNum = new Random();
    foreach (var player in listOfPlayers)
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


// metoda za razvrstavanje igrača u liste za pozicije
void organisePlayers(Dictionary<string, (string position, int rating)> playersOnPosition, string positionName, bool printPlayers, int numberOfPlayersToPrint)
{
    playersOnPosition.Clear();
    foreach (var player in playersInfo.Keys)
    {
        if (playersInfo[player].position.Equals(positionName))
        {
            playersOnPosition!.Add(player, playersInfo[player]);
        }
    }
    var sortedOnPosition = from entry in playersOnPosition orderby entry.Value.rating descending select entry;

    if (printPlayers)
    {
        int i = numberOfPlayersToPrint;
        foreach (var player in playersOnPosition)
        {
            if (i is 0)
            {
                break;
            }
            Console.WriteLine($"{player.Value.position}  -  {player.Key}\t{player.Value.rating}");
            i--;
        }
    }
}


void CheckPlayers(Dictionary<string, (string position, int rating)> playersOnPosition, int minNumber, string positionName)
{
    organisePlayers(playersOnPosition!, positionName, false, 0);
    if (playersOnPosition!.Count < minNumber)
    {
        Console.WriteLine($"Nema dovoljno igrača na poziciji {positionName}!");
        if (AreYouSure("vratiti se na izbornik"))
        {
            ReturnToIzbornik();
        }
    }
}

void UpdatePlayerRating(Dictionary<string, (string position, int rating)> playerDict)
{
    foreach (var player in playerDict.Keys)
    {
        playersInfo[player] = (playerDict[player].position, playerDict[player].rating);
    }
}


(int firstTeam, int secondTeam) GenerateRandomResults()
{
    Random randomInt = new Random();
    return (randomInt.Next(0, 4), randomInt.Next(0, 4));  // mozda nije najbolje rjesenje
}


void ostaliRezultati()
{
    for (int i = 0; i < 5; i++)
    {
        (bool odigrano, int kolo, string firstTeam, string secondTeam, int rezultatFirstTeam, int rezultatSecondTeam) p = protivnici![i];
        (int firstTeamGolovi, int secondTeamGolovi) = GenerateRandomResults();
        p.odigrano = true;
        p.rezultatFirstTeam = firstTeamGolovi;
        p.rezultatSecondTeam = secondTeamGolovi;
        protivnici[i] = p;
        if (firstTeamGolovi > secondTeamGolovi)
        {
            (int b, int r) oldValues = bodoviOstali![p.firstTeam];
            bodoviOstali![p.firstTeam] = (oldValues.b + 3, oldValues.r + (p.rezultatFirstTeam - p.rezultatSecondTeam));
        }
        else if (p.rezultatFirstTeam < p.rezultatSecondTeam)
        {
            (int b, int r) oldValues = bodoviOstali![p.secondTeam];
            bodoviOstali![p.secondTeam] = (oldValues.b + 3, oldValues.r + (p.rezultatSecondTeam - p.rezultatFirstTeam));
        }
        else
        {
            (int b, int r) oldValuesFirst = bodoviOstali![p.firstTeam];
            (int b, int r) oldValuesSecond = bodoviOstali![p.secondTeam];
            bodoviOstali![p.firstTeam] = (oldValuesFirst.b + 1, oldValuesFirst.r);
            bodoviOstali![p.secondTeam] = (oldValuesSecond.b + 1, oldValuesSecond.r);
        }
    }
}


// main izbornik
void Izbornik()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("1 - Odradi trening\n2 - Odigraj utakmicu\n3 - Statistika\n4 - Kontrola igraca\n0 - Izlaz iz aplikacije\n\n");
        switch (UnosIzbora("broj za izbor"))
        {
            case 1: // Odradi trening
                if (AreYouSure("odraditi trening"))
                {
                    OdradiTrening();
                    break;
                }
                break;

            case 2:  // Odigraj utakmicu
                if (AreYouSure("odigrati utakmicu"))
                {
                    OdigrajUtakmicu();
                    break;
                }
                break;

            case 3: // Statistika
                Statistika();
                break;

            case 4:  // Kontrola igraca
                KontrolaIgraca();
                break;

            case 0:
                Environment.Exit(0);
                break;

            default:
                break;
        }
    }
}


void OdradiTrening()
{
    Console.Clear();
    CalculateNewRating(playersInfo, -5, -5, true);
    ReturnToIzbornik();
}


void OdigrajUtakmicu()
{
    if (playersInfo.Count < 20) // 26
    {
        Console.WriteLine("Nema dovoljno igrača!");
        ReturnToIzbornik();
    }

    CheckPlayers(gk!, 1, "GK");
    CheckPlayers(df!, 4, "DF");
    CheckPlayers(mf!, 3, "MF");
    CheckPlayers(fw!, 3, "FW");

    ekipa!.Clear();
    ekipa!.Add(gk!.ElementAt(1).Key, gk!.ElementAt(1).Value);

    for (int i = 0; i < 4; i++)
    {
        ekipa.Add(df!.ElementAt(i).Key, df!.ElementAt(i).Value);
    }

    for (int i = 0; i < 3; i++)
    {
        ekipa.Add(mf!.ElementAt(i).Key, mf!.ElementAt(i).Value);
        ekipa.Add(fw!.ElementAt(i).Key, fw!.ElementAt(i).Value);
    }

    (int golEkipa, int golProtivnici) golovi = GenerateRandomResults();

    Dictionary<string, (string position, int rating)> strijelci = new Dictionary<string, (string position, int rating)>();
    if (golovi.golEkipa > 0)
    {
        Random randomInt = new Random();
        while (strijelci.Count < golovi.golEkipa)
        {
            int pozicijaStrijelca = randomInt.Next(0, fw!.Count - 1);
            if (strijelci.ContainsKey(fw!.ElementAt(pozicijaStrijelca).Key))
            {
                continue;
            }
            strijelci.Add(fw!.ElementAt(pozicijaStrijelca).Key, fw!.ElementAt(pozicijaStrijelca).Value);
        }
        CalculateNewRating(strijelci, 5, 5, false);
        UpdatePlayerRating(strijelci);
    }

    if (golovi.golEkipa > golovi.golProtivnici)
        CalculateNewRating(ekipa, 2, 2, false);
    else
        CalculateNewRating(ekipa, -2, -2, false);

    UpdatePlayerRating(ekipa);
    rezultatiEkipe!.Add(golovi);
    string rezultat = $"{golovi.golEkipa}:{golovi.golProtivnici}";

    Console.Clear();
    Console.WriteLine("Rezultati utakmice:");
    Console.WriteLine();

    ostaliRezultati();
    PrintLine();
    Console.WriteLine($"{rezultat}\n\nOstali rezultati kola:\n");
    Random randInt = new Random();
    int kolo = randInt.Next(1, 4);
    foreach (var utakmica in protivnici!)
    {
        if (utakmica.kolo == kolo)
        {
            Console.WriteLine($"{utakmica.fristTeam} - {utakmica.secondTeam}\t{utakmica.rezultatFitstTeam}:{utakmica.rezultatSecondTeam}\n");
        }
    }
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

    while (true)
    {
        Console.Clear();
        Console.WriteLine("1 - Ispis onako kako su spremljeni\n2 - Ispis po rating uzlazno\n3 - Ispis po ratingu silazno\n4 - Ispis igrača po imenu i prezimenu" +
            "\n5 - Ispis igrača po ratingu\n6 - Ispis igrača po poziciji\n7 - Ispis trenutno prvih 11 igrača\n8 - Ispis strijelaca i koliko golova imaju" +
            "\n9 - Ispis svih rezultata ekipe\n10 - Ispis rezultat svih ekipa\n11 - Ispis tablice grupe\n12 - Povratak na izbornik");

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

            case 5: // po ratingu??                                                <--------------------  REFACTOR!!
                Console.Clear();
                Console.WriteLine("Ispis igrača po ratingu:");
                PrintLine();
                Console.Write("\nUpisite rating: ");

                int inputNum = UnosIzbora("broj ratinga [1 - 100]");
                while (inputNum < 0 || inputNum > 101)
                {
                    inputNum = UnosIzbora("broj ratinga [1 - 100]");
                }

                //var playersWithSelectedRating = playersInfo.Select()
                foreach (var player in playersInfo.Keys)
                {
                    if (playersInfo[player].rating == inputNum)
                    {
                        Console.WriteLine($"{player} {playersInfo[player].position}");
                    }
                }
                ReturnToIzbornik();
                break;

            case 6: // po poziciji
                Console.Clear();
                Console.WriteLine("Ispis igrača po poziciji:\n\nIme\tPozicija\tRating\n");
                PrintLine();

                Console.WriteLine("GK igrači:\nIme\t\tRating\n");
                organisePlayers(gk!, "GK", true, gk!.Count);

                Console.WriteLine("\n\nMF igrači:\nIme\t\tRating\n");
                organisePlayers(mf!, "MF", true, mf!.Count);

                Console.WriteLine("\n\nDF igrači:\nIme\t\tRating\n");
                organisePlayers(df!, "DF", true, df!.Count);

                Console.WriteLine("\n\nFW igrači:\nIme\t\tRating\n");
                organisePlayers(fw!, "FW", true, fw!.Count);

                ReturnToIzbornik();
                break;

            case 7:  // top 11 po pozicijama
                Console.Clear();
                Console.WriteLine("Ispis top 11 igrača po pozicijama:");      
                PrintLine();

                organisePlayers(gk!, "GK", true, 1);
                organisePlayers(df!, "DF", true, 4);
                organisePlayers(mf!, "MF", true, 3);
                organisePlayers(fw!, "FW", true, 3);

                ReturnToIzbornik();
                break;

            case 8: // ispis strijelaca i koliko golova imaju  
                Console.Clear();
                Console.WriteLine("Ispis strijelaca i koliko golova imaju:\n\nIme\tGolovi");
                PrintLine();

                Random randomInt = new Random();
                organisePlayers(fw!, "FW", false, 0);
                foreach (var player in fw!)
                {
                    int brojGolova = randomInt.Next(0, 300);
                    Console.WriteLine($"{player.Key}\t{brojGolova}");
                }

                ReturnToIzbornik();
                break;

            case 9: // svi rezultati ekipe ??      

                Console.Clear();
                Console.WriteLine("Ispis rezultata ekipe:");
                PrintLine();

                if (rezultatiEkipe!.Count > 0)
                {
                    int j = 1;
                    foreach (var result in rezultatiEkipe)
                    {
                        Console.WriteLine($"{j}. rezultat: \t{result.rezEkipe}:{result.rezProtivnika}\n");
                        j++;
                    }
                }
                else
                    Console.WriteLine("Nije još odigrana nijedna utakmica!");

                ReturnToIzbornik();
                break;

            case 10: // rezultat svih ekipa                
                Console.Clear();
                Console.WriteLine("Ispis rezultata svih ekipa:");
                PrintLine();

                if (protivnici![0].odigrano is false)
                {
                    ostaliRezultati();
                }

                foreach (var rezultat in protivnici)
                {
                    Console.WriteLine($"{rezultat.kolo}. kolo.......{rezultat.fristTeam} - {rezultat.secondTeam}.......{rezultat.rezultatFitstTeam}:{rezultat.rezultatSecondTeam}\n");
                }


                /*
                foreach (var ekipa in bodoviOstali!)
                {
                    Console.WriteLine($"{ekipa.Key}\tBodovi: {ekipa.Value}\n");
                }*/

                ReturnToIzbornik();
                break;

            case 11: // tablica grupe 
                Console.Clear();
                Console.WriteLine("Ispis tablice grupe:");
                PrintLine();

                if (protivnici![0].odigrano is false)
                {
                    ostaliRezultati();
                }
                var sortedBodovi = from ekipa in bodoviOstali orderby ekipa.Value.bodovi descending select ekipa;
                int i = 1;
                foreach (var ekipa in sortedBodovi)
                {
                    Console.WriteLine($"{i}. {ekipa.Key}\tBodovi: {ekipa.Value.bodovi}\tGol razlika: {ekipa.Value.golRazlika}\n");
                    i++;
                }

                ReturnToIzbornik();
                break;

            case 12:
                Izbornik();
                break;

            default:
                break;
        }
    } 
}


void KontrolaIgraca()
{
    while (true)
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

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Uredi igrača {playerName}");
                    PrintLine();
                    Console.WriteLine("1 - Uredi ime i prezime igrača\n2 - Uredi poziciju igrača (GK, DF, MF ili FW)\n3 - Uredi rating igrača [1 - 100]\n0 - povratak na izbornik");
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
                } 

            case 0:
                Izbornik();
                break;

            default:
                break;
        }
    } 
}

Izbornik();