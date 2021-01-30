

public class GlobalClassManager 
{
  private Scene scene_ = null;
  public Scene scene
  {
    get { return scene_; }
    set { scene_ = value; }
  }

  private ObjectPool objectPool = null;
  public ObjectPool ObjectsPool
  {
    get { return objectPool; }
    set { objectPool = value; }
  }

  static private GlobalClassManager instance;
  static public GlobalClassManager Instance()
  {
    if (instance == null)
    {
      instance = new GlobalClassManager();
    }
    return instance;
  }

  private GlobalClassManager() { }
}