using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{
    private bool AttackStart = false;
    public GameObject HeartIconInst;
    private float AnimTime = 0;
    public GameObject CurrentEnemy;
    public Text DmgText;
    public Vector3 IconOriginalPos;
    public GameObject DamageIconInCurrEnemy;





    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().material.color = Color.red;
        AttackStart = true;
        CurrentEnemy = other.gameObject;
        CurrentEnemy.GetComponent<Renderer>().material.color = Color.red;
        DamageIconInCurrEnemy = CurrentEnemy.transform.Find("Canvas").transform.Find("DamageIcon").gameObject; //ищем иконку урона в текущей карте врага
    }

    private void OnTriggerStay(Collider other)
    {
        GetComponent<Renderer>().material.color = Color.red;
        CurrentEnemy.GetComponent<Renderer>().material.color = Color.red;

    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        CurrentEnemy.GetComponent<Renderer>().material.color = Color.white;

    }

    void Start()
    {
        
    }

    void Update()
    {
        if (AttackStart == true)
        {
            AttackStart = false;
            CurrentEnemy.GetComponent<CharacterAttributes>().Health -= GetComponent<CharacterAttributes>().Damage; // наносим урон хп врагу
            
            DamageIconInCurrEnemy.transform.Find("DamageText").GetComponent<Text>().text = '-' + GetComponent<CharacterAttributes>().Damage.ToString();  //иконка урона подтягивает значение урона
            StartCoroutine(DamageAnimationPlay(DamageIconInCurrEnemy)); //анимация движения иконки урона
            

        }

        IEnumerator DamageAnimationPlay(GameObject HeartIconInst)
        {
            HeartIconInst.SetActive(true);
            AnimTime = 0;
            IconOriginalPos = HeartIconInst.transform.position;

            while (AnimTime <= 0.5f)
            {
                HeartIconInst.transform.Translate(Vector3.up*Time.deltaTime*2);
                AnimTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            HeartIconInst.transform.position = IconOriginalPos;
            HeartIconInst.SetActive(false);
        }

    }
}
