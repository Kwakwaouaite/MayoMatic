using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MidiParser;

namespace MayoMatic
{
    [System.Serializable]
    public class NoteData {
        public Note prefab;
        public int midiTrack;
    }

    public class Ingredients : MonoBehaviour
    {
        public ScoreManager scoreManager;
        public SoundManager soundManager;
        
        private bool activated = true;
        public bool Activated {
            get {return activated;}
            set {
                if(value = activated) return;
                if(value){
                    //coroutine = StartCoroutine(IngredientStream());
                }else{
                    //StopCoroutine(coroutine);
                }
                activated = value;
            }
        }

        [Header("Note Data")]
        public NoteData ANote;
        public NoteData BNote;
        public NoteData YNote;
        public NoteData XNote;

        [Header("Steam")]
        public Transform StartStream;
        public Transform EndStream;
        [Min(0)]
        public double tightening = 500;
        public float activeTimeOffset = 1000;

        [Header("Note played")]
        public float noteYDestruction = 0;

        private Stack<Note> notes;
        private List<Note> notesPlayed;
        private List<Note> notesMissed;

        //private Coroutine coroutine = null;

        //private float streamCenter;
        //private float streamRadius;
        private Transform stream;

        private int streamBPM;

        private void Start()
        {
            /*streamCenter = Mathf.Lerp(StartStream.position.x, EndStream.position.x, 0.5f);
            streamRadius = (Mathf.Abs(StartStream.position.x) + Mathf.Abs(EndStream.position.x)) / 2;*/
            stream = new GameObject("Stream").transform;
            stream.parent = transform;
            stream.position = transform.position;

            notes = new Stack<Note>();
            MidiTrack[] tracks = soundManager.Tracks;

            CreateNotesFromTrack(tracks[ANote.midiTrack], ANote.prefab);
            CreateNotesFromTrack(tracks[BNote.midiTrack], BNote.prefab);
            CreateNotesFromTrack(tracks[YNote.midiTrack], YNote.prefab);
            CreateNotesFromTrack(tracks[XNote.midiTrack], XNote.prefab);

            Stack<Note> sortNotes = new Stack<Note>(); 
            while (notes.Count > 0) { 
                Note tmp = notes.Pop(); 
        
                while (sortNotes.Count > 0 && sortNotes.Peek().PlayTime < tmp.PlayTime) 
                {  
                    notes.Push(sortNotes.Pop()); 
                } 
        
                sortNotes.Push(tmp); 
            } 
            notes = sortNotes;

            notesPlayed = new List<Note>();
            notesMissed = new List<Note>();
            streamBPM = soundManager.BPM;

            /*if(activated && coroutine == null) {
                coroutine = StartCoroutine(IngredientStream()); 
            }*/
        }

        private void CreateNotesFromTrack (MidiTrack track, Note notePrefab){
            Stack<MidiEvent> reversedStreamNotes = new Stack<MidiEvent>(track.MidiEvents.Where(midiEvent => midiEvent.MidiEventType == MidiEventType.NoteOn));
            while (reversedStreamNotes.Count != 0) {
                MidiEvent midi = reversedStreamNotes.Pop();
                float time = (float)(midi.Time * soundManager.TicksToMs);
                Note note = Instantiate(notePrefab, new Vector2(transform.position.x + (float)(-time / tightening), transform.position.y), Quaternion.identity, stream);
                note.PlayTime = time;
                note.gameObject.SetActive(false);
                notes.Push(note);
            }
        }

        /*private IEnumerator IngredientStream () {
            while(activated) {
                yield return new WaitForSeconds(1 / (streamBPM / 60));
                float time =  Time.time * 1000;
                Debug.Log(time);

                //TODO utiliser une piste musicale
                //Temp
                /*Note newNote = step == 0 ? ANote : step == 1 ? BNote : step == 2 ? YNote : XNote;
                ++step;

                if(step == 4){
                    step = 0;
                }

                Note note = Instantiate(newNote, StartStream.position, Quaternion.identity);
                note.speed = speed;
                notes.Add(note);
            }
        }*/

        private void Update () {
            stream.position = new Vector2(transform.position.x + (float)(soundManager.MusicTime / tightening), stream.position.y);
            UpdateNote();
            CheckInput();
        }
        
        private void UpdateNote () {
            List<Note> notesToDestroy = new List<Note>();

            if(notes.Count != 0){
                Note activeNote = notes.Peek();
                float time = soundManager.MusicTime;
                if(!activeNote.Playable && time + activeTimeOffset / 2 > activeNote.PlayTime){
                    activeNote.Playable = true;
                }
                else if(time - activeTimeOffset / 2 > activeNote.PlayTime){
                    activeNote.Playable = false;
                    notes.Pop();
                    notesMissed.Add(activeNote);
                }
            }

            foreach(Note note in notes) {
                float posX = note.transform.position.x;
                if(posX > StartStream.position.x){
                    note.gameObject.SetActive(true);
                }
            }

            foreach(Note note in notesMissed) {
                float posX = note.transform.position.x;

                if(posX > EndStream.position.x){
                    notesToDestroy.Add(note);
                }
            }

            foreach(Note note in notesToDestroy){
                notesMissed.Remove(note);
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
        }

        private void CheckInput () {
            if(notes.Count == 0) return;
            Note activeNote = notes.Peek();

            bool aPressed = Input.GetButtonDown("ANote");
            bool bPressed = Input.GetButtonDown("BNote");
            bool yPressed = Input.GetButtonDown("YNote");
            bool xPressed = Input.GetButtonDown("XNote");

            if(aPressed || bPressed || yPressed || xPressed){
                NoteType noteType = activeNote.Type;

                if((aPressed && noteType == NoteType.A) || (bPressed && noteType == NoteType.B) ||
                   (yPressed && noteType == NoteType.Y) || (xPressed && noteType == NoteType.X)) {
                    if(activeNote.Playable){
                        GoodInput();
                    }else{
                        WrongInput();
                    }
                }else{
                    WrongInput();
                }
            }
        }

        private void GoodInput () {
            Note activeNote = notes.Pop();
            if(scoreManager) scoreManager.IngredientAdded(1 - Mathf.Abs(soundManager.MusicTime - activeNote.PlayTime) / activeTimeOffset * 2);

            activeNote.transform.parent = transform.parent;
            activeNote.Play();
            notesPlayed.Add(activeNote);
        }

        private void WrongInput () {
            //TODO
        }

        /*private void SetActiveNote (Note note, bool active) {
            note.Playable = active;
            if(active){
                activeNote = note;
            }else{
                activeNote = null;
            }
        }*/
    }
}
