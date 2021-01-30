
[System.Serializable]
public class MinMax<T>
{
  public MinMax(T mi, T ma)
  {
    min = mi;
    max = ma;
  }
  public T min;
  public T max;
};
