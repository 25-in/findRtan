using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;  //섞기위해 필요한 것.

public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    float time;
    public GameObject card;
    public static gameManager I;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endTxt;

    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update

    void Start()
    {
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };   //리스트 생성

        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray(); //랜덤하게  -1.0부터 1.0까지의 범위 중 소숫점 임의의 값을 구할 수 있다. 1.0도 포함

        for (int i = 0; i < 16; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;  //위에 만든 newCard를 프리팹 cards를 찾아서 걔의 위치로 바꿔주기.
            //(0,1,2,3) (4,5,6,7) (8,9,10,11) (12,13,14,15)
            float x = (i / 4) * 1.4f - 2.1f;    //4로나눈 몫이 0,1,2,3일때
            float y = (i % 4) * 1.4f - 3.0f;    //4로나눈 나머지가 0,1,2,3일때
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString(); //rtan0, rtan1 ...
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; //시간을 흐르게해서 그것을 더해준다.
        timeTxt.text = time.ToString("N2");

        if(time > 30.0f)
        {
            Time.timeScale = 0f;
            endTxt.SetActive(true);
        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;   //rtan0, rtan1 ...
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name; //rtan0, rtan1 ...

        if (firstCardImage == secondCardImage)  //이미지이름끼리 같은지 비교.
        {
            //없애기
            firstCard.GetComponent<card>().destroyCard();   // card.cs의 destroyCard함수를 불러온다. GetComponent<card>() = card 스크립트를 불러옴
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft == 2)
            {
                //남은카드가 2개일떄 종료시키자
                Time.timeScale = 0f;
                endTxt.SetActive(true);
                //Invoke("GameEnd", 1f);
            }
        }
        else
        {
            //다시 뒤집기
            firstCard.GetComponent<card>().closeCard();   // card.cs의 closeCard함수를 불러온다. GetComponent<card>() = card 스크립트를 불러옴
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;   //매칭이 끝나면 비워준다.
        secondCard = null;
    }

    /*
    void GameEnd()
    {
        Time.timeScale = 0f;
        endTxt.SetActive(true);
    }
    */
}
