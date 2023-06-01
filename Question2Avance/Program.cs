using System.Linq.Expressions;

void ShowProfit(){
    Console.Write("Købspris: ");
    int purchasePrice = int.Parse(Console.ReadLine());
    Console.Write("Salgspris: ");
    int salesPrice = int.Parse(Console.ReadLine());

    int profit = salesPrice - purchasePrice;
    int profitMargin = profit * 100 / purchasePrice;
    Console.WriteLine($"Avance: {profit} kr.-");
    Console.WriteLine($"Avance: {profitMargin}%");
}

void CalculateProfit(){
    Console.Write("Købspris: ");
    int purchasePrice = int.Parse(Console.ReadLine());
    Console.Write("Ønsket avance (%): ");
    int profitMargin = int.Parse(Console.ReadLine());

    int salesPrice = purchasePrice * (100 + profitMargin) / 100;
    int profit = salesPrice - purchasePrice;
    Console.WriteLine($"Salgspris: {salesPrice} kr.-");
    Console.WriteLine($"Avance: {profit}");
}

int Main(){
    Console.WriteLine("---------AVANCEREGNER---------");
    Console.WriteLine("1. Beregn avance");
    Console.WriteLine("2. Beregn salgspris");
    Console.WriteLine("q. Afslut");
    Console.Write("Vælg: ");
    string choice = Console.ReadLine();
    switch (choice){
        case "1":
            ShowProfit();
            return 0;
        case "2":
            CalculateProfit();
            return 0;
        case "q":
            return 1;
        default:
            Console.WriteLine("Ugyldigt valg!");
            return 0;
    }
}

int exitCode = 0;
while (exitCode == 0){
    exitCode = Main();
}
