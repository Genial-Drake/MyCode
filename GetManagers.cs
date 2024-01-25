using UnityEngine;

public class GetManagers : MonoBehaviour
{
    public MasterScript mS; // MasterScript
    public PlayerDataSO pD; // PlayerData
    public EnemyDataSO eD; // PlayerData
    
    private void Awake() 
    { 
        mS = FindObjectOfType<MasterScript>();
        pD = mS.playerDataSO;
        eD = mS.enemyDataSO;
    }
}
