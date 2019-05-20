using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class video_config : MonoBehaviour
{
  private Renderer rend;
  private VideoPlayer videos;
    private bool voltou;
    public string url;

    private bool teste = true;
    void Start()

    {
        //recolhendo componentes
        videos = GetComponent<VideoPlayer>();
        rend = GetComponent<Renderer>();
        //path do video
        url = Application.persistentDataPath + "/video.mp4";
        //colocando a path
        if (File.Exists(url))
            videos.url = "file://" + url;
        //else caso não tenha o video (pode voltar para a primeira cena para baixar novamente ou mostrar um aviso de error)
    }

    void Update()
    {
        //caso o obj esteja visivel
        if (rend.isVisible)
        {
            if (voltou)
            {
                //tocar automaticamente 
                 videos.Play();
                voltou = false;
            }
            //caso queira pausar ou recomeçar no touch
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (videos.isPlaying)
                        videos.Pause();
                    else
                        videos.Play();
                }
            }
        }
        else //caso não
        {
            voltou = true;
            videos.Pause();
        }  
        }
}
