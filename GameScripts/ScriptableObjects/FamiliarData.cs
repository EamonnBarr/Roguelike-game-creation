using UnityEngine;

[CreateAssetMenu(fileName = "Familiar.asset", menuName = "Familiars/FamiliarObject")]

//Sets values for the familiar which can be used in the unity inspector to change these.
public class FamiliarData : ScriptableObject
{
    public string familirType;
    public float speed;
    public float fireDelay;
    public GameObject bulletPrefab;
}
