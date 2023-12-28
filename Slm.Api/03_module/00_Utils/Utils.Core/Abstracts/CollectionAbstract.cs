namespace Slm.Utils.Core.Abstracts;

/// <summary>
/// 抽象集合
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class CollectionAbstract<TEntity> : IList<TEntity>
{
    protected readonly List<TEntity> Collection = new List<TEntity>();

    public IEnumerator<TEntity> GetEnumerator()
    {

        return Collection.GetEnumerator();
    }

    public void Add(TEntity item)
    {
        Collection.Add(item);
    }

    public void Clear()
    {
        Collection.Clear();
    }

    public bool Contains(TEntity item)
    {
        return Collection.Contains(item);
    }

    public void CopyTo(TEntity[] array, int arrayIndex)
    {
        Collection.CopyTo(array, arrayIndex);
    }

    public bool Remove(TEntity item)
    {
        return Collection.Remove(item);
    }

    public int Count => Collection.Count;

    public bool IsReadOnly { get; }


    public int IndexOf(TEntity item)
    {
        return Collection.IndexOf(item);
    }

    public void Insert(int index, TEntity item)
    {
        Collection.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Collection.RemoveAt(index);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public TEntity this[int index]
    {
        get => Collection[index];
        set => Collection[index] = value;
    }
}
