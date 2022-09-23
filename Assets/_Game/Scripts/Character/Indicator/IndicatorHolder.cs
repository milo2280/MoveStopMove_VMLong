using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHolder : MonoBehaviour
{
    public GameObject holder;
    public GameUnit indicatorPrefab;

    private Dictionary<Enemy, Indicator> dictIndicator = new Dictionary<Enemy, Indicator>();

    public void OnReset()
    {
        dictIndicator.Clear();
    }

    public void ShowAllIndicator()
    {
        holder.SetActive(true);
    }

    public void HideAllIndicator()
    {
        holder.SetActive(false);
    }

    public void AddIndicator(Enemy enemy)
    {
        Indicator indicator = SimplePool.Spawn<Indicator>(indicatorPrefab);
        indicator.OnInit(enemy);
        dictIndicator.Add(enemy, indicator);
        enemy.AssignIndicator(indicator);
    }

    public void RemoveIndicator(Enemy enemy)
    {
        SimplePool.Despawn(dictIndicator[enemy]);
        dictIndicator.Remove(enemy);
    }
}
