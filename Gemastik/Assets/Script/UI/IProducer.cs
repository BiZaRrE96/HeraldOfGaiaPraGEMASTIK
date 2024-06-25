using System.Collections.Generic;

public interface IProducer
{
    List<IResource> GetRequiredResources();
    List<IResource> GetProducts();
    float ProductionRate();
    float PowerConsumption();
}

public interface IResource
{
    public string getName();
}
