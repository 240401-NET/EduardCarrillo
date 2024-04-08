using System.Dynamic;
using System.Text.Json;

namespace MTG;

    public class Card
    {
        public string CardName { get; set; }
        public string CardType { get; set; }
        public int CardManaCost { get; set; }
        public int CardQuantity { get; set; }

        public override string ToString()
        {
            return $"Name: {CardName}, Type: {CardType}, Mana Cost: {CardManaCost}, Quantity: {CardQuantity}";
        }
    }