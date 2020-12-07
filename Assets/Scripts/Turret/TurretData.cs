using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TurretData
{
    public GameObject turretPrefab;
    public GameObject previewPrefab;
    public int cost;
/*    public GameObject upgradedPrefab;
    public int upgradedCost;*/
    public TurretType type;
    public int tier;
    public enum TurretType
    {
        GunnerTurret,
        LaserTurret,
        RocketTurret,
        ShockTurret
    }
}
