

void Izbornik()
{
    Console.WriteLine("1 - Odradi trening\n2 - Odigraj utakmicu\n3 - Statistika\n4 - Kontrola igraca\n0 - Izlaz iz aplikacije\n\n");
    int choice;
    bool flag = false;
    do
    {
        Console.Write("Unesi broj za izbor: ");
        flag = int.TryParse(Console.ReadLine(), out choice);
    } while (flag == false);

    switch (choice)
    {   
        case 0:
            //izlaz iz aplikacije
            break;
        case 1:
            //Odradi trening
            OdradiTrening();
            break;
        case 2:
            //Odigraj utakmicu
            break;
        case 3:
            //Statistika
            break;
        case 4:
            //Kontrola igraca
            break;
        default:
            Console.WriteLine("Izbor mora biti 0, 1, 2, 3 ili 4.");
            break;
    }
}

void OdradiTrening()
{

}

Dictionary<string, (string position, int rating)> nogometasi = new Dictionary<string, (string position, int rating)>()
{
    { "", ("GK", 20)}
};


Izbornik();


