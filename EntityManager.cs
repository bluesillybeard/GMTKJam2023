using System.Collections.Generic;

public sealed class EntityManager
{
    List<IEntity> entities;

    public EntityManager()
    {
        entities = new List<IEntity>();
        entitiesForStep = entities.ToArray();
    }

    public void Draw()
    {
        foreach(var e in entities)
        {
            e.Draw();
        }
    }

    IEntity[] entitiesForStep;
    public void Step()
    {
        entitiesForStep = entities.ToArray();
        foreach(var e in entitiesForStep)
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

    public void AddEntityFirst(IEntity e)
    {
        entities.Insert(0, e);
    }

    public void AddEntityAfter(IEntity toAdd, IEntity subject)
    {
        int index = entities.IndexOf(subject);
        entities.Insert(index+1, toAdd);
    }
    public void AddEntityBefore(IEntity toAdd, IEntity subject)
    {
        int index = entities.IndexOf(subject);
        entities.Insert(index-1, toAdd);
    }

    public IEnumerable<T> FindEntities<T>()
    where T : IEntity
    {
        foreach(var e in entitiesForStep)
        {
            if(e is T t)
            {
                yield return t;
            }
        }
    }

    public T? FindEntity<T>()
    where T : IEntity
    {
        foreach(var e in entitiesForStep)
        {
            if(e is T t)
            {
                return t;
            }
        }
        return default(T);
    }

    public IReadOnlyList<IEntity> Entities => entitiesForStep;
}