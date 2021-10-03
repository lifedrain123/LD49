using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    // 关卡数据
    public LevelData[] levelDatas;
    // 当前关卡编号
    public int currentLevel;
    // 当前命
    public int currentLife;
    // 当前高度
    public float currentHeight;
    // 当前一格的长度
    public float length;
    // 当前一格的高度
    public float height;
    // 关卡名称
    public string levelName;
    // 当前目标高度
    public float targetHeight;
    // 当前砖块
    public List<GameObject> blocks;
    // 砖块预制件
    public GameObject blockPrefab;
    // 当前位置
    public Vector3 currentPosition;
    // Ark预制件
    public GameObject arkPrefab;
    // 当前Ark
    public GameObject currentArk;
    // Ark初始位置
    public Vector3 arkPosition;
    // 当前砖块数量
    public int blockNumber;
    // 球预制件
    public GameObject ballPrefab;
    // 当前球
    public GameObject currentBall;
    // 球初始偏移位置
    public Vector3 ballPosition;
    // 是否开始
    public bool start;
    // 关卡描述
    public Text levelText;
    // 目前高度
    public Text currentHeightText;
    // 目标高度
    public Text targetHeightText;
    // 剩余生命
    public Text remainLifeText;
    // 底部y轴坐标
    public float downBorderY;
    // 过关计时器
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        Draw();
    }

    // Update is called once per frame
    void Update()
    {
        JudgeLevelClear();
        UpdateText();
        if(Input.GetButtonDown("Reset"))
        {
            this.currentLife --;
            Clean();
            Draw();
        }
    }
    
    void FixedUpdate()
    {
        move();
    }

    public void move()
    {
        if(!start && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            start = true;
            Ball ball =  currentBall.GetComponent<Ball>();
            ball.rb.velocity = Vector2.down * ball.speed;
        }
    }

    public void Draw()
    {
        length = levelDatas[currentLevel].length;
        height = levelDatas[currentLevel].height;
        levelName = levelDatas[currentLevel].levelName;
        targetHeight = levelDatas[currentLevel].targetHeight;
        currentPosition = levelDatas[currentLevel].initialPosition;
        Vector3 tempPosition;
        Respawn();
        for(int i = 0;i < levelDatas[currentLevel].blocks.Length; i++)
        {
            int row = i / levelDatas[currentLevel].maxColumn;
            int column = i % levelDatas[currentLevel].maxColumn;
            tempPosition = currentPosition + new Vector3(length,0,0) * column + new Vector3(0,height,0) * row;
            GameObject tempBlock = Instantiate(blockPrefab,tempPosition,Quaternion.Euler(0, 0, levelDatas[currentLevel].blocks[i].rotation));
            tempBlock.transform.localScale = new Vector3(levelDatas[currentLevel].blocks[i].scale,25,1);
            tempBlock.GetComponent<SpriteRenderer>().color = levelDatas[currentLevel].blocks[i].color;
            tempBlock.GetComponent<Block>().gameSystem = this;
            blocks.Add(tempBlock);
            blockNumber ++;
        }
    }

    public void JudgeLevelClear()
    {
        if(blockNumber == 0)
        {
            timer += Time.deltaTime;
            if(currentHeight >= targetHeight)
            {
                currentLevel ++;
                if(currentLevel < levelDatas.Length)
                {
                    Clean();
                    Draw();
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            if(timer > 15f)
            {
                currentLife --;
                Clean();
                Draw();
            }
        }
    }

    public void Clean()
    {        
        start = false;
        timer = 0;
        currentHeight = 0;
        for(int i = 0; i < blocks.Count; i++)
        {
            Destroy(blocks[i]);
        }
        Destroy(currentArk);
        Destroy(currentBall);
    }

    public void Respawn()
    {
        currentArk = Instantiate(arkPrefab,arkPosition,Quaternion.Euler(0, 0, 0));
        currentBall = Instantiate(ballPrefab,arkPosition + ballPosition,Quaternion.Euler(0, 0, 0));
        currentBall.GetComponent<Ball>().gameSystem = this;
    }

    public void BallOut()
    {
        this.currentLife --;
        start = false;
        Destroy(currentBall);
        currentBall = Instantiate(ballPrefab,currentArk.transform.position + ballPosition,Quaternion.Euler(0, 0, 0));
        currentBall.GetComponent<Ball>().gameSystem = this;
    }

    public void UpdateText()
    {
        levelText.text = levelDatas[currentLevel].levelName;
        currentHeightText.text = currentHeight.ToString("0.00");
        targetHeightText.text = targetHeight.ToString();
        remainLifeText.text = currentLife.ToString();
    }

}
