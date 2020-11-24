using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretData
{
    // Start is called before the first frame update
    public GameObject turretPrefab;
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
