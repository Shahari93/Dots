using System;

[Serializable]
public class SaveableData
{
    public int userCoinsAmount;
    public int[] upgradeCoinsCostAmount = new int[1];
    public float[] powerupDurationData = new float[1];
}