using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PurchaseDetails/PurchaseDetails", order = 1)]
public class PurchaseDetails : ScriptableObject
{
    public int TotalPage;
    public int ScreenParPage;  // max 9 par screen

    public List<PurchaseData> Purchasedata = new List<PurchaseData>();
    public Vector3[] spawnPoints;
}

[System.Serializable]
public class PurchaseData
{
    public GameObject ScreenObj;

}