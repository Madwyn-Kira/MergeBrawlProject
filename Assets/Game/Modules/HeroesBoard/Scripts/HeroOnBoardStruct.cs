using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroOnBoardStruct
{
    [SerializeField]
    public HeroType HeroType;
    [SerializeField]
    public int CellID;

    public HeroOnBoardStruct(HeroType heroType, int Cellid)
    {
        HeroType = heroType;
        CellID = Cellid;
    }
}
