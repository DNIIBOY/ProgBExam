Console.Write("Værdi 1: ");
int val1 = int.Parse(Console.ReadLine());
Console.Write("Værdi 2: ");
int val2 = int.Parse(Console.ReadLine());

int min = val1 < val2 ? val1 : val2;
int max = val1 > val2 ? val1 : val2;
Console.WriteLine($"Maximum: {max}");

List<int> numbers = new();
for (int i = 0; i < min; i++){
    numbers.Add(i);
}

Console.WriteLine(numbers);
foreach (var num in numbers){
    Console.WriteLine(num);
}