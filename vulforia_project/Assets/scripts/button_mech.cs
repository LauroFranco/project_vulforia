using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class button_mech : MonoBehaviour
{
    //possiveis botões
    public Button cartao;
    public Button desenho;
    public Button camiseta;
    public Button redownload;
    // file
    private WWW www;
    //canvas 
    public Canvas canvasprimario;
    public Canvas canvassecundario;
    public Canvas canvaserror;
    // Barra de progresso do download
    public Slider barradedownload;
    //booleanas
    public bool loadinON;
    public bool instalacaoON = true;


    void Start()
    {
        //controle dos huds
        barradedownload.maxValue = 100;
        canvasprimario.enabled = true;
        canvassecundario.enabled = false;
        canvaserror.enabled = false;
        //mecanica dos botões
        cartao.onClick.AddListener(OnClick);
        redownload.onClick.AddListener(errordown);
        camiseta.onClick.AddListener(OtherClick);
        desenho.onClick.AddListener(OtherClick);
    }

    void Update()
    {
        //controle da barra de progresso
        if (loadinON)
            loading();
        //caso tenha que instalar o arquivo
        if (instalacaoON)
        {
            //se a instalação for concluida
            if (www.isDone)
            {
                instalacao();
                instalacaoON = false;
                StartCoroutine(verificacao());
            }
        }
    }
    //ação do botão principal
    void OnClick() {
        //caso o arquivo ja esteja no aparelho
        if (System.IO.File.Exists(Application.persistentDataPath + "/video.mp4"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else //caso não esteja
        {
            www = new WWW("https://whatsnextdigital.com.br/cdn/Shared/videos/whatsnextdigital_video.mp4");
            canvasprimario.enabled = false;
            canvassecundario.enabled = true;
            loadinON = true;
        }
        }
    //ação dos outros botões
    void OtherClick() {
        //colocar a ação aqui
    }
    //controle da barra de progresso (apenas executa caso seja necessario)
    void loading() {
        barradedownload.value = www.progress *100;
    }
    //intalação do video
    void instalacao() {
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/video.mp4", www.bytes);
    }
    //verificando se baixou o arquivo
    IEnumerator verificacao()
    {
        yield return new WaitForSeconds(4f);
        //caso o arquivo esteja lá
        if (System.IO.File.Exists(Application.persistentDataPath + "/video.mp4"))
        {
            canvasprimario.enabled = true;
            canvassecundario.enabled = false;
            canvaserror.enabled = false;
        }
        else {
            canvasprimario.enabled = false;
            canvassecundario.enabled = false;
            canvaserror.enabled = true;
            //Debug.Log(www.error); descobrir o erro
        }

    }
    //ação do botão de redownload (caso necessario)
    void errordown() {
        www = new WWW("https://whatsnextdigital.com.br/cdn/Shared/videos/whatsnextdigital_video.mp4");
        canvasprimario.enabled = false;
        canvassecundario.enabled = true;
        canvaserror.enabled = false;
        loadinON = true;
        instalacaoON = true;
    }
}
