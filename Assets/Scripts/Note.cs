using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MayoMatic
{
    public enum NoteType {
        A,
        B,
        Y,
        X
    }

    public class Note : MonoBehaviour
    {
        [SerializeField]
        private NoteType type = NoteType.A;
        public NoteType Type {get{return type;}}

        [HideInInspector]
        public float PlayTime;

        [SerializeField]
        [Min(0)]
        private float fallSpeed = 15;
        [SerializeField]
        [Min(0)]
        private float fallRotateSpeed = 10;
        [SerializeField]
        [Min(0)]
        private float playJumpForce = 5;
        [SerializeField]
        private Text text = null;
        [SerializeField]
        private GameObject button = null;

        private float fallVelocity = 0;

        bool played = false;
        private float playedTime;

        private void Update () {
            if(!played){
                //transform.position += Vector3.right * speed * Time.deltaTime;
            }else{
                transform.position += Vector3.up * fallVelocity * Time.deltaTime;
                fallVelocity -= fallSpeed * Time.deltaTime;

                if(fallVelocity < 0) {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 180), fallRotateSpeed * Time.deltaTime);
                }
            }
        }

        public bool Playable {
            get{return text.color == Color.white;}
            set {
                if(value){
                    text.color = Color.white;
                }else{
                    text.color = Color.black;
                }
            }
        }

        public void Play () {
            played = true;
            fallVelocity = playJumpForce;
            playedTime = Time.time;
            Destroy(button);
        }
    }
}