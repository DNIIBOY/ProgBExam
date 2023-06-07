using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

namespace Bilag;

public class Bilag
{
    public void linqfuncFindBog(string bognavn = "Under Havet")
    {
        List<bøger> bog;
        using (BogEntitet e = new BogEntitet())
        {
            bog = (from r in e.bog 
                    where r.navn == bognavn 
                    select r).
            ToList();
        }
        foreach (var row in bog)
        {
            Console.WriteLine($"bog : {row.navn} \t antal : {row.antal.ToString()}");
        }
    }

    public void lampdafuncFindBog(string bognavn = "Under Havet")
    {
        BogEntitet e = new BogEntitet();
        var bog = e.bog.Where(x => x.navn == bognavn).ToList();
        foreach (var row in bog)
        {
            Console.WriteLine($"bog : {row.navn} \t antal : {row.antal.ToString()}");
        }
    }



    public void linqfuncFindAll()
    {
        List<bøger> bog;
        var table = new ConsoleTable(
            new ConsoleTableOptions {
                Columns = new[] {"Title", "Antal"},
                EnableCount = false
            }
        );
        using (BogEntitet e = new BogEntitet())
        {
            bog = (from r in e.bog
                    select r).
                ToList();
        }
        foreach (var row in bog)
        {
            table.AddRow(row.navn, row.antal.ToString());
        }
        table.Write();
    }
    
}

public class BogEntitet : IDisposable
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
        return;
    }
}

public class bøger
{
    public string navn  { get; set; }
    public int antal { get; set; }
}