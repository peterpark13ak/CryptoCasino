using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.DTO.Game
{
    public class GameSymbolDTO
    {

        public GameSymbolDTO(int rolledNumber, int min, int max)
        {
            this.PopulateFields(rolledNumber, min, max);
            this.RolledNumber = rolledNumber;
        }

        public string SymbolName { get; private set; }
        public double Coefficient { get; private set; }

        public int RolledNumber { get; set; }

        private void PopulateFields(int number, int min, int max)
        {
            if(number < min || number > max)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                if(number < 45)
                {
                    this.SymbolName = "low";
                    this.Coefficient = 0.4;
                }
                else if(number >44 && number < 80)
                {
                    this.SymbolName = "medium";
                    this.Coefficient = 0.6;
                }
                else if(number > 79 && number < 95)
                {
                    this.SymbolName = "high";
                    this.Coefficient = 0.8;
                }
                else
                {
                    this.SymbolName = "wild";
                    this.Coefficient = 0;
                }
            }
        }
    }
}
