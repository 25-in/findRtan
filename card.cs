using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openCard()
    {
        anim.SetBool("isOpen", true);
        // Animator isOpen = True
        transform.Find("front").gameObject.SetActive(true);
        // Front setActive = True
        transform.Find("back").gameObject.SetActive(false);
        // Back setActive = False

        if (gameManager.I.firstCard == null)    // 첫번째카드를 가지고 있지 않다면 지금 카드를 첫번째 카드에 넣는다.
        {
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            gameManager.I.secondCard = gameObject;  //첫번째카드를 가지고 있다면 두번째카드에 지금 오픈한 카드를 넣는다.
            gameManager.I.isMatched();  //첫번째카드와 두번째카드를 비교하는 함수를 호출한다.
        }
    }
    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 0.5f); //1초 후에 실행해야하니까 invoke를
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 0.5f);
    }

    void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }
}
