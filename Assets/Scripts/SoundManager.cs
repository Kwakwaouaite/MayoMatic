using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiParser;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private MidiFile midiFile;
    public int BPM {get; private set;}
    private List<MidiEvent> notes;
    public IReadOnlyList<MidiEvent> Notes {get{return notes.AsReadOnly();}}

    private void Awake() {
        midiFile = new MidiFile("Assets/Ressources/song.mid");

        // also known as pulses per quarter note
        BPM = midiFile.TicksPerQuarterNote / 4;

        if(midiFile.TracksCount == 0) return;

        notes = new List<MidiEvent>();
        foreach(MidiEvent midiEvent in midiFile.Tracks[0].MidiEvents) {
            if(midiEvent.MidiEventType == MidiEventType.NoteOn) notes.Add(midiEvent);
        }
    }
}
