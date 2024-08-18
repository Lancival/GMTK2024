using UnityEngine;

[System.Serializable]
public struct MusicTrack
{
    public string track_name;
    public AudioClip clip;
}

public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] tracks;

    public AudioClip GetClipFromName(string track_name)
    {
        foreach (var track in tracks)
        {
            if (track.track_name == track_name)
            {
                return track.clip;
            }
        }
        return null;
    }

}
