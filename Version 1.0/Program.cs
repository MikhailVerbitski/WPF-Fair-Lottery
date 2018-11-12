using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version_1._0
{
    class CreateGame
    {
        public CreateGame()
        {
            Visual.Welcome();
            Persona persona = (Visual.EntranceOrRegistration()) ? Visual.Registration() : Visual.Entrance();

            persona.Play();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CreateGame a = new CreateGame();
        }
    }
}