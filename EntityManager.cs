public sealed class EntityManager
{
    List<IEntity> entities;

    public EntityManager()
    {
        entities = new List<IEntity>();
    }

    public void Draw()
    {
        foreach(var e in entities)
        {
            e.Draw();
        }
    }

    public void Step()
    {
        foreach(var e in entities)
        {
            e.Step(this);
        }
    }

    public void RemoveEntity(IEntity e)
    {
        entities.Remove(e);
    }

    public void AddEntity(IEntity e)
    {
        entities.Add(e);
    }

    public IEnumerable<T> FindEntities<T>()
    where T : IEntity
    {
        foreach(var e in entities)
        {
            if(e is T t)
            {
                yield return t;
            }
        }
    }
}