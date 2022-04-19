using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Results {
    public List<ResultData> results;

    public Results() {
        results = new List<ResultData>();
    }
}

[System.Serializable]
public class ResultData
{
    public float result;
    public string dateTime;
    public string username;

    public ResultData(float result, string username) {
        this.result = result;
        this.username = username;
        this.dateTime = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm");
    }
}
