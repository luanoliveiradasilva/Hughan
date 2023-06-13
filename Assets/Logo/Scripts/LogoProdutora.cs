using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LogoProdutora : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string cenaDestino; // Nome da cena para a qual deseja trocar

    void Start()
    {
        StartCoroutine(ReproduzirVideo());
    }

    IEnumerator ReproduzirVideo()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.Play();

        yield return new WaitForSeconds((float)videoPlayer.clip.length);

        videoPlayer.Stop();
        TrocarParaCenaDestino();
    }

    void TrocarParaCenaDestino()
    {
        SceneManager.LoadScene(cenaDestino);
    }
}
