using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBuff : MonoBehaviour
{
    public BuffType buff;
    public enum BuffType
    {
        Speed,
        Regen
    }
}
