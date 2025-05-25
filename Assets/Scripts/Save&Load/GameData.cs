using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int stage;
    public float time;
    public int attempts;
    public List<int> hiddenNumber = new List<int>();
    public SerlizableDectionary<int, string> TrialData = new SerlizableDectionary<int, string>();
    public bool HaveHint;
    public bool HaveHintRemove;

    public GameData()
    {
        this.stage = 0;
        this.time = 0;
        this.attempts = 0;
        this.TrialData = new SerlizableDectionary<int, string>();
        this.hiddenNumber = new List<int>();
        this.HaveHint = false;
        this.HaveHintRemove = false;

    }
}
