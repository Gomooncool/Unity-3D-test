
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttributes : MonoBehaviour
{
    public int Health;
    public int Damage;
    public int Speed;
    public int TotalActionPoints;
    public int CurrentActionPoints;
    private Text HpText;
    private Text DMGText;
    // Start is called before the first frame update
    void Start()
    {
        
        Speed = 3;
        TotalActionPoints = 3;
        CurrentActionPoints = TotalActionPoints;
        
        HpText = gameObject.transform.Find("CanvasHpDMG").transform.Find("Hp").transform.gameObject.GetComponent<Text>();
        DMGText = gameObject.transform.Find("CanvasHpDMG").transform.Find("DMG").transform.gameObject.GetComponent<Text>();

       

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            
            gameObject.transform.Find("Canvas").transform.Find("SkullIcon").gameObject.SetActive(true);
            Destroy(gameObject, 1);
           
        }


       // if (CurrentActionPoints == 0)
         //   GameObject.Find("TurnManager").GetComponent<TurnManager>().EnemyActionPointsOver();
         //   GameObject.Find("TurnManager").GetComponent<TurnManager>().PlayerActionPointsOver();
            HpText.text = Health.ToString();
            DMGText.text = Damage.ToString();


    }




}
