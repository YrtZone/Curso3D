using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour
{
    public float vidaJogador = 100f;

    public float vidaMaxima = 100f;
    public Image  barraVida;

    private bool podeSalvar = false;

    private Vector3 ultimaPosicaoSalva;
    private float ultimaVidaSalva;
    private bool temDadosSalvos = false;

    void Start()
    {
        CarregarDadosJogador();
        AtualizarBarraVida();
    }

  
    void Update()
    {
        AtualizarBarraVida();
        TakeDamage();

        if (podeSalvar && Keyboard.current.bKey.wasPressedThisFrame)
        {
            SalvarDadosJogador();
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            LimparDadosJogador();
        }
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {            
            Debug.Log("Pressione 'B' para salvar o jogo.");
            podeSalvar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {
            Debug.Log("Você saiu da área de salvamento.");
            podeSalvar = false;
        }
    }

    void SalvarDadosJogador()
    {
        ultimaPosicaoSalva = transform.position;
        ultimaVidaSalva = vidaJogador;

        PlayerPrefs.SetFloat("PosX", ultimaPosicaoSalva.x);
        PlayerPrefs.SetFloat("PosY", ultimaPosicaoSalva.y);
        PlayerPrefs.SetFloat("PosZ", ultimaPosicaoSalva.z);
        PlayerPrefs.SetFloat("Vida", ultimaVidaSalva);
        PlayerPrefs.SetInt("TemDadosSalvos", 1);
        Debug.Log("Jogo salvo com sucesso!");
        podeSalvar = false;
    }

    void LimparDadosJogador()
    {
        PlayerPrefs.DeleteKey("PosX");
        PlayerPrefs.DeleteKey("PosY");
        PlayerPrefs.DeleteKey("PosZ");
        PlayerPrefs.DeleteKey("Vida");
        PlayerPrefs.DeleteKey("TemDadosSalvos");

        ultimaPosicaoSalva = Vector3.zero;
        ultimaVidaSalva = 0f;
        temDadosSalvos = false;

        vidaJogador = vidaMaxima;
        AtualizarBarraVida();

        Debug.Log("Dados do jogador limpos.");

    }

    void CarregarDadosJogador()
    {
        if(PlayerPrefs.GetInt("TemDadosSalvos") == 1)
        {
            float x = PlayerPrefs.GetFloat("PosX");
            float y = PlayerPrefs.GetFloat("PosY");
            float z = PlayerPrefs.GetFloat("PosZ");
            ultimaPosicaoSalva = new Vector3(x, y, z);
            ultimaVidaSalva = PlayerPrefs.GetFloat("Vida");
            transform.position = ultimaPosicaoSalva;
            vidaJogador = ultimaVidaSalva;
            temDadosSalvos = true;
            Debug.Log("Dados do jogador carregados com sucesso!");
        }
        else
        {
            Debug.Log("Nenhum dado salvo encontrado.");
        }
    }
    void AtualizarBarraVida()
    {
        if (barraVida == null) return;

        float vidaNormalizada = Mathf.Clamp01(vidaJogador / vidaMaxima);
        barraVida.fillAmount = vidaNormalizada;
    }
    public void TakeDamage()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            vidaJogador -= 10;
            AtualizarBarraVida();

            if (vidaJogador <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}