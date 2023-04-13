using System;

namespace OppgaveCSGOPairProgramming
{
    /*
       We will program a console version of CS:GO with terrorists and counterterrorists. 
       A team consists of 5 team members. All players have a property bool IsDead {get; set;} 

       The terrorists must have a method bool FindBombSite() which goes randomly towards “A”
       which uses the method IsSuccessful(10) for them to find A. They also have a method 
       KillCounterTerrorist(CounterTerrorist ct) which uses IsSuccessful(7). When A is found,
       they must call a method called PlantBomb(). The game then ends up in a time mode that
       ticks down until the bomb is detonated. (Use a For loop that counts down instead of up) 
       It takes 5 time units to plant the bomb and 15 time units from planting to detonation. 
       When the bomb explodes, the game ends and the terrorists win the battle. 

       Counter-terrorists have a method called DefuseBomb() that takes 5 time units to complete.
       They also have a method KillTerrorist(Terrorist terrorist) IsSuccessful(5) to find a
       random from the terrorist team and kill him. Then set the property IsDead =true; When a bomb
       is planted, KillTerrorist() increments so that it uses IsSuccessful(3) instead. All members
       must be dead before DefuseBomb() can be called. If they manage to complete DefuseBomb(), the
       counter-terrorists win

       Print to the console for each method that is called what is happening in the game.

       The program runs in a While(true) Loop until one of the teams has won, the teams take turns
       calling their methods. The terrorists can alternate between FindBombSite and KillCounterTerrorist
       and the CounterTerrorists use KillTerrorist().

       public static bool IsSuccessful(int maxValue)
       {
                Random rand = new Random();
                return rand.Next(0, maxValue) == 2;
       }


        List/array Terrorists 
        Terrorist Class
            bool FindBombSite()
                IsSuccessful(10)
            KillCounterTerrorist(CounterTerrorist ct)
                IsSuccessful(7)
            PlantBomb()
            bool IsDead
        List/array CounterTerrorist
        CounterTerrorist class
            KillTerrorist(Terrorist terrorist)
                IsSuccessful(5 - 3)
            DefuseBomb()
            bool IsDead

        Time mode
        // need to work out teams
     */
    internal class Program
    {
        private static List<Terrorist> _terrorists;
        private static List<CounterTerrorist> _counterTerrorists;
        private static bool _thereIsABomb;
        private static int _bombTimer;
        public static int DefuseTimer;
        private static bool terroristsAreAllDead => _terrorists.All(t => t.IsDead);
        private static bool counterterroristsAreAllDead => _counterTerrorists.All(t => t.IsDead);
        static void Main(string[] args)
        {
            _terrorists = new List<Terrorist>()
            {
                new Terrorist(),
                new Terrorist(),
                new Terrorist(),
                new Terrorist(),
                new Terrorist()
            };
            _counterTerrorists = new List<CounterTerrorist>()
            {
                new CounterTerrorist(),
                new CounterTerrorist(),
                new CounterTerrorist(),
                new CounterTerrorist(),
                new CounterTerrorist()
            };
            _bombTimer = 15;
            DefuseTimer = 5;
            _thereIsABomb = false;
            var lookForBombSite = true;
            var isBombSiteFound = false;
            
            while (IsGameNotOver())
            {
                Terrorist terrorist = GetALiveTerrorist();
                CounterTerrorist counterTerrorist = GetALiveCounterTerrorist();

                if (lookForBombSite)
                {
                    isBombSiteFound = terrorist.FindBombSite();
                }
                else
                {
                    terrorist.KillCounterTerrorist(counterTerrorist);
                }
                
                counterTerrorist.KillTerrorist(terrorist, _thereIsABomb);

                if (isBombSiteFound)
                {
                    _thereIsABomb = terrorist.PlantBomb();
                    CountdownMode();
                }
                lookForBombSite = !lookForBombSite;
            }
            ShowWhoWon();
        }

        private static void ShowWhoWon()
        {
            if (_bombTimer == 0)
            {
                Console.WriteLine("The bomb went off, the terrorists have won.");
            }
            else if (DefuseTimer == 0)
            {
                Console.WriteLine("The bomb has been defused, counter-terrorists win.");
            }
            else if (terroristsAreAllDead)
            {
                Console.WriteLine("All of the terrorists have been killed, the counter-terrorists have won.");
            }
            else if (counterterroristsAreAllDead)
            {
                Console.WriteLine("All of the counter-terrorists have been killed, the terrorists have won.");
            }
        }

        private static void CountdownMode()
        {
            Console.WriteLine("A bomb has been planted.");
            for (int i = _bombTimer; i > 0; i--)
            {
                if (_bombTimer == 0 || DefuseTimer == 0 || counterterroristsAreAllDead)
                {
                    break;
                }

                Terrorist terrorist = GetALiveTerrorist();
                CounterTerrorist counterTerrorist = GetALiveCounterTerrorist();

                if (!terroristsAreAllDead)
                {
                    counterTerrorist.KillTerrorist(terrorist, _thereIsABomb);
                }
                else
                {
                    counterTerrorist.DefuseBomb();
                    Console.WriteLine("A counter-terrorist is trying to defuse the bomb.");
                }
                if (!counterterroristsAreAllDead && !terroristsAreAllDead)
                {
                    terrorist.KillCounterTerrorist(counterTerrorist);
                }

                _bombTimer--;
                Console.WriteLine("BEEP!");
            }
        }

        private static bool IsGameNotOver()
        {
            if (counterterroristsAreAllDead || terroristsAreAllDead || _bombTimer == 0 || DefuseTimer == 0)  { return false; }
            else { return true; }
        }

        private static Terrorist GetALiveTerrorist()
        {
            return _terrorists.FirstOrDefault(t => !t.IsDead);
        }
        private static CounterTerrorist GetALiveCounterTerrorist()
        {
            return _counterTerrorists.FirstOrDefault(ct => !ct.IsDead);
        }
    }
}