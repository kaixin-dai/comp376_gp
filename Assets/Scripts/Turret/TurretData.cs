using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TurretData
{
    // Start is called before the first frame update
    public GameObject turretPrefab;
    public GameObject previewPrefab;
    public int cost;
    public GameObject upgradedPrefab;
    public int upgradedCost;
    public TurretType type;
    public enum TurretType
    {
        GunnerTurret,
        RocketTurret,
        LaserTurret,
        ShockTurret
    }
}
