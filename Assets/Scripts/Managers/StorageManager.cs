using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    #region Singleton

    private static StorageManager instance;

    private void Awake() {
        instance = this;
    }

    public static StorageManager GetInstance() {
        return instance;
    }

    #endregion

    private string resultPath = "/ResultData.json";

    public void SaveResult() {
        Results results = ReadResults();

        if(results == null) {
            results = new Results();
        }

        float time = TimerController.GetInstance().GetTime();
        string username = PlayerPrefs.GetString("USERNAME");

        ResultData timeToSave = new ResultData(time, username);
        results.results.Add(timeToSave);

        System.IO.File.WriteAllText(Application.persistentDataPath + resultPath, JsonUtility.ToJson(results));
    }

    public Results ReadResults() {
        try{
            string response = System.IO.File.ReadAllText(Application.persistentDataPath + resultPath);
            return JsonUtility.FromJson<Results>(response);
        } catch {
            return null;
        }

    }

    public Results SortResults(Results results) {
        List<ResultData> sordeted =  results.results.OrderBy((element) => element.dateTime).ToList();
        results.results = sordeted;
        return results;
    } 
}