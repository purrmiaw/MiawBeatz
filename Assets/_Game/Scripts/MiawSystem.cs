//using UnityEngine;
//using DG.Tweening;

//public class MiawSystem 
//{
//	public void Start()
//	{
//        //EgoEvents<MouseDownEvent>.AddHandler(Handle);

//        constraint.ForEachGameObject((egoComponent, transform, miawComponent) =>
//        {
//            //Sequence mySequence = DOTween.Sequence();
//            //mySequence.Append(transform.DOJump(new Vector2(2, -1), 5, 1, 2));
//            //mySequence.Append(transform.DOJump(new Vector2(4, -2), 5, 1, 2));
//            //mySequence.PlayForward();

//            float delay = miawComponent.Delay; //(float)miawComponent.Step * (float)0.5;
//            float theTime = miawComponent.Height * (float)0.5;

//            Sequence mySequence = DOTween.Sequence();
//            mySequence.PrependInterval(delay);
//            mySequence.Append(transform.DOMoveY(0, (float)theTime));
//            mySequence.PlayForward();

//        });


//    }

//	public  void Update()
//	{
//        // For each GameObject that fits the constraint...
//        constraint.ForEachGameObject((egoComponent, transform, miawComponent) =>
//        {
//            // ...move it by the velocity in its Movement Component
//            //transform.Translate(movement.velocity * Time.deltaTime);

//            if (Input.GetButtonDown("Jump"))
//            {

//                //Vector2 miawPosition = new Vector2(transform.position.x, transform.position.y);

//            }
//        });

//    }

//	public override void FixedUpdate()
//	{
		
//	}

//}