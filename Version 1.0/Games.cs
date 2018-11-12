using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version_1._0
{
    interface Game
    {
        int CountItems { get; }
        Item this[int i] { get; }
        bool Run(Persona persona);
        void Select(int i);
        void MakeBet(Persona persona, int i, decimal bet);
        decimal SumBet { get; }
        decimal Result { get; }
        int GetWinNumber(int i);
    }
    class ItemWarehouse
    {
        protected List<Pair<int, string>> Name;
        protected List<Pair<int, string>> ShowItem;
        protected List<Pair<int, decimal>> Bet;
        protected List<Pair<int, bool>> Selected;
        protected List<int> WinNumber;
        protected int countItems;
        protected decimal result;

        public decimal Ratio { get; protected set; }      //Коэфициент
        public int CountItems { get { return countItems; } }
        public decimal SumBet { get { return Bet.Sum(x => x.second); } }
        public decimal Result { get { return result; } }

        protected ItemWarehouse(decimal Ratio, int CountItem)
        {
            Name = new List<Pair<int, string>>();
            ShowItem = new List<Pair<int, string>>();
            Bet = new List<Pair<int, decimal>>();
            Selected = new List<Pair<int, bool>>();
            WinNumber = new List<int>();
            this.Ratio = Ratio;
            this.countItems = CountItem;
        }
        public virtual void Select(int i)
        {
            IEnumerable<Pair<int, bool>> select = Selected.Where(x => x.first == i);
            if (select.Count() == 0)
                Selected.Add(new Pair<int, bool>(i, true));
        }
        public void MakeBet(Persona persona, int i, decimal bet)
        {
            IEnumerable<Pair<int, decimal>> bbb = Bet.Where(x => x.first == i);
            if (bbb.Count() > 0)
                bbb.First().second += bet;
            else
                Bet.Add(new Pair<int, decimal>(i, bet));
            persona.Money -= bet;
        }
        public int GetWinNumber(int i)
        {
            return this.WinNumber[i];
        }
    }
    class Dice : ItemWarehouse, Game
    {
        public Item this[int i]
        {
            get
            {
                IEnumerable<Pair<int, decimal>> bet = Bet.Where(x => x.first == i);
                IEnumerable<Pair<int, bool>> select = Selected.Where(x => x.first == i);
                return new Item(Name.Where(x => x.first == i).First().second,
                                Name.Where(x => x.first == i).First().second,
                                (bet.Count() > 0) ? bet.First().second : 0,
                                (select.Count() > 0) ? select.First().second : false);
            }
        }
        public Dice() : base(3, 6)     //шанс окупаемости 50%
        {
            Name.Add(new Pair<int, string>(0, "One"));
            Name.Add(new Pair<int, string>(1, "Two"));
            Name.Add(new Pair<int, string>(2, "Three"));
            Name.Add(new Pair<int, string>(3, "Four"));
            Name.Add(new Pair<int, string>(4, "Five"));
            Name.Add(new Pair<int, string>(5, "Six"));
        }
        public bool Run(Persona persona)
        {
            Visual.ShowGame(this);
            Visual.Select(this,persona);
            CalculationOfTheOutcome(persona);
            Visual.ShowResult(this, 0);
            return Visual.StillPlay(this, persona);
        }
        private void CalculationOfTheOutcome(Persona persona)
        {
            WinNumber.Add(new Random().Next(0, CountItems - 1));
            IEnumerable<Pair<int, decimal>> bet = Bet.Where(x => x.first == WinNumber[0]);
            result = ((bet.Count() > 0) ? bet.First().second : 0) * Ratio - SumBet;
            persona.Money += result;
        }
    }
    class Cups : ItemWarehouse, Game
    {
        public Item this[int i]
        {
            get
            {
                IEnumerable<Pair<int, decimal>> bet = Bet.Where(x => x.first == i);
                IEnumerable<Pair<int, bool>> select = Selected.Where(x => x.first == i);
                return new Item(Name.Where(x => x.first == i).First().second,
                                Name.Where(x => x.first == i).First().second,
                                (bet.Count() > 0) ? bet.First().second : 0,
                                (select.Count() > 0) ? select.First().second : false);
            }
        }

        public Cups() : base(1.2m, 2)
        {
            Name.Add(new Pair<int, String>(0, "White"));
            Name.Add(new Pair<int, String>(1, "Black"));
        }
        public override void Select(int i)
        {
            Selected.Add(new Pair<int, bool>(i, true));
        }
        public bool Run(Persona persona)
        {
            Visual.ShowGame(this);
            Visual.Select(this, persona, false,true);
            int WhiteCount = 0;
            int BlackCount = 0;
            for (int i = 0; i < 20; i++)
            {
                Visual.Select(this, persona, true, false);
                WinNumber.Add(new Random().Next(0, 100 - WhiteCount - BlackCount));
                WinNumber[i] = (WinNumber[i] <= 80 - WhiteCount) ? 0 : 1;
                if (WinNumber[i] == 0)
                    WhiteCount++;
                else
                    BlackCount++;
                if (Selected[i].first == WinNumber[i])
                {
                    Bet[0].second *= Ratio;
                    Ratio += (WhiteCount + BlackCount) / 10m;
                    Visual.ShowResult(this, WinNumber[i]);
                    if ( !(Visual.PlayFurther()) )
                    {
                        result += Bet[0].second;
                        break;
                    }
                }
                else
                {
                    result = -Bet[0].second;
                    Visual.YouLoss();
                    break;
                }
            }
            persona.Money += result;
            return Visual.StillPlay(this, persona);
        }
    }



    //class Lottery : Game
    //{
    //    public Lottery()
    //    {
    //        uint r = Convert.ToUInt32(new Random(DateTime.Now.Millisecond).Next(17000000));
    //        things = new Thing[2] { new Thing("Win",r), new Thing("Loss") };
    //        ratio = 250000000;
    //    }
    //    public bool Run(Persona persona)
    //    {
    //        Visual.ShowGame(this);
    //        Visual.Select(this);
    //        CalculationOfTheOutcome(persona);
    //        Console.WriteLine("\n" + "Игра окончена");
    //        return Visual.StillPlay();
    //    }
    //    public void CalculationOfTheOutcome(Persona persona)
    //    {
    //        int r = new Random().Next(0, things.Length - 1);
    //        Visual.ShowResult(this, r);
    //        persona.Money -= things.Sum(thing => thing.Bet);
    //        persona.Money += things[r].Bet * ratio;
    //    }
    //}
}
