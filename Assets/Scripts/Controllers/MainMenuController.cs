using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
   [SerializeField]
   private Font font;

   [SerializeField]
   private int fontSize = 42;

   [SerializeField]
   private GameObject mainMenuGameObject;

   [SerializeField]
   private GameObject leaderboardGameObject;

   [SerializeField]
   private GameObject errorText;

   private InputField usernameInputField;
   private GameObject leaderboard;
   private string resultPath = "/ResultData.json";

   void Start() {
       usernameInputField = GameObject.FindGameObjectsWithTag("UsernameInput")[0].GetComponent<InputField>();

       string usernameFromPrefs = PlayerPrefs.GetString("USERNAME");

       if (usernameFromPrefs != null)  {
           usernameInputField.text = usernameFromPrefs;
       }
   }


   public void Play() {
       if (usernameInputField.text != null && !usernameInputField.text.Equals("")) {
            PlayerPrefs.SetString("USERNAME", usernameInputField.text);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1;
       } else {
           errorText.SetActive(true);
       }
   }

   public void Quit() {
       Application.Quit();
   }

   public void HideErrorText() {
       errorText.SetActive(false);
   }

   public void OpenLeaderboard() {
        leaderboardGameObject.SetActive(true);
        mainMenuGameObject.SetActive(false);

        leaderboard = GameObject.FindGameObjectWithTag("Leaderboard");
        LoadLeaderBoard();
   }

   public void BackToMainMenu() {
        leaderboardGameObject.SetActive(false);
        mainMenuGameObject.SetActive(true);
   }

   private void LoadLeaderBoard() {
       Results results =  ReadResults();
       int yOffset = 10;
       int index = 1;
       foreach (ResultData resultData in results.results) {
           if (index > 10) {
               break;
           }
            print(resultData.dateTime);
            GameObject empty = new GameObject();
            Text text = empty.AddComponent<Text>();
            text.font = font;
            text.fontSize = fontSize;
            text.text = $"{index}. {resultData.username} <color=green>{resultData.result.ToString("0.00")}</color> {resultData.dateTime}";
            empty.transform.SetParent(leaderboard.transform);
            
            SetTextPostion(text, yOffset);
            yOffset += 70;
            index++;
        }
   }

    public Results ReadResults() {
        try{
            string response = System.IO.File.ReadAllText(Application.persistentDataPath + resultPath);
            Results fromFile =  JsonUtility.FromJson<Results>(response);
            List<ResultData> sordeted =  fromFile.results.OrderBy((element) => element.result).ToList();

            fromFile.results = sordeted;
            return fromFile;
        } catch {
            return null;
        }

    }

    private void SetTextPostion(Text text, int yOffset) {
        RectTransform rectTransform = text.GetComponent<RectTransform>();

        rectTransform.sizeDelta = leaderboard.GetComponent<RectTransform>().sizeDelta;
        text.transform.position = leaderboard.transform.position;
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.pivot = new Vector2(0.5f, 1);
        text.alignment = TextAnchor.UpperCenter;
        text.transform.position -= new Vector3(-10, yOffset, 0.0f);
    }
}
