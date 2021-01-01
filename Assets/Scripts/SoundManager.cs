﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiParser;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private MidiFile midiFile;
    public int PPQ {get; private set;}
    public MidiTrack[] Tracks;
    //public IReadOnlyList<MidiEvent> Notes {get{return notes.AsReadOnly();}}

    public int BPM;
    public float musicOffset = 0.3f;

    private float songPosition;

    private float dspSongTime;

    private AudioSource musicSource;

    public float MusicTime {
        get{
            return songPosition * 1000f;
        }
    }

    public double TicksToMs{
        get{
            return 60000D / (BPM * PPQ);
        }
    }

    private void Awake() {
        midiFile = new MidiFile(Resources.Load<TextAsset>("MayonnaiseTousInstruMidi").bytes);

        PPQ = midiFile.TicksPerQuarterNote;

        if(midiFile.TracksCount == 0) return;

        Tracks = midiFile.Tracks;

        musicSource = GetComponent<AudioSource>();
    }

    private void Start () {
        dspSongTime = (float)AudioSettings.dspTime + 1;
        musicSource.PlayScheduled(dspSongTime);
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - musicOffset);
    }
}
