using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction;
using TMPro;

public class CardChoiceManager : MonoBehaviour
{
    [System.Serializable]
    public class Card
    {
        public string cardName; // Name of the card
        public GameObject cardObject; // GameObject representing the card
        public string description; // Description to show during teaching
        public int expectedChoiceIndex; // The index of the correct choice (e.g., 0 for correct, 1 for wrong)
    }

    [Header("Card Configuration")]
    public List<Card> cards = new List<Card>(); // List of cards to configure in the Inspector

    [Header("UI Elements")]
    //public TextMeshProUGUI descriptionText; // Text to display the card description
    //public TextMeshProUGUI feedbackText; // Text to display feedback during assessment
   // public TextMeshProUGUI scoreText; // Text to display the score
    public PointableUnityEventWrapper choiceButton1; // Button for first choice
    public PointableUnityEventWrapper choiceButton2; // Button for second choice
    public PointableUnityEventWrapper choiceButton3; // Button for second choice
    public PointableUnityEventWrapper choiceButton4; // Button for second choice
   // public PointableUnityEventWrapper choiceButton5; // Button for second choice
   // public Button nextButton; // Button to proceed through teaching

    private int currentCardIndex = 0;
   // private int score = 0;

    void Start()
    {
        choiceButton1.WhenSelect.AddListener((PointerEvent evt) => MakeChoice(0)); // Assign index 0 to choiceButton1
        choiceButton2.WhenSelect.AddListener((PointerEvent evt) => MakeChoice(1)); // Assign index 1 to choiceButton2
        choiceButton3.WhenSelect.AddListener((PointerEvent evt) => MakeChoice(2)); // Assign index 1 to choiceButton2
        choiceButton4.WhenSelect.AddListener((PointerEvent evt) => MakeChoice(3)); // Assign index 1 to choiceButton2
        choiceButton4.WhenSelect.AddListener((PointerEvent evt) => MakeChoice(4)); // Assign index 1 to choiceButton2
                                                                                   // nextButton.onClick.AddListener(() => NextTeachingStep());

        if (cards.Count == 0)
        {
            Debug.LogError("No cards configured! Please add cards in the Inspector.");
            return;
        }

       
    }

    #region Teaching Phase
    public void StartTeaching()
    {
        //feedbackText.text = ""; // Clear feedback
     //   scoreText.text = ""; // Hide score during teaching
       // descriptionText.text = ""; // Clear description text
        currentCardIndex = 0;

        Debug.Log("Starting teaching phase...");
        Debug.Log($"Initial card: {cards[0].cardName}, Description: {cards[0].description}");

        choiceButton1.gameObject.SetActive(false); // Hide choice buttons
        choiceButton2.gameObject.SetActive(false);
        choiceButton3.gameObject.SetActive(false);
        choiceButton4.gameObject.SetActive(false);
        //choiceButton5.gameObject.SetActive(false);
       // nextButton.gameObject.SetActive(true); // Show "Next" button

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
           // descriptionText.text = cards[currentCardIndex].description;

            // Debug to confirm the description is updated
            Debug.Log($"Displaying card: {cards[currentCardIndex].cardName}, Description: {cards[currentCardIndex].description}");
        }
        else
        {
            // End teaching phase
            //descriptionText.text = "Teaching complete! Starting assessment...";
           // nextButton.gameObject.SetActive(false);
           
        }
    }


    public void NextTeachingStep()
    {
        currentCardIndex++;
        DisplayTeachingCard();
    }
    #endregion

    #region Assessment Phase
    public void StartAssessment()
    {
        //feedbackText.text = "";
        //scoreText.text = $"Score: {score}";
        currentCardIndex = 0;

        choiceButton1.gameObject.SetActive(true); // Show choice buttons
        choiceButton2.gameObject.SetActive(true);
        choiceButton3.gameObject.SetActive(true);
        choiceButton4.gameObject.SetActive(true);
        //choiceButton5.gameObject.SetActive(true);
      //  nextButton.gameObject.SetActive(false); // Hide "Next" button

        DisplayAssessmentCard();
    }

    public void DisplayAssessmentCard()
    {
        if (currentCardIndex < cards.Count)
        {
            // Enable the current card
            foreach (var card in cards)
                card.cardObject.SetActive(false); // Deactivate all cards
            cards[currentCardIndex].cardObject.SetActive(true);

            // Prompt for user choice
           // descriptionText.text = "Choose the Right Emotion?";
        }
        else
        {
            // End assessment phase
           // descriptionText.text = "Assessment complete!";
           // feedbackText.text = "Final Score: " + score;
            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);
            choiceButton3.gameObject.SetActive(false);
            choiceButton4.gameObject.SetActive(false);
            //choiceButton5.gameObject.SetActive(false);

        }
    }

    private void MakeChoice(int choiceIndex)
    {
        if (currentCardIndex >= cards.Count) return;

        Card currentCard = cards[currentCardIndex];

        // Check if the user's choice matches the card's expected choice
        if (choiceIndex == currentCard.expectedChoiceIndex)
        {
          //  score++;
            //feedbackText.text = "Correct! You've earned a point!";
        }
        else
        {
           // feedbackText.text = "Wrong! Better luck next time.";
            currentCard.cardObject.SetActive(true);

        }

        UpdateScore();
        currentCardIndex++;
        DisplayAssessmentCard();
    }

    private void UpdateScore()
    {
       // scoreText.text = $"Score: {score}";
    }
    #endregion
}
