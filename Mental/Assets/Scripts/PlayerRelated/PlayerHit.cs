using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public Transform hurtBox;
    public Transform HitBox;
    
    public List<GameObject> boxes;
    public bool check;
    public int DamageAmount;
    public float delay;
    public bool leftClicking;
    public bool rightClicking;
    public bool middleClicking;

    private void Start()
    {
        HitBox = this.transform.Find("Hitbox");
        hurtBox = this.transform.Find("Hurtbox");
        check = true;
    }
    private void Update()
    {
        boxes = hurtBox.GetComponent<GetCollider>().filteredColList;
        MouseInput();
        PlayerInput();
    }
    private void PlayerInput()
    {
        if(leftClicking && check)
        {
            check = false;
            StartCoroutine(MeleeHitCycle(delay));
            Debug.Log("Hit");
        }
        
    }
    private IEnumerator MeleeHitCycle(float AttackDelay)
    {
        if(boxes.Count != 0)
        {
            boxes[0].transform.root.GetComponent<GeneralData>().Health -= DamageAmount;
            //Execute animation here
            yield return new WaitForSeconds(AttackDelay);
            check = true;
        }
        else
        {
            //Execute animation here
            yield return new WaitForSeconds(AttackDelay);
            check = true;
        }
    }
    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftClicking = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            rightClicking = true;
        }
        else if (Input.GetMouseButtonDown(2))
        {
            middleClicking = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            leftClicking = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightClicking = false;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            middleClicking = false;
        }
    }
}
