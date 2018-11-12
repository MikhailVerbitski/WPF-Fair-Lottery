using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    interface Game
    {
        void Run(Persone persona);
        int Raffle(Persone persona);
        int MakeBet(Persone persona);
    }
    //class Dice : Game
    //{
    //    public Dice()
    //    {

    //    }
    //    public void Run(Persone persona)
    //    {
    //        int ID_Raffle = Raffle(persona,1);
    //        int ID_Bet = MakeBet(persona, ID_Raffle, 1);
    //        Result(persona, ID_Raffle, ID_Bet, 2);
    //        Visual.ShowResult(ID_Bet, "Dice"); 
    //    }
    //    private int Raffle(Persone persona, int RoomThrow)
    //    {
    //        return Table.Raffle.CreateRaffle(persona.sID_Persone, new Random().Next(1, Table.Items.GetCountInGame("Dice") * RoomThrow ));
    //    }
    //    private int MakeBet(Persone persona, int ID_of_Raffle, int RoomThrow)
    //    {
    //        Pair<int,int> IdAndNum = Visual.ShowAndSelectItemDice();
    //        return Table.Bet.CreateBet(IdAndNum.First, ID_of_Raffle, Visual.MakeBet(), IdAndNum.Second);
    //    }
    //    private void Result(Persone persona, int ID_Raffle, int ID_Bet, decimal ratio)
    //    {
    //        decimal size = Table.Bet.GetSize(ID_Bet);
    //        decimal result = (Table.Raffle.GetWinNumber(ID_Raffle) == Table.Bet.GetNumber(ID_Bet)) ? (size * ratio) : -size;
    //        Table.Persone.UpdateMoney(persona.sID_Persone, result);
    //        Table.Raffle.SetResult(ID_Raffle, result);
    //    }
    //    private void Throw()
    //    {

    //    }
    //}
    //class Lottery : Game
    //{
    //    public Lottery()
    //    {

    //    }
    //    public void Run(Persone persone)
    //    {
    //        int ID_Raffle = Raffle(persone);     //выбор выйгрышного
    //        //
    //            //покупка билетов
    //        //еще?
    //        //рузультат
    //        //отображение
    //    }
    //    private int Raffle(Persone persone)
    //    {
    //        return Table.Raffle.CreateRaffle(persone.sID_Persone, new Random().Next(1, 1000000));
    //    }
    //    private int MakeBet(Persone persone, int ID_of_Raffle, int RoomThrow)
    //    {
    //        Pair<int, int> IdAndNum = Visual.ShowAndSelectItemLottery();
    //        //return Table.Bet.CreateBet(IdAndNum.First, ID_of_Raffle, Visual.MakeBet(), IdAndNum.Second);
    //    }
    //    private int Result(Persone persone)
    //    {

    //    }
    //}
    //class Cups : Game
    //{
    //    public Cups()
    //    {

    //    }
    //    public void Run(Persone persona)
    //    {
                //выбор выйгрышного
                //покупка билетов
                //рузультат
                //отображение
    //    }
    //}
}
