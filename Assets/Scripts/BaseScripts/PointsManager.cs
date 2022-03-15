
using UnityEngine;

public class PointsManager
{
    private static PointsManager pointsManager;
    private int points;
    private UnityEngine.UI.Text pointsText;

    private PointsManager() {}

    public static PointsManager GetPointsManager() {
        if (pointsManager == null) {
            pointsManager = new PointsManager();
        }
        return pointsManager;
    }

    public int GetPoints() {
        return points;
    }

    public void SetPoints(int points) {
        this.points = points;
    }

    public void AddPoints(int points) {
        this.points += points;
    }

    public void SubtractPoints(int points) {
        this.points -= points;

        if (this.points < 0) {
            this.points = 0;
        }
    }
}
