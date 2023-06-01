Console.Write("Købspris: ");
int purchasePrice = int.Parse(Console.ReadLine());
Console.Write("Salgspris: ");
int salesPrice = int.Parse(Console.ReadLine());

int profit = salesPrice - purchasePrice;
Console.WriteLine("Profit: " + profit);