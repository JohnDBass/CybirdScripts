using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour {

    public int scoreSpacing = 20; // Space between each line
    public Transform entryContainer; // Empty game object that holds scores
    private Transform entryTemplate; // Empty game object that contains two text objects named "Rank" and "Score"

    private int compare = 0;
    private int rankReplaced = 0;
    private int temp;
    private int[] highScoreList = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private string bonus;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        entryTemplate = entryContainer.Find("EntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        if (!PlayerPrefs.HasKey("HighScoreListInit"))
        {
            PlayerPrefs.SetString("HighScoreListInit", "");
            PlayerPrefsX.SetIntArray("HighScoreList", highScoreList);
        }

        else
        {
            highScoreList = PlayerPrefsX.GetIntArray("HighScoreList");
        }

        if (PlayerPrefs.HasKey("CurrentScore"))
        {
            compare = PlayerPrefs.GetInt("CurrentScore");
        }

        for (int position = 1; position <= 10; position++)
        {
            if (compare > highScoreList[position - 1])
            {
                temp = highScoreList[position - 1];
                highScoreList[position - 1] = compare;
                compare = temp;

                if (rankReplaced == 0)
                {
                    rankReplaced = position;
                }
            }

            if (PlayerPrefs.GetInt("CurrentScore") == 0)
            {
                entryContainer.Find("EndMessage").GetComponent<Text>().text = "";
            }

            else
            {
                EndString();
                entryContainer.Find("EndMessage").GetComponent<Text>().text =
                    "Congratulations! Your score is " + PlayerPrefs.GetInt("CurrentScore").ToString() + ".\n" + bonus;
            }

            CreateScoreEntry(position, highScoreList[position - 1]);
        }

        PlayerPrefsX.SetIntArray("HighScoreList", highScoreList);
    }

    private void CreateScoreEntry(int rank, int score)
    {
        string rankString;
        Transform scoreEntry = Instantiate(entryTemplate, entryContainer);

        switch(rank)
        {
            default:
            {
                rankString = rank.ToString() + "TH";
            } break;

            case 1:
            {
                rankString = "1ST";
            } break;

            case 2:
            {
                rankString = "2ND";
            }break;

            case 3:
            {
                rankString = "3rD";
            } break;
        }

        scoreEntry.Find("Rank").GetComponent<Text>().text = rankString;
        scoreEntry.Find("Score").GetComponent<Text>().text = score.ToString();

        RectTransform scoreEntryPos = scoreEntry.GetComponent<RectTransform>();
        scoreEntryPos.anchoredPosition = new Vector2(0, -scoreSpacing * (rank - 1));
        scoreEntry.gameObject.SetActive(true);
    }

    private void EndString()
    {
        switch (rankReplaced)
        {
            default:
                {
                    bonus = "Great work.";
                } break;

            case 0:
                {
                    bonus = "better luck next time.";
                } break;

            case 1:
                {
                    bonus = "You're the best!";
                } break;

            case 2:
                {
                    bonus = "Amazing!";
                } break;

            case 3:
                {
                    bonus = "Very impressive.";
                } break;

            case 4:
            case 5:
            case 6:
                {
                    bonus = "Almost there!";
                }
                break;
        }
                
    }

}

