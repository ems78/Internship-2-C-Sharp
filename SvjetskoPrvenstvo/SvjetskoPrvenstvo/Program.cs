
Dictionary<string, (string position, int rating)> playersInfo = new Dictionary<string, (string position, int rating)>()
{
    { "Ivo Grbić", ("GK", 74)},
    { "Ivica Ivušić", ("GK", 72)},
    { "Luka Modrić", ("MF", 88)},
    { "Marcelo Brozović", ("MF", 86)},
    { "Mateo Kovačević", ("MF", 84)},
    { "Ivan Perišić", ("MF", 84)},
    { "Ivan Rakitić", ("MF", 82)},
    { "Mario Pašalić", ("MF", 81)},
    { "Duje Ćaleta-Car", ("DF", 78)},
    { "Dejan Lovren", ("DF", 78)},
    { "Joško Gvardiol", ("DF", 81)},
    { "Domagoj Vida", ("DF", 76)},
    { "Josip Šutalo", ("DF", 75)},
    { "Josip Juranović", ("DF", 75)},
    { "Andrej Kramarić", ("FW", 82)},
    { "Ante Rebić", ("FW", 80)},
    { "Ante Budimir", ("FW", 76)},
    { "Nikola Kalinić", ("FW", 74)},
    { "Petar Musa", ("FW", 74)},
    { "Damir Kreilach", ("FW", 73)}
};

int UnosIzbora()
{
    int choice;
    bool flag = false;
    do
    {
        Console.Write("\nUnesite broj za izbor: ");
        flag = int.TryParse(Console.ReadLine(), out choice);
    } while (flag == false);

    return choice;
}

void ReturnToIzbornik()
{
    Console.Write("\n\n1 - Povratak na izbornik\n---------------------------");

    int back;
    while ((back = UnosIzbora()) != 1)
    {
        Console.WriteLine("Opcija ne postoji!");
    }
    Izbornik();
}

void Izbornik()
{
    Console.Clear();
    Console.WriteLine("1 - Odradi trening\n2 - Odigraj utakmicu\n3 - Statistika\n4 - Kontrola igraca\n0 - Izlaz iz aplikacije\n\n");
    
    int choice = UnosIzbora();

    bool repeat = true;
    do
    {
        switch (choice)
        {
            case 0:
                //izlaz iz aplikacije
                Environment.Exit(0);
                break;
            case 1:
                //Odradi trening
                OdradiTrening(playersInfo);
                repeat = false;
                break;
            case 2:
                //Odigraj utakmicu
                repeat = false;
                break;
            case 3:
                //Statistika
                repeat = false;
                Statistika(playersInfo);
                break;
            case 4:
                //Kontrola igraca
                KontrolaIgraca(playersInfo);
                repeat = false;
                break;
            default:
                Console.WriteLine("Izbor mora biti 0, 1, 2, 3 ili 4.");
                choice = UnosIzbora();
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
        //Console.WriteLine(percentage);
        var oldRating = player.Value.rating;                                                              //change rating in the dict ?? 
        int newRating = (int)(oldRating * (1 + percentage));
        Console.Write($"{player.Key} ({player.Value.position}) - old rating: {oldRating}\tnew rating: {newRating}\n");
        playersInfo[player.Key] = (player.Value.position, newRating);       
    }

    ReturnToIzbornik();
}


void OdigrajUtakmicu(Dictionary<string, (string position, int rating)> playersInfo)
{

}


void Statistika(Dictionary<string, (string position, int rating)> playersInfo)
{
    Console.Clear();
    Console.WriteLine("1 - Ispis svih igrača");

    int choice;
    while ((choice = UnosIzbora()) != 1)
    {
        Console.Write("Unesite broj za izbor: ");
    }

    Console.Clear();
    Console.WriteLine($"Ispis svih igrača\n{'-'*30}\n1 - Ispis onako kako su spremljeni\n2 - Ispis po rating uzlazno\n 3 - Ispis po ratingu silazno\n" +
        $"4 - Ispis igrača po imenu i prezimenu\n5 - Ispis igrača po ratingu\n6 - Ispis igrača po poziciji\n7 - Ispis trenutno prvih 11 igrača\n" +
        $"8 - Ispis strijelaca i koliko golova imaju\n9 - Ispis svih rezultata ekipe\n10 - Ispis rezultat svih ekipa\n11 - Ispis tablice grupe");

    choice = UnosIzbora();

    bool repeat = true;
    do
    {
        switch (choice)
        {
            case 1:

                repeat = false;
                break;
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            default:
                choice = UnosIzbora();
                break;
        }
    } while (repeat);

}

void KontrolaIgraca(Dictionary<string, (string position, int rating)> playersInfo)
{
    Console.Clear();

    Console.WriteLine("1 - Unos novog igrača\n2 - Brisanje igrača\n3 - Uređivanje igrača\n0 - Izlaz iz kontrole igrača");

    int choice = UnosIzbora();
    bool repeat = true;
    do
    {
        switch (choice)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("Unos novog igrača\n\n");  // unos samo jednog igraca ali stavit u petlju u slucaju greske imena ili neke druge informacije
                if (playersInfo.Count == 26)
                {
                    Console.WriteLine("Ekipa je popunjena.");
                    ReturnToIzbornik();
                    break;
                }
                else
                {
                    Console.Write("Ime i prezime: ");
                    var newPlayer = Console.ReadLine();
                    if (playersInfo.ContainsKey(newPlayer))  // ako je pri unosu kriva kapitalizacija??
                    {
                        Console.WriteLine("Igrač je već unesen!");
                        break;
                    }
                    Console.Write("\nPozicija: ");
                    var newPlayerPosition = Console.ReadLine();
                    //provjera unosa pozicije 
                    //dodavanje novog igraca u rijecnik
                }
                repeat= false;
                break;
            case 2:
                Console.Clear();
                Console.WriteLine("Brisanje igrača\n\n");

                repeat= false;
                break;
            case 3:
                repeat= false;
                break;
            case 0:
                Izbornik();
                repeat= false;
                break;
            default:
                choice= UnosIzbora();
                break;
        }
    } while (repeat);
    KontrolaIgraca(playersInfo);
}


Izbornik();


