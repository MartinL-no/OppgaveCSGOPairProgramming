namespace OppgaveCSGOPairProgramming;

internal class Terrorist
{
    public bool IsDead { get; set; }

    public Terrorist()
    {
        IsDead = false;
    }

    public bool FindBombSite()
    {
        if (IsSuccessful(10) == true) { 
            return true;
        }
        else { return false; }
    }

    public bool PlantBomb()
    {
        return true;
    }

    public bool IsSuccessful(int maxValue)
    {
        Random rand = new Random();
        return rand.Next(0, maxValue) == 2;
    }

    public void KillCounterTerrorist(CounterTerrorist counterTerrorist)
    {
        if (IsSuccessful(7) == true)
        {
            counterTerrorist.IsDead = true;
            Console.WriteLine("A terrorist managed to kill a counter-terrorist.");
        }
        else
        {
            Console.WriteLine("A terrorist tried to kill a counter-terrorist but missed.");
        }
    }
}