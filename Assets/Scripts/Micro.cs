using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Micro : MonoBehaviour {
    public Paddle paddle;
    [SerializeField]
    private string[] m_Keywords;
    private AudioClip clip;
    private float playback;
    public int frequency;
    private KeywordRecognizer m_Recognizer;

    void Start() {
        m_Recognizer = new KeywordRecognizer(m_Keywords);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
        //clip = Microphone.Start("", false, 60, 44100);
    }

    private void Update() {
        float[] spectrum = new float[1024];

        GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++) {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }

		int la = Mathf.FloorToInt( frequency * spectrum.Length / AudioSettings.outputSampleRate *2);

      //  print(spectrum[la]);
        Debug.DrawRay(new Vector3(la - 1, Mathf.Log(spectrum[la - 1]) + 10, 2), Vector3.up* 10, Color.green);
		Debug.DrawRay(new Vector3(Mathf.Log(la - 1), Mathf.Log(spectrum[la - 1]), 3), Vector3.up * 10, Color.blue);
	}

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        //Debug.Log(builder.ToString());

        float[] array = { 0 };

        switch (args.text) {
            case "oh":
                paddle.Move(-1);
                break;
            case "i":
                paddle.Move(1);
                break;
        }
    }
}
