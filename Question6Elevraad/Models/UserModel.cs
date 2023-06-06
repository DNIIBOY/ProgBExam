using System.Security.AccessControl;

namespace Question6Elevraad.Models;

public class User{
    public string Name;
    public string StudentClass;
    public int Age;
    public bool CanDrink;

    public User(){}
    public User(string name, string studentClass, int age, bool canDrink){
        Name = name;
        StudentClass = studentClass;
        Age = age;
        CanDrink = canDrink;
    }
}
