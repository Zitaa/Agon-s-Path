using Game;

public class SingletonPattern 
{
    private static SingletonPattern instance = new SingletonPattern();
    public static SingletonPattern Instance { get { return instance; } }

    protected GameManager GetGame()
    {
        return GameBehaviour.instance.GetGame();
    }
}
