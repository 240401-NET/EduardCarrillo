
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace MTG
{
    public class CardManager
    {
        public List<Card> cards = new List<Card>();
        private string filePath = "magic_cards.json";

        public void LoadCards()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                cards = JsonSerializer.Deserialize<List<Card>>(json);
            }
            else
            {
                Console.WriteLine("File Does Not Exist");
            }
        }

        public void SaveCards() //Saving data/cards to magic_cards.json file
        {
            string json = JsonSerializer.Serialize(cards);
            File.WriteAllText(filePath, json);
        }

        public void AddCard(string name, string type, int manaCost, int quantity) //Adding new card with validators and checking is card already exists
        {
            if (!InputValidator.IsValidNonEmptyString(name)) //Checking to see if there was no input
            {
                Console.WriteLine("Invalid card name! Please enter a valid card name.");
                return;
            }

            if (!InputValidator.IsValidNonEmptyString(type)) //Checking to see if there was no input
            {
                Console.WriteLine("Invalid card type! Please enter valid card type.");
                return;
            }

            if (!InputValidator.IsValidCardType(type)) //Checking to see if input was one of the pre-existing card types
            {
                Console.WriteLine("Please enter a valid card type: Land, Basic Land, Creature, Artifact, Enchantment, Instant, Sorcery, or Planeswalker.");
                return;
            }

            if (!InputValidator.IsValidValidInteger(manaCost.ToString(), out _)) //Checking to see if input was int >=0
            {
                Console.WriteLine("Invalid mana cost! Please enter a valid mana cost.");
                return;
            }

            if (!InputValidator.IsValidValidInteger(quantity.ToString(), out _)) //Checking to see if input was int >=0
            {
                Console.WriteLine("Invalid quantity! Please enter a valid quantity.");
                return;
            }

            Card existingCard = cards.FirstOrDefault(c => c.CardName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existingCard != null)
            {
                int maxToAdd = Math.Min(4 - existingCard.CardQuantity, quantity); // This calculates how many copies can be added without exceeding 4
                if (maxToAdd <= 0)
                {
                    Console.WriteLine($"The card \"{name}\" already has the maximum allowed quantity (4 copies).");
                    return;
                }
                existingCard.CardQuantity += maxToAdd;
                Console.WriteLine($"Added {maxToAdd} copies of \"{name}\" to the collection.");
            }
            else
            {
                if (quantity > 4)
                {
                    Console.WriteLine("You cannot add more than 4 copies of the same card.");
                    return;
                }
                cards.Add(new Card { CardName = name, CardType = type, CardManaCost = manaCost, CardQuantity = quantity });
                Console.WriteLine($"The card \"{name}\" was added successfully.");
            }
        }


        public void DeleteCard(string name, int quantity)
        {
            Card cardToRemove = cards.Find(c => c.CardName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (cardToRemove != null)
            {
                if (quantity >= cardToRemove.CardQuantity)
                {
                    cards.Remove(cardToRemove);
                }
                else
                {
                    cardToRemove.CardQuantity -= quantity;
                }
            }
            else
            {
                Console.WriteLine($"The card \"{name}\" does not exist.");
            }
        }

        public void ListCards()
        {
            List<Card> sortedCards = cards.OrderBy(c => c.CardName).ToList(); //This will list all cards in deck by alphabetical order.
            foreach (Card card in sortedCards)
            {
                Console.WriteLine(card);
            }

            int totalQuantity = sortedCards.Sum(c => c.CardQuantity);
            Console.WriteLine($"Total cards in Deck: {totalQuantity}"); //This will list total number of all cards in deck.
        }
    }
}
