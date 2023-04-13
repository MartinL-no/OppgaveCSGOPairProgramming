namespace OppgaveCSGOPairProgramming;

internal class CounterTerrorist
{
    public bool IsDead { get; set; }

    public CounterTerrorist()
    {
        IsDead = false;
    }

    public void KillTerrorist(Terrorist terrorist, bool thereIsABomb)
    {
        if (!thereIsABomb)
        {
            if (IsSuccessful(5) == true)
            {
                terrorist.IsDead = true;
                Console.WriteLine("A counter-terrorist managed to kill a terrorist.");
            }
            else
            {
                Console.WriteLine("A counter-terrorist tried to kill a terrorist but missed.");
            }
        }
        else
        {
            if (IsSuccessful(3) == true)
            {
                terrorist.IsDead = true;
                Console.WriteLine("A counter-terrorist managed to kill a terrorist.");
            }
            else
            {
                Console.WriteLine("A counter-terrorist tried to kill a terrorist but missed.");
            }
        }
    }

    public bool IsSuccessful(int maxValue)
    {
        Random rand = new Random();
        return rand.Next(0, maxValue) == 2;
    }

    public void DefuseBomb()
    {
        Program.DefuseTimer--;
    }
}