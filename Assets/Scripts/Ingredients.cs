using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MayoMatic
{
    public class Ingredients : MonoBehaviour
    {
        public ScoreManager scoreManager;

        [Header("Stream")]
        [Min(0)]
        public float tempo = 1;
        [Min(0)]
        public float speed = 1;
        
        private bool activated = true;
        public bool Activated {
            get {return activated;}
            set {
                if(value = activated) return;
                if(value){
                    coroutine = StartCoroutine(IngredientStream());
                }else{
                    StopCoroutine(coroutine);
                }
                activated = value;
            }
        }

        [Header("Note Prefabs")]
        public Note ANote;
        public Note BNote;
        public Note YNote;
        public Note XNote;

        [Header("Steam position")]
        public Transform StartStream;
        public Transform EndStream;
        [Min(0)]
        public float activeNoteOffset = 1;

        [Header("Note played")]
        public float noteYDestruction = 0;

        private List<Note> notes;
        private List<Note> notesPlayed;
        private Note activeNote;

        private Coroutine coroutine = null;

        private float streamCenter;
        private float streamRadius;

        //Temp
        private int step;

        private void Start()
        {
            if(activated && coroutine == null) {
                coroutine = StartCoroutine(IngredientStream()); 
            }

            streamCenter = Mathf.Lerp(StartStream.position.x, EndStream.position.x, 0.5f);
            streamRadius = (Mathf.Abs(StartStream.position.x) + Mathf.Abs(EndStream.position.x)) / 2;

            notes = new List<Note>();
            notesPlayed = new List<Note>();
        }

        private IEnumerator IngredientStream () {
            while(activated) {
                yield return new WaitForSeconds(tempo);

                //TODO utiliser une piste musicale
                //Temp
                Note newNote = step == 0 ? ANote : step == 1 ? BNote : step == 2 ? YNote : XNote;
                ++step;

                if(step == 4){
                    step = 0;
                }

                Note note = Instantiate(newNote, StartStream.position, Quaternion.identity);
                note.speed = speed;
                notes.Add(note);
            }
        }

        private void Update () {
            UpdateNote();
            CheckInput();
        }
        
        private void UpdateNote () {
            List<Note> notesToDestroy = new List<Note>();

            foreach(Note note in notes) {
                float posX = note.transform.position.x;

                if(posX > EndStream.position.x){
                    notesToDestroy.Add(note);
                }else if(Mathf.Abs(posX) <= activeNoteOffset){
                    SetActiveNote(note, true);
                }
            }

            foreach(Note note in notesToDestroy){
                notes.Remove(note);
                Destroy(note.gameObject);
            }
            notesToDestroy.Clear();

            foreach(Note note in notesPlayed) {
                if(note.transform.position.y < noteYDestruction){
                    notesToDestroy.Add(note);
                }
            }

            foreach(Note note in notesToDestroy){
                notesPlayed.Remove(note);
                Destroy(note.gameObject);
            }
            notesToDestroy.Clear();

            if(activeNote){
                if(Mathf.Abs(activeNote.transform.position.x) > activeNoteOffset){
                    SetActiveNote(activeNote, false);
                }
            }
        }

        private void CheckInput () {
            bool aPressed = Input.GetButtonDown("ANote");
            bool bPressed = Input.GetButtonDown("BNote");
            bool yPressed = Input.GetButtonDown("YNote");
            bool xPressed = Input.GetButtonDown("XNote");

            if(aPressed || bPressed || yPressed || xPressed){
                if(!activeNote){
                    WrongInput();
                    return;
                }

                NoteType noteType = activeNote.Type;

                if((aPressed && noteType == NoteType.A) || (bPressed && noteType == NoteType.B) ||
                   (yPressed && noteType == NoteType.Y) || (xPressed && noteType == NoteType.X)) {
                    GoodInput();
                }else{
                    WrongInput();
                }
            }
        }

        private void GoodInput () {
            if(scoreManager) scoreManager.IngredientAdded(1 - Mathf.Abs(activeNote.transform.position.x - streamCenter) / activeNoteOffset);

            notes.Remove(activeNote);
            activeNote.Play();
            notesPlayed.Add(activeNote);
            SetActiveNote(activeNote, false);
        }

        private void WrongInput () {
            //TODO
        }

        private void SetActiveNote (Note note, bool active) {
            note.Playable = active;
            if(active){
                activeNote = note;
            }else{
                activeNote = null;
            }
        }
    }
}
