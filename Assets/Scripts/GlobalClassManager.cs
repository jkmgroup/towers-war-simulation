

public class GlobalClassManager 
{
  private ObjectEmitter towersEmitter = null;
  public ObjectEmitter TowersEmitter
  {
    get { return towersEmitter; }
    set { towersEmitter = value; }
  }
  private ObjectPool objectPool = null;
  public ObjectPool ObjectsPool
  {
    get { return objectPool; }
    set { objectPool = value; }
  }
  private TowerCounte towerCounte = new TowerCounte();
  public TowerCounte TowersCounte
  {
    get => towerCounte;
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