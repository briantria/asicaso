/* author: Brian Tria
 * created: June 2, 2019
 * description: 
 *
 */

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLifePointSystem", menuName = "ScriptableObject/Life Point System", order = 51)]
public class LifePointSystem : ScriptableObject
{
    public delegate void LifePointAction(int remainingLifePoints);
    public static event LifePointAction OnReset;
    public static event LifePointAction OnDamage;
    public static event LifePointAction OnHeal;
    public static event LifePointAction OnDeath;


    public int InitialLifePoints;
    private int remainingLifePoints;

    public void Reset()
    {
        remainingLifePoints = InitialLifePoints;

        if (OnReset != null)
        {
            OnReset(remainingLifePoints);
        }
    }

    public int GetRemainingLifePoints()
    {
        return remainingLifePoints;
    }

    public void DoDamage(int damage)
    {
        remainingLifePoints -= damage;

        if (OnDamage != null)
        {
            OnDamage(remainingLifePoints);
        }

        if (remainingLifePoints <= 0 && OnDeath != null)
        {
            OnDeath(0);
        }
    }

    public void DoHeal(int healPoints)
    {
        remainingLifePoints += healPoints;

        if (OnHeal != null)
        {
            OnHeal(remainingLifePoints);
        }
    }
}