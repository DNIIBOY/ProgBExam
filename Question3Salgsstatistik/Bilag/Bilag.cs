using System;
using System.Collections.Generic;
using System.Linq;

namespace Bilag;

public class Bilag
{
    void bilagdel1()
    {
        using (bogEntitet e = new bogEntitet())
        {
            var bog = (from r in e.bog 
                    where r.navn == "Under Havet" 
                    select r).
            ToList();
        }
    }
    
}








public class bogEntitet : IDisposable
{
    public List<bøger> bog = new List<bøger>()
    {
        new bøger{navn = "Under Havet", antal = 2},
        new bøger{navn = "Harry Potter", antal = 2},
        new bøger
        {
            navn = "Titel",
            antal = 7
        }
    };

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

public class bøger
{
    public string navn  { get; set; }
    public int antal { get; set; }
}