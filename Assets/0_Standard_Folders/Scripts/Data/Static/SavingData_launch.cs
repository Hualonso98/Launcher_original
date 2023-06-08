using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SavingData_launch
{
    public static int appSelected = 0;
    public static string pathSelected = "";

    public static bool protocolMode = false;

    public static string firstDate;
    public static string lastDate;

    //Launcher, Protocolo, Gestures, MT, BBT, Clothespin, Fruits, Supermarket
    public static bool[] gamesAllowed = new bool[] { true, true, true, true, true, true, true, true };
}