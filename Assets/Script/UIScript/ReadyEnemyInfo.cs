using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyEnemyInfo : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text enemyName;
    [SerializeField] Text hp;
    [SerializeField] Text df;
    [SerializeField] Text damage;
    [SerializeField] Text attackSpeed;
    [SerializeField] Text attackRange;
    [SerializeField] Text moveSpeed;
    [SerializeField] Text explain;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    public void SetData(UnitDataScriptableObject data,Transform transform)
    {
        image.sprite = data.sprite;
        enemyName.text = data.name;
        hp.text = data.maxHp.ToString();
        df.text = data.df.ToString();

        damage.text = data.damage.ToString();
        attackSpeed.text = data.attackSpeed.ToString();
        attackRange.text = data.attackRange.ToString();
        moveSpeed.text = data.moveSpeed.ToString();
        explain.text = data.explain.ToString();

        this.transform.position = transform.position + (Vector3.right * 100);
    }


}
