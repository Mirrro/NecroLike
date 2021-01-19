using UnityEngine;
using AICreatures;

[RequireComponent(typeof(AIEntity))]
public abstract class CreatureAbility : MonoBehaviour
{
    [SerializeField] protected int triggerAmount;
    protected int kills = 0;
    private void Awake()
    {
        GetComponent<AIEntity>().killEvent.AddListener(CountKill);
    }

    public void CountKill(AIEntity kill)
    {
        if(++kills>=triggerAmount)
        {
            TriggerAbility(kill);
            kills = 0;
        }
    }
    
    public abstract void TriggerAbility(AIEntity kill);

}
