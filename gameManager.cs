using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;  //�������� �ʿ��� ��.

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
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };   //����Ʈ ����

        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray(); //�����ϰ�  -1.0���� 1.0������ ���� �� �Ҽ��� ������ ���� ���� �� �ִ�. 1.0�� ����

        for (int i = 0; i < 16; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;  //���� ���� newCard�� ������ cards�� ã�Ƽ� ���� ��ġ�� �ٲ��ֱ�.
            //(0,1,2,3) (4,5,6,7) (8,9,10,11) (12,13,14,15)
            float x = (i / 4) * 1.4f - 2.1f;    //4�γ��� ���� 0,1,2,3�϶�
            float y = (i % 4) * 1.4f - 3.0f;    //4�γ��� �������� 0,1,2,3�϶�
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString(); //rtan0, rtan1 ...
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; //�ð��� �帣���ؼ� �װ��� �����ش�.
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

        if (firstCardImage == secondCardImage)  //�̹����̸����� ������ ��.
        {
            //���ֱ�
            firstCard.GetComponent<card>().destroyCard();   // card.cs�� destroyCard�Լ��� �ҷ��´�. GetComponent<card>() = card ��ũ��Ʈ�� �ҷ���
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft == 2)
            {
                //����ī�尡 2���ϋ� �����Ű��
                Time.timeScale = 0f;
                endTxt.SetActive(true);
                //Invoke("GameEnd", 1f);
            }
        }
        else
        {
            //�ٽ� ������
            firstCard.GetComponent<card>().closeCard();   // card.cs�� closeCard�Լ��� �ҷ��´�. GetComponent<card>() = card ��ũ��Ʈ�� �ҷ���
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;   //��Ī�� ������ ����ش�.
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
