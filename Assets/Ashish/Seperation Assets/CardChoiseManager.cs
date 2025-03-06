using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction;
using TMPro;

public class CardChoiseManager : MonoBehaviour
{
    [System.Serializable]
    public class Card
    {
        public string cardName; // Name of the card
        public GameObject cardObject; // GameObject representing the card
        public string description; // Description to show during teaching
    }

    [Header("Card Configuration")]
    public List<Card> cards = new List<Card>(); // List of cards to configure in the Inspector

    [Header("UI Elements")]
    public TextMeshProUGUI descriptionText; // Text to display the card description
    public TextMeshProUGUI feedbackText; // Text to display feedback during assessment
                                         // public TextMeshProUGUI scoreText; // Text to display the score

    private int currentCardIndex = 0;
    // private int score = 0;

    void Start()
    {

        if (cards.Count == 0)
        {
            Debug.LogError("No cards configured! Please add cards in the Inspector.");
            return;
        }


    }

    #region Teaching Phase
    public void StartTeaching()
    {
        feedbackText.text = ""; // Clear feedback
                                //   scoreText.text = ""; // Hide score during teaching
        descriptionText.text = ""; // Clear description text
        currentCardIndex = 0;

        Debug.Log("Starting teaching phase...");
        Debug.Log($"Initial card: {cards[0].cardName}, Description: {cards[0].description}");



        DisplayTeachingCard();
    }


    private void DisplayTeachingCard()
    {
        if (currentCardIndex < cards.Count)
        {
            // Disable all cards and enable only the current one
            foreach (var card in cards)
                card.cardObject.SetActive(false);
            cards[currentCardIndex].cardObject.SetActive(true);

            // Update the description
            descriptionText.text = cards[currentCardIndex].description;

            // Debug to confirm the description is updated
            Debug.Log($"Displaying card: {cards[currentCardIndex].cardName}, Description: {cards[currentCardIndex].description}");
        }
        else
        {
            // End teaching phase
            descriptionText.text = "Teaching complete! Starting assessment...";
            // nextButton.gameObject.SetActive(false);

        }
    }


    public void NextTeachingStep()
    {
        currentCardIndex++;
        DisplayTeachingCard();
    }
    #endregion

}