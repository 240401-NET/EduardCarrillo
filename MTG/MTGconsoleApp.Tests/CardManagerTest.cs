using MTG;
using MTGconsoleApp;
using Xunit;

namespace MTGconsoleApp.Tests;

public class CardManagerTest
{
    [Fact]
    public void AddCard_WhenNewCardAdded_ShouldIncreaseCardCount()
    {
        //Arreange - Create any variables or objects you'll need for the test
        CardManager manager = new CardManager();
        manager.LoadCards();
        int initialCount = manager.cards.Count;

        //Act - Call the code that you are trying to test
        manager.AddCard("Test Card", "Creature", 3, 2);

        //Assert - Conditions under which the test will pass/fail
        int finalCount = manager.cards.Count;
        Assert.Equal(initialCount + 1, finalCount);
    }
}