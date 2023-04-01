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

        if (gameManager.I.firstCard == null)    // ù��°ī�带 ������ ���� �ʴٸ� ���� ī�带 ù��° ī�忡 �ִ´�.
        {
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            gameManager.I.secondCard = gameObject;  //ù��°ī�带 ������ �ִٸ� �ι�°ī�忡 ���� ������ ī�带 �ִ´�.
            gameManager.I.isMatched();  //ù��°ī��� �ι�°ī�带 ���ϴ� �Լ��� ȣ���Ѵ�.
        }
    }
    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 0.5f); //1�� �Ŀ� �����ؾ��ϴϱ� invoke��
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
