namespace MTG;

public class Program
{
    static void Main(string[] args)
    {
        CardManager manager = new CardManager();
        manager.LoadCards();

        while (true)
        {
            Console.WriteLine("\nMagic the Gathering Deck Manager");
            Console.WriteLine("1. Add Card");
            Console.WriteLine("2. Delete Card");
            Console.WriteLine("3. List Cards");
            Console.WriteLine("4. Draw a Hand");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input! Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter card name: ");
                    string name = Console.ReadLine();
                    Card existingCard = manager.cards.FirstOrDefault(c => c.CardName.Equals(name, StringComparison.OrdinalIgnoreCase));
                    if (existingCard != null)
                    {
                        Console.Write($"Card '{existingCard.CardName}' already exists. Enter quantity to add: ");
                        int quantity;
                        if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                        {
                            Console.WriteLine("Invalid quantity! Please enter a valid number.");
                            continue;
                        }
                        manager.AddCard(existingCard.CardName, existingCard.CardType, existingCard.CardManaCost, quantity);
                        manager.SaveCards();
                        // Console.WriteLine($"Added {quantity} copy/copies of '{existingCard.CardName}' successfully.");
                    }
                    else
                    {
                        Console.Write("Enter card type: ");
                        string type = Console.ReadLine();
                        Console.Write("Enter mana cost: ");
                        int manaCost;
                        if (!int.TryParse(Console.ReadLine(), out manaCost))
                        {
                            Console.WriteLine("Invalid mana cost! Please enter a number.");
                            continue;
                        }
                        Console.Write("Enter quantity: ");
                        int quantity;
                        if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                        {
                            Console.WriteLine("Invalid quantity! Please enter a valid number.");
                            continue;
                        }
                        manager.AddCard(name, type, manaCost, quantity);
                        manager.SaveCards();
                        //Console.WriteLine($"Card '{name}' added successfully.");
                    }
                    break;


                case 2:
                    Console.Write("Enter the name of the card to delete: ");
                    string cardNameToDelete = Console.ReadLine();
                    Console.Write("Enter the quantity of copies to delete: ");
                    int deleteQuantity;
                    if (!int.TryParse(Console.ReadLine(), out deleteQuantity) || deleteQuantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity! Please enter a valid number.");
                        continue;
                    }
                    int initialCount = manager.cards.Count;
                    manager.DeleteCard(cardNameToDelete, deleteQuantity);
                    manager.SaveCards();
                    int finalCount = manager.cards.Count;
                    //int totalCountDeleted = (initialCount - finalCount);
                    if (finalCount < initialCount || manager.cards.Any(c => c.CardName.Equals(cardNameToDelete, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine($"Deleted {deleteQuantity} copy/copies of '{cardNameToDelete}' successfully. ");
                        //Need to fix this so it will display ACTUAL number of copies deleted.
                    }
                    else
                    {
                        Console.WriteLine("No cards were deleted. Please check the card name and try again.");
                    }
                    break;
                case 3:
                    Console.WriteLine("List Cards");
                    manager.ListCards();
                    break;

                case 4:
                    Console.WriteLine("Drawing a hand of seven random cards:");

                    List<Card> hand = new List<Card>();
                    Random random = new Random();

                    for (int i = 0; i < 7; i++)
                    {
                        int randomIndex = random.Next(manager.cards.Count);
                        hand.Add(manager.cards[randomIndex]);
                    }

                    // Display the initial hand
                    Console.WriteLine("Initial Hand:");
                    foreach (Card card in hand)
                    {
                        Console.WriteLine(card.CardName);
                    }

                    bool keepHand = false;
                    do
                    {
                        Console.Write("Do you want to keep this hand? (Y/N): ");
                        string response = Console.ReadLine();
                        if (response.Trim().ToUpper() == "Y")
                        {
                            keepHand = true;
                        }
                        else if (response.Trim().ToUpper() == "N")
                        {
                            // Draw a new hand
                            hand.Clear();
                            for (int i = 0; i < 7; i++)
                            {
                                int randomIndex = random.Next(manager.cards.Count);
                                hand.Add(manager.cards[randomIndex]);
                            }

                            // Display the new hand
                            Console.WriteLine("New Hand:");
                            foreach (Card card in hand)
                            {
                                Console.WriteLine(card.CardName);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input! Please enter Y or N.");
                        }
                    } while (!keepHand);
                    break;
                case 5:
                    Console.WriteLine("Exiting MTG Manager...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please choose a valid option."); //User needs to choose 1-5 on Menu
                    break;
            }
        }
    }
}